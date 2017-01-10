// Decompiled with JetBrains decompiler
// Type: RtmpSharp.IO.AMF3.ByteArray
// Assembly: rtmp-sharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8588136F-A4B9-4004-9712-4EA13AA4AF9D
// Assembly location: C:\Users\Hesa\Desktop\eZ_Source\bin\Debug\rtmp-sharp.dll

using Complete.IO.Zlib;
using System;
using System.ComponentModel;
using System.IO;
using System.IO.Compression;

namespace RtmpSharp.IO.AMF3
{
  [TypeConverter(typeof (ByteArrayConverter))]
  [SerializedName("flex.messaging.io.ByteArray")]
  [Serializable]
  public class ByteArray
  {
    private ObjectEncoding _objectEncoding = ObjectEncoding.Amf3;
    private readonly SerializationContext _serializationContext;
    private DataInput _dataInput;
    private DataOutput _dataOutput;

    public uint Length
    {
      get
      {
        return (uint) this.MemoryStream.Length;
      }
    }

    public uint Position
    {
      get
      {
        return (uint) this.MemoryStream.Position;
      }
      set
      {
        this.MemoryStream.Position = (long) value;
      }
    }

    public uint BytesAvailable
    {
      get
      {
        return this.Length - this.Position;
      }
    }

    internal MemoryStream MemoryStream { get; private set; }

    public ObjectEncoding ObjectEncoding
    {
      get
      {
        return this._objectEncoding;
      }
      set
      {
        this._objectEncoding = value;
        this._dataInput.ObjectEncoding = value;
        this._dataOutput.ObjectEncoding = value;
      }
    }

    public ByteArray()
    {
      this.MemoryStream = new MemoryStream();
      this.ReloadStreams();
    }

    public ByteArray(SerializationContext serializationContext)
      : this()
    {
      this._serializationContext = serializationContext;
    }

    public ByteArray(MemoryStream ms, SerializationContext serializationContext)
    {
      this._serializationContext = serializationContext;
      this.MemoryStream = ms;
      this.ReloadStreams();
    }

    public ByteArray(byte[] buffer, SerializationContext serializationContext)
    {
      this._serializationContext = serializationContext;
      this.MemoryStream = new MemoryStream(buffer);
      this.ReloadStreams();
    }

    private void ReloadStreams()
    {
      this._dataOutput = new DataOutput(new AmfWriter((Stream) this.MemoryStream, this._serializationContext, this._objectEncoding));
      this._dataInput = new DataInput(new AmfReader((Stream) this.MemoryStream, this._serializationContext));
    }

    public byte[] GetBuffer()
    {
      return this.MemoryStream.GetBuffer();
    }

    public byte[] ToArray()
    {
      return this.MemoryStream.ToArray();
    }

    public void Compress()
    {
      this.Compress(ByteArray.CompressionAlgorithm.Zlib);
    }

    public void Deflate()
    {
      this.Compress(ByteArray.CompressionAlgorithm.Deflate);
    }

    public void Compress(ByteArray.CompressionAlgorithm algorithm)
    {
      byte[] array = this.MemoryStream.ToArray();
      this.MemoryStream.Close();
      MemoryStream memoryStream = new MemoryStream();
      DeflateStream deflateStream = algorithm == ByteArray.CompressionAlgorithm.Zlib ? (DeflateStream) new ZlibStream((Stream) memoryStream, CompressionMode.Compress, true) : new DeflateStream((Stream) memoryStream, CompressionMode.Compress, true);
      using (deflateStream)
        deflateStream.Write(array, 0, array.Length);
      this.MemoryStream = memoryStream;
      this._dataOutput = new DataOutput(new AmfWriter((Stream) this.MemoryStream, this._serializationContext));
      this._dataInput = new DataInput(new AmfReader((Stream) this.MemoryStream, this._serializationContext));
    }

    public void Inflate()
    {
      this.Uncompress(ByteArray.CompressionAlgorithm.Deflate);
    }

    public void Uncompress()
    {
      this.Uncompress(ByteArray.CompressionAlgorithm.Zlib);
    }

    public void Uncompress(ByteArray.CompressionAlgorithm algorithm)
    {
      this.Position = 0U;
      MemoryStream memoryStream = new MemoryStream();
      byte[] buffer = new byte[1024];
      DeflateStream deflateStream = algorithm == ByteArray.CompressionAlgorithm.Zlib ? (DeflateStream) new ZlibStream((Stream) this.MemoryStream, CompressionMode.Decompress, false) : new DeflateStream((Stream) this.MemoryStream, CompressionMode.Decompress, false);
      while (true)
      {
        int count = deflateStream.Read(buffer, 0, buffer.Length);
        if (count != 0)
          memoryStream.Write(buffer, 0, count);
        else
          break;
      }
      this.MemoryStream.Dispose();
      this.MemoryStream = memoryStream;
      this.MemoryStream.Position = 0L;
      this._dataOutput = new DataOutput(new AmfWriter((Stream) this.MemoryStream, this._serializationContext));
      this._dataInput = new DataInput(new AmfReader((Stream) this.MemoryStream, this._serializationContext));
    }

    public bool ReadBoolean()
    {
      return this._dataInput.ReadBoolean();
    }

    public byte ReadByte()
    {
      return this._dataInput.ReadByte();
    }

    public byte[] ReadBytes(int count)
    {
      return this._dataInput.ReadBytes(count);
    }

    public double ReadDouble()
    {
      return this._dataInput.ReadDouble();
    }

    public float ReadFloat()
    {
      return this._dataInput.ReadFloat();
    }

    public short ReadInt16()
    {
      return this._dataInput.ReadInt16();
    }

    public int ReadInt32()
    {
      return this._dataInput.ReadInt32();
    }

    public object ReadObject()
    {
      return this._dataInput.ReadObject();
    }

    public ushort ReadUInt16()
    {
      return this._dataInput.ReadUInt16();
    }

    public int ReadUInt24()
    {
      return this._dataInput.ReadUInt24();
    }

    public uint ReadUInt32()
    {
      return this._dataInput.ReadUInt32();
    }

    public string ReadUtf()
    {
      return this._dataInput.ReadUtf();
    }

    public string ReadUtf(int length)
    {
      return this._dataInput.ReadUtf(length);
    }

    public void WriteBoolean(bool value)
    {
      this._dataOutput.WriteBoolean(value);
    }

    public void WriteByte(byte value)
    {
      this._dataOutput.WriteByte(value);
    }

    public void WriteBytes(byte[] buffer)
    {
      this._dataOutput.WriteBytes(buffer);
    }

    public void WriteDouble(double value)
    {
      this._dataOutput.WriteDouble(value);
    }

    public void WriteFloat(float value)
    {
      this._dataOutput.WriteFloat(value);
    }

    public void WriteInt16(short value)
    {
      this._dataOutput.WriteInt16(value);
    }

    public void WriteInt32(int value)
    {
      this._dataOutput.WriteInt32(value);
    }

    public void WriteObject(object value)
    {
      this._dataOutput.WriteObject(value);
    }

    public void WriteUInt16(ushort value)
    {
      this._dataOutput.WriteUInt16(value);
    }

    public void WriteUInt24(int value)
    {
      this._dataOutput.WriteUInt24(value);
    }

    public void WriteUInt32(uint value)
    {
      this._dataOutput.WriteUInt32(value);
    }

    public void WriteUtf(string value)
    {
      this._dataOutput.WriteUtf(value);
    }

    public void WriteUtfBytes(string value)
    {
      this._dataOutput.WriteUtfBytes(value);
    }

    public enum CompressionAlgorithm
    {
      Deflate,
      Zlib,
    }
  }
}
