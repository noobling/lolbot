// Decompiled with JetBrains decompiler
// Type: RtmpSharp.Messaging.Messages.CommandOperation
// Assembly: rtmp-sharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8588136F-A4B9-4004-9712-4EA13AA4AF9D
// Assembly location: C:\Users\Hesa\Desktop\eZ_Source\bin\Debug\rtmp-sharp.dll

namespace RtmpSharp.Messaging.Messages
{
  public enum CommandOperation
  {
    Subscribe = 0,
    Unsubscribe = 1,
    Poll = 2,
    DataUpdateAttributes = 3,
    ClientSync = 4,
    ClientPing = 5,
    ClusterRequest = 7,
    DataUpdate = 7,
    Login = 8,
    Logout = 9,
    InvalidateSubscription = 10,
    ChannelDisconnected = 12,
    Unknown = 10000,
  }
}
