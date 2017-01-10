// Decompiled with JetBrains decompiler
// Type: BananaLib.RiotObjects.Team.TeamInfo
// Assembly: BananaLib, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 75213AF3-E339-4AEB-B3FE-095F85BC5F53
// Assembly location: C:\Users\Hesa\Desktop\eZ\BananaLib.dll

using RtmpSharp.IO;
using System;

namespace BananaLib.RiotObjects.Team
{
  [SerializedName("com.riotgames.team.TeamInfo")]
  [Serializable]
  public class TeamInfo
  {
    [SerializedName("secondsUntilEligibleForDeletion")]
    public double SecondsUntilEligibleForDeletion { get; set; }

    [SerializedName("memberStatusString")]
    public string MemberStatusString { get; set; }

    [SerializedName("tag")]
    public string Tag { get; set; }

    [SerializedName("name")]
    public string Name { get; set; }

    [SerializedName("memberStatus")]
    public string MemberStatus { get; set; }

    [SerializedName("teamId")]
    public TeamId TeamId { get; set; }
  }
}
