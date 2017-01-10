// Decompiled with JetBrains decompiler
// Type: BananaLib.RiotObjects.Platform.PlayerStatSummary
// Assembly: BananaLib, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 75213AF3-E339-4AEB-B3FE-095F85BC5F53
// Assembly location: C:\Users\Hesa\Desktop\eZ\BananaLib.dll

using RtmpSharp.IO;
using System;

namespace BananaLib.RiotObjects.Platform
{
  [SerializedName("com.riotgames.platform.statistics.PlayerStatSummary")]
  [Serializable]
  public class PlayerStatSummary
  {
    [SerializedName("maxRating")]
    public int MaxRating { get; set; }

    [SerializedName("playerStatSummaryTypeString")]
    public string PlayerStatSummaryTypeString { get; set; }

    [SerializedName("aggregatedStats")]
    public SummaryAggStats AggregatedStats { get; set; }

    [SerializedName("modifyDate")]
    public DateTime ModifyDate { get; set; }

    [SerializedName("leaves")]
    public object Leaves { get; set; }

    [SerializedName("playerStatSummaryType")]
    public string PlayerStatSummaryType { get; set; }

    [SerializedName("userId")]
    public double UserId { get; set; }

    [SerializedName("losses")]
    public int Losses { get; set; }

    [SerializedName("rating")]
    public int Rating { get; set; }

    [SerializedName("aggregatedStatsJson")]
    public object AggregatedStatsJson { get; set; }

    [SerializedName("wins")]
    public int Wins { get; set; }
  }
}
