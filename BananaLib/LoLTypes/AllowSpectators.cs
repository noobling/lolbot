// Decompiled with JetBrains decompiler
// Type: BananaLib.LoLTypes.AllowSpectators
// Assembly: BananaLib, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 75213AF3-E339-4AEB-B3FE-095F85BC5F53
// Assembly location: C:\Users\Hesa\Desktop\eZ\BananaLib.dll

using System.ComponentModel;

namespace BananaLib.LoLTypes
{
  public enum AllowSpectators
  {
    [Description("NONE")] None,
    [Description("ALL")] All,
    [Description("LOBBYONLY")] LobbyOnly,
    [Description("DROPINONLY")] DropInOnly,
  }
}
