// Decompiled with JetBrains decompiler
// Type: BananaLib.RiotObjects.Platform.LeaverPenaltyStats
// Assembly: BananaLib, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 75213AF3-E339-4AEB-B3FE-095F85BC5F53
// Assembly location: C:\Users\Hesa\Desktop\eZ\BananaLib.dll

using RtmpSharp.IO;
using System;

namespace BananaLib.RiotObjects.Platform
{
  [SerializedName("com.riotgames.platform.statistics.LeaverPenaltyStats")]
  [Serializable]
  public class LeaverPenaltyStats
  {
    [SerializedName("lastLevelIncrease")]
    public object LastLevelIncrease { get; set; }

    [SerializedName("level")]
    public int Level { get; set; }

    [SerializedName("lastDecay")]
    public DateTime LastDecay { get; set; }

    [SerializedName("userInformed")]
    public bool UserInformed { get; set; }

    [SerializedName("points")]
    public int Points { get; set; }
  }
}
