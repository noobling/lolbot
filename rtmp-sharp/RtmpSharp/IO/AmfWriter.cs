// Decompiled with JetBrains decompiler
// Type: RtmpSharp.IO.AmfWriter
// Assembly: rtmp-sharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8588136F-A4B9-4004-9712-4EA13AA4AF9D
// Assembly location: C:\Users\Hesa\Desktop\eZ_Source\bin\Debug\rtmp-sharp.dll

using RtmpSharp.IO.AMF0;
using RtmpSharp.IO.AMF0.AMFWriters;
using RtmpSharp.IO.AMF3;
using RtmpSharp.IO.AMF3.AMFWriters;
using RtmpSharp.IO.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Xml.Linq;

namespace RtmpSharp.IO
{
  public class AmfWriter : IDisposable
  {
    private static readonly int[] UInt29Range = new int[2]{ 0, 536870911 };
    private static readonly int[] Int29Range = new int[2]{ -268435456, 268435455 };
    private static readonly DateTime Epoch = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
    private static readonly AmfWriterMap Amf0Writers;
    private static readonly AmfWriterMap Amf3Writers;
    private readonly Dictionary<object, int> _amf0ObjectReferences;
    private readonly Dictionary<ClassDescription, int> _amf3ClassDefinitionReferences;
    private readonly Dictionary<object, int> _amf3ObjectReferences;
    private readonly Dictionary<object, int> _amf3StringReferences;
    private readonly ObjectEncoding _objectEncoding;
    private readonly BinaryWriter _underlying;

    public SerializationContext SerializationContext { get; }

    public long Length
    {
      get
      {
        return this._underlying.BaseStream.Length;
      }
    }

    public long Position
    {
      get
      {
        return this._underlying.BaseStream.Position;
      }
    }

    public bool DataAvailable
    {
      get
      {
        return this.Position < this.Length;
      }
    }

    static AmfWriter()
    {
      Type[] typeArray1 = new Type[6]{ typeof (sbyte), typeof (byte), typeof (short), typeof (ushort), typeof (int), typeof (uint) };
      Type[] typeArray2 = new Type[5]{ typeof (long), typeof (ulong), typeof (float), typeof (double), typeof (Decimal) };
      AmfWriterMap amfWriterMap1 = new AmfWriterMap((IAmfItemWriter) new Amf0ObjectWriter());
      Type key1 = typeof (Array);
      Amf0ArrayWriter amf0ArrayWriter = new Amf0ArrayWriter();
      amfWriterMap1.Add(key1, (IAmfItemWriter) amf0ArrayWriter);
      Type key2 = typeof (AsObject);
      Amf0AsObjectWriter amf0AsObjectWriter = new Amf0AsObjectWriter();
      amfWriterMap1.Add(key2, (IAmfItemWriter) amf0AsObjectWriter);
      Type key3 = typeof (bool);
      Amf0BooleanWriter amf0BooleanWriter = new Amf0BooleanWriter();
      amfWriterMap1.Add(key3, (IAmfItemWriter) amf0BooleanWriter);
      Type key4 = typeof (char);
      Amf0CharWriter amf0CharWriter = new Amf0CharWriter();
      amfWriterMap1.Add(key4, (IAmfItemWriter) amf0CharWriter);
      Type key5 = typeof (DateTime);
      Amf0DateTimeWriter amf0DateTimeWriter = new Amf0DateTimeWriter();
      amfWriterMap1.Add(key5, (IAmfItemWriter) amf0DateTimeWriter);
      Type key6 = typeof (Enum);
      Amf0EnumWriter amf0EnumWriter = new Amf0EnumWriter();
      amfWriterMap1.Add(key6, (IAmfItemWriter) amf0EnumWriter);
      Type key7 = typeof (Guid);
      Amf0GuidWriter amf0GuidWriter = new Amf0GuidWriter();
      amfWriterMap1.Add(key7, (IAmfItemWriter) amf0GuidWriter);
      Type key8 = typeof (string);
      Amf0StringWriter amf0StringWriter = new Amf0StringWriter();
      amfWriterMap1.Add(key8, (IAmfItemWriter) amf0StringWriter);
      Type key9 = typeof (XDocument);
      Amf0XDocumentWriter amf0XdocumentWriter = new Amf0XDocumentWriter();
      amfWriterMap1.Add(key9, (IAmfItemWriter) amf0XdocumentWriter);
      Type key10 = typeof (XElement);
      Amf0XElementWriter amf0XelementWriter = new Amf0XElementWriter();
      amfWriterMap1.Add(key10, (IAmfItemWriter) amf0XelementWriter);
      AmfWriter.Amf0Writers = amfWriterMap1;
      Amf0NumberWriter amf0NumberWriter = new Amf0NumberWriter();
      foreach (Type key11 in ((IEnumerable<Type>) typeArray1).Concat<Type>((IEnumerable<Type>) typeArray2))
        AmfWriter.Amf0Writers.Add(key11, (IAmfItemWriter) amf0NumberWriter);
      AmfWriterMap amfWriterMap2 = new AmfWriterMap((IAmfItemWriter) new Amf3ObjectWriter());
      Type key12 = typeof (Array);
      Amf3ArrayWriter amf3ArrayWriter = new Amf3ArrayWriter();
      amfWriterMap2.Add(key12, (IAmfItemWriter) amf3ArrayWriter);
      Type key13 = typeof (AsObject);
      Amf3AsObjectWriter amf3AsObjectWriter = new Amf3AsObjectWriter();
      amfWriterMap2.Add(key13, (IAmfItemWriter) amf3AsObjectWriter);
      Type key14 = typeof (bool);
      Amf3BooleanWriter amf3BooleanWriter = new Amf3BooleanWriter();
      amfWriterMap2.Add(key14, (IAmfItemWriter) amf3BooleanWriter);
      Type key15 = typeof (ByteArray);
      Amf3ByteArrayWriter amf3ByteArrayWriter = new Amf3ByteArrayWriter();
      amfWriterMap2.Add(key15, (IAmfItemWriter) amf3ByteArrayWriter);
      Type key16 = typeof (char);
      Amf3CharWriter amf3CharWriter = new Amf3CharWriter();
      amfWriterMap2.Add(key16, (IAmfItemWriter) amf3CharWriter);
      Type key17 = typeof (DateTime);
      Amf3DateTimeWriter amf3DateTimeWriter = new Amf3DateTimeWriter();
      amfWriterMap2.Add(key17, (IAmfItemWriter) amf3DateTimeWriter);
      Type key18 = typeof (Enum);
      Amf3EnumWriter amf3EnumWriter = new Amf3EnumWriter();
      amfWriterMap2.Add(key18, (IAmfItemWriter) amf3EnumWriter);
      Type key19 = typeof (Guid);
      Amf3GuidWriter amf3GuidWriter = new Amf3GuidWriter();
      amfWriterMap2.Add(key19, (IAmfItemWriter) amf3GuidWriter);
      Type key20 = typeof (string);
      Amf3StringWriter amf3StringWriter = new Amf3StringWriter();
      amfWriterMap2.Add(key20, (IAmfItemWriter) amf3StringWriter);
      Type key21 = typeof (XDocument);
      Amf3XDocumentWriter amf3XdocumentWriter = new Amf3XDocumentWriter();
      amfWriterMap2.Add(key21, (IAmfItemWriter) amf3XdocumentWriter);
      Type key22 = typeof (XElement);
      Amf3XElementWriter amf3XelementWriter = new Amf3XElementWriter();
      amfWriterMap2.Add(key22, (IAmfItemWriter) amf3XelementWriter);
      Type key23 = typeof (byte[]);
      Amf3NativeByteArrayWriter nativeByteArrayWriter = new Amf3NativeByteArrayWriter();
      amfWriterMap2.Add(key23, (IAmfItemWriter) nativeByteArrayWriter);
      AmfWriter.Amf3Writers = amfWriterMap2;
      Amf3IntWriter amf3IntWriter = new Amf3IntWriter();
      foreach (Type key11 in typeArray1)
        AmfWriter.Amf3Writers.Add(key11, (IAmfItemWriter) amf3IntWriter);
      Amf3DoubleWriter amf3DoubleWriter = new Amf3DoubleWriter();
      foreach (Type key11 in typeArray2)
        AmfWriter.Amf3Writers.Add(key11, (IAmfItemWriter) amf3DoubleWriter);
    }

    public AmfWriter(Stream stream, SerializationContext serializationContext)
      : this(stream, serializationContext, ObjectEncoding.Amf3)
    {
    }

    public AmfWriter(Stream stream, SerializationContext serializationContext, ObjectEncoding objectEncoding)
    {
      this._objectEncoding = objectEncoding;
      this._underlying = new BinaryWriter(stream);
      this._amf0ObjectReferences = new Dictionary<object, int>();
      this._amf3ObjectReferences = new Dictionary<object, int>();
      this._amf3StringReferences = new Dictionary<object, int>();
      this._amf3ClassDefinitionReferences = new Dictionary<ClassDescription, int>();
      this.SerializationContext = serializationContext;
    }

    public void Dispose()
    {
      BinaryWriter underlying = this._underlying;
      if (underlying == null)
        return;
      underlying.Dispose();
    }

    public static void EnableFlash10Writers()
    {
      Func<bool, IAmfItemWriter> func1 = (Func<bool, IAmfItemWriter>) (isFixed => (IAmfItemWriter) new Amf3VectorWriter<int>(Amf3TypeMarkers.VectorInt, (Action<AmfWriter, IList>) ((writer, list) => writer.WriteAmf3Vector<int>(false, isFixed, list, new Action<int>(writer.WriteInt32)))));
      Func<bool, IAmfItemWriter> func2 = (Func<bool, IAmfItemWriter>) (isFixed => (IAmfItemWriter) new Amf3VectorWriter<uint>(Amf3TypeMarkers.VectorInt, (Action<AmfWriter, IList>) ((writer, list) => writer.WriteAmf3Vector<uint>(false, isFixed, list, (Action<uint>) (i => writer.WriteInt32((int) i))))));
      Func<bool, IAmfItemWriter> func3 = (Func<bool, IAmfItemWriter>) (isFixed => (IAmfItemWriter) new Amf3VectorWriter<double>(Amf3TypeMarkers.VectorInt, (Action<AmfWriter, IList>) ((writer, list) => writer.WriteAmf3Vector<double>(false, isFixed, list, new Action<double>(writer.WriteDouble)))));
      Func<bool, IAmfItemWriter> func4 = (Func<bool, IAmfItemWriter>) (isFixed => (IAmfItemWriter) new Amf3VectorWriter<object>(Amf3TypeMarkers.VectorInt, (Action<AmfWriter, IList>) ((writer, list) => writer.WriteAmf3Vector<object>(true, isFixed, list, new Action<object>(writer.WriteAmf3Item)))));
      foreach (KeyValuePair<Type, IAmfItemWriter> keyValuePair in new Dictionary<Type, IAmfItemWriter>() { { typeof (int[]), func1(true) }, { typeof (List<int>), func1(false) }, { typeof (uint[]), func2(true) }, { typeof (List<uint>), func2(false) }, { typeof (double[]), func3(true) }, { typeof (List<double>), func3(false) }, { typeof (object[]), func4(true) }, { typeof (List<object>), func4(false) } })
        AmfWriter.Amf3Writers[keyValuePair.Key] = keyValuePair.Value;
    }

    private static IAmfItemWriter GetAmfWriter(AmfWriterMap writerMap, Type type)
    {
      IAmfItemWriter amfItemWriter;
      if (writerMap.TryGetValue(type, out amfItemWriter) || type.BaseType != (Type) null && writerMap.TryGetValue(type.BaseType, out amfItemWriter))
        return amfItemWriter;
      lock (writerMap)
      {
        if (writerMap.TryGetValue(type, out amfItemWriter))
          return amfItemWriter;
        IAmfItemWriter local_0_1 = writerMap.DefaultWriter;
        writerMap.Add(type, local_0_1);
        return local_0_1;
      }
    }

    public void Reset()
    {
      this._amf0ObjectReferences.Clear();
      this._amf3ObjectReferences.Clear();
      this._amf3StringReferences.Clear();
      this._amf3ClassDefinitionReferences.Clear();
    }

    public long Seek(int offset, SeekOrigin origin)
    {
      return this._underlying.Seek(offset, origin);
    }

    public void Flush()
    {
      this._underlying.Flush();
    }

    public void Write(byte value)
    {
      this._underlying.Write(value);
    }

    public void Write(byte[] value)
    {
      this._underlying.Write(value);
    }

    public void Write(byte[] bytes, int index, int count)
    {
      this._underlying.Write(bytes, index, count);
    }

    public void WriteByte(byte value)
    {
      this.Write(value);
    }

    public void WriteBytes(byte[] buffer)
    {
      if (buffer == null)
        throw new ArgumentNullException("buffer");
      this.Write(buffer);
    }

    internal void WriteMarker(Amf0TypeMarkers marker)
    {
      this.Write((byte) marker);
    }

    internal void WriteMarker(Amf3TypeMarkers marker)
    {
      this.Write((byte) marker);
    }

    public void WriteInt16(short value)
    {
      this.WriteBigEndian(BitConverter.GetBytes(value));
    }

    public void WriteUInt16(ushort value)
    {
      this.WriteBigEndian(BitConverter.GetBytes(value));
    }

    public void WriteDouble(double value)
    {
      this.WriteBigEndian(BitConverter.GetBytes(value));
    }

    public void WriteFloat(float value)
    {
      this.WriteBigEndian(BitConverter.GetBytes(value));
    }

    public void WriteInt32(int value)
    {
      this.WriteBigEndian(BitConverter.GetBytes(value));
    }

    public void WriteUInt32(uint value)
    {
      this.WriteBigEndian(BitConverter.GetBytes(value));
    }

    public void WriteReverseInt(int value)
    {
      byte[] bytes = new byte[4];
      bytes[3] = (byte) ((int) byte.MaxValue & value >> 24);
      bytes[2] = (byte) ((int) byte.MaxValue & value >> 16);
      bytes[1] = (byte) ((int) byte.MaxValue & value >> 8);
      bytes[0] = (byte) ((int) byte.MaxValue & value);
      this.Write(bytes, 0, bytes.Length);
    }

    public void WriteUInt24(int value)
    {
      if (value < AmfWriter.UInt29Range[0] || value > AmfWriter.UInt29Range[1])
        throw new ArgumentOutOfRangeException("value");
      this.WriteBytes(new byte[3]
      {
        (byte) ((int) byte.MaxValue & value >> 16),
        (byte) ((int) byte.MaxValue & value >> 8),
        (byte) ((int) byte.MaxValue & value)
      });
    }

    public void WriteBoolean(bool value)
    {
      this.Write(value ? (byte) 1 : (byte) 0);
    }

    internal void WriteUtfPrefixed(string str)
    {
      if (str == null)
        throw new ArgumentNullException("str");
      this.WriteUtfPrefixed(Encoding.UTF8.GetBytes(str));
    }

    private void WriteUtfPrefixed(byte[] buffer)
    {
      if (buffer == null)
        throw new ArgumentNullException("buffer");
      if (buffer.Length > (int) ushort.MaxValue)
        throw new SerializationException("String is larger than maximum encodable value.");
      this.WriteUInt16((ushort) buffer.Length);
      this.Write(buffer);
    }

    private void WriteBigEndian(byte[] bytes)
    {
      if (BitConverter.IsLittleEndian)
        Array.Reverse((Array) bytes);
      this.Write(bytes);
    }

    public void WriteAmfItem(object data)
    {
      this.WriteAmfItem(this._objectEncoding, data);
    }

    public void WriteAmfItem(ObjectEncoding objectEncoding, object data)
    {
      if (data == null)
      {
        this.WriteMarker(Amf0TypeMarkers.Null);
      }
      else
      {
        if (this.WriteAmf0ReferenceOnExistence(data))
          return;
        Type type = data.GetType();
        if (objectEncoding != ObjectEncoding.Amf0)
        {
          if (objectEncoding != ObjectEncoding.Amf3)
            throw new ArgumentOutOfRangeException("objectEncoding");
          this.WriteMarker(Amf0TypeMarkers.Amf3Object);
          this.WriteAmf3Item(data);
        }
        else
          AmfWriter.GetAmfWriter(AmfWriter.Amf0Writers, type).WriteData(this, data);
      }
    }

    internal void AddAmf0Reference(object value)
    {
      this._amf0ObjectReferences.Add(value, this._amf0ObjectReferences.Count);
    }

    internal bool WriteAmf0ReferenceOnExistence(object value)
    {
      int num;
      if (!this._amf0ObjectReferences.TryGetValue(value, out num))
        return false;
      this.WriteMarker(Amf0TypeMarkers.Reference);
      this.WriteUInt16((ushort) this._amf0ObjectReferences[value]);
      return true;
    }

    internal void WriteAmf0StringSpecial(string str)
    {
      if (str == null)
        throw new ArgumentNullException("str");
      byte[] bytes = Encoding.UTF8.GetBytes(str);
      if (bytes.Length < (int) ushort.MaxValue)
      {
        this.WriteMarker(Amf0TypeMarkers.String);
        this.WriteUtfPrefixed(bytes);
      }
      else
      {
        this.WriteMarker(Amf0TypeMarkers.LongString);
        this.WriteAmf0UtfLong(bytes);
      }
    }

    internal void WriteAmf0UtfLong(string value)
    {
      this.WriteAmf0UtfLong(Encoding.UTF8.GetBytes(value));
    }

    private void WriteAmf0UtfLong(byte[] buffer)
    {
      if (buffer == null)
        throw new ArgumentNullException("buffer");
      this.WriteUInt32((uint) buffer.Length);
      this.WriteBytes(buffer);
    }

    public void WriteAmf0Item(object data)
    {
      if (data == null)
      {
        this.WriteMarker(Amf0TypeMarkers.Null);
      }
      else
      {
        if (this.WriteAmf0ReferenceOnExistence(data))
          return;
        Type type = data.GetType();
        AmfWriter.GetAmfWriter(AmfWriter.Amf0Writers, type).WriteData(this, data);
      }
    }

    internal void WriteAmf0AsObject(AsObject obj)
    {
      if (obj == null)
        throw new ArgumentNullException("obj");
      this.AddAmf0Reference((object) obj);
      bool flag = string.IsNullOrEmpty(obj.TypeName);
      this.WriteMarker(flag ? Amf0TypeMarkers.Object : Amf0TypeMarkers.TypedObject);
      if (!flag)
        this.WriteUtfPrefixed(obj.TypeName);
      foreach (KeyValuePair<string, object> keyValuePair in obj)
      {
        this.WriteUtfPrefixed(keyValuePair.Key);
        this.WriteAmf0Item(keyValuePair.Value);
      }
      this.WriteUInt16((ushort) 0);
      this.WriteMarker(Amf0TypeMarkers.ObjectEnd);
    }

    internal void WriteAmf0TypedObject(object obj)
    {
      if (obj == null)
        throw new ArgumentNullException("obj");
      if (this.SerializationContext == null)
        throw new NullReferenceException("Cannot serialize objects because no SerializationContext was provided.");
      this.AddAmf0Reference(obj);
      Type type = obj.GetType();
      string fullName = type.FullName;
      ClassDescription classDescription = this.SerializationContext.GetClassDescription(type, obj);
      if (classDescription == null)
        throw new SerializationException(string.Format("Couldn't get class description for {0}.", (object) fullName));
      this.WriteMarker(Amf0TypeMarkers.TypedObject);
      this.WriteUtfPrefixed(classDescription.Name);
      foreach (IMemberWrapper member in classDescription.Members)
      {
        this.WriteUtfPrefixed(member.SerializedName);
        this.WriteAmf0Item(member.GetValue(obj));
      }
      this.WriteUInt16((ushort) 0);
      this.WriteMarker(Amf0TypeMarkers.ObjectEnd);
    }

    internal void WriteAmf0DateTime(DateTime value)
    {
      this.WriteDouble(value.ToUniversalTime().Subtract(AmfWriter.Epoch).TotalMilliseconds);
      this.WriteUInt16((ushort) 0);
    }

    internal void WriteAmf0XDocument(XDocument document)
    {
      if (document == null)
        throw new ArgumentNullException("document");
      this.AddAmf0Reference((object) document);
      this.WriteAmf0UtfLong(document.ToString());
    }

    internal void WriteAmf0XElement(XElement element)
    {
      if (element == null)
        throw new ArgumentNullException("element");
      this.AddAmf0Reference((object) element);
      this.WriteAmf0UtfLong(element.ToString());
    }

    internal void WriteAmf0Array(Array array)
    {
      if (array == null)
        throw new ArgumentNullException("array");
      this.AddAmf0Reference((object) array);
      this.WriteInt32(array.Length);
      foreach (object data in array)
        this.WriteAmf0Item(data);
    }

    internal void WriteAmf0AssociativeArray(IDictionary<string, object> dictionary)
    {
      if (dictionary == null)
        throw new ArgumentNullException("dictionary");
      this.AddAmf0Reference((object) dictionary);
      this.WriteMarker(Amf0TypeMarkers.EcmaArray);
      this.WriteInt32(dictionary.Count);
      foreach (KeyValuePair<string, object> keyValuePair in (IEnumerable<KeyValuePair<string, object>>) dictionary)
      {
        this.WriteUtfPrefixed(keyValuePair.Key);
        this.WriteAmf0Item(keyValuePair.Value);
      }
      this.WriteUInt16((ushort) 0);
      this.WriteMarker(Amf0TypeMarkers.ObjectEnd);
    }

    private void AddAmf3Reference(string obj)
    {
      this.AddAmf3Reference(this._amf3StringReferences, (object) obj);
    }

    private void AddAmf3Reference(object obj)
    {
      this.AddAmf3Reference(this._amf3ObjectReferences, obj);
    }

    private void AddAmf3Reference(Dictionary<object, int> referenceDictionary, object obj)
    {
      referenceDictionary.Add(obj, referenceDictionary.Count);
    }

    private void WriteAmf3InlineHeader(int value)
    {
      this.WriteAmf3Int(value << 1 | 1);
    }

    private bool WriteAmf3ReferenceOnExistence(string obj)
    {
      return this.WriteAmf3ReferenceOnExistence(this._amf3StringReferences, (object) obj);
    }

    private bool WriteAmf3ReferenceOnExistence(object obj)
    {
      return this.WriteAmf3ReferenceOnExistence(this._amf3ObjectReferences, obj);
    }

    private bool WriteAmf3ReferenceOnExistence(Dictionary<object, int> referenceDictionary, object obj)
    {
      int num;
      if (!referenceDictionary.TryGetValue(obj, out num))
        return false;
      this.WriteAmf3Int(num << 1);
      return true;
    }

    public void WriteAmf3Item(object data)
    {
      if (data == null)
      {
        this.WriteAmf3Null();
      }
      else
      {
        Type type = data.GetType();
        AmfWriter.GetAmfWriter(AmfWriter.Amf3Writers, type).WriteData(this, data);
      }
    }

    internal void WriteAmf3Null()
    {
      this.WriteMarker(Amf3TypeMarkers.Null);
    }

    internal void WriteAmf3BoolSpecial(bool value)
    {
      this.WriteMarker(value ? Amf3TypeMarkers.True : Amf3TypeMarkers.False);
    }

    internal void WriteAmf3Array(Array array)
    {
      if (array == null)
        throw new ArgumentNullException("array");
      if (this.WriteAmf3ReferenceOnExistence((object) array))
        return;
      this.AddAmf3Reference((object) array);
      this.WriteAmf3InlineHeader(array.Length);
      this.WriteAmf3Utf(string.Empty);
      foreach (object data in array)
        this.WriteAmf3Item(data);
    }

    internal void WriteAmf3Array(IEnumerable enumerable)
    {
      if (enumerable == null)
        throw new ArgumentNullException("enumerable");
      if (this.WriteAmf3ReferenceOnExistence((object) enumerable))
        return;
      IList list = enumerable.ToList();
      this.AddAmf3Reference((object) list);
      this.WriteAmf3InlineHeader(list.Count);
      this.WriteAmf3Utf(string.Empty);
      foreach (object data in (IEnumerable) list)
        this.WriteAmf3Item(data);
    }

    internal void WriteAmf3AssociativeArray(IDictionary<string, object> dictionary)
    {
      if (dictionary == null)
        throw new ArgumentNullException("dictionary");
      if (this.WriteAmf3ReferenceOnExistence((object) dictionary))
        return;
      this.AddAmf3Reference((object) dictionary);
      this.WriteAmf3InlineHeader(0);
      foreach (KeyValuePair<string, object> keyValuePair in (IEnumerable<KeyValuePair<string, object>>) dictionary)
      {
        this.WriteAmf3Utf(keyValuePair.Key);
        this.WriteAmf3Item(keyValuePair.Value);
      }
      this.WriteAmf3Utf(string.Empty);
    }

    internal void WriteAmf3ByteArray(ByteArray byteArray)
    {
      if (byteArray == null)
        throw new ArgumentNullException("byteArray");
      if (this.WriteAmf3ReferenceOnExistence((object) byteArray))
        return;
      this.AddAmf3Reference((object) byteArray);
      this.WriteAmf3InlineHeader((int) byteArray.Length);
      this.WriteBytes(byteArray.MemoryStream.ToArray());
    }

    internal void WriteAmf3Utf(string str)
    {
      if (str == null)
        throw new ArgumentNullException("str");
      if (str == string.Empty)
      {
        this.WriteAmf3InlineHeader(0);
      }
      else
      {
        if (this.WriteAmf3ReferenceOnExistence(str))
          return;
        this.AddAmf3Reference(str);
        byte[] bytes = Encoding.UTF8.GetBytes(str);
        this.WriteAmf3InlineHeader(bytes.Length);
        this.WriteBytes(bytes);
      }
    }

    internal void WriteAmf3DateTime(DateTime value)
    {
      if (this.WriteAmf3ReferenceOnExistence((object) value))
        return;
      TimeSpan timeSpan = value.ToUniversalTime().Subtract(AmfWriter.Epoch);
      this.WriteAmf3InlineHeader(0);
      this.WriteDouble(timeSpan.TotalMilliseconds);
    }

    internal void WriteAmf3Int(int value)
    {
      value &= 536870911;
      if (value < 128)
        this.WriteByte((byte) value);
      else if (value < 16384)
      {
        this.WriteByte((byte) (value >> 7 & (int) sbyte.MaxValue | 128));
        this.WriteByte((byte) (value & (int) sbyte.MaxValue));
      }
      else if (value < 2097152)
      {
        this.WriteByte((byte) (value >> 14 & (int) sbyte.MaxValue | 128));
        this.WriteByte((byte) (value >> 7 & (int) sbyte.MaxValue | 128));
        this.WriteByte((byte) (value & (int) sbyte.MaxValue));
      }
      else
      {
        this.WriteByte((byte) (value >> 22 & (int) sbyte.MaxValue | 128));
        this.WriteByte((byte) (value >> 15 & (int) sbyte.MaxValue | 128));
        this.WriteByte((byte) (value >> 8 & (int) sbyte.MaxValue | 128));
        this.WriteByte((byte) (value & (int) byte.MaxValue));
      }
    }

    internal void WriteAmf3NumberSpecial(int value)
    {
      if (value >= AmfWriter.Int29Range[0] && value <= AmfWriter.Int29Range[1])
      {
        this.WriteMarker(Amf3TypeMarkers.Integer);
        this.WriteAmf3Int(value);
      }
      else
      {
        this.WriteMarker(Amf3TypeMarkers.Double);
        this.WriteAmf3Double((double) value);
      }
    }

    internal void WriteAmf3Double(double value)
    {
      this.WriteDouble(value);
    }

    internal void WriteAmf3XDocument(XDocument document)
    {
      this.WriteAmf3Utf((document != null ? document.ToString() : (string) null) ?? string.Empty);
    }

    internal void WriteAmf3XElement(XElement element)
    {
      this.WriteAmf3Utf((element != null ? element.ToString() : (string) null) ?? string.Empty);
    }

    internal void WriteAmf3Vector<T>(bool writeTypeName, bool fixedSize, IList list, Action<T> writeElement)
    {
      if (list == null)
        throw new ArgumentNullException("list");
      if (this.WriteAmf3ReferenceOnExistence((object) list))
        return;
      this.AddAmf3Reference((object) list);
      this.WriteAmf3InlineHeader(list.Count);
      this.WriteByte(fixedSize ? (byte) 1 : (byte) 0);
      if (writeTypeName)
        this.WriteAmf3Utf("*");
      foreach (object obj in (IEnumerable) list)
        writeElement((T) obj);
    }

    internal void WriteAmf3Dictionary(IDictionary dictionary)
    {
      if (dictionary == null)
        throw new ArgumentNullException("dictionary");
      if (this.WriteAmf3ReferenceOnExistence((object) dictionary))
        return;
      int count = dictionary.Count;
      this.AddAmf3Reference((object) dictionary);
      this.WriteAmf3InlineHeader(count);
      this.WriteByte((byte) 0);
      IDictionaryEnumerator enumerator = dictionary.GetEnumerator();
      while (enumerator.MoveNext())
      {
        this.WriteAmf3Item(enumerator.Key);
        this.WriteAmf3Item(enumerator.Value);
      }
    }

    internal void WriteAmf3Object(object obj)
    {
      if (obj == null)
        throw new ArgumentNullException("obj");
      if (this.SerializationContext == null)
        throw new NullReferenceException("Cannot serialize objects because no SerializationContext was provided.");
      if (this.WriteAmf3ReferenceOnExistence(obj))
        return;
      this.AddAmf3Reference(obj);
      ClassDescription classDescription = this.SerializationContext.GetClassDescription(obj);
      int num;
      if (this._amf3ClassDefinitionReferences.TryGetValue(classDescription, out num))
        this.WriteAmf3InlineHeader(num << 1);
      else if (obj is AsObject && !(obj as AsObject).IsTyped)
      {
        this.WriteByte((byte) 11);
        this.WriteByte((byte) 1);
        foreach (KeyValuePair<string, object> keyValuePair in obj as AsObject)
        {
          this.WriteAmf3Utf(keyValuePair.Key);
          this.WriteAmf3Item(keyValuePair.Value);
        }
        this.WriteByte((byte) 1);
      }
      else
      {
        this._amf3ClassDefinitionReferences.Add(classDescription, this._amf3ClassDefinitionReferences.Count);
        this.WriteAmf3InlineHeader(((classDescription.Members.Length << 1 | (classDescription.IsDynamic ? 1 : 0)) << 1 | (classDescription.IsExternalizable ? 1 : 0)) << 1 | 1);
        this.WriteAmf3Utf(classDescription.Name);
        if (classDescription.IsExternalizable)
        {
          IExternalizable externalizable = obj as IExternalizable;
          if (externalizable == null)
            throw new SerializationException("Externalizable class does not implement IExternalizable");
          DataOutput dataOutput = new DataOutput(this);
          externalizable.WriteExternal((IDataOutput) dataOutput);
        }
        else
        {
          foreach (IMemberWrapper member in classDescription.Members)
            this.WriteAmf3Utf(member.SerializedName);
          foreach (IMemberWrapper member in classDescription.Members)
            this.WriteAmf3Item(member.GetValue(obj));
          if (!classDescription.IsDynamic)
            return;
          IDictionary<string, object> dictionary = obj as IDictionary<string, object>;
          if (dictionary == null)
            throw new SerializationException("Dynamic class does not implement IDictionary");
          foreach (KeyValuePair<string, object> keyValuePair in (IEnumerable<KeyValuePair<string, object>>) dictionary)
          {
            this.WriteAmf3Utf(keyValuePair.Key);
            this.WriteAmf3Item(keyValuePair.Value);
          }
          this.WriteAmf3Utf(string.Empty);
        }
      }
    }
  }
}
