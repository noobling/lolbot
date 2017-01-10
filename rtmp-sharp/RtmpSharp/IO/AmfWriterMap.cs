// Decompiled with JetBrains decompiler
// Type: RtmpSharp.IO.AmfWriterMap
// Assembly: rtmp-sharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8588136F-A4B9-4004-9712-4EA13AA4AF9D
// Assembly location: C:\Users\Hesa\Desktop\eZ_Source\bin\Debug\rtmp-sharp.dll

using System;
using System.Collections.Generic;

namespace RtmpSharp.IO
{
  internal class AmfWriterMap : Dictionary<Type, IAmfItemWriter>
  {
    public IAmfItemWriter DefaultWriter { get; }

    public AmfWriterMap(IAmfItemWriter defaultWriter)
    {
      this.DefaultWriter = defaultWriter;
    }
  }
}
