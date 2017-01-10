// Decompiled with JetBrains decompiler
// Type: BananaLib.RiotObjects.Team.CreatedTeam
// Assembly: BananaLib, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 75213AF3-E339-4AEB-B3FE-095F85BC5F53
// Assembly location: C:\Users\Hesa\Desktop\eZ\BananaLib.dll

using RtmpSharp.IO;
using System;

namespace BananaLib.RiotObjects.Team
{
  [SerializedName("com.riotgames.team.CreatedTeam")]
  [Serializable]
  public class CreatedTeam
  {
    [SerializedName("timeStamp")]
    public double TimeStamp { get; set; }

    [SerializedName("teamId")]
    public TeamId TeamId { get; set; }
  }
}
