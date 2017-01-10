// Decompiled with JetBrains decompiler
// Type: RtmpSharp.Messaging.Messages.AcknowledgeMessage
// Assembly: rtmp-sharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8588136F-A4B9-4004-9712-4EA13AA4AF9D
// Assembly location: C:\Users\Hesa\Desktop\eZ_Source\bin\Debug\rtmp-sharp.dll

using RtmpSharp.IO;
using System;

namespace RtmpSharp.Messaging.Messages
{
  [SerializedName("flex.messaging.messages.AcknowledgeMessage")]
  [Serializable]
  public class AcknowledgeMessage : AsyncMessage
  {
    public AcknowledgeMessage()
    {
      this.Timestamp = (long) Environment.TickCount;
    }
  }
}
