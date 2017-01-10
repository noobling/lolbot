// Decompiled with JetBrains decompiler
// Type: BananaLib.RiotObjects.Platform.ObfuscatedParticipant
// Assembly: BananaLib, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 75213AF3-E339-4AEB-B3FE-095F85BC5F53
// Assembly location: C:\Users\Hesa\Desktop\eZ\BananaLib.dll

using RtmpSharp.IO;
using System;

namespace BananaLib.RiotObjects.Platform
{
  [SerializedName("com.riotgames.platform.game.ObfuscatedParticipant")]
  [Serializable]
  public class ObfuscatedParticipant : Participant
  {
    [SerializedName("badges")]
    public int Badges { get; set; }

    [SerializedName("clientInSynch")]
    public bool ClientInSynch { get; set; }

    [SerializedName("gameUniqueId")]
    public int GameUniqueId { get; set; }

    [SerializedName("pickMode")]
    public int PickMode { get; set; }
  }
}
