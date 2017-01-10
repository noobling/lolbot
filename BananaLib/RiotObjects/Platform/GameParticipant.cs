// Decompiled with JetBrains decompiler
// Type: BananaLib.RiotObjects.Platform.GameParticipant
// Assembly: BananaLib, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 75213AF3-E339-4AEB-B3FE-095F85BC5F53
// Assembly location: C:\Users\Hesa\Desktop\eZ\BananaLib.dll

using RtmpSharp.IO;
using System;

namespace BananaLib.RiotObjects.Platform
{
  [SerializedName("com.riotgames.platform.game.GameParticipant")]
  [Serializable]
  public class GameParticipant : Participant
  {
    [SerializedName("summonerName")]
    public string SummonerName { get; set; }

    [SerializedName("summonerInternalName")]
    public string SummonerInternalName { get; set; }

    [SerializedName("pickMode")]
    public int PickMode { get; set; }

    [SerializedName("pickTurn")]
    public int PickTurn { get; set; }

    [SerializedName("badges")]
    public int Badges { get; set; }

    [SerializedName("team")]
    public int Team { get; set; }

    [SerializedName("teamName")]
    public string TeamName { get; set; }

    [SerializedName("isFriendly")]
    public bool IsFriendly { get; set; }

    [SerializedName("isMe")]
    public bool IsMe { get; set; }

    [SerializedName("isGameOwner")]
    public bool IsGameOwner { get; set; }
  }
}
