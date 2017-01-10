// Decompiled with JetBrains decompiler
// Type: BananaLib.RiotObjects.Platform.ChampionStatInfo
// Assembly: BananaLib, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 75213AF3-E339-4AEB-B3FE-095F85BC5F53
// Assembly location: C:\Users\Hesa\Desktop\eZ\BananaLib.dll

using RtmpSharp.IO;
using System;
using System.Collections.Generic;

namespace BananaLib.RiotObjects.Platform
{
  [SerializedName("com.riotgames.platform.statistics.ChampionStatInfo")]
  [Serializable]
  public class ChampionStatInfo
  {
    [SerializedName("totalGamesPlayed")]
    public int TotalGamesPlayed { get; set; }

    [SerializedName("accountId")]
    public double AccountId { get; set; }

    [SerializedName("stats")]
    public List<AggregatedStat> Stats { get; set; }

    [SerializedName("championId")]
    public double ChampionId { get; set; }
  }
}
