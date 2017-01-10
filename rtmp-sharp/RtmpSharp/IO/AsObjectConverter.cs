// Decompiled with JetBrains decompiler
// Type: RtmpSharp.IO.AsObjectConverter
// Assembly: rtmp-sharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8588136F-A4B9-4004-9712-4EA13AA4AF9D
// Assembly location: C:\Users\Hesa\Desktop\eZ_Source\bin\Debug\rtmp-sharp.dll

using Complete;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;

namespace RtmpSharp.IO
{
  public class AsObjectConverter : TypeConverter
  {
    public static SerializationContext DefaultSerializationContext = new SerializationContext();
    private readonly SerializationContext _serializationContext;

    public AsObjectConverter()
    {
      this._serializationContext = AsObjectConverter.DefaultSerializationContext;
    }

    public AsObjectConverter(SerializationContext serializationContext)
    {
      this._serializationContext = serializationContext;
    }

    public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
    {
      return !destinationType.IsValueType && !destinationType.IsEnum && (!destinationType.IsArray && !destinationType.IsAbstract) && !destinationType.IsInterface;
    }

    public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
    {
      object instance = MethodFactory.CreateInstance(destinationType);
      ClassDescription classDescription = this._serializationContext.GetClassDescription(destinationType, instance);
      IDictionary<string, object> dictionary = value as IDictionary<string, object>;
      if (dictionary == null)
        return base.ConvertTo(context, culture, value, destinationType);
      foreach (KeyValuePair<string, object> keyValuePair in (IEnumerable<KeyValuePair<string, object>>) dictionary)
      {
        IMemberWrapper memberWrapper;
        if (classDescription.TryGetMember(keyValuePair.Key, out memberWrapper))
          memberWrapper.SetValue(instance, keyValuePair.Value);
      }
      return instance;
    }
  }
}
