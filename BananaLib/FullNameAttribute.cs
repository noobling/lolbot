// Decompiled with JetBrains decompiler
// Type: BananaLib.FullNameAttribute
// Assembly: BananaLib, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 75213AF3-E339-4AEB-B3FE-095F85BC5F53
// Assembly location: C:\Users\Hesa\Desktop\eZ\BananaLib.dll

using System;

namespace BananaLib
{
  [AttributeUsage(AttributeTargets.Field)]
  public class FullNameAttribute : Attribute
  {
    public string Value { get; }

    public FullNameAttribute(string value)
    {
      this.Value = value;
    }
  }
}
