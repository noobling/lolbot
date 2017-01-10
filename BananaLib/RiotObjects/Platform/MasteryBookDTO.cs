// Decompiled with JetBrains decompiler
// Type: BananaLib.RiotObjects.Platform.MasteryBookDTO
// Assembly: BananaLib, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 75213AF3-E339-4AEB-B3FE-095F85BC5F53
// Assembly location: C:\Users\Hesa\Desktop\eZ\BananaLib.dll

using RtmpSharp.IO;
using System;
using System.Collections.Generic;

namespace BananaLib.RiotObjects.Platform
{
  [SerializedName("com.riotgames.platform.summoner.masterybook.MasteryBookDTO")]
  [Serializable]
  public class MasteryBookDTO
  {
    [SerializedName("bookPagesJson")]
    public object BookPagesJson { get; set; }

    [SerializedName("bookPages")]
    public List<MasteryBookPageDTO> BookPages { get; set; }

    [SerializedName("dateString")]
    public string DateString { get; set; }

    [SerializedName("summonerId")]
    public double SummonerId { get; set; }
  }
}
