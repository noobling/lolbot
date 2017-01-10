// Decompiled with JetBrains decompiler
// Type: RtmpSharp.TypeSerializer
// Assembly: rtmp-sharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8588136F-A4B9-4004-9712-4EA13AA4AF9D
// Assembly location: C:\Users\Hesa\Desktop\eZ_Source\bin\Debug\rtmp-sharp.dll

using System;
using System.ComponentModel;

namespace RtmpSharp
{
  public static class TypeSerializer
  {
    public static void RegisterTypeConverters()
    {
      TypeDescriptor.AddAttributes(typeof (string), new Attribute[1]
      {
        (Attribute) new TypeConverterAttribute(typeof (RtmpSharp.IO.TypeConverters.StringConverter))
      });
    }
  }
}
