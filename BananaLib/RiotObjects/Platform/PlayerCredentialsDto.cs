// Decompiled with JetBrains decompiler
// Type: BananaLib.RiotObjects.Platform.PlayerCredentialsDto
// Assembly: BananaLib, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 75213AF3-E339-4AEB-B3FE-095F85BC5F53
// Assembly location: C:\Users\Hesa\Desktop\eZ\BananaLib.dll

using RtmpSharp.IO;
using System;

namespace BananaLib.RiotObjects.Platform
{
  [SerializedName("com.riotgames.platform.game.PlayerCredentialsDto")]
  [Serializable]
  public class PlayerCredentialsDto
  {
    [SerializedName("summonerName")]
    public string SummonerName { get; set; }

    [SerializedName("gameId")]
    public double GameId { get; set; }

    [SerializedName("championId")]
    public int ChampionId { get; set; }

    [SerializedName("playerId")]
    public double PlayerId { get; set; }

    [SerializedName("serverIp")]
    public string ServerIp { get; set; }

    [SerializedName("serverPort")]
    public int ServerPort { get; set; }

    [SerializedName("encryptionKey")]
    public string EncryptionKey { get; set; }

    [SerializedName("handshakeToken")]
    public string HandshakeToken { get; set; }

    [SerializedName("observer")]
    public bool Observer { get; set; }

    [SerializedName("observerServerIp")]
    public string ObserverServerIp { get; set; }

    [SerializedName("observerServerPort")]
    public int ObserverServerPort { get; set; }

    [SerializedName("observerEncryptionKey")]
    public string ObserverEncryptionKey { get; set; }
  }
}
