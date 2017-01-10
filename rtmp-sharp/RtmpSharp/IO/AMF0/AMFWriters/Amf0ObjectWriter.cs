// Decompiled with JetBrains decompiler
// Type: RtmpSharp.IO.AMF0.AMFWriters.Amf0ObjectWriter
// Assembly: rtmp-sharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8588136F-A4B9-4004-9712-4EA13AA4AF9D
// Assembly location: C:\Users\Hesa\Desktop\eZ_Source\bin\Debug\rtmp-sharp.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace RtmpSharp.IO.AMF0.AMFWriters
{
  internal class Amf0ObjectWriter : IAmfItemWriter
  {
    public void WriteData(AmfWriter writer, object obj)
    {
      IDictionary<string, object> dictionary;
      if ((dictionary = obj as IDictionary<string, object>) != null)
      {
        writer.WriteAmf0AssociativeArray(dictionary);
      }
      else
      {
        IEnumerable source;
        if ((source = obj as IEnumerable) != null)
        {
          writer.WriteMarker(Amf0TypeMarkers.StrictArray);
          writer.WriteAmf0Array((Array) source.Cast<object>().ToArray<object>());
        }
        else
          writer.WriteAmf0TypedObject(obj);
      }
    }
  }
}
