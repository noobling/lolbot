﻿// Decompiled with JetBrains decompiler
// Type: RtmpSharp.IO.AMF0.AMFWriters.Amf0GuidWriter
// Assembly: rtmp-sharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8588136F-A4B9-4004-9712-4EA13AA4AF9D
// Assembly location: C:\Users\Hesa\Desktop\eZ_Source\bin\Debug\rtmp-sharp.dll

using System;

namespace RtmpSharp.IO.AMF0.AMFWriters
{
  internal class Amf0GuidWriter : IAmfItemWriter
  {
    public void WriteData(AmfWriter writer, object obj)
    {
      writer.WriteMarker(Amf0TypeMarkers.String);
      writer.WriteUtfPrefixed(((Guid) obj).ToString());
    }
  }
}
