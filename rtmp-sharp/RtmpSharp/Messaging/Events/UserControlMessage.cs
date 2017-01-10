// Decompiled with JetBrains decompiler
// Type: RtmpSharp.Messaging.Events.UserControlMessage
// Assembly: rtmp-sharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8588136F-A4B9-4004-9712-4EA13AA4AF9D
// Assembly location: C:\Users\Hesa\Desktop\eZ_Source\bin\Debug\rtmp-sharp.dll

using RtmpSharp.Net;

namespace RtmpSharp.Messaging.Events
{
  internal class UserControlMessage : RtmpEvent
  {
    public UserControlMessageType EventType { get; private set; }

    public int[] Values { get; private set; }

    public UserControlMessage(UserControlMessageType eventType, int[] values)
      : base(MessageType.UserControlMessage)
    {
      this.EventType = eventType;
      this.Values = values;
    }
  }
}
