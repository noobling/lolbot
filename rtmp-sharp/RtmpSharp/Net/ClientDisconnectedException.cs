// Decompiled with JetBrains decompiler
// Type: RtmpSharp.Net.ClientDisconnectedException
// Assembly: rtmp-sharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8588136F-A4B9-4004-9712-4EA13AA4AF9D
// Assembly location: C:\Users\Hesa\Desktop\eZ_Source\bin\Debug\rtmp-sharp.dll

using System;

namespace RtmpSharp.Net
{
  public class ClientDisconnectedException : Exception
  {
    public string Description;
    public Exception Exception;

    public ClientDisconnectedException()
    {
    }

    internal ClientDisconnectedException(string description)
    {
      this.Description = description;
    }

    internal ClientDisconnectedException(string description, Exception exception)
    {
      this.Description = description;
      this.Exception = exception;
    }
  }
}
