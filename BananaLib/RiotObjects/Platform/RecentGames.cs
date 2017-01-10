// Decompiled with JetBrains decompiler
// Type: BananaLib.RiotObjects.Platform.RecentGames
// Assembly: BananaLib, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 75213AF3-E339-4AEB-B3FE-095F85BC5F53
// Assembly location: C:\Users\Hesa\Desktop\eZ\BananaLib.dll

using RtmpSharp.IO;
using System;
using System.Collections.Generic;

namespace BananaLib.RiotObjects.Platform
{
  [SerializedName("com.riotgames.platform.statistics.RecentGames")]
  [Serializable]
  public class RecentGames
  {
    [SerializedName("recentGamesJson")]
    public object RecentGamesJson { get; set; }

    [SerializedName("playerGameStatsMap")]
    public object PlayerGameStatsMap { get; set; }

    [SerializedName("gameStatistics")]
    public List<PlayerGameStats> GameStatistics { get; set; }

    [SerializedName("userId")]
    public double UserId { get; set; }
  }
}
