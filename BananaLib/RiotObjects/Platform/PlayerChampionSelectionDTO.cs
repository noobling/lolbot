// Decompiled with JetBrains decompiler
// Type: BananaLib.RiotObjects.Platform.PlayerChampionSelectionDTO
// Assembly: BananaLib, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 75213AF3-E339-4AEB-B3FE-095F85BC5F53
// Assembly location: C:\Users\Hesa\Desktop\eZ\BananaLib.dll

using RtmpSharp.IO;
using System;

namespace BananaLib.RiotObjects.Platform
{
  [SerializedName("com.riotgames.platform.game.PlayerChampionSelectionDTO")]
  [Serializable]
  public class PlayerChampionSelectionDTO
  {
    [SerializedName("summonerInternalName")]
    public string SummonerInternalName { get; set; }

    [SerializedName("championId")]
    public int ChampionId { get; set; }

    [SerializedName("selectedSkinIndex")]
    public int SelectedSkinIndex { get; set; }

    [SerializedName("spell1Id")]
    public double Spell1Id { get; set; }

    [SerializedName("spell2Id")]
    public double Spell2Id { get; set; }
  }
}
