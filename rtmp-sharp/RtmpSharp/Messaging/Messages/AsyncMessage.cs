// Decompiled with JetBrains decompiler
// Type: RtmpSharp.Messaging.Messages.AsyncMessage
// Assembly: rtmp-sharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8588136F-A4B9-4004-9712-4EA13AA4AF9D
// Assembly location: C:\Users\Hesa\Desktop\eZ_Source\bin\Debug\rtmp-sharp.dll

using RtmpSharp.IO;
using System;

namespace RtmpSharp.Messaging.Messages
{
  [SerializedName("flex.messaging.messages.AsyncMessage")]
  [Serializable]
  public class AsyncMessage : FlexMessage
  {
    [SerializedName("correlationId")]
    public string CorrelationId { get; set; }
  }
}
