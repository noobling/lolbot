// Decompiled with JetBrains decompiler
// Type: BananaLib.RiotObjects.Platform.SummonerIconInventoryDTO
// Assembly: BananaLib, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 75213AF3-E339-4AEB-B3FE-095F85BC5F53
// Assembly location: C:\Users\Hesa\Desktop\eZ\BananaLib.dll

using RtmpSharp.IO;
using System;
using System.Collections.Generic;

namespace BananaLib.RiotObjects.Platform
{
  [SerializedName("com.riotgames.platform.summoner.icon.SummonerIconInventoryDTO")]
  [Serializable]
  public class SummonerIconInventoryDTO
  {
    [SerializedName("summonerId")]
    public double SummonerId { get; set; }

    [SerializedName("summonerIcons")]
    public List<SummonerIcon> SummonerIcons { get; set; }
  }
}
