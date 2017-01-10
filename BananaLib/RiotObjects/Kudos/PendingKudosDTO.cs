// Decompiled with JetBrains decompiler
// Type: BananaLib.RiotObjects.Kudos.PendingKudosDTO
// Assembly: BananaLib, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 75213AF3-E339-4AEB-B3FE-095F85BC5F53
// Assembly location: C:\Users\Hesa\Desktop\eZ\BananaLib.dll

using RtmpSharp.IO;
using System;

namespace BananaLib.RiotObjects.Kudos
{
  [SerializedName("com.riotgames.kudos.dto.PendingKudosDTO")]
  [Serializable]
  public class PendingKudosDTO
  {
    [SerializedName("pendingCounts")]
    public int[] PendingCounts { get; set; }
  }
}
