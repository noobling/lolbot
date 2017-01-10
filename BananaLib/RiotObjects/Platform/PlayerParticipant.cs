// Decompiled with JetBrains decompiler
// Type: BananaLib.RiotObjects.Platform.PlayerParticipant
// Assembly: BananaLib, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 75213AF3-E339-4AEB-B3FE-095F85BC5F53
// Assembly location: C:\Users\Hesa\Desktop\eZ\BananaLib.dll

using RtmpSharp.IO;
using System;

namespace BananaLib.RiotObjects.Platform
{
  [SerializedName("com.riotgames.platform.game.PlayerParticipant")]
  [Serializable]
  public class PlayerParticipant : GameParticipant
  {
    [SerializedName("accountId")]
    public double AccountId { get; set; }

    [SerializedName("profileIconId")]
    public int ProfileIconId { get; set; }

    [SerializedName("summonerId")]
    public double SummonerId { get; set; }

    [SerializedName("summonerLevel")]
    public double SummonerLevel { get; set; }

    [SerializedName("clientInSynch")]
    public bool ClientInSynch { get; set; }
  }
}
