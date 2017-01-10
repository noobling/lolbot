// Decompiled with JetBrains decompiler
// Type: RtmpSharp.Messaging.Messages.CommandMessage
// Assembly: rtmp-sharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8588136F-A4B9-4004-9712-4EA13AA4AF9D
// Assembly location: C:\Users\Hesa\Desktop\eZ_Source\bin\Debug\rtmp-sharp.dll

using RtmpSharp.IO;
using System;

namespace RtmpSharp.Messaging.Messages
{
  [SerializedName("DSC", Canonical = false)]
  [SerializedName("flex.messaging.messages.CommandMessage")]
  [Serializable]
  public class CommandMessage : AsyncMessage
  {
    [SerializedName("messageRefType")]
    public string MessageRefType { get; set; }

    [SerializedName("operation")]
    public CommandOperation Operation { get; set; }
  }
}
