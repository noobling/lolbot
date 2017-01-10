// Decompiled with JetBrains decompiler
// Type: BananaLib.RestService.IpBannedException
// Assembly: BananaLib, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 75213AF3-E339-4AEB-B3FE-095F85BC5F53
// Assembly location: C:\Users\Hesa\Desktop\eZ\BananaLib.dll

using System;

namespace BananaLib.RestService
{
  public sealed class IpBannedException : Exception
  {
    public override string Message { get; }

    public IpBannedException()
    {
    }

    public IpBannedException(string message)
    {
      this.Message = message;
    }
  }
}
