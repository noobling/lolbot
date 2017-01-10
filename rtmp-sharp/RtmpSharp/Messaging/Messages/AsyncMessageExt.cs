// Decompiled with JetBrains decompiler
// Type: RtmpSharp.Messaging.Messages.AsyncMessageExt
// Assembly: rtmp-sharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8588136F-A4B9-4004-9712-4EA13AA4AF9D
// Assembly location: C:\Users\Hesa\Desktop\eZ_Source\bin\Debug\rtmp-sharp.dll

using RtmpSharp.IO;
using RtmpSharp.IO.AMF3;
using System;
using System.Collections.Generic;

namespace RtmpSharp.Messaging.Messages
{
  [SerializedName("DSA", Canonical = false)]
  [SerializedName("flex.messaging.messages.AsyncMessageExt")]
  [Serializable]
  public class AsyncMessageExt : AsyncMessage, IExternalizable
  {
    [SerializedName("correlationIdBytes")]
    public ByteArray CorrelationIdBytes
    {
      get
      {
        return Uuid.ToBytes(this.CorrelationId);
      }
      set
      {
        this.CorrelationId = Uuid.ToString(value);
      }
    }

    [SerializedName("clientIdBytes")]
    public ByteArray ClientIdBytes
    {
      get
      {
        return Uuid.ToBytes(this.ClientId);
      }
      set
      {
        this.ClientId = Uuid.ToString(value);
      }
    }

    [SerializedName("messageIdBytes")]
    public ByteArray MessageIdBytes
    {
      get
      {
        return Uuid.ToBytes(this.MessageId);
      }
      set
      {
        this.MessageId = Uuid.ToString(value);
      }
    }

    public AsyncMessageExt()
    {
      this.Timestamp = (long) DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
    }

    public virtual void ReadExternal(IDataInput input)
    {
      List<byte> byteList1 = this.ReadFlags(input);
      int bits1 = 0;
      for (int index = 0; index < byteList1.Count; ++index)
      {
        byte num = byteList1[index];
        if (index == 0)
        {
          if (((int) num & 1) != 0)
            this.Body = input.ReadObject();
          if (((int) num & 2) != 0)
            this.ClientId = (string) input.ReadObject();
          if (((int) num & 4) != 0)
            this.Destination = (string) input.ReadObject();
          if (((int) num & 8) != 0)
            this.Headers = input.ReadObject() as AsObject;
          if (((int) num & 16) != 0)
            this.MessageId = (string) input.ReadObject();
          if (((int) num & 32) != 0)
            this.Timestamp = Convert.ToInt64(input.ReadObject());
          if (((int) num & 64) != 0)
            this.TimeToLive = Convert.ToInt64(input.ReadObject());
          bits1 = 7;
        }
        else if (index == 1)
        {
          if (((int) num & 1) != 0)
            this.ClientIdBytes = (ByteArray) input.ReadObject();
          if (((int) num & 2) != 0)
            this.MessageIdBytes = (ByteArray) input.ReadObject();
          bits1 = 2;
        }
        this.ReadRemaining(input, (int) num, bits1);
      }
      List<byte> byteList2 = this.ReadFlags(input);
      for (int index = 0; index < byteList2.Count; ++index)
      {
        byte num = byteList2[index];
        int bits2 = 0;
        if (index == 0)
        {
          if (((int) num & 1) != 0)
            this.CorrelationId = (string) input.ReadObject();
          if (((int) num & 2) != 0)
            this.CorrelationIdBytes = (ByteArray) input.ReadObject();
          bits2 = 2;
        }
        this.ReadRemaining(input, (int) num, bits2);
      }
    }

    public virtual void WriteExternal(IDataOutput output)
    {
      byte num1 = 0;
      if (this.Body != null)
        num1 |= (byte) 1;
      if (this.ClientId != null && this.ClientIdBytes == null)
        num1 |= (byte) 2;
      if (this.Destination != null)
        num1 |= (byte) 4;
      if (this.Headers != null)
        num1 |= (byte) 8;
      if (this.MessageId != null && this.MessageIdBytes == null)
        num1 |= (byte) 16;
      if (this.Timestamp != 0L)
        num1 |= (byte) 32;
      if (this.TimeToLive != 0L)
        num1 |= (byte) 64;
      byte num2 = 0;
      if (this.ClientIdBytes != null)
        num2 |= (byte) 1;
      if (this.MessageIdBytes != null)
        num2 |= (byte) 2;
      if ((int) num2 != 0)
        num1 |= (byte) 128;
      output.WriteByte(num1);
      if (((int) num1 & 128) != 0)
        output.WriteByte(num2);
      if (this.Body != null)
        output.WriteObject(this.Body);
      if (this.ClientId != null && this.ClientIdBytes == null)
        output.WriteObject((object) this.ClientId);
      if (this.Destination != null)
        output.WriteObject((object) this.Destination);
      if (this.Headers != null)
        output.WriteObject((object) this.Headers);
      if (this.MessageId != null && this.MessageIdBytes == null)
        output.WriteObject((object) this.MessageId);
      if (this.Timestamp != 0L)
        output.WriteObject((object) this.Timestamp);
      if (this.TimeToLive != 0L)
        output.WriteObject((object) this.TimeToLive);
      if (this.ClientIdBytes != null)
        output.WriteObject((object) this.ClientIdBytes);
      if (this.MessageIdBytes != null)
        output.WriteObject((object) this.MessageIdBytes);
      byte num3 = 0;
      if (this.CorrelationId != null && this.CorrelationIdBytes == null)
        num3 |= (byte) 1;
      if (this.CorrelationIdBytes != null)
        num3 |= (byte) 2;
      output.WriteByte(num3);
      if (this.CorrelationId != null && this.CorrelationIdBytes == null)
        output.WriteObject((object) this.CorrelationId);
      if (this.CorrelationIdBytes == null)
        return;
      output.WriteObject((object) this.CorrelationIdBytes);
    }

    protected List<byte> ReadFlags(IDataInput input)
    {
      List<byte> byteList = new List<byte>();
      byte num;
      do
      {
        num = input.ReadByte();
        byteList.Add(num);
      }
      while (((int) num & 128) != 0);
      return byteList;
    }

    protected void ReadRemaining(IDataInput input, int flag, int bits)
    {
      if (flag >> bits == 0)
        return;
      for (int index = bits; index < 6; ++index)
      {
        if ((flag >> index & 1) != 0)
          input.ReadObject();
      }
    }
  }
}
