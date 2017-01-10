// Decompiled with JetBrains decompiler
// Type: RtmpSharp.IO.AMF3.IDataOutput
// Assembly: rtmp-sharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8588136F-A4B9-4004-9712-4EA13AA4AF9D
// Assembly location: C:\Users\Hesa\Desktop\eZ_Source\bin\Debug\rtmp-sharp.dll

namespace RtmpSharp.IO.AMF3
{
  public interface IDataOutput
  {
    void WriteObject(object value);

    void WriteBoolean(bool value);

    void WriteByte(byte value);

    void WriteBytes(byte[] buffer);

    void WriteDouble(double value);

    void WriteFloat(float value);

    void WriteInt16(short value);

    void WriteUInt16(ushort value);

    void WriteUInt24(int value);

    void WriteInt32(int value);

    void WriteUInt32(uint value);

    void WriteUtf(string value);

    void WriteUtfBytes(string value);
  }
}
