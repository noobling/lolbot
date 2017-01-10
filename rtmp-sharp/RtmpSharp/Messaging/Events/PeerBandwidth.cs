// Decompiled with JetBrains decompiler
// Type: RtmpSharp.Messaging.Events.PeerBandwidth
// Assembly: rtmp-sharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8588136F-A4B9-4004-9712-4EA13AA4AF9D
// Assembly location: C:\Users\Hesa\Desktop\eZ_Source\bin\Debug\rtmp-sharp.dll

using RtmpSharp.Net;

namespace RtmpSharp.Messaging.Events
{
  internal class PeerBandwidth : RtmpEvent
  {
    public int AcknowledgementWindowSize { get; private set; }

    public PeerBandwidth.BandwidthLimitType LimitType { get; private set; }

    private PeerBandwidth()
      : base(MessageType.SetPeerBandwidth)
    {
    }

    public PeerBandwidth(int acknowledgementWindowSize, PeerBandwidth.BandwidthLimitType limitType)
      : this()
    {
      this.AcknowledgementWindowSize = acknowledgementWindowSize;
      this.LimitType = limitType;
    }

    public PeerBandwidth(int acknowledgementWindowSize, byte limitType)
      : this()
    {
      this.AcknowledgementWindowSize = acknowledgementWindowSize;
      this.LimitType = (PeerBandwidth.BandwidthLimitType) limitType;
    }

    public enum BandwidthLimitType : byte
    {
      Hard,
      Soft,
      Dynamic,
    }
  }
}
