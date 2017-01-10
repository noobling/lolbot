// Decompiled with JetBrains decompiler
// Type: RtmpSharp.Messaging.CommandMessageReceivedEventArgs
// Assembly: rtmp-sharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8588136F-A4B9-4004-9712-4EA13AA4AF9D
// Assembly location: C:\Users\Hesa\Desktop\eZ_Source\bin\Debug\rtmp-sharp.dll

using RtmpSharp.Messaging.Messages;
using System;

namespace RtmpSharp.Messaging
{
  public class CommandMessageReceivedEventArgs : EventArgs
  {
    public readonly string DsId;
    public readonly string Endpoint;
    public readonly int InvokeId;
    public readonly CommandMessage Message;
    public readonly CommandOperation Operation;
    public AcknowledgeMessageExt Result;

    internal CommandMessageReceivedEventArgs(CommandMessage message, string endpoint, string dsId, int invokeId)
    {
      this.DsId = dsId;
      this.Operation = message.Operation;
      this.Endpoint = endpoint;
      this.Message = message;
      this.InvokeId = invokeId;
    }
  }
}
