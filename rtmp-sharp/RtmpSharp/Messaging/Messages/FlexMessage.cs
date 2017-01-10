// Decompiled with JetBrains decompiler
// Type: RtmpSharp.Messaging.Messages.FlexMessage
// Assembly: rtmp-sharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8588136F-A4B9-4004-9712-4EA13AA4AF9D
// Assembly location: C:\Users\Hesa\Desktop\eZ_Source\bin\Debug\rtmp-sharp.dll

using RtmpSharp.IO;

namespace RtmpSharp.Messaging.Messages
{
  public class FlexMessage
  {
    [SerializedName("clientId")]
    public string ClientId { get; set; }

    [SerializedName("destination")]
    public string Destination { get; set; }

    [SerializedName("messageId")]
    public string MessageId { get; set; }

    [SerializedName("timestamp")]
    public long Timestamp { get; set; }

    [SerializedName("timeToLive")]
    public long TimeToLive { get; set; }

    [SerializedName("body")]
    public object Body { get; set; }

    [SerializedName("headers")]
    public AsObject Headers { get; set; }

    public FlexMessage()
    {
      this.MessageId = Uuid.NewUuid();
    }
  }
}
