// Decompiled with JetBrains decompiler
// Type: BananaLib.RiotObjects.Platform.GameTypeConfigDTO
// Assembly: BananaLib, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 75213AF3-E339-4AEB-B3FE-095F85BC5F53
// Assembly location: C:\Users\Hesa\Desktop\eZ\BananaLib.dll

using RtmpSharp.IO;
using System;

namespace BananaLib.RiotObjects.Platform
{
  [SerializedName("com.riotgames.platform.game.GameTypeConfigDTO")]
  [Serializable]
  public class GameTypeConfigDTO
  {
    [SerializedName("id")]
    public int Id { get; set; }

    [SerializedName("allowTrades")]
    public bool AllowTrades { get; set; }

    [SerializedName("name")]
    public string Name { get; set; }

    [SerializedName("mainPickTimerDuration")]
    public int MainPickTimerDuration { get; set; }

    [SerializedName("exclusivePick")]
    public bool ExclusivePick { get; set; }

    [SerializedName("duplicatePick")]
    public bool DuplicatePick { get; set; }

    [SerializedName("teamChampionPool")]
    public bool TeamChampionPool { get; set; }

    [SerializedName("pickMode")]
    public string PickMode { get; set; }

    [SerializedName("maxAllowableBans")]
    public int MaxAllowableBans { get; set; }

    [SerializedName("banTimerDuration")]
    public int BanTimerDuration { get; set; }

    [SerializedName("postPickTimerDuration")]
    public int PostPickTimerDuration { get; set; }

    [SerializedName("crossTeamChampionPool")]
    public bool CrossTeamChampionPool { get; set; }
  }
}
