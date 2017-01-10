// Decompiled with JetBrains decompiler
// Type: RtmpSharp.Net.StatusAsObject
// Assembly: rtmp-sharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8588136F-A4B9-4004-9712-4EA13AA4AF9D
// Assembly location: C:\Users\Hesa\Desktop\eZ_Source\bin\Debug\rtmp-sharp.dll

using RtmpSharp.IO;
using System;

namespace RtmpSharp.Net
{
  internal class StatusAsObject : AsObject
  {
    private Exception _exception;

    public StatusAsObject(Exception exception)
    {
      this._exception = exception;
    }

    public StatusAsObject(string code, string level, string description, object application, ObjectEncoding objectEncoding)
    {
      this["code"] = (object) code;
      this["level"] = (object) level;
      this["description"] = (object) description;
      this["application"] = application;
      this["objectEncoding"] = (object) (double) objectEncoding;
    }

    public StatusAsObject(string code, string level, string description)
    {
      this["code"] = (object) code;
      this["level"] = (object) level;
      this["description"] = (object) description;
    }
  }
}
