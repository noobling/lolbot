// Decompiled with JetBrains decompiler
// Type: RtmpSharp.IO.AMF0.AMFWriters.Amf0DateTimeWriter
// Assembly: rtmp-sharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8588136F-A4B9-4004-9712-4EA13AA4AF9D
// Assembly location: C:\Users\Hesa\Desktop\eZ_Source\bin\Debug\rtmp-sharp.dll

using System;

namespace RtmpSharp.IO.AMF0.AMFWriters
{
  internal class Amf0DateTimeWriter : IAmfItemWriter
  {
    public void WriteData(AmfWriter writer, object obj)
    {
      writer.WriteMarker(Amf0TypeMarkers.Date);
      writer.WriteAmf0DateTime((DateTime) obj);
    }
  }
}
