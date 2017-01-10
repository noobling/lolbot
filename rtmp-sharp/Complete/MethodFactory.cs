// Decompiled with JetBrains decompiler
// Type: Complete.MethodFactory
// Assembly: rtmp-sharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8588136F-A4B9-4004-9712-4EA13AA4AF9D
// Assembly location: C:\Users\Hesa\Desktop\eZ_Source\bin\Debug\rtmp-sharp.dll

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Complete
{
  internal static class MethodFactory
  {
    private static readonly ConcurrentDictionary<object, MethodFactory.ConstructorCall> ConstructorCache = new ConcurrentDictionary<object, MethodFactory.ConstructorCall>();
    private static readonly ConcurrentDictionary<object, MethodFactory.MethodCall> MethodCache = new ConcurrentDictionary<object, MethodFactory.MethodCall>();

    private static MethodFactory.ExpressionArgsPair GetObjectCreatorExpressions(ConstructorInfo constructor)
    {
      ParameterExpression args = Expression.Parameter(typeof (object[]), "args");
      Expression[] callArgsExpressions = MethodFactory.GetMethodCallArgsExpressions(constructor.GetParameters(), args);
      NewExpression newExpression = Expression.New(constructor, callArgsExpressions);
      return new MethodFactory.ExpressionArgsPair()
      {
        Expression = (Expression) newExpression,
        Parameters = new ParameterExpression[1]
        {
          args
        }
      };
    }

    public static MethodFactory.ConstructorCall CompileObjectConstructor(ConstructorInfo constructor)
    {
      MethodFactory.ExpressionArgsPair creatorExpressions = MethodFactory.GetObjectCreatorExpressions(constructor);
      return Expression.Lambda<MethodFactory.ConstructorCall>(creatorExpressions.Expression, creatorExpressions.Parameters).Compile();
    }

    public static MethodFactory.ConstructorCall CompileBoxedObjectConstructor(ConstructorInfo constructor)
    {
      MethodFactory.ExpressionArgsPair creatorExpressions = MethodFactory.GetObjectCreatorExpressions(constructor);
      return Expression.Lambda<MethodFactory.ConstructorCall>((Expression) Expression.Convert(creatorExpressions.Expression, typeof (object)), creatorExpressions.Parameters).Compile();
    }

    public static T CreateInstance<T>(Type type)
    {
      return (T) MethodFactory.CreateInstance(type);
    }

    public static object CreateInstance(Type type)
    {
      return MethodFactory.ConstructorCache.GetOrAdd((object) type, (Func<object, MethodFactory.ConstructorCall>) (_ =>
      {
        ConstructorInfo constructor = ((IEnumerable<ConstructorInfo>) type.GetConstructors()).FirstOrDefault<ConstructorInfo>((Func<ConstructorInfo, bool>) (x => x.GetParameters().Length == 0));
        
        if (constructor == null)
          throw new ArgumentException("Type does not have any accessible parameterless constructors.");
        return MethodFactory.CompileObjectConstructor(constructor);
      }))(new object[0]);
    }

    private static MethodFactory.ExpressionArgsPair GetMethodCallExpressions(MethodInfo method)
    {
      ParameterExpression parameterExpression = Expression.Parameter(typeof (object), "instance");
      UnaryExpression unaryExpression = Expression.Convert((Expression) parameterExpression, method.DeclaringType);
      ParameterExpression args = Expression.Parameter(typeof (object[]), "args");
      Expression[] callArgsExpressions = MethodFactory.GetMethodCallArgsExpressions(method.GetParameters(), args);
      Expression expression = (Expression) Expression.Call(method.IsStatic ? (Expression) null : (Expression) unaryExpression, method, callArgsExpressions);
      if (method.ReturnType == typeof (void))
        expression = (Expression) Expression.Block(expression, (Expression) Expression.Default(typeof (object)));
      return new MethodFactory.ExpressionArgsPair()
      {
        Expression = expression,
        Parameters = new ParameterExpression[2]
        {
          parameterExpression,
          args
        }
      };
    }

    public static MethodFactory.MethodCall CompileMethodCall(MethodInfo method)
    {
      MethodFactory.ExpressionArgsPair methodCallExpressions = MethodFactory.GetMethodCallExpressions(method);
      return Expression.Lambda<MethodFactory.MethodCall>(methodCallExpressions.Expression, methodCallExpressions.Parameters).Compile();
    }

    public static MethodFactory.MethodCall CompileBoxedMethodCall(MethodInfo method)
    {
      MethodFactory.ExpressionArgsPair methodCallExpressions = MethodFactory.GetMethodCallExpressions(method);
      return Expression.Lambda<MethodFactory.MethodCall>((Expression) Expression.Convert(methodCallExpressions.Expression, typeof (object)), methodCallExpressions.Parameters).Compile();
    }

    public static T CallMethod<T>(MethodInfo method, object instance, object[] arguments)
    {
      return (T) MethodFactory.CallMethod(method, instance, arguments);
    }

    public static object CallMethod(MethodInfo method, object instance, object[] arguments)
    {
      return MethodFactory.MethodCache.GetOrAdd((object) method, (Func<object, MethodFactory.MethodCall>) (_ => MethodFactory.CompileMethodCall(method)))(instance, arguments);
    }

    private static Expression[] GetMethodCallArgsExpressions(ParameterInfo[] parameters, ParameterExpression args)
    {
      return Enumerable.Range(0, parameters.Length).Select<int, Expression>((Func<int, Expression>) (i => (Expression) Expression.Convert((Expression) Expression.ArrayIndex((Expression) args, (Expression) Expression.Constant((object) i)), parameters[i].ParameterType))).ToArray<Expression>();
    }

    public delegate object ConstructorCall(object[] args);

    public delegate object MethodCall(object instance, object[] args);

    private struct ExpressionArgsPair
    {
      public Expression Expression;
      public ParameterExpression[] Parameters;
    }
  }
}
