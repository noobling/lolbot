// Decompiled with JetBrains decompiler
// Type: RtmpSharp.Messaging.Events.Command
// Assembly: rtmp-sharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8588136F-A4B9-4004-9712-4EA13AA4AF9D
// Assembly location: C:\Users\Hesa\Desktop\eZ_Source\bin\Debug\rtmp-sharp.dll

using RtmpSharp.Net;

namespace RtmpSharp.Messaging.Events
{
  internal class Command : RtmpEvent
  {
    public Method MethodCall { get; internal set; }

    public byte[] Buffer { get; internal set; }

    public int InvokeId { get; internal set; }

    public object ConnectionParameters { get; internal set; }

    public Command(MessageType messageType)
      : base(messageType)
    {
    }
  }
}
