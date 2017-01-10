// Decompiled with JetBrains decompiler
// Type: BananaLib.RiotObjects.Platform.ItemEffect
// Assembly: BananaLib, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 75213AF3-E339-4AEB-B3FE-095F85BC5F53
// Assembly location: C:\Users\Hesa\Desktop\eZ\BananaLib.dll

using RtmpSharp.IO;
using System;

namespace BananaLib.RiotObjects.Platform
{
  [SerializedName("com.riotgames.platform.catalog.ItemEffect")]
  [Serializable]
  public class ItemEffect
  {
    [SerializedName("effectId")]
    public int EffectId { get; set; }

    [SerializedName("itemEffectId")]
    public int ItemEffectId { get; set; }

    [SerializedName("effect")]
    public Effect Effect { get; set; }

    [SerializedName("value")]
    public string Value { get; set; }

    [SerializedName("itemId")]
    public int ItemId { get; set; }
  }
}
