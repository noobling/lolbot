// Decompiled with JetBrains decompiler
// Type: BananaLib.RiotObjects.Platform.SlotEntry
// Assembly: BananaLib, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 75213AF3-E339-4AEB-B3FE-095F85BC5F53
// Assembly location: C:\Users\Hesa\Desktop\eZ\BananaLib.dll

using RtmpSharp.IO;
using System;

namespace BananaLib.RiotObjects.Platform
{
  [SerializedName("com.riotgames.platform.summoner.spellbook.SlotEntry")]
  [Serializable]
  public class SlotEntry
  {
    [SerializedName("runeId")]
    public int RuneId { get; set; }

    [SerializedName("runeSlotId")]
    public int RuneSlotId { get; set; }
  }
}
