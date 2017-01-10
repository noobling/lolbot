// Decompiled with JetBrains decompiler
// Type: Complete.ExceptionalEventArgs
// Assembly: rtmp-sharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8588136F-A4B9-4004-9712-4EA13AA4AF9D
// Assembly location: C:\Users\Hesa\Desktop\eZ_Source\bin\Debug\rtmp-sharp.dll

using System;

namespace Complete
{
  internal class ExceptionalEventArgs : EventArgs
  {
    public string Description;
    public Exception Exception;

    public ExceptionalEventArgs(string description)
    {
      this.Description = description;
    }

    public ExceptionalEventArgs(string description, Exception exception)
    {
      this.Description = description;
      this.Exception = exception;
    }
  }
}
