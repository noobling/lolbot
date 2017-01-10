// Decompiled with JetBrains decompiler
// Type: BananaLib.RiotObjects.Platform.TalentRow
// Assembly: BananaLib, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 75213AF3-E339-4AEB-B3FE-095F85BC5F53
// Assembly location: C:\Users\Hesa\Desktop\eZ\BananaLib.dll

using RtmpSharp.IO;
using System;
using System.Collections.Generic;

namespace BananaLib.RiotObjects.Platform
{
  [SerializedName("com.riotgames.platform.summoner.TalentRow")]
  [Serializable]
  public class TalentRow
  {
    [SerializedName("index")]
    public int Index { get; set; }

    [SerializedName("talents")]
    public List<Talent> Talents { get; set; }

    [SerializedName("tltGroupId")]
    public int TltGroupId { get; set; }

    [SerializedName("pointsToActivate")]
    public int PointsToActivate { get; set; }

    [SerializedName("tltRowId")]
    public int TltRowId { get; set; }
  }
}
