// Decompiled with JetBrains decompiler
// Type: BananaLib.RiotObjects.Platform.SummonerRune
// Assembly: BananaLib, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 75213AF3-E339-4AEB-B3FE-095F85BC5F53
// Assembly location: C:\Users\Hesa\Desktop\eZ\BananaLib.dll

using RtmpSharp.IO;
using System;

namespace BananaLib.RiotObjects.Platform
{
  [SerializedName("com.riotgames.platform.summoner.runes.SummonerRune")]
  [Serializable]
  public class SummonerRune
  {
    [SerializedName("purchased")]
    public DateTime Purchased { get; set; }

    [SerializedName("purchaseDate")]
    public DateTime PurchaseDate { get; set; }

    [SerializedName("runeId")]
    public int RuneId { get; set; }

    [SerializedName("quantity")]
    public int Quantity { get; set; }

    [SerializedName("rune")]
    public Rune Rune { get; set; }

    [SerializedName("summonerId")]
    public double SummonerId { get; set; }
  }
}
