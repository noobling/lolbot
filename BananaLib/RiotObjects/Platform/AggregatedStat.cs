// Decompiled with JetBrains decompiler
// Type: BananaLib.RiotObjects.Platform.AggregatedStat
// Assembly: BananaLib, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 75213AF3-E339-4AEB-B3FE-095F85BC5F53
// Assembly location: C:\Users\Hesa\Desktop\eZ\BananaLib.dll

using RtmpSharp.IO;
using System;

namespace BananaLib.RiotObjects.Platform
{
  [SerializedName("com.riotgames.platform.statistics.AggregatedStat")]
  [Serializable]
  public class AggregatedStat
  {
    [SerializedName("statType")]
    public string StatType { get; set; }

    [SerializedName("count")]
    public double Count { get; set; }

    [SerializedName("value")]
    public double Value { get; set; }

    [SerializedName("championId")]
    public double ChampionId { get; set; }
  }
}
