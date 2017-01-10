// Decompiled with JetBrains decompiler
// Type: Complete.IO.Zlib.ZlibStream
// Assembly: rtmp-sharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8588136F-A4B9-4004-9712-4EA13AA4AF9D
// Assembly location: C:\Users\Hesa\Desktop\eZ_Source\bin\Debug\rtmp-sharp.dll

using System;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Complete.IO.Zlib
{
  public class ZlibStream : DeflateStream
  {
    private static readonly byte[] ZlibHeader = new byte[2]
    {
      (byte) 88,
      (byte) 133
    };
    private readonly Adler32 _adler32;
    private readonly bool _leaveOpen;
    private readonly CompressionMode _mode;
    private bool _firstReadWrite;
    private Stream _stream;

    public ZlibStream(Stream stream, CompressionMode mode)
      : this(stream, mode, false)
    {
    }

    public ZlibStream(Stream stream, CompressionMode mode, bool leaveOpen)
      : base(stream, mode, true)
    {
      this._stream = stream;
      this._leaveOpen = leaveOpen;
      this._mode = mode;
      this._firstReadWrite = true;
      this._adler32 = new Adler32();
    }

    public override int Read(byte[] buffer, int offset, int count)
    {
      if (this._firstReadWrite)
      {
        this._firstReadWrite = false;
        this._stream.ReadByte();
        this._stream.ReadByte();
      }
      return base.Read(buffer, offset, count);
    }

    public override async Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
    {
      if (this._firstReadWrite)
      {
        this._firstReadWrite = false;
        byte[] numArray = await this._stream.ReadBytesAsync(2);
      }
      return await base.ReadAsync(buffer, offset, count, cancellationToken);
    }

    public override void Write(byte[] buffer, int offset, int count)
    {
      if (this._firstReadWrite)
      {
        this._firstReadWrite = false;
        this._stream.Write(ZlibStream.ZlibHeader, 0, ZlibStream.ZlibHeader.Length);
      }
      base.Write(buffer, offset, count);
      this._adler32.Update(buffer, offset, count);
    }

    public override async Task WriteAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
    {
      if (this._firstReadWrite)
      {
        this._firstReadWrite = false;
        await this._stream.WriteAsync(ZlibStream.ZlibHeader, 0, ZlibStream.ZlibHeader.Length, cancellationToken);
      }
      await base.WriteAsync(buffer, offset, count, cancellationToken);
      this._adler32.Update(buffer, offset, count);
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this._stream != null && this._mode == CompressionMode.Compress)
      {
        byte[] checksumBytes = this.GetChecksumBytes();
        this._stream.Write(checksumBytes, 0, checksumBytes.Length);
        if (!this._leaveOpen)
          this._stream.Close();
        this._stream = (Stream) null;
      }
      base.Dispose(disposing);
    }

    private byte[] GetChecksumBytes()
    {
      return BitConverter.GetBytes(IPAddress.HostToNetworkOrder(this._adler32.Checksum));
    }
  }
}
