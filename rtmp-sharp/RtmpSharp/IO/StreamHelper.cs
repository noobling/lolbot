// Decompiled with JetBrains decompiler
// Type: RtmpSharp.IO.StreamHelper
// Assembly: rtmp-sharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8588136F-A4B9-4004-9712-4EA13AA4AF9D
// Assembly location: C:\Users\Hesa\Desktop\eZ_Source\bin\Debug\rtmp-sharp.dll

using System;
using System.IO;
using System.Threading.Tasks;

namespace RtmpSharp.IO
{
  internal static class StreamHelper
  {
    public static byte[] ReadBytes(Stream stream, int count)
    {
      if (stream == null)
        throw new ArgumentNullException("stream");
      byte[] buffer = new byte[count];
      int offset = 0;
      while (count > 0)
      {
        int num = stream.Read(buffer, offset, count);
        if (num != 0)
        {
          offset += num;
          count -= num;
        }
        else
          break;
      }
      if (offset != buffer.Length)
        throw new EndOfStreamException();
      return buffer;
    }

    public static async Task<byte[]> ReadBytesAsync(Stream stream, int count)
    {
      if (stream == null)
        throw new ArgumentNullException("stream");
      byte[] result = new byte[count];
      int bytesRead = 0;
      while (count > 0)
      {
        int num = await stream.ReadAsync(result, bytesRead, count);
        if (num != 0)
        {
          bytesRead += num;
          count -= num;
        }
        else
          break;
      }
      if (bytesRead != result.Length)
        throw new EndOfStreamException();
      return result;
    }
  }
}
