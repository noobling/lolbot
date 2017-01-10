// Decompiled with JetBrains decompiler
// Type: RtmpSharp.Net.RtmpHeader
// Assembly: rtmp-sharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8588136F-A4B9-4004-9712-4EA13AA4AF9D
// Assembly location: C:\Users\Hesa\Desktop\eZ_Source\bin\Debug\rtmp-sharp.dll

namespace RtmpSharp.Net
{
  internal class RtmpHeader
  {
    public int PacketLength { get; set; }

    public int StreamId { get; set; }

    public MessageType MessageType { get; set; }

    public int MessageStreamId { get; set; }

    public int Timestamp { get; set; }

    public bool IsTimerRelative { get; set; }

    public static int GetHeaderLength(ChunkMessageHeaderType chunkMessageHeaderType)
    {
      switch (chunkMessageHeaderType)
      {
        case ChunkMessageHeaderType.New:
          return 11;
        case ChunkMessageHeaderType.SameSource:
          return 7;
        case ChunkMessageHeaderType.TimestampAdjustment:
          return 3;
        case ChunkMessageHeaderType.Continuation:
          return 0;
        default:
          return -1;
      }
    }

    public RtmpHeader Clone()
    {
      return (RtmpHeader) this.MemberwiseClone();
    }
  }
}
