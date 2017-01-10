// Decompiled with JetBrains decompiler
// Type: BananaLib.GameServerAddressAttribute
// Assembly: BananaLib, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 75213AF3-E339-4AEB-B3FE-095F85BC5F53
// Assembly location: C:\Users\Hesa\Desktop\eZ\BananaLib.dll

using System;

namespace BananaLib
{
  [AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
  public class GameServerAddressAttribute : Attribute
  {
    public string Value { get; }

    public GameServerAddressAttribute(string value)
    {
      this.Value = value;
    }
  }
}
