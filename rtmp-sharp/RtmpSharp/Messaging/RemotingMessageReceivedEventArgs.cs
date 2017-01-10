// Decompiled with JetBrains decompiler
// Type: RtmpSharp.Messaging.RemotingMessageReceivedEventArgs
// Assembly: rtmp-sharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8588136F-A4B9-4004-9712-4EA13AA4AF9D
// Assembly location: C:\Users\Hesa\Desktop\eZ_Source\bin\Debug\rtmp-sharp.dll

using RtmpSharp.Messaging.Messages;
using System;

namespace RtmpSharp.Messaging
{
  public class RemotingMessageReceivedEventArgs : EventArgs
  {
    public readonly string Destination;
    public readonly string Endpoint;
    public readonly int InvokeId;
    public readonly RemotingMessage Message;
    public readonly string MessageId;
    public readonly string Operation;
    public ErrorMessage Error;
    public AcknowledgeMessageExt Result;

    internal RemotingMessageReceivedEventArgs(RemotingMessage message, string endpoint, string clientId, int invokeId)
    {
      this.Message = message;
      this.Operation = message.Operation;
      this.Destination = message.Destination;
      this.Endpoint = endpoint;
      this.MessageId = clientId;
      this.InvokeId = invokeId;
    }
  }
}
