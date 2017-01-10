// Decompiled with JetBrains decompiler
// Type: BananaLib.RiotObjects.Platform.TeamPlayerAggregatedStatsDTO
// Assembly: BananaLib, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 75213AF3-E339-4AEB-B3FE-095F85BC5F53
// Assembly location: C:\Users\Hesa\Desktop\eZ\BananaLib.dll

using RtmpSharp.IO;
using System;

namespace BananaLib.RiotObjects.Platform
{
  [SerializedName("com.riotgames.platform.statistics.team.TeamPlayerAggregatedStatsDTO")]
  [Serializable]
  public class TeamPlayerAggregatedStatsDTO
  {
    [SerializedName("playerId")]
    public double PlayerId { get; set; }

    [SerializedName("aggregatedStats")]
    public AggregatedStats AggregatedStats { get; set; }
  }
}
