// Decompiled with JetBrains decompiler
// Type: RtmpSharp.IO.Uuid
// Assembly: rtmp-sharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8588136F-A4B9-4004-9712-4EA13AA4AF9D
// Assembly location: C:\Users\Hesa\Desktop\eZ_Source\bin\Debug\rtmp-sharp.dll

using RtmpSharp.IO.AMF3;
using System;
using System.Globalization;
using System.Text;

namespace RtmpSharp.IO
{
  public static class Uuid
  {
    public static string NewUuid()
    {
      return Guid.NewGuid().ToString("D").ToUpperInvariant();
    }

    public static string ToString(ByteArray b)
    {
      if (b == null || (int) b.Length != 16)
        return (string) null;
      byte[] array = b.ToArray();
      StringBuilder stringBuilder = new StringBuilder();
      for (int index = 0; index < array.Length; ++index)
      {
        if (index == 4 || index == 6 || (index == 8 || index == 10))
          stringBuilder.Append('-');
        stringBuilder.AppendFormat("{0:X2}", (object) array[index]);
      }
      return stringBuilder.ToString();
    }

    public static ByteArray ToBytes(string s)
    {
      if (s == null || s.Length != 36)
        return (ByteArray) null;
      s = s.Replace("-", "");
      ByteArray byteArray = new ByteArray();
      int startIndex = 0;
      while (startIndex < s.Length)
      {
        byte result;
        if (!byte.TryParse(s.Substring(startIndex, 2), NumberStyles.HexNumber, (IFormatProvider) null, out result))
          return (ByteArray) null;
        byteArray.WriteByte(result);
        startIndex += 2;
      }
      byteArray.Position = 0U;
      return byteArray;
    }
  }
}
