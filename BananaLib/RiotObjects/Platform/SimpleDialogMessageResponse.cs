// Decompiled with JetBrains decompiler
// Type: BananaLib.RiotObjects.Platform.SimpleDialogMessageResponse
// Assembly: BananaLib, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 75213AF3-E339-4AEB-B3FE-095F85BC5F53
// Assembly location: C:\Users\Hesa\Desktop\eZ\BananaLib.dll

using RtmpSharp.IO;
using System;

namespace BananaLib.RiotObjects.Platform
{
  [SerializedName("com.riotgames.platform.messaging.persistence.SimpleDialogMessageResponse")]
  [Serializable]
  public class SimpleDialogMessageResponse
  {
    [SerializedName("command")]
    public string Command { get; set; }

    [SerializedName("accountId")]
    public double AccountId { get; set; }

    [SerializedName("msgId")]
    public string MessageId { get; set; }
  }
}
