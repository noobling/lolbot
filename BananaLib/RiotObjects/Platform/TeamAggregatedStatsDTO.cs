// Decompiled with JetBrains decompiler
// Type: BananaLib.RiotObjects.Platform.TeamAggregatedStatsDTO
// Assembly: BananaLib, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 75213AF3-E339-4AEB-B3FE-095F85BC5F53
// Assembly location: C:\Users\Hesa\Desktop\eZ\BananaLib.dll

using BananaLib.RiotObjects.Team;
using RtmpSharp.IO;
using System;
using System.Collections.Generic;

namespace BananaLib.RiotObjects.Platform
{
  [SerializedName("com.riotgames.platform.statistics.team.TeamAggregatedStatsDTO")]
  [Serializable]
  public class TeamAggregatedStatsDTO
  {
    [SerializedName("queueType")]
    public string QueueType { get; set; }

    [SerializedName("serializedToJson")]
    public string SerializedToJson { get; set; }

    [SerializedName("playerAggregatedStatsList")]
    public List<TeamPlayerAggregatedStatsDTO> PlayerAggregatedStatsList { get; set; }

    [SerializedName("teamId")]
    public TeamId TeamId { get; set; }
  }
}
