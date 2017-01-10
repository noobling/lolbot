// Decompiled with JetBrains decompiler
// Type: BananaLib.RiotObjects.Platform.BotParticipant
// Assembly: BananaLib, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 75213AF3-E339-4AEB-B3FE-095F85BC5F53
// Assembly location: C:\Users\Hesa\Desktop\eZ\BananaLib.dll

using RtmpSharp.IO;
using System;
using System.Collections.Generic;

namespace BananaLib.RiotObjects.Platform
{
  [SerializedName("com.riotgames.platform.game.BotParticipant")]
  [Serializable]
  public class BotParticipant : GameParticipant
  {
    [SerializedName("botSkillLevelName")]
    public string BotSkillLevelName { get; set; }

    [SerializedName("botSkillLevel")]
    public double BotSkillLevel { get; set; }

    [SerializedName("teamId")]
    public string TeamId { get; set; }

    [SerializedName("champion")]
    public List<ChampionDTO> Champion { get; set; }
  }
}
