// Decompiled with JetBrains decompiler
// Type: BananaLib.RiotObjects.Platform.SummonerIcon
// Assembly: BananaLib, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 75213AF3-E339-4AEB-B3FE-095F85BC5F53
// Assembly location: C:\Users\Hesa\Desktop\eZ\BananaLib.dll

using RtmpSharp.IO;
using System;

namespace BananaLib.RiotObjects.Platform
{
  [SerializedName("com.riotgames.platform.summoner.icon.SummonerIcon")]
  [Serializable]
  public class SummonerIcon
  {
    [SerializedName("iconId")]
    public int IconId { get; set; }

    [SerializedName("purchaseDate")]
    public DateTime PurchaseDate { get; set; }

    [SerializedName("summonerId")]
    public double SummonerId { get; set; }
  }
}
