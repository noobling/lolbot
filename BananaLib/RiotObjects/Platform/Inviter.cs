// Decompiled with JetBrains decompiler
// Type: BananaLib.RiotObjects.Platform.Inviter
// Assembly: BananaLib, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 75213AF3-E339-4AEB-B3FE-095F85BC5F53
// Assembly location: C:\Users\Hesa\Desktop\eZ\BananaLib.dll

using RtmpSharp.IO;
using System;

namespace BananaLib.RiotObjects.Platform
{
  [SerializedName("com.riotgames.platform.gameinvite.contract.Inviter")]
  [Serializable]
  public class Inviter
  {
    [SerializedName("previousSeasonHighestTier")]
    public string PreviousSeasonHighestTier { get; set; }

    [SerializedName("summonerName")]
    public string SummonerName { get; set; }

    [SerializedName("summonerId")]
    public double SummonerId { get; set; }
  }
}
