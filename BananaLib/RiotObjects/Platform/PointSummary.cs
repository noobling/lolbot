// Decompiled with JetBrains decompiler
// Type: BananaLib.RiotObjects.Platform.PointSummary
// Assembly: BananaLib, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 75213AF3-E339-4AEB-B3FE-095F85BC5F53
// Assembly location: C:\Users\Hesa\Desktop\eZ\BananaLib.dll

using RtmpSharp.IO;
using System;

namespace BananaLib.RiotObjects.Platform
{
  [SerializedName("com.riotgames.platform.reroll.pojo.PointSummary")]
  [Serializable]
  public class PointSummary
  {
    [SerializedName("pointsToNextRoll")]
    public double PointsToNextRoll { get; set; }

    [SerializedName("maxRolls")]
    public int MaxRolls { get; set; }

    [SerializedName("numberOfRolls")]
    public int NumberOfRolls { get; set; }

    [SerializedName("pointsCostToRoll")]
    public double PointsCostToRoll { get; set; }

    [SerializedName("currentPoints")]
    public double CurrentPoints { get; set; }
  }
}
