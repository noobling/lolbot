// Decompiled with JetBrains decompiler
// Type: BananaLib.RiotObjects.Platform.AccountSummary
// Assembly: BananaLib, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 75213AF3-E339-4AEB-B3FE-095F85BC5F53
// Assembly location: C:\Users\Hesa\Desktop\eZ\BananaLib.dll

using RtmpSharp.IO;
using System;

namespace BananaLib.RiotObjects.Platform
{
  [SerializedName("com.riotgames.platform.account.AccountSummary")]
  [Serializable]
  public class AccountSummary
  {
    [SerializedName("groupCount")]
    public int GroupCount { get; set; }

    [SerializedName("username")]
    public string Username { get; set; }

    [SerializedName("accountId")]
    public double AccountId { get; set; }

    [SerializedName("summonerInternalName")]
    public object SummonerInternalName { get; set; }

    [SerializedName("admin")]
    public bool Admin { get; set; }

    [SerializedName("hasBetaAccess")]
    public bool HasBetaAccess { get; set; }

    [SerializedName("summonerName")]
    public object SummonerName { get; set; }

    [SerializedName("partnerMode")]
    public bool PartnerMode { get; set; }

    [SerializedName("needsPasswordReset")]
    public bool NeedsPasswordReset { get; set; }

    [SerializedName("futureData")]
    public object FutureData { get; set; }
  }
}
