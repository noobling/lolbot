// Decompiled with JetBrains decompiler
// Type: BananaLib.RiotObjects.Platform.PlayerLifetimeStats
// Assembly: BananaLib, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 75213AF3-E339-4AEB-B3FE-095F85BC5F53
// Assembly location: C:\Users\Hesa\Desktop\eZ\BananaLib.dll

using RtmpSharp.IO;
using System;

namespace BananaLib.RiotObjects.Platform
{
  [SerializedName("com.riotgames.platform.statistics.PlayerLifetimeStats")]
  [Serializable]
  public class PlayerLifetimeStats
  {
    [SerializedName("playerStatSummaries")]
    public PlayerStatSummaries PlayerStatSummaries { get; set; }

    [SerializedName("leaverPenaltyStats")]
    public LeaverPenaltyStats LeaverPenaltyStats { get; set; }

    [SerializedName("previousFirstWinOfDay")]
    public DateTime PreviousFirstWinOfDay { get; set; }

    [SerializedName("userId")]
    public double UserId { get; set; }

    [SerializedName("dodgeStreak")]
    public int DodgeStreak { get; set; }

    [SerializedName("dodgePenaltyDate")]
    public object DodgePenaltyDate { get; set; }

    [SerializedName("playerStatsJson")]
    public object PlayerStatsJson { get; set; }

    [SerializedName("playerStats")]
    public PlayerStats PlayerStats { get; set; }
  }
}
