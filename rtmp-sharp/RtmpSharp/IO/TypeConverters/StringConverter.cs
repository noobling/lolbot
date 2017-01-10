// Decompiled with JetBrains decompiler
// Type: RtmpSharp.IO.TypeConverters.StringConverter
// Assembly: rtmp-sharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8588136F-A4B9-4004-9712-4EA13AA4AF9D
// Assembly location: C:\Users\Hesa\Desktop\eZ_Source\bin\Debug\rtmp-sharp.dll

using System;
using System.ComponentModel;
using System.Globalization;

namespace RtmpSharp.IO.TypeConverters
{
  internal class StringConverter : System.ComponentModel.StringConverter
  {
    public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
    {
      if (sourceType == typeof (char))
        return true;
      return base.CanConvertFrom(context, sourceType);
    }

    public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
    {
      if (value is char)
        return (object) value.ToString();
      return base.ConvertFrom(context, culture, value);
    }

    public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
    {
      string str = value as string;
      if (str == null)
        return base.ConvertTo(context, culture, value, destinationType);
      if (str.Length == 0)
        return (object) null;
      if (str.Length == 1)
        return (object) str[0];
      throw new ArgumentException("Cannot convert string to char: string length is too long.");
    }

    public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
    {
      if (destinationType == typeof (string))
        return true;
      return base.CanConvertTo(context, destinationType);
    }
  }
}
