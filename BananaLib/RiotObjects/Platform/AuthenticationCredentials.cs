// Decompiled with JetBrains decompiler
// Type: BananaLib.RiotObjects.Platform.AuthenticationCredentials
// Assembly: BananaLib, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 75213AF3-E339-4AEB-B3FE-095F85BC5F53
// Assembly location: C:\Users\Hesa\Desktop\eZ\BananaLib.dll

using RtmpSharp.IO;
using System;

namespace BananaLib.RiotObjects.Platform
{
  [SerializedName("com.riotgames.platform.login.AuthenticationCredentials")]
  [Serializable]
  public class AuthenticationCredentials
  {
    [SerializedName("username")]
    public string Username { get; set; }

    [SerializedName("password")]
    public string Password { get; set; }

    [SerializedName("partnerCredentials")]
    public object PartnerCredentials { get; set; }

    [SerializedName("oldPassword")]
    public object OldPassword { get; set; }

    [SerializedName("securityAnswer")]
    public object SecurityAnswer { get; set; }

    [SerializedName("domain")]
    public string Domain { get; set; }

    [SerializedName("clientVersion")]
    public string ClientVersion { get; set; }

    [SerializedName("locale")]
    public string Locale { get; set; }

    [SerializedName("authToken")]
    public string AuthToken { get; set; }

    [SerializedName("operatingSystem")]
    public string OperatingSystem { get; set; }

    [SerializedName("ipAddress")]
    public string IpAddress { get; set; }
    }
}
