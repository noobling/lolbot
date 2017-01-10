// Decompiled with JetBrains decompiler
// Type: RtmpSharp.IO.AMF3.AMFWriters.Amf3ObjectWriter
// Assembly: rtmp-sharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8588136F-A4B9-4004-9712-4EA13AA4AF9D
// Assembly location: C:\Users\Hesa\Desktop\eZ_Source\bin\Debug\rtmp-sharp.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace RtmpSharp.IO.AMF3.AMFWriters
{
  internal class Amf3ObjectWriter : IAmfItemWriter
  {
    public void WriteData(AmfWriter writer, object obj)
    {
      if (!(obj is IExternalizable))
      {
        IDictionary<string, object> dictionary1;
        if ((dictionary1 = obj as IDictionary<string, object>) != null)
        {
          writer.WriteMarker(Amf3TypeMarkers.Array);
          writer.WriteAmf3AssociativeArray(dictionary1);
          return;
        }
        IDictionary dictionary2;
        if ((dictionary2 = obj as IDictionary) != null)
        {
          writer.WriteMarker(Amf3TypeMarkers.Dictionary);
          writer.WriteAmf3Dictionary(dictionary2);
        }
        IEnumerable source;
        if ((source = obj as IEnumerable) != null)
        {
          writer.WriteMarker(Amf3TypeMarkers.Array);
          writer.WriteAmf3Array((Array) source.Cast<object>().ToArray<object>());
          return;
        }
      }
      writer.WriteMarker(Amf3TypeMarkers.Object);
      writer.WriteAmf3Object(obj);
    }
  }
}
