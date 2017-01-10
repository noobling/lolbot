// Decompiled with JetBrains decompiler
// Type: RtmpSharp.Net.EventReceivedEventArgs
// Assembly: rtmp-sharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8588136F-A4B9-4004-9712-4EA13AA4AF9D
// Assembly location: C:\Users\Hesa\Desktop\eZ_Source\bin\Debug\rtmp-sharp.dll

using RtmpSharp.Messaging;
using System;

namespace RtmpSharp.Net
{
  internal class EventReceivedEventArgs : EventArgs
  {
    public RtmpEvent Event { get; set; }

    public EventReceivedEventArgs(RtmpEvent @event)
    {
      this.Event = @event;
    }
  }
}
