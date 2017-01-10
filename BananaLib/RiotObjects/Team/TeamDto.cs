// Decompiled with JetBrains decompiler
// Type: BananaLib.RiotObjects.Team.TeamDto
// Assembly: BananaLib, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 75213AF3-E339-4AEB-B3FE-095F85BC5F53
// Assembly location: C:\Users\Hesa\Desktop\eZ\BananaLib.dll

using RtmpSharp.IO;
using System;
using System.Collections.Generic;

namespace BananaLib.RiotObjects.Team
{
  [SerializedName("com.riotgames.team.dto.TeamDTO")]
  [Serializable]
  public class TeamDto
  {
    [SerializedName("teamStatSummary")]
    public TeamStatSummary TeamStatSummary { get; set; }

    [SerializedName("status")]
    public string Status { get; set; }

    [SerializedName("tag")]
    public string Tag { get; set; }

    [SerializedName("roster")]
    public RosterDto Roster { get; set; }

    [SerializedName("lastGameDate")]
    public object LastGameDate { get; set; }

    [SerializedName("modifyDate")]
    public DateTime ModifyDate { get; set; }

    [SerializedName("messageOfDay")]
    public object MessageOfDay { get; set; }

    [SerializedName("teamId")]
    public TeamId TeamId { get; set; }

    [SerializedName("lastJoinDate")]
    public DateTime LastJoinDate { get; set; }

    [SerializedName("secondLastJoinDate")]
    public DateTime SecondLastJoinDate { get; set; }

    [SerializedName("secondsUntilEligibleForDeletion")]
    public double SecondsUntilEligibleForDeletion { get; set; }

    [SerializedName("matchHistory")]
    public List<object> MatchHistory { get; set; }

    [SerializedName("name")]
    public string Name { get; set; }

    [SerializedName("thirdLastJoinDate")]
    public DateTime ThirdLastJoinDate { get; set; }

    [SerializedName("createDate")]
    public DateTime CreateDate { get; set; }
  }
}
