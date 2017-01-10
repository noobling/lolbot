// Decompiled with JetBrains decompiler
// Type: BananaLib.RiotObjects.Exceptions.LoginFailedException
// Assembly: BananaLib, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 75213AF3-E339-4AEB-B3FE-095F85BC5F53
// Assembly location: C:\Users\Hesa\Desktop\eZ\BananaLib.dll

using RtmpSharp.IO;
using RtmpSharp.Messaging.Messages;
using System;

namespace BananaLib.RiotObjects.Exceptions
{
  [SerializedName("com.riotgames.platform.login.LoginFailedException")]
  [Serializable]
  public class LoginFailedException : ErrorMessage
  {
    [SerializedName("errorCode")]
    public string ErrorCode { get; set; }

    [SerializedName("message")]
    public string Message { get; set; }
  }
}
