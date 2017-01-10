// Decompiled with JetBrains decompiler
// Type: BananaLib.RiotObjects.Platform.AllPublicSummonerDataDTO
// Assembly: BananaLib, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 75213AF3-E339-4AEB-B3FE-095F85BC5F53
// Assembly location: C:\Users\Hesa\Desktop\eZ\BananaLib.dll

using RtmpSharp.IO;
using System;

namespace BananaLib.RiotObjects.Platform
{
  [SerializedName("com.riotgames.platform.summoner.AllPublicSummonerDataDTO")]
  [Serializable]
  public class AllPublicSummonerDataDTO
  {
    [SerializedName("summoner")]
    public BasePublicSummonerDTO Summoner { get; set; }

    [SerializedName("summonerLevelAndPoints")]
    public SummonerLevelAndPoints SummonerLevelAndPoints { get; set; }

    [SerializedName("summonerTalentsAndPoints")]
    public SummonerTalentsAndPoints SummonerTalentsAndPoints { get; set; }

    [SerializedName("summonerDefaultSpells")]
    public SummonerDefaultSpells SummonerDefaultSpells { get; set; }

    [SerializedName("summonerLevel")]
    public SummonerLevel SummonerLevel { get; set; }

    [SerializedName("spellBook")]
    public SpellBookDTO SpellBook { get; set; }
  }
}
