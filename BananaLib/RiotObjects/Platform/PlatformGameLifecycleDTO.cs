// Decompiled with JetBrains decompiler
// Type: BananaLib.RiotObjects.Platform.PlatformGameLifecycleDTO
// Assembly: BananaLib, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 75213AF3-E339-4AEB-B3FE-095F85BC5F53
// Assembly location: C:\Users\Hesa\Desktop\eZ\BananaLib.dll

using RtmpSharp.IO;
using System;

namespace BananaLib.RiotObjects.Platform
{
  [SerializedName("com.riotgames.platform.game.PlatformGameLifecycleDTO")]
  [Serializable]
  public class PlatformGameLifecycleDTO
  {
    [SerializedName("gameSpecificLoyaltyRewards")]
    public object GameSpecificLoyaltyRewards { get; set; }

    [SerializedName("reconnectDelay")]
    public int ReconnectDelay { get; set; }

    [SerializedName("lastModifiedDate")]
    public object LastModifiedDate { get; set; }

    [SerializedName("game")]
    public GameDTO Game { get; set; }

    [SerializedName("playerCredentials")]
    public PlayerCredentialsDto PlayerCredentials { get; set; }

    [SerializedName("gameName")]
    public string GameName { get; set; }

    [SerializedName("connectivityStateEnum")]
    public object ConnectivityStateEnum { get; set; }
  }
}
