// Decompiled with JetBrains decompiler
// Type: BananaLib.RiotObjects.Platform.SummonerLevel
// Assembly: BananaLib, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 75213AF3-E339-4AEB-B3FE-095F85BC5F53
// Assembly location: C:\Users\Hesa\Desktop\eZ\BananaLib.dll

using RtmpSharp.IO;
using System;

namespace BananaLib.RiotObjects.Platform
{
  [SerializedName("com.riotgames.platform.summoner.SummonerLevel")]
  [Serializable]
  public class SummonerLevel
  {
    [SerializedName("summonerLevel")]
    public int Level { get; set; }

    [SerializedName("summonerTier")]
    public double SummonerTier { get; set; }

    [SerializedName("infTierMod")]
    public double InfTierMod { get; set; }

    [SerializedName("expTierMod")]
    public double ExpTierMod { get; set; }

    [SerializedName("expToNextLevel")]
    public double ExpToNextLevel { get; set; }
  }
}
