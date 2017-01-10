// Decompiled with JetBrains decompiler
// Type: RtmpSharp.Messaging.MessageReceivedEventArgs
// Assembly: rtmp-sharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8588136F-A4B9-4004-9712-4EA13AA4AF9D
// Assembly location: C:\Users\Hesa\Desktop\eZ_Source\bin\Debug\rtmp-sharp.dll

using RtmpSharp.Messaging.Messages;
using System;

namespace RtmpSharp.Messaging
{
  public class MessageReceivedEventArgs : EventArgs
  {
    public readonly string ClientId;
    public readonly AsyncMessageExt Message;
    public readonly string Subtopic;

    internal MessageReceivedEventArgs(string clientId, string subtopic, AsyncMessageExt message)
    {
      this.ClientId = clientId;
      this.Subtopic = subtopic;
      this.Message = message;
    }
  }
}
