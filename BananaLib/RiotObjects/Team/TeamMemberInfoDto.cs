// Decompiled with JetBrains decompiler
// Type: BananaLib.RiotObjects.Team.TeamMemberInfoDto
// Assembly: BananaLib, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 75213AF3-E339-4AEB-B3FE-095F85BC5F53
// Assembly location: C:\Users\Hesa\Desktop\eZ\BananaLib.dll

using RtmpSharp.IO;
using System;

namespace BananaLib.RiotObjects.Team
{
  [SerializedName("com.riotgames.team.dto.TeamMemberInfoDTO")]
  [Serializable]
  public class TeamMemberInfoDto
  {
    [SerializedName("joinDate")]
    public DateTime JoinDate { get; set; }

    [SerializedName("playerName")]
    public string PlayerName { get; set; }

    [SerializedName("inviteDate")]
    public DateTime InviteDate { get; set; }

    [SerializedName("status")]
    public string Status { get; set; }

    [SerializedName("playerId")]
    public double PlayerId { get; set; }
  }
}
