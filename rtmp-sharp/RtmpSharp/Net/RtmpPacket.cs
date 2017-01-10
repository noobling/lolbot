// Decompiled with JetBrains decompiler
// Type: RtmpSharp.Net.RtmpPacket
// Assembly: rtmp-sharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8588136F-A4B9-4004-9712-4EA13AA4AF9D
// Assembly location: C:\Users\Hesa\Desktop\eZ_Source\bin\Debug\rtmp-sharp.dll

using RtmpSharp.Messaging;
using System;

namespace RtmpSharp.Net
{
  internal class RtmpPacket
  {
    public RtmpHeader Header { get; set; }

    public RtmpEvent Body { get; set; }

    public byte[] Buffer { get; }

    public int Length { get; }

    public int CurrentLength { get; private set; }

    public bool IsComplete
    {
      get
      {
        return this.Length == this.CurrentLength;
      }
    }

    public RtmpPacket(RtmpHeader header)
    {
      this.Header = header;
      this.Length = header.PacketLength;
      this.Buffer = new byte[this.Length];
    }

    public RtmpPacket(RtmpEvent body)
    {
      this.Body = body;
    }

    public RtmpPacket(RtmpHeader header, RtmpEvent body)
      : this(header)
    {
      this.Body = body;
      this.Length = header.PacketLength;
    }

    internal void AddBytes(byte[] bytes)
    {
      Array.Copy((Array) bytes, 0, (Array) this.Buffer, this.CurrentLength, bytes.Length);
      this.CurrentLength = this.CurrentLength + bytes.Length;
    }
  }
}
