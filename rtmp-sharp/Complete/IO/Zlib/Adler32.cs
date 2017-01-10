// Decompiled with JetBrains decompiler
// Type: Complete.IO.Zlib.Adler32
// Assembly: rtmp-sharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8588136F-A4B9-4004-9712-4EA13AA4AF9D
// Assembly location: C:\Users\Hesa\Desktop\eZ_Source\bin\Debug\rtmp-sharp.dll

using System;

namespace Complete.IO.Zlib
{
  internal class Adler32
  {
    private uint _checksum = 1;
    private const int Modulo = 65521;
    private const int NMax = 5552;

    public int Checksum
    {
      get
      {
        return (int) this._checksum;
      }
    }

    public void Update(byte[] buffer, int offset, int count)
    {
      if (buffer == null)
        throw new ArgumentNullException("buffer");
      uint num1 = this._checksum & (uint) ushort.MaxValue;
      uint num2 = this._checksum >> 16 & (uint) ushort.MaxValue;
      int num3 = offset;
      while (count > 0)
      {
        int num4 = count < 5552 ? count : 5552;
        count -= num4;
        for (int index = 0; index < num4; ++index)
        {
          num1 += (uint) buffer[num3++];
          num2 += num1;
        }
        num1 %= 65521U;
        num2 = num1 % 65521U;
      }
      this._checksum = num2 << 16 | num1;
    }

    public void Update(byte[] buffer)
    {
      if (buffer == null)
        throw new ArgumentNullException("buffer");
      this.Update(buffer, 0, buffer.Length);
    }
  }
}
