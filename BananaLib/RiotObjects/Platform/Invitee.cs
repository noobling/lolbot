// Decompiled with JetBrains decompiler
// Type: BananaLib.RiotObjects.Platform.Invitee
// Assembly: BananaLib, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 75213AF3-E339-4AEB-B3FE-095F85BC5F53
// Assembly location: C:\Users\Hesa\Desktop\eZ\BananaLib.dll

using RtmpSharp.IO;
using System;

namespace BananaLib.RiotObjects.Platform
{
  [SerializedName("com.riotgames.platform.gameinvite.contract.Invitee")]
  [Serializable]
  public class Invitee
  {
    [SerializedName("inviteeStateAsString")]
    public string InviteeState { get; set; }

    [SerializedName("summonerName")]
    public string SummonerName { get; set; }

    [SerializedName("inviteeState")]
    public string inviteeState { get; set; }

    [SerializedName("summonerId")]
    public double SummonerId { get; set; }
  }
}
