// Decompiled with JetBrains decompiler
// Type: RtmpSharp.IO.AMF3.AMFWriters.Amf3VectorWriter`1
// Assembly: rtmp-sharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8588136F-A4B9-4004-9712-4EA13AA4AF9D
// Assembly location: C:\Users\Hesa\Desktop\eZ_Source\bin\Debug\rtmp-sharp.dll

using System;
using System.Collections;

namespace RtmpSharp.IO.AMF3.AMFWriters
{
  internal class Amf3VectorWriter<T> : IAmfItemWriter
  {
    private readonly Amf3TypeMarkers _typeMarker;
    private readonly Action<AmfWriter, IList> _write;

    public Amf3VectorWriter(Amf3TypeMarkers typeMarker, Action<AmfWriter, IList> write)
    {
      this._typeMarker = typeMarker;
      this._write = write;
    }

    public void WriteData(AmfWriter writer, object obj)
    {
      writer.WriteMarker(this._typeMarker);
      this._write(writer, obj as IList);
    }
  }
}
