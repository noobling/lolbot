// Decompiled with JetBrains decompiler
// Type: RtmpSharp.Messaging.Messages.AcknowledgeMessageExt
// Assembly: rtmp-sharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8588136F-A4B9-4004-9712-4EA13AA4AF9D
// Assembly location: C:\Users\Hesa\Desktop\eZ_Source\bin\Debug\rtmp-sharp.dll

using RtmpSharp.IO;
using RtmpSharp.IO.AMF3;
using System;
using System.Collections.Generic;

namespace RtmpSharp.Messaging.Messages
{
  [SerializedName("DSK", Canonical = false)]
  [SerializedName("flex.messaging.messages.AcknowledgeMessageExt")]
  [Serializable]
  public class AcknowledgeMessageExt : AsyncMessageExt
  {
    public override void ReadExternal(IDataInput input)
    {
      base.ReadExternal(input);
      List<byte> byteList = this.ReadFlags(input);
      for (int flag = 0; flag < byteList.Count; ++flag)
        this.ReadRemaining(input, flag, 0);
    }

    public override void WriteExternal(IDataOutput output)
    {
      base.WriteExternal(output);
      output.WriteByte((byte) 0);
    }
  }
}
