// Decompiled with JetBrains decompiler
// Type: RtmpSharp.Net.RtmpHandshake
// Assembly: rtmp-sharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8588136F-A4B9-4004-9712-4EA13AA4AF9D
// Assembly location: C:\Users\Hesa\Desktop\eZ_Source\bin\Debug\rtmp-sharp.dll

using RtmpSharp.IO;
using System.IO;
using System.Threading.Tasks;

namespace RtmpSharp.Net
{
    internal struct RtmpHandshake
    {
        private const int HandshakeRandomSize = 1528;
        private const int HandshakeSize = 1536;
        public byte[] Random;
        public uint Time;
        public uint Time2;
        public byte Version;

        public RtmpHandshake Clone()
        {
            return new RtmpHandshake()
            {
                Version = this.Version,
                Time = this.Time,
                Time2 = this.Time2,
                Random = this.Random
            };
        }

        public static async Task<RtmpHandshake> ReadAsync(Stream stream, bool readVersion)
        {
            RtmpHandshake rtmpHandshake;
            using (AmfReader amfReader = new AmfReader(new MemoryStream(await StreamHelper.ReadBytesAsync(stream, 1536 + (readVersion ? 1 : 0))), (SerializationContext)null))
                rtmpHandshake = new RtmpHandshake()
                {
                    Version = readVersion ? amfReader.ReadByte() : (byte)0,
                    Time = amfReader.ReadUInt32(),
                    Time2 = amfReader.ReadUInt32(),
                    Random = amfReader.ReadBytes(1528)
                };
            return rtmpHandshake;
        }

        public static RtmpHandshake Read(Stream stream, bool readVersion)
        {
            int count = 1536 + (readVersion ? 1 : 0);
            using (AmfReader amfReader = new AmfReader(new MemoryStream(StreamHelper.ReadBytes(stream, count)), null))
            {
                return new RtmpHandshake()
                {
                    Version = readVersion ? amfReader.ReadByte() : (byte)0,
                    Time = amfReader.ReadUInt32(),
                    Time2 = amfReader.ReadUInt32(),
                    Random = amfReader.ReadBytes(1528)
                };
            }
        }

        public static Task WriteAsync(Stream stream, RtmpHandshake h, bool writeVersion)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                using (AmfWriter amfWriter = new AmfWriter(memoryStream, null))
                {
                    if (writeVersion)
                        amfWriter.WriteByte(h.Version);
                    amfWriter.WriteUInt32(h.Time);
                    amfWriter.WriteUInt32(h.Time2);
                    amfWriter.WriteBytes(h.Random);
                    byte[] array = memoryStream.ToArray();
                    return stream.WriteAsync(array, 0, array.Length);
                }
            }
        }

        public static Task WriteAsync(Stream stream, RtmpHandshake h, RtmpHandshake h2, bool writeVersion)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                using (AmfWriter amfWriter = new AmfWriter(memoryStream, null))
                {
                    if (writeVersion)
                        amfWriter.WriteByte(h.Version);
                    amfWriter.WriteUInt32(h.Time);
                    amfWriter.WriteUInt32(h.Time2);
                    amfWriter.WriteBytes(h.Random);
                    amfWriter.WriteUInt32(h2.Time);
                    amfWriter.WriteUInt32(h2.Time2);
                    amfWriter.WriteBytes(h2.Random);
                    byte[] array = memoryStream.ToArray();
                    return stream.WriteAsync(array, 0, array.Length);
                }
            }
        }
    }
}