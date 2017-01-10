// Decompiled with JetBrains decompiler
// Type: RtmpSharp.Messaging.RtmpEvent
// Assembly: rtmp-sharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8588136F-A4B9-4004-9712-4EA13AA4AF9D
// Assembly location: C:\Users\Hesa\Desktop\eZ_Source\bin\Debug\rtmp-sharp.dll

using RtmpSharp.Net;

namespace RtmpSharp.Messaging
{
  internal abstract class RtmpEvent
  {
    public RtmpHeader Header { get; set; }

    public int Timestamp { get; set; }

    public MessageType MessageType { get; set; }

    protected RtmpEvent(MessageType messageType)
    {
      this.MessageType = messageType;
    }
  }
}
