// Decompiled with JetBrains decompiler
// Type: BananaLib.RiotObjects.Team.TeamStatSummary
// Assembly: BananaLib, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 75213AF3-E339-4AEB-B3FE-095F85BC5F53
// Assembly location: C:\Users\Hesa\Desktop\eZ\BananaLib.dll

using RtmpSharp.IO;
using System;
using System.Collections.Generic;

namespace BananaLib.RiotObjects.Team
{
  [SerializedName("com.riotgames.team.stats.TeamStatSummary")]
  [Serializable]
  public class TeamStatSummary
  {
    [SerializedName("teamStatDetails")]
    public List<TeamStatDetail> TeamStatDetails { get; set; }

    [SerializedName("teamIdString")]
    public string TeamIdString { get; set; }

    [SerializedName("teamId")]
    public TeamId TeamId { get; set; }
  }
}
