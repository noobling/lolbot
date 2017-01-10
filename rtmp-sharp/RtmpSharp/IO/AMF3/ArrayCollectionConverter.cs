// Decompiled with JetBrains decompiler
// Type: RtmpSharp.IO.AMF3.ArrayCollectionConverter
// Assembly: rtmp-sharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8588136F-A4B9-4004-9712-4EA13AA4AF9D
// Assembly location: C:\Users\Hesa\Desktop\eZ_Source\bin\Debug\rtmp-sharp.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;

namespace RtmpSharp.IO.AMF3
{
  public class ArrayCollectionConverter : TypeConverter
  {
    private static readonly Type[] ConvertibleTypes = new Type[2]{ typeof (ArrayCollection), typeof (IList) };

    public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
    {
      return MiniTypeConverter.ConvertTo(value, destinationType);
    }

    public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
    {
      if (!destinationType.IsArray)
        return ((IEnumerable<Type>) ArrayCollectionConverter.ConvertibleTypes).Any<Type>((Func<Type, bool>) (x => x == destinationType));
      return true;
    }
  }
}
