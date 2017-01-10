// Decompiled with JetBrains decompiler
// Type: BananaLib.RiotObjects.Team.MatchHistorySummary
// Assembly: BananaLib, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 75213AF3-E339-4AEB-B3FE-095F85BC5F53
// Assembly location: C:\Users\Hesa\Desktop\eZ\BananaLib.dll

using RtmpSharp.IO;
using System;

namespace BananaLib.RiotObjects.Team
{
  [SerializedName("com.riotgames.team.stats.MatchHistorySummary")]
  [Serializable]
  public class MatchHistorySummary
  {
    [SerializedName("gameMode")]
    public string GameMode { get; set; }

    [SerializedName("mapId")]
    public int MapId { get; set; }

    [SerializedName("assists")]
    public int Assists { get; set; }

    [SerializedName("opposingTeamName")]
    public string OpposingTeamName { get; set; }

    [SerializedName("invalid")]
    public bool Invalid { get; set; }

    [SerializedName("deaths")]
    public int Deaths { get; set; }

    [SerializedName("gameId")]
    public double GameId { get; set; }

    [SerializedName("kills")]
    public int Kills { get; set; }

    [SerializedName("win")]
    public bool Win { get; set; }

    [SerializedName("date")]
    public double Date { get; set; }

    [SerializedName("opposingTeamKills")]
    public int OpposingTeamKills { get; set; }
  }
}
