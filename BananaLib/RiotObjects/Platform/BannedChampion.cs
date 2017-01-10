// Decompiled with JetBrains decompiler
// Type: BananaLib.RiotObjects.Platform.BannedChampion
// Assembly: BananaLib, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 75213AF3-E339-4AEB-B3FE-095F85BC5F53
// Assembly location: C:\Users\Hesa\Desktop\eZ\BananaLib.dll

using RtmpSharp.IO;
using System;

namespace BananaLib.RiotObjects.Platform
{
  [SerializedName("com.riotgames.platform.game.BannedChampion")]
  [Serializable]
  public class BannedChampion
  {
    [SerializedName("pickTurn")]
    public int PickTurn { get; set; }

    [SerializedName("championId")]
    public int ChampionId { get; set; }

    [SerializedName("teamId")]
    public int TeamId { get; set; }
  }
}
