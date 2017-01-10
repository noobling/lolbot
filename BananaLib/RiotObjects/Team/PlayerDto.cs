// Decompiled with JetBrains decompiler
// Type: BananaLib.RiotObjects.Team.PlayerDto
// Assembly: BananaLib, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 75213AF3-E339-4AEB-B3FE-095F85BC5F53
// Assembly location: C:\Users\Hesa\Desktop\eZ\BananaLib.dll

using RtmpSharp.IO;
using System;
using System.Collections.Generic;

namespace BananaLib.RiotObjects.Team
{
  [SerializedName("com.riotgames.team.dto.PlayerDTO")]
  [Serializable]
  public class PlayerDto
  {
    [SerializedName("playerId")]
    public double PlayerId { get; set; }

    [SerializedName("teamsSummary")]
    public List<object> TeamsSummary { get; set; }

    [SerializedName("createdTeams")]
    public List<object> CreatedTeams { get; set; }

    [SerializedName("playerTeams")]
    public List<object> PlayerTeams { get; set; }
  }
}
