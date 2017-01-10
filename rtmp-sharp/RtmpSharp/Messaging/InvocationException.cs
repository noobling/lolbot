// Decompiled with JetBrains decompiler
// Type: RtmpSharp.Messaging.InvocationException
// Assembly: rtmp-sharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8588136F-A4B9-4004-9712-4EA13AA4AF9D
// Assembly location: C:\Users\Hesa\Desktop\eZ_Source\bin\Debug\rtmp-sharp.dll

using RtmpSharp.Messaging.Messages;
using System;

namespace RtmpSharp.Messaging
{
  public class InvocationException : Exception
  {
    public string FaultCode { get; set; }

    public string FaultString { get; set; }

    public string FaultDetail { get; set; }

    public object RootCause { get; set; }

    public object ExtendedData { get; set; }

    public object SourceException { get; set; }

    public override string Message
    {
      get
      {
        return this.FaultString;
      }
    }

    public override string StackTrace
    {
      get
      {
        return this.FaultDetail;
      }
    }

    internal InvocationException(ErrorMessage errorMessage)
    {
      this.SourceException = (object) errorMessage;
      this.FaultCode = errorMessage.FaultCode;
      this.FaultString = errorMessage.FaultString;
      this.FaultDetail = errorMessage.FaultDetail;
      this.RootCause = errorMessage.RootCause;
      this.ExtendedData = errorMessage.ExtendedData;
    }

    public InvocationException()
    {
    }
  }
}
