// Decompiled with JetBrains decompiler
// Type: BananaLib.RiotObjects.Platform.RuneSlot
// Assembly: BananaLib, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 75213AF3-E339-4AEB-B3FE-095F85BC5F53
// Assembly location: C:\Users\Hesa\Desktop\eZ\BananaLib.dll

using RtmpSharp.IO;
using System;

namespace BananaLib.RiotObjects.Platform
{
  [SerializedName("com.riotgames.platform.summoner.RuneSlot")]
  [Serializable]
  public class RuneSlot
  {
    [SerializedName("id")]
    public int Id { get; set; }

    [SerializedName("minLevel")]
    public int MinLevel { get; set; }

    [SerializedName("runeType")]
    public RuneType RuneType { get; set; }
  }
}
