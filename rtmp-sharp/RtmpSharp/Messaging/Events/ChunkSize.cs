// Decompiled with JetBrains decompiler
// Type: RtmpSharp.Messaging.Events.ChunkSize
// Assembly: rtmp-sharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8588136F-A4B9-4004-9712-4EA13AA4AF9D
// Assembly location: C:\Users\Hesa\Desktop\eZ_Source\bin\Debug\rtmp-sharp.dll

using RtmpSharp.Net;

namespace RtmpSharp.Messaging.Events
{
  internal class ChunkSize : RtmpEvent
  {
    public int Size { get; private set; }

    public ChunkSize(int size)
      : base(MessageType.SetChunkSize)
    {
      if (size > 16777215)
        size = 16777215;
      this.Size = size;
    }
  }
}
