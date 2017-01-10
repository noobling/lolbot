// Decompiled with JetBrains decompiler
// Type: RtmpSharp.Net.RtmpPacketWriter
// Assembly: rtmp-sharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8588136F-A4B9-4004-9712-4EA13AA4AF9D
// Assembly location: C:\Users\Hesa\Desktop\eZ_Source\bin\Debug\rtmp-sharp.dll

using Complete;
using RtmpSharp.IO;
using RtmpSharp.Messaging;
using RtmpSharp.Messaging.Events;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace RtmpSharp.Net
{
  internal class RtmpPacketWriter
  {
    private int _writeChunkSize = 128;
    private const int DefaultChunkSize = 128;
    private readonly ObjectEncoding _objectEncoding;
    private readonly AutoResetEvent _packetAvailableEvent;
    private readonly ConcurrentQueue<RtmpPacket> _queuedPackets;
    private readonly Dictionary<int, RtmpHeader> _rtmpHeaders;
    private readonly Dictionary<int, RtmpPacket> _rtmpPackets;
    private readonly AmfWriter _writer;

    public bool Continue { get; set; }

    public event EventHandler<ExceptionalEventArgs> Disconnected;

    public RtmpPacketWriter(AmfWriter writer, ObjectEncoding objectEncoding)
    {
      this._objectEncoding = objectEncoding;
      this._writer = writer;
      this._rtmpHeaders = new Dictionary<int, RtmpHeader>();
      this._rtmpPackets = new Dictionary<int, RtmpPacket>();
      this._queuedPackets = new ConcurrentQueue<RtmpPacket>();
      this._packetAvailableEvent = new AutoResetEvent(false);
      this.Continue = true;
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

    public void WriteLoop()
    {
      try
      {
        while (this.Continue)
        {
          this._packetAvailableEvent.WaitOne();
          RtmpPacket result;
          while (this._queuedPackets.TryDequeue(out result))
            this.WritePacket(result);
        }
      }
      catch (Exception ex)
      {
        this.OnDisconnected(new ExceptionalEventArgs("rtmp-packet-writer", ex));
      }
    }

    private static ChunkMessageHeaderType GetMessageHeaderType(RtmpHeader header, RtmpHeader previousHeader)
    {
      if (previousHeader == null || header.MessageStreamId != previousHeader.MessageStreamId || !header.IsTimerRelative)
        return ChunkMessageHeaderType.New;
      if (header.PacketLength != previousHeader.PacketLength || header.MessageType != previousHeader.MessageType)
        return ChunkMessageHeaderType.SameSource;
      return header.Timestamp == previousHeader.Timestamp ? ChunkMessageHeaderType.Continuation : ChunkMessageHeaderType.TimestampAdjustment;
    }

    public void Queue(RtmpEvent message, int streamId, int messageStreamId)
    {
      RtmpHeader header = new RtmpHeader();
      RtmpPacket rtmpPacket = new RtmpPacket(header, message);
      header.StreamId = streamId;
      header.Timestamp = message.Timestamp;
      header.MessageStreamId = messageStreamId;
      header.MessageType = message.MessageType;
      if (message.Header != null)
        header.IsTimerRelative = message.Header.IsTimerRelative;
      this._queuedPackets.Enqueue(rtmpPacket);
      this._packetAvailableEvent.Set();
    }

    private static int GetBasicHeaderLength(int streamId)
    {
      if (streamId >= 320)
        return 3;
      return streamId < 64 ? 1 : 2;
    }

    private void WritePacket(RtmpPacket packet)
    {
      RtmpHeader header = packet.Header;
      int streamId = header.StreamId;
      RtmpEvent body = packet.Body;
      byte[] messageBytes = this.GetMessageBytes(header, body);
      header.PacketLength = messageBytes.Length;
      RtmpHeader previousHeader;
      this._rtmpHeaders.TryGetValue(streamId, out previousHeader);
      this._rtmpHeaders[streamId] = header;
      this._rtmpPackets[streamId] = packet;
      this.WriteMessageHeader(header, previousHeader);
      bool flag = true;
      int index = 0;
      while (index < header.PacketLength)
      {
        if (!flag)
          this.WriteBasicHeader(ChunkMessageHeaderType.Continuation, header.StreamId);
        int count = index + this._writeChunkSize > header.PacketLength ? header.PacketLength - index : this._writeChunkSize;
        this._writer.Write(messageBytes, index, count);
        flag = false;
        index += this._writeChunkSize;
      }
      ChunkSize chunkSize = body as ChunkSize;
      if (chunkSize == null)
        return;
      this._writeChunkSize = chunkSize.Size;
    }

    private void WriteBasicHeader(ChunkMessageHeaderType messageHeaderFormat, int streamId)
    {
      byte num = (byte) messageHeaderFormat;
      if (streamId <= 63)
        this._writer.WriteByte((byte) (((int) num << 6) + streamId));
      else if (streamId <= 320)
      {
        this._writer.WriteByte((byte) ((uint) num << 6));
        this._writer.WriteByte((byte) (streamId - 64));
      }
      else
      {
        this._writer.WriteByte((byte) ((int) num << 6 | 1));
        this._writer.WriteByte((byte) (streamId - 64 & (int) byte.MaxValue));
        this._writer.WriteByte((byte) (streamId - 64 >> 8));
      }
    }

    private void WriteMessageHeader(RtmpHeader header, RtmpHeader previousHeader)
    {
      ChunkMessageHeaderType messageHeaderType = RtmpPacketWriter.GetMessageHeaderType(header, previousHeader);
      this.WriteBasicHeader(messageHeaderType, header.StreamId);
      int num = header.Timestamp < 16777215 ? header.Timestamp : 16777215;
      switch (messageHeaderType)
      {
        case ChunkMessageHeaderType.New:
          this._writer.WriteUInt24(num);
          this._writer.WriteUInt24(header.PacketLength);
          this._writer.WriteByte((byte) header.MessageType);
          this._writer.WriteReverseInt(header.MessageStreamId);
          goto case ChunkMessageHeaderType.Continuation;
        case ChunkMessageHeaderType.SameSource:
          this._writer.WriteUInt24(num);
          this._writer.WriteUInt24(header.PacketLength);
          this._writer.WriteByte((byte) header.MessageType);
          goto case ChunkMessageHeaderType.Continuation;
        case ChunkMessageHeaderType.TimestampAdjustment:
          this._writer.WriteUInt24(num);
          goto case ChunkMessageHeaderType.Continuation;
        case ChunkMessageHeaderType.Continuation:
          if (num < 16777215)
            break;
          this._writer.WriteInt32(header.Timestamp);
          break;
        default:
          throw new ArgumentException("headerType");
      }
    }

    private byte[] GetMessageBytes(RtmpEvent message, Action<AmfWriter, RtmpEvent> handler)
    {
      using (MemoryStream memoryStream = new MemoryStream())
      {
        using (AmfWriter amfWriter = new AmfWriter((Stream) memoryStream, this._writer.SerializationContext, this._objectEncoding))
        {
          handler(amfWriter, message);
          return memoryStream.ToArray();
        }
      }
    }

    private byte[] GetMessageBytes(RtmpHeader header, RtmpEvent message)
    {
      switch (header.MessageType)
      {
        case MessageType.SetChunkSize:
          return this.GetMessageBytes(message, (Action<AmfWriter, RtmpEvent>) ((w, o) => w.WriteInt32(((ChunkSize) o).Size)));
        case MessageType.AbortMessage:
          return this.GetMessageBytes(message, (Action<AmfWriter, RtmpEvent>) ((w, o) => w.WriteInt32(((Abort) o).StreamId)));
        case MessageType.Acknowledgement:
          return this.GetMessageBytes(message, (Action<AmfWriter, RtmpEvent>) ((w, o) => w.WriteInt32(((Acknowledgement) o).BytesRead)));
        case MessageType.UserControlMessage:
          return this.GetMessageBytes(message, (Action<AmfWriter, RtmpEvent>) ((w, o) =>
          {
            UserControlMessage userControlMessage = (UserControlMessage) o;
            w.WriteUInt16((ushort) userControlMessage.EventType);
            foreach (int num in userControlMessage.Values)
              w.WriteInt32(num);
          }));
        case MessageType.WindowAcknowledgementSize:
          return this.GetMessageBytes(message, (Action<AmfWriter, RtmpEvent>) ((w, o) => w.WriteInt32(((WindowAcknowledgementSize) o).Count)));
        case MessageType.SetPeerBandwidth:
          return this.GetMessageBytes(message, (Action<AmfWriter, RtmpEvent>) ((w, o) =>
          {
            PeerBandwidth peerBandwidth = (PeerBandwidth) o;
            w.WriteInt32(peerBandwidth.AcknowledgementWindowSize);
            w.WriteByte((byte) peerBandwidth.LimitType);
          }));
        case MessageType.Audio:
        case MessageType.Video:
          return this.GetMessageBytes(message, (Action<AmfWriter, RtmpEvent>) ((w, o) => w.WriteBytes(((ByteData) o).Data)));
        case MessageType.DataAmf3:
          return this.GetMessageBytes(message, (Action<AmfWriter, RtmpEvent>) ((w, o) => this.WriteData(w, o, ObjectEncoding.Amf3)));
        case MessageType.SharedObjectAmf3:
          return new byte[0];
        case MessageType.CommandAmf3:
          return this.GetMessageBytes(message, (Action<AmfWriter, RtmpEvent>) ((w, o) =>
          {
            w.WriteByte((byte) 0);
            this.WriteCommandOrData(w, o, ObjectEncoding.Amf3);
          }));
        case MessageType.DataAmf0:
          return this.GetMessageBytes(message, (Action<AmfWriter, RtmpEvent>) ((w, o) => this.WriteData(w, o, ObjectEncoding.Amf0)));
        case MessageType.SharedObjectAmf0:
          return new byte[0];
        case MessageType.CommandAmf0:
          return this.GetMessageBytes(message, (Action<AmfWriter, RtmpEvent>) ((w, o) => this.WriteCommandOrData(w, o, ObjectEncoding.Amf0)));
        case MessageType.Aggregate:
          Debugger.Break();
          return new byte[0];
        default:
          throw new ArgumentOutOfRangeException("Unknown RTMP message type: " + (object) (int) header.MessageType);
      }
    }

    private void WriteData(AmfWriter writer, RtmpEvent o, ObjectEncoding encoding)
    {
      Command command = o as Command;
      if (command.MethodCall == null)
        this.WriteCommandOrData(writer, o, encoding);
      else
        writer.WriteBytes(command.Buffer);
    }

    private void WriteCommandOrData(AmfWriter writer, RtmpEvent o, ObjectEncoding encoding)
    {
      Command command = o as Command;
      Method methodCall = command.MethodCall;
      bool flag1 = command is Invoke;
      bool flag2 = methodCall.CallStatus == CallStatus.Request;
      bool flag3 = methodCall.CallStatus == CallStatus.Result;
      if (flag2)
        writer.WriteAmfItem(ObjectEncoding.Amf0, (object) methodCall.Name);
      else
        writer.WriteAmfItem(ObjectEncoding.Amf0, methodCall.IsSuccess ? (object) "_result" : (object) "_error");
      if (flag1)
      {
        writer.WriteAmfItem(ObjectEncoding.Amf0, (object) command.InvokeId);
        writer.WriteAmfItem(encoding, command.ConnectionParameters);
      }
      if (flag2 | flag3)
      {
        foreach (object parameter in methodCall.Parameters)
          writer.WriteAmfItem(encoding, parameter);
      }
      else
      {
        if (!flag1)
          return;
        if (!methodCall.IsSuccess)
          methodCall.Parameters = new object[1]
          {
            (object) new StatusAsObject("NetConnection.Call.Failed", "error", "Call failed.")
          };
        writer.WriteAmfItem(encoding, (object) methodCall.Parameters);
      }
    }
  }
}
