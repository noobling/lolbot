// Decompiled with JetBrains decompiler
// Type: BananaLib.RiotObjects.Team.TeamStatDetail
// Assembly: BananaLib, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 75213AF3-E339-4AEB-B3FE-095F85BC5F53
// Assembly location: C:\Users\Hesa\Desktop\eZ\BananaLib.dll

using RtmpSharp.IO;
using System;

namespace BananaLib.RiotObjects.Team
{
  [SerializedName("com.riotgames.team.stats.TeamStatDetail")]
  [Serializable]
  public class TeamStatDetail
  {
    [SerializedName("maxRating")]
    public int MaxRating { get; set; }

    [SerializedName("teamIdString")]
    public string TeamIdString { get; set; }

    [SerializedName("seedRating")]
    public int SeedRating { get; set; }

    [SerializedName("losses")]
    public int Losses { get; set; }

    [SerializedName("rating")]
    public int Rating { get; set; }

    [SerializedName("teamStatTypeString")]
    public string TeamStatTypeString { get; set; }

    [SerializedName("averageGamesPlayed")]
    public int AverageGamesPlayed { get; set; }

    [SerializedName("teamId")]
    public TeamId TeamId { get; set; }

    [SerializedName("wins")]
    public int Wins { get; set; }

    [SerializedName("teamStatType")]
    public string TeamStatType { get; set; }
  }
}
