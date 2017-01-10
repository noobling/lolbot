// Decompiled with JetBrains decompiler
// Type: RtmpSharp.IO.AMF3.ByteArrayConverter
// Assembly: rtmp-sharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8588136F-A4B9-4004-9712-4EA13AA4AF9D
// Assembly location: C:\Users\Hesa\Desktop\eZ_Source\bin\Debug\rtmp-sharp.dll

using System;
using System.ComponentModel;
using System.Globalization;

namespace RtmpSharp.IO.AMF3
{
  public class ByteArrayConverter : TypeConverter
  {
    public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
    {
      if (destinationType == typeof (byte[]))
        return true;
      return base.CanConvertTo(context, destinationType);
    }

    public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
    {
      if (value == null)
        throw new ArgumentNullException();
      if (destinationType == typeof (byte[]))
        return (object) ((ByteArray) value).MemoryStream.ToArray();
      return base.ConvertTo(context, culture, value, destinationType);
    }
  }
}
