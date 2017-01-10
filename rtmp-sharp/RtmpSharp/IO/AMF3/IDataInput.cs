// Decompiled with JetBrains decompiler
// Type: RtmpSharp.IO.AMF3.IDataInput
// Assembly: rtmp-sharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8588136F-A4B9-4004-9712-4EA13AA4AF9D
// Assembly location: C:\Users\Hesa\Desktop\eZ_Source\bin\Debug\rtmp-sharp.dll

namespace RtmpSharp.IO.AMF3
{
  public interface IDataInput
  {
    object ReadObject();

    bool ReadBoolean();

    byte ReadByte();

    byte[] ReadBytes(int count);

    double ReadDouble();

    float ReadFloat();

    short ReadInt16();

    ushort ReadUInt16();

    int ReadUInt24();

    int ReadInt32();

    uint ReadUInt32();

    string ReadUtf();

    string ReadUtf(int length);
  }
}
