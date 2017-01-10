// Decompiled with JetBrains decompiler
// Type: RtmpSharp.Messaging.Events.UserControlMessageType
// Assembly: rtmp-sharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8588136F-A4B9-4004-9712-4EA13AA4AF9D
// Assembly location: C:\Users\Hesa\Desktop\eZ_Source\bin\Debug\rtmp-sharp.dll

namespace RtmpSharp.Messaging.Events
{
  internal enum UserControlMessageType : ushort
  {
    StreamBegin = 0,
    StreamEof = 1,
    StreamDry = 2,
    SetBufferLength = 3,
    StreamIsRecorded = 4,
    PingRequest = 6,
    PingResponse = 7,
  }
}
