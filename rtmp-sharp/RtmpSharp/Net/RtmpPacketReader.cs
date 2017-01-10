// Decompiled with JetBrains decompiler
// Type: RtmpSharp.Net.RtmpPacketReader
// Assembly: rtmp-sharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8588136F-A4B9-4004-9712-4EA13AA4AF9D
// Assembly location: C:\Users\Hesa\Desktop\eZ_Source\bin\Debug\rtmp-sharp.dll

using Complete;
using RtmpSharp.IO;
using RtmpSharp.Messaging;
using RtmpSharp.Messaging.Events;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;

namespace RtmpSharp.Net
{
  internal class RtmpPacketReader
  {
    private int _readChunkSize = 128;
    private const int DefaultChunkSize = 128;
    private readonly AmfReader _reader;
    private readonly Dictionary<int, RtmpHeader> _rtmpHeaders;
    private readonly Dictionary<int, RtmpPacket> _rtmpPackets;

    public bool Continue { get; set; }

    public event EventHandler<EventReceivedEventArgs> EventReceived;

    public event EventHandler<ExceptionalEventArgs> Disconnected;

    public RtmpPacketReader(AmfReader reader)
    {
      this._reader = reader;
      this._rtmpHeaders = new Dictionary<int, RtmpHeader>();
      this._rtmpPackets = new Dictionary<int, RtmpPacket>();
      this.Continue = true;
    }

    private void OnEventReceived(EventReceivedEventArgs e)
    {
      // ISSUE: reference to a compiler-generated field
      EventHandler<EventReceivedEventArgs> eventReceived = this.EventReceived;
      if (eventReceived == null)
        return;
      EventReceivedEventArgs e1 = e;
      eventReceived((object) this, e1);
    }

    private void OnDisconnected(ExceptionalEventArgs e)
    {
      this.Continue = false;
      // ISSUE: reference to a compiler-generated field
      EventHandler<ExceptionalEventArgs> disconnected = this.Disconnected;
      if (disconnected == null)
        return;
      ExceptionalEventArgs e1 = e;
      disconnected((object) this, e1);
    }

    public void ReadLoop()
    {
      try
      {
        while (this.Continue)
        {
          RtmpHeader header = this.ReadHeader();
          this._rtmpHeaders[header.StreamId] = header;
          RtmpPacket packet1;
          if (!this._rtmpPackets.TryGetValue(header.StreamId, out packet1) || packet1 == null)
          {
            packet1 = new RtmpPacket(header);
            this._rtmpPackets[header.StreamId] = packet1;
          }
          byte[] bytes = this._reader.ReadBytes(Math.Min(packet1.Length + (header.Timestamp >= 16777215 ? 4 : 0) - packet1.CurrentLength, this._readChunkSize));
          packet1.AddBytes(bytes);
          if (packet1.IsComplete)
          {
            this._rtmpPackets.Remove(header.StreamId);
            RtmpEvent packet2 = this.ParsePacket(packet1);
            this.OnEventReceived(new EventReceivedEventArgs(packet2));
            ChunkSize chunkSize = packet2 as ChunkSize;
            if (chunkSize != null)
              this._readChunkSize = chunkSize.Size;
            Abort abort = packet2 as Abort;
            if (abort != null)
              this._rtmpPackets.Remove(abort.StreamId);
          }
        }
      }
      catch (Exception ex)
      {
        this.OnDisconnected(new ExceptionalEventArgs("rtmp-packet-reader", ex));
      }
    }

    private static int GetChunkStreamId(byte chunkBasicHeaderByte, AmfReader reader)
    {
      int num = (int) chunkBasicHeaderByte & 63;
      switch (num)
      {
        case 0:
          return (int) reader.ReadByte() + 64;
        case 1:
          return (int) reader.ReadByte() + (int) reader.ReadByte() * 256 + 64;
        default:
          return num;
      }
    }

    private RtmpHeader ReadHeader()
    {
      int num1 = (int) this._reader.ReadByte();
      AmfReader reader = this._reader;
      int chunkStreamId = RtmpPacketReader.GetChunkStreamId((byte) num1, reader);
      int num2 = 6;
      ChunkMessageHeaderType messageHeaderType = (ChunkMessageHeaderType) (num1 >> num2);
      RtmpHeader rtmpHeader1 = new RtmpHeader();
      rtmpHeader1.StreamId = chunkStreamId;
      int num3 = (uint) messageHeaderType > 0U ? 1 : 0;
      rtmpHeader1.IsTimerRelative = num3 != 0;
      RtmpHeader rtmpHeader2 = rtmpHeader1;
      RtmpHeader rtmpHeader3;
      if (!this._rtmpHeaders.TryGetValue(chunkStreamId, out rtmpHeader3) && messageHeaderType != ChunkMessageHeaderType.New)
        rtmpHeader3 = rtmpHeader2.Clone();
      switch (messageHeaderType)
      {
        case ChunkMessageHeaderType.New:
          rtmpHeader2.Timestamp = this._reader.ReadUInt24();
          rtmpHeader2.PacketLength = this._reader.ReadUInt24();
          rtmpHeader2.MessageType = (MessageType) this._reader.ReadByte();
          rtmpHeader2.MessageStreamId = this._reader.ReadReverseInt();
          break;
        case ChunkMessageHeaderType.SameSource:
          rtmpHeader2.Timestamp = this._reader.ReadUInt24();
          rtmpHeader2.PacketLength = this._reader.ReadUInt24();
          rtmpHeader2.MessageType = (MessageType) this._reader.ReadByte();
          rtmpHeader2.MessageStreamId = rtmpHeader3.MessageStreamId;
          break;
        case ChunkMessageHeaderType.TimestampAdjustment:
          rtmpHeader2.Timestamp = this._reader.ReadUInt24();
          rtmpHeader2.PacketLength = rtmpHeader3.PacketLength;
          rtmpHeader2.MessageType = rtmpHeader3.MessageType;
          rtmpHeader2.MessageStreamId = rtmpHeader3.MessageStreamId;
          break;
        case ChunkMessageHeaderType.Continuation:
          rtmpHeader2.Timestamp = rtmpHeader3.Timestamp;
          rtmpHeader2.PacketLength = rtmpHeader3.PacketLength;
          rtmpHeader2.MessageType = rtmpHeader3.MessageType;
          rtmpHeader2.MessageStreamId = rtmpHeader3.MessageStreamId;
          rtmpHeader2.IsTimerRelative = rtmpHeader3.IsTimerRelative;
          break;
        default:
          throw new SerializationException("Unexpected header type: " + (object) (int) messageHeaderType);
      }
      if (rtmpHeader2.Timestamp == 16777215)
        rtmpHeader2.Timestamp = this._reader.ReadInt32();
      return rtmpHeader2;
    }

    private RtmpEvent ParsePacket(RtmpPacket packet, Func<AmfReader, RtmpEvent> handler)
    {
      AmfReader amfReader = new AmfReader((Stream) new MemoryStream(packet.Buffer, false), this._reader.SerializationContext);
      RtmpHeader header = packet.Header;
      RtmpEvent rtmpEvent = handler(amfReader);
      RtmpHeader rtmpHeader = header;
      rtmpEvent.Header = rtmpHeader;
      int timestamp = header.Timestamp;
      rtmpEvent.Timestamp = timestamp;
      return rtmpEvent;
    }

    private RtmpEvent ParsePacket(RtmpPacket packet)
    {
      switch (packet.Header.MessageType)
      {
        case MessageType.SetChunkSize:
          return this.ParsePacket(packet, (Func<AmfReader, RtmpEvent>) (r => (RtmpEvent) new ChunkSize(r.ReadInt32())));
        case MessageType.AbortMessage:
          return this.ParsePacket(packet, (Func<AmfReader, RtmpEvent>) (r => (RtmpEvent) new Abort(r.ReadInt32())));
        case MessageType.Acknowledgement:
          return this.ParsePacket(packet, (Func<AmfReader, RtmpEvent>) (r => (RtmpEvent) new Acknowledgement(r.ReadInt32())));
        case MessageType.UserControlMessage:
          return this.ParsePacket(packet, (Func<AmfReader, RtmpEvent>) (r =>
          {
            ushort num = r.ReadUInt16();
            List<int> intList = new List<int>();
            while (r.Length - r.Position >= 4L)
              intList.Add(r.ReadInt32());
            return (RtmpEvent) new UserControlMessage((UserControlMessageType) num, intList.ToArray());
          }));
        case MessageType.WindowAcknowledgementSize:
          return this.ParsePacket(packet, (Func<AmfReader, RtmpEvent>) (r => (RtmpEvent) new WindowAcknowledgementSize(r.ReadInt32())));
        case MessageType.SetPeerBandwidth:
          return this.ParsePacket(packet, (Func<AmfReader, RtmpEvent>) (r => (RtmpEvent) new PeerBandwidth(r.ReadInt32(), r.ReadByte())));
        case MessageType.Audio:
          return this.ParsePacket(packet, (Func<AmfReader, RtmpEvent>) (r => (RtmpEvent) new AudioData(packet.Buffer)));
        case MessageType.Video:
          return this.ParsePacket(packet, (Func<AmfReader, RtmpEvent>) (r => (RtmpEvent) new VideoData(packet.Buffer)));
        case MessageType.DataAmf3:
          return this.ParsePacket(packet, (Func<AmfReader, RtmpEvent>) (r => RtmpPacketReader.ReadCommandOrData(r, (Command) new NotifyAmf3())));
        case MessageType.CommandAmf3:
          return this.ParsePacket(packet, (Func<AmfReader, RtmpEvent>) (r =>
          {
            int num = (int) r.ReadByte();
            return RtmpPacketReader.ReadCommandOrData(r, (Command) new InvokeAmf3());
          }));
        case MessageType.DataAmf0:
          return this.ParsePacket(packet, (Func<AmfReader, RtmpEvent>) (r => RtmpPacketReader.ReadCommandOrData(r, (Command) new NotifyAmf0())));
        case MessageType.CommandAmf0:
          return this.ParsePacket(packet, (Func<AmfReader, RtmpEvent>) (r => RtmpPacketReader.ReadCommandOrData(r, (Command) new InvokeAmf0())));
        default:
          return (RtmpEvent) null;
      }
    }

    private static RtmpEvent ReadCommandOrData(AmfReader r, Command command)
    {
      string methodName = (string) r.ReadAmf0Item();
      command.InvokeId = Convert.ToInt32(r.ReadAmf0Item());
      command.ConnectionParameters = r.ReadAmf0Item();
      List<object> objectList = new List<object>();
      while (r.DataAvailable)
        objectList.Add(r.ReadAmf0Item());
      command.MethodCall = new Method(methodName, objectList.ToArray(), true, CallStatus.Request);
      return (RtmpEvent) command;
    }
  }
}
