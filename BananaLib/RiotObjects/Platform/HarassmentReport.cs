// Decompiled with JetBrains decompiler
// Type: BananaLib.RiotObjects.Platform.HarassmentReport
// Assembly: BananaLib, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 75213AF3-E339-4AEB-B3FE-095F85BC5F53
// Assembly location: C:\Users\Hesa\Desktop\eZ\BananaLib.dll

using RtmpSharp.IO;
using System;

namespace BananaLib.RiotObjects.Platform
{
  [SerializedName("com.riotgames.platform.harassment.HarassmentReport")]
  [Serializable]
  public class HarassmentReport
  {
    [SerializedName("reportedSummonerId")]
    public double SummonerID { get; set; }

    [SerializedName("ipAddress")]
    public double IPAddress { get; set; }

    [SerializedName("gameId")]
    public double GameID { get; set; }

    [SerializedName("reportSource")]
    public string ReportSource { get; set; }

    [SerializedName("comment")]
    public string Comment { get; set; }

    [SerializedName("reportingSummonerId")]
    public double ReportingSummonerID { get; set; }

    [SerializedName("offense")]
    public string Offense { get; set; }
  }
}
