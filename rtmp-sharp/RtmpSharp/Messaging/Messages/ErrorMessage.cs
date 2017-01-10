// Decompiled with JetBrains decompiler
// Type: RtmpSharp.Messaging.Messages.ErrorMessage
// Assembly: rtmp-sharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8588136F-A4B9-4004-9712-4EA13AA4AF9D
// Assembly location: C:\Users\Hesa\Desktop\eZ_Source\bin\Debug\rtmp-sharp.dll

using RtmpSharp.IO;
using System;

namespace RtmpSharp.Messaging.Messages
{
  [SerializedName("flex.messaging.messages.ErrorMessage")]
  [Serializable]
  public class ErrorMessage : FlexMessage
  {
    [SerializedName("faultCode")]
    public string FaultCode { get; set; }

    [SerializedName("faultString")]
    public string FaultString { get; set; }

    [SerializedName("faultDetail")]
    public string FaultDetail { get; set; }

    [SerializedName("rootCause")]
    public object RootCause { get; set; }

    [SerializedName("extendedData")]
    public object ExtendedData { get; set; }

    [SerializedName("correlationId")]
    public object CorrelationId { get; set; }
  }
}
