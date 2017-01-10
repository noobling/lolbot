// Decompiled with JetBrains decompiler
// Type: BananaLib.RiotObjects.Platform.ChampionSkinDTO
// Assembly: BananaLib, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 75213AF3-E339-4AEB-B3FE-095F85BC5F53
// Assembly location: C:\Users\Hesa\Desktop\eZ\BananaLib.dll

using RtmpSharp.IO;
using System;

namespace BananaLib.RiotObjects.Platform
{
  [SerializedName("com.riotgames.platform.catalog.champion.ChampionSkinDTO")]
  [Serializable]
  public class ChampionSkinDTO
  {
    [SerializedName("championId")]
    public int ChampionId { get; set; }

    [SerializedName("skinId")]
    public int SkinId { get; set; }

    [SerializedName("freeToPlayReward")]
    public bool FreeToPlayReward { get; set; }

    [SerializedName("stillObtainable")]
    public bool StillObtainable { get; set; }

    [SerializedName("lastSelected")]
    public bool LastSelected { get; set; }

    [SerializedName("skinIndex")]
    public int SkinIndex { get; set; }

    [SerializedName("owned")]
    public bool Owned { get; set; }

    [SerializedName("winCountRemaining")]
    public int WinCountRemaining { get; set; }

    [SerializedName("purchaseDate")]
    public double PurchaseDate { get; set; }

    [SerializedName("endDate")]
    public double EndDate { get; set; }
  }
}
