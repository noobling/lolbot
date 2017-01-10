// Decompiled with JetBrains decompiler
// Type: RtmpSharp.IO.AMF3.DataInput
// Assembly: rtmp-sharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8588136F-A4B9-4004-9712-4EA13AA4AF9D
// Assembly location: C:\Users\Hesa\Desktop\eZ_Source\bin\Debug\rtmp-sharp.dll

using System;

namespace RtmpSharp.IO.AMF3
{
  internal class DataInput : IDataInput
  {
    private readonly AmfReader _reader;

    public ObjectEncoding ObjectEncoding { get; set; }

    public DataInput(AmfReader reader)
    {
      this._reader = reader;
      this.ObjectEncoding = ObjectEncoding.Amf3;
    }

    public object ReadObject()
    {
      switch (this.ObjectEncoding)
      {
        case ObjectEncoding.Amf0:
          return this._reader.ReadAmf0Item();
        case ObjectEncoding.Amf3:
          return this._reader.ReadAmf3Item();
        default:
          throw new ArgumentOutOfRangeException();
      }
    }

    public string ReadUtf()
    {
      return this._reader.ReadUtf();
    }

    public string ReadUtf(int length)
    {
      return this._reader.ReadUtf(length);
    }

    public int ReadUInt24()
    {
      return this._reader.ReadUInt24();
    }

    public ushort ReadUInt16()
    {
      return this._reader.ReadUInt16();
    }

    public int ReadInt32()
    {
      return this._reader.ReadInt32();
    }

    public uint ReadUInt32()
    {
      return this._reader.ReadUInt32();
    }

    public short ReadInt16()
    {
      return this._reader.ReadInt16();
    }

    public float ReadFloat()
    {
      return this._reader.ReadFloat();
    }

    public double ReadDouble()
    {
      return this._reader.ReadDouble();
    }

    public byte[] ReadBytes(int count)
    {
      return this._reader.ReadBytes(count);
    }

    public byte ReadByte()
    {
      return this._reader.ReadByte();
    }

    public bool ReadBoolean()
    {
      return this._reader.ReadBoolean();
    }
  }
}
