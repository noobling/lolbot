// Decompiled with JetBrains decompiler
// Type: BananaLib.RiotObjects.Platform.Effect
// Assembly: BananaLib, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 75213AF3-E339-4AEB-B3FE-095F85BC5F53
// Assembly location: C:\Users\Hesa\Desktop\eZ\BananaLib.dll

using RtmpSharp.IO;
using System;

namespace BananaLib.RiotObjects.Platform
{
  [SerializedName("com.riotgames.platform.catalog.Effect")]
  [Serializable]
  public class Effect
  {
    [SerializedName("effectId")]
    public int EffectId { get; set; }

    [SerializedName("gameCode")]
    public string GameCode { get; set; }

    [SerializedName("name")]
    public string Name { get; set; }

    [SerializedName("categoryId")]
    public object CategoryId { get; set; }

    [SerializedName("runeType")]
    public RuneType RuneType { get; set; }
  }
}
