// Decompiled with JetBrains decompiler
// Type: BananaLib.RiotObjects.Platform.StoreFulfillmentNotification
// Assembly: BananaLib, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 75213AF3-E339-4AEB-B3FE-095F85BC5F53
// Assembly location: C:\Users\Hesa\Desktop\eZ\BananaLib.dll

using RtmpSharp.IO;
using System;

namespace BananaLib.RiotObjects.Platform
{
  [SerializedName("com.riotgames.platform.messaging.StoreFulfillmentNotification")]
  [Serializable]
  public class StoreFulfillmentNotification
  {
    [SerializedName("rp")]
    public double Rp { get; set; }

    [SerializedName("ip")]
    public double Ip { get; set; }

    [SerializedName("inventoryType")]
    public string InventoryType { get; set; }

    [SerializedName("data")]
    public object Data { get; set; }
  }
}
