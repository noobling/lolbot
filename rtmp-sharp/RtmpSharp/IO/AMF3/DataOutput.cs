// Decompiled with JetBrains decompiler
// Type: RtmpSharp.IO.AMF3.DataOutput
// Assembly: rtmp-sharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8588136F-A4B9-4004-9712-4EA13AA4AF9D
// Assembly location: C:\Users\Hesa\Desktop\eZ_Source\bin\Debug\rtmp-sharp.dll

using System;
using System.Text;

namespace RtmpSharp.IO.AMF3
{
  internal class DataOutput : IDataOutput
  {
    private readonly AmfWriter _writer;

    public ObjectEncoding ObjectEncoding { get; set; }

    public DataOutput(AmfWriter writer)
    {
      this._writer = writer;
      this.ObjectEncoding = ObjectEncoding.Amf3;
    }

    public void WriteObject(object value)
    {
      switch (this.ObjectEncoding)
      {
        case ObjectEncoding.Amf0:
          this._writer.WriteAmfItem(ObjectEncoding.Amf0, value);
          break;
        case ObjectEncoding.Amf3:
          this._writer.WriteAmf3Item(value);
          break;
        default:
          throw new ArgumentOutOfRangeException();
      }
    }

    public void WriteBoolean(bool value)
    {
      this._writer.WriteBoolean(value);
    }

    public void WriteUInt32(uint value)
    {
      this._writer.WriteUInt32(value);
    }

    public void WriteByte(byte value)
    {
      this._writer.WriteByte(value);
    }

    public void WriteBytes(byte[] buffer)
    {
      this._writer.WriteBytes(buffer);
    }

    public void WriteDouble(double value)
    {
      this._writer.WriteDouble(value);
    }

    public void WriteFloat(float value)
    {
      this._writer.WriteFloat(value);
    }

    public void WriteInt16(short value)
    {
      this._writer.WriteInt16(value);
    }

    public void WriteInt32(int value)
    {
      this._writer.WriteInt32(value);
    }

    public void WriteUInt16(ushort value)
    {
      this._writer.WriteUInt16(value);
    }

    public void WriteUInt24(int value)
    {
      this._writer.WriteUInt24(value);
    }

    public void WriteUtf(string value)
    {
      this._writer.WriteUtfPrefixed(value);
    }

    public void WriteUtfBytes(string value)
    {
      this._writer.WriteBytes(Encoding.UTF8.GetBytes(value));
    }
  }
}
