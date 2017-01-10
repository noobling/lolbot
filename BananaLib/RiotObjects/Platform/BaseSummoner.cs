// Decompiled with JetBrains decompiler
// Type: BananaLib.RiotObjects.Platform.BaseSummoner
// Assembly: BananaLib, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 75213AF3-E339-4AEB-B3FE-095F85BC5F53
// Assembly location: C:\Users\Hesa\Desktop\eZ\BananaLib.dll

using RtmpSharp.IO;

namespace BananaLib.RiotObjects.Platform
{
  public class BaseSummoner
  {
    [SerializedName("sumId")]
    public double SummonerId { get; set; }

    [SerializedName("acctId")]
    public double AccountId { get; set; }

    [SerializedName("name")]
    public string Name { get; set; }

    [SerializedName("publicName")]
    public string InternalName { get; set; }

    [SerializedName("profileIconId")]
    public int ProfileIconId { get; set; }
  }
}
