﻿// Decompiled with JetBrains decompiler
// Type: BananaLib.RiotObjects.Platform.PlayerGameStats
// Assembly: BananaLib, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 75213AF3-E339-4AEB-B3FE-095F85BC5F53
// Assembly location: C:\Users\Hesa\Desktop\eZ\BananaLib.dll

using RtmpSharp.IO;
using System;
using System.Collections.Generic;

namespace BananaLib.RiotObjects.Platform
{
  [SerializedName("com.riotgames.platform.statistics.PlayerGameStats")]
  [Serializable]
  public class PlayerGameStats
  {
    [SerializedName("skinName")]
    public object SkinName { get; set; }

    [SerializedName("ranked")]
    public bool Ranked { get; set; }

    [SerializedName("skinIndex")]
    public int SkinIndex { get; set; }

    [SerializedName("fellowPlayers")]
    public List<FellowPlayerInfo> FellowPlayers { get; set; }

    [SerializedName("gameType")]
    public string GameType { get; set; }

    [SerializedName("experienceEarned")]
    public double ExperienceEarned { get; set; }

    [SerializedName("rawStatsJson")]
    public object RawStatsJson { get; set; }

    [SerializedName("eligibleFirstWinOfDay")]
    public bool EligibleFirstWinOfDay { get; set; }

    [SerializedName("difficulty")]
    public object Difficulty { get; set; }

    [SerializedName("gameMapId")]
    public int GameMapId { get; set; }

    [SerializedName("leaver")]
    public bool Leaver { get; set; }

    [SerializedName("spell1")]
    public double Spell1 { get; set; }

    [SerializedName("spell2")]
    public double Spell2 { get; set; }

    [SerializedName("gameTypeEnum")]
    public string GameTypeEnum { get; set; }

    [SerializedName("teamId")]
    public double TeamId { get; set; }

    [SerializedName("summonerId")]
    public double SummonerId { get; set; }

    [SerializedName("statistics")]
    public List<RawStat> Statistics { get; set; }

    [SerializedName("afk")]
    public bool Afk { get; set; }

    [SerializedName("id")]
    public object Id { get; set; }

    [SerializedName("boostXpEarned")]
    public double BoostXpEarned { get; set; }

    [SerializedName("level")]
    public double Level { get; set; }

    [SerializedName("invalid")]
    public bool Invalid { get; set; }

    [SerializedName("userId")]
    public double UserId { get; set; }

    [SerializedName("createDate")]
    public DateTime CreateDate { get; set; }

    [SerializedName("userServerPing")]
    public int UserServerPing { get; set; }

    [SerializedName("adjustedRating")]
    public int AdjustedRating { get; set; }

    [SerializedName("premadeSize")]
    public int PremadeSize { get; set; }

    [SerializedName("boostIpEarned")]
    public double BoostIpEarned { get; set; }

    [SerializedName("gameId")]
    public double GameId { get; set; }

    [SerializedName("timeInQueue")]
    public int TimeInQueue { get; set; }

    [SerializedName("ipEarned")]
    public double IpEarned { get; set; }

    [SerializedName("eloChange")]
    public int EloChange { get; set; }

    [SerializedName("gameMode")]
    public string GameMode { get; set; }

    [SerializedName("difficultyString")]
    public object DifficultyString { get; set; }

    [SerializedName("KCoefficient")]
    public double KCoefficient { get; set; }

    [SerializedName("teamRating")]
    public int TeamRating { get; set; }

    [SerializedName("subType")]
    public string SubType { get; set; }

    [SerializedName("queueType")]
    public string QueueType { get; set; }

    [SerializedName("premadeTeam")]
    public bool PremadeTeam { get; set; }

    [SerializedName("predictedWinPct")]
    public double PredictedWinPct { get; set; }

    [SerializedName("rating")]
    public double Rating { get; set; }

    [SerializedName("championId")]
    public double ChampionId { get; set; }
  }
}
