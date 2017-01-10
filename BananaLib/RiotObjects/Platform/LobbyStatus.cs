// Decompiled with JetBrains decompiler
// Type: BananaLib.RiotObjects.Platform.LobbyStatus
// Assembly: BananaLib, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 75213AF3-E339-4AEB-B3FE-095F85BC5F53
// Assembly location: C:\Users\Hesa\Desktop\eZ\BananaLib.dll

using RtmpSharp.IO;
using System;
using System.Collections.Generic;

namespace BananaLib.RiotObjects.Platform
{
  [SerializedName("com.riotgames.platform.gameinvite.contract.LobbyStatus")]
  [Serializable]
  public class LobbyStatus
  {
    [SerializedName("chatKey")]
    public string ChatKey { get; set; }

    [SerializedName("gameMetaData")]
    public string GameData { get; set; }

    [SerializedName("owner")]
    public Player Owner { get; set; }

    [SerializedName("members")]
    public List<Member> Members { get; set; }

    [SerializedName("invitees")]
    public List<Invitee> Invitees { get; set; }

    [SerializedName("invitationId")]
    public string InvitationID { get; set; }
  }
}
