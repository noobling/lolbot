// Decompiled with JetBrains decompiler
// Type: RtmpSharp.Messaging.ConnectMessageEventArgs
// Assembly: rtmp-sharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8588136F-A4B9-4004-9712-4EA13AA4AF9D
// Assembly location: C:\Users\Hesa\Desktop\eZ_Source\bin\Debug\rtmp-sharp.dll

using RtmpSharp.IO;
using RtmpSharp.Messaging.Messages;

namespace RtmpSharp.Messaging
{
  public class ConnectMessageEventArgs : CommandMessageReceivedEventArgs
  {
    public readonly string AuthToken;
    public readonly string ClientId;
    public readonly AsObject ConnectionParameters;

    internal ConnectMessageEventArgs(string clientId, string authToken, CommandMessage message, string endpoint, string dsId, int invokeId, AsObject cParameters)
      : base(message, endpoint, dsId, invokeId)
    {
      this.ClientId = clientId;
      this.AuthToken = authToken;
      this.ConnectionParameters = cParameters;
    }
  }
}
