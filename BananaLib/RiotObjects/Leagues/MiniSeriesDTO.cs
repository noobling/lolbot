// Decompiled with JetBrains decompiler
// Type: BananaLib.RiotObjects.Leagues.MiniSeriesDTO
// Assembly: BananaLib, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 75213AF3-E339-4AEB-B3FE-095F85BC5F53
// Assembly location: C:\Users\Hesa\Desktop\eZ\BananaLib.dll

using RtmpSharp.IO;
using System;

namespace BananaLib.RiotObjects.Leagues
{
  [SerializedName("com.riotgames.leagues.pojo.MiniSeriesDTO")]
  [Serializable]
  public class MiniSeriesDTO
  {
    [SerializedName("progress")]
    public object Progress { get; set; }

    [SerializedName("target")]
    public int Target { get; set; }

    [SerializedName("losses")]
    public int Losses { get; set; }

    [SerializedName("timeLeftToPlayMillis")]
    public double TimeLeftToPlayMillis { get; set; }

    [SerializedName("wins")]
    public int Wins { get; set; }
  }
}
