// Decompiled with JetBrains decompiler
// Type: BananaLib.RiotObjects.Platform.Rune
// Assembly: BananaLib, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 75213AF3-E339-4AEB-B3FE-095F85BC5F53
// Assembly location: C:\Users\Hesa\Desktop\eZ\BananaLib.dll

using RtmpSharp.IO;
using System;
using System.Collections.Generic;

namespace BananaLib.RiotObjects.Platform
{
  [SerializedName("com.riotgames.platform.catalog.runes.Rune")]
  [Serializable]
  public class Rune
  {
    [SerializedName("imagePath")]
    public object ImagePath { get; set; }

    [SerializedName("toolTip")]
    public object ToolTip { get; set; }

    [SerializedName("tier")]
    public int Tier { get; set; }

    [SerializedName("itemId")]
    public int ItemId { get; set; }

    [SerializedName("runeType")]
    public RuneType RuneType { get; set; }

    [SerializedName("duration")]
    public int Duration { get; set; }

    [SerializedName("gameCode")]
    public int GameCode { get; set; }

    [SerializedName("itemEffects")]
    public List<ItemEffect> ItemEffects { get; set; }

    [SerializedName("baseType")]
    public string BaseType { get; set; }

    [SerializedName("description")]
    public string Description { get; set; }

    [SerializedName("name")]
    public string Name { get; set; }

    [SerializedName("uses")]
    public object Uses { get; set; }
  }
}
