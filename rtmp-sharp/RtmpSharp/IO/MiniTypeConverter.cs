using Complete;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Reflection;

namespace RtmpSharp.IO
{
    internal static class MiniTypeConverter
    {
        private static readonly MethodInfo EnumerableToArrayMethod = typeof(MiniTypeConverter).GetMethod("EnumerableToArray", BindingFlags.Static | BindingFlags.NonPublic);
        private static readonly ConcurrentDictionary<Type, MethodInfo> EnumerableToArrayCache = new ConcurrentDictionary<Type, MethodInfo>();
        private static readonly ConcurrentDictionary<Type, AdderMethodInfo> AdderMethodCache = new ConcurrentDictionary<Type, AdderMethodInfo>();

        private static T[] EnumerableToArray<T>(IEnumerable enumerable)
        {
            return enumerable.Cast<T>().ToArray();
        }

        public static object ConvertTo(object value, Type targetType)
        {
            if (value == null)
                return CreateDefaultValue(targetType);
            Type type1 = value.GetType();
            if (type1 == targetType || targetType.IsInstanceOfType(value))
                return value;
            if (type1.IsConvertible() && targetType.IsConvertible())
            {
                if (!targetType.IsEnum)
                    return ConvertObject(type1, targetType, value);
                string str = value as string;
                if (str != null)
                    return Enum.Parse(targetType, str, true);
                return Enum.ToObject(targetType, value);
            }
            IEnumerable source1 = value as IEnumerable;
            if (targetType.IsArray && source1 != null)
            {
                Type elementType = type1.GetElementType();
                Type destinationElementType = targetType.GetElementType();
                IEnumerable<object> source2 = source1.Cast<object>();
                if (!destinationElementType.IsAssignableFrom(elementType))
                    source2 = source2.Select(x => ConvertTo(x, destinationElementType));
                return EnumerableToArrayCache.GetOrAdd(destinationElementType, type => EnumerableToArrayMethod.MakeGenericMethod(type)).Invoke(null, new object[1] { source2 });
            }
            IDictionary<string, object> dictionary1 = value as IDictionary<string, object>;
            Type interfaceType1 = TryGetInterfaceType(targetType, typeof(IDictionary<,>));
            if (dictionary1 != null && interfaceType1 != null)
            {
                object instance = MethodFactory.CreateInstance(targetType);
                AdderMethodInfo orAdd = AdderMethodCache.GetOrAdd(interfaceType1, type => new AdderMethodInfo(type));
                foreach (KeyValuePair<string, object> keyValuePair in dictionary1)
                    orAdd.Method.Invoke(instance, new object[2]
                    {
                        ConvertTo( keyValuePair.Key, orAdd.TypeGenericParameters[0]),
                        ConvertTo(keyValuePair.Value, orAdd.TypeGenericParameters[1])
                    });
                return instance;
            }
            IDictionary dictionary2 = value as IDictionary;
            if (typeof(IDictionary).IsAssignableFrom(targetType) && dictionary2 != null)
            {
                IDictionary instance = (IDictionary)MethodFactory.CreateInstance(targetType);
                foreach (DictionaryEntry dictionaryEntry in dictionary2)
                    instance.Add(dictionaryEntry.Key, dictionaryEntry.Value);
                return instance;
            }
            Type interfaceType2 = TryGetInterfaceType(targetType, typeof(IList<>));
            if (interfaceType2 != null && source1 != null)
            {
                object instance = MethodFactory.CreateInstance(targetType);
                AdderMethodInfo orAdd = AdderMethodCache.GetOrAdd(interfaceType2, type => new AdderMethodInfo(type));
                foreach (object obj in source1)
                    orAdd.Method.Invoke(instance, new object[1]
                    {
                        ConvertTo(obj, orAdd.TypeGenericParameters[0])
                    });
                return instance;
            }
            if (typeof(IList).IsAssignableFrom(targetType) && source1 != null)
            {
                IList instance = (IList)MethodFactory.CreateInstance(targetType);
                foreach (object obj in source1)
                    instance.Add(obj);
                return instance;
            }
            if (targetType == typeof(Guid))
            {
                string input = value as string;
                if (input != null)
                    return Guid.Parse(input);
                byte[] b = value as byte[];
                if (b != null)
                    return (object)new Guid(b);
            }
            if (!targetType.IsNullable())
                return ConvertObject(type1, targetType, value);
            Type underlyingType = Nullable.GetUnderlyingType(targetType);
            return Convert.ChangeType(value, underlyingType, CultureInfo.InvariantCulture);
        }

        private static object ConvertObject(Type sourceType, Type targetType, object value)
        {
            try
            {
                TypeConverter converter = TypeDescriptor.GetConverter(sourceType);

                if (converter.CanConvertTo(targetType))
                    return converter.ConvertTo(null, CultureInfo.InvariantCulture, value, targetType);
                return Convert.ChangeType(value, targetType, CultureInfo.InvariantCulture);
            }
            catch (Exception)
            {
                //Console.WriteLine(ex.StackTrace);
            }
            return null;
        }

        private static object CreateDefaultValue(Type type)
        {
            if (type.IsValueType)
                return Activator.CreateInstance(type);
            return null;
        }

        private static Type TryGetInterfaceType(Type targetType, Type type)
        {
            return ((IEnumerable<Type>)targetType.GetInterfaces()).Where(x => x.IsGenericType).FirstOrDefault(x => typeof(IDictionary<,>) == x.GetGenericTypeDefinition());
        }

        private struct AdderMethodInfo
        {
            public readonly MethodInfo Method;
            public readonly Type[] TypeGenericParameters;

            public AdderMethodInfo(Type genericType)
            {
                Method = genericType.GetMethod("Add");
                TypeGenericParameters = genericType.GetGenericArguments();
            }
        }
    }
}