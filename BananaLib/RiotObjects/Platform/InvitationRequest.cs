// Decompiled with JetBrains decompiler
// Type: BananaLib.RiotObjects.Platform.InvitationRequest
// Assembly: BananaLib, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 75213AF3-E339-4AEB-B3FE-095F85BC5F53
// Assembly location: C:\Users\Hesa\Desktop\eZ\BananaLib.dll

using RtmpSharp.IO;
using System;

namespace BananaLib.RiotObjects.Platform
{
  [SerializedName("com.riotgames.platform.gameinvite.contract.InvitationRequest")]
  [Serializable]
  public class InvitationRequest
  {
    [SerializedName("gameMetaData")]
    public string GameMetaData { get; set; }

    [SerializedName("invitationStateAsString")]
    public string InvitationStateAsString { get; set; }

    [SerializedName("invitationState")]
    public string InvitationState { get; set; }

    [SerializedName("invitationId")]
    public string InvitationId { get; set; }

    [SerializedName("inviter")]
    public Inviter Inviter { get; set; }

    [SerializedName("owner")]
    public Player Owner { get; set; }
  }
}
