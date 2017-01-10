// Decompiled with JetBrains decompiler
// Type: BananaLib.RiotObjects.Platform.SpellBookPageDTO
// Assembly: BananaLib, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 75213AF3-E339-4AEB-B3FE-095F85BC5F53
// Assembly location: C:\Users\Hesa\Desktop\eZ\BananaLib.dll

using RtmpSharp.IO;
using System;
using System.Collections.Generic;

namespace BananaLib.RiotObjects.Platform
{
  [SerializedName("com.riotgames.platform.summoner.spellbook.SpellBookPageDTO")]
  [Serializable]
  public class SpellBookPageDTO
  {
    [SerializedName("slotEntries")]
    public List<SlotEntry> SlotEntries { get; set; }

    [SerializedName("summonerId")]
    public double SummonerId { get; set; }

    [SerializedName("createDate")]
    public DateTime CreateDate { get; set; }

    [SerializedName("name")]
    public string Name { get; set; }

    [SerializedName("pageId")]
    public int PageId { get; set; }

    [SerializedName("current")]
    public bool Current { get; set; }
  }
}
