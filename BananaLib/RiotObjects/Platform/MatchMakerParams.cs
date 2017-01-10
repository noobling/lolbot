// Decompiled with JetBrains decompiler
// Type: BananaLib.RiotObjects.Platform.MatchMakerParams
// Assembly: BananaLib, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 75213AF3-E339-4AEB-B3FE-095F85BC5F53
// Assembly location: C:\Users\Hesa\Desktop\eZ\BananaLib.dll

using RtmpSharp.IO;
using System;
using System.Collections.Generic;

namespace BananaLib.RiotObjects.Platform
{
  [SerializedName("com.riotgames.platform.matchmaking.MatchMakerParams")]
  [Serializable]
  public class MatchMakerParams
  {
    [SerializedName("lastMaestroMessage")]
    public object LastMaestroMessage { get; set; }

    [SerializedName("teamId")]
    public object TeamId { get; set; }

    [SerializedName("languages")]
    public object Languages { get; set; }

    [SerializedName("botDifficulty")]
    public string BotDifficulty { get; set; }

    [SerializedName("team")]
    public List<int> Team { get; set; }

    [SerializedName("queueIds")]
    public int[] QueueIds { get; set; }

    [SerializedName("invitationId")]
    public object InvitationId { get; set; }
  }
}
