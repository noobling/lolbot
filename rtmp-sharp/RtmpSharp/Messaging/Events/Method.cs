// Decompiled with JetBrains decompiler
// Type: RtmpSharp.Messaging.Events.Method
// Assembly: rtmp-sharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8588136F-A4B9-4004-9712-4EA13AA4AF9D
// Assembly location: C:\Users\Hesa\Desktop\eZ_Source\bin\Debug\rtmp-sharp.dll

namespace RtmpSharp.Messaging.Events
{
  internal class Method
  {
    public CallStatus CallStatus { get; internal set; }

    public string Name { get; internal set; }

    public bool IsSuccess { get; internal set; }

    public object[] Parameters { get; internal set; }

    internal Method(string methodName, object[] parameters, bool isSuccess = true, CallStatus status = CallStatus.Request)
    {
      this.Name = methodName;
      this.Parameters = parameters;
      this.IsSuccess = isSuccess;
      this.CallStatus = status;
    }
  }
}
