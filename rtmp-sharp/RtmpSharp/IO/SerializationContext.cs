// Decompiled with JetBrains decompiler
// Type: RtmpSharp.IO.SerializationContext
// Assembly: rtmp-sharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8588136F-A4B9-4004-9712-4EA13AA4AF9D
// Assembly location: C:\Users\Hesa\Desktop\eZ_Source\bin\Debug\rtmp-sharp.dll

using System;
using System.Collections.Generic;

namespace RtmpSharp.IO
{
  public class SerializationContext
  {
    private readonly FallbackStrategy _fallbackStrategy;
    private readonly ObjectWrapperFactory _objectWrapperFactory;
    private readonly SerializerObjectFactory _serializerObjectFactory;

    public SerializationContext()
    {
      this._fallbackStrategy = FallbackStrategy.DynamicObject;
      this._serializerObjectFactory = new SerializerObjectFactory();
      this._objectWrapperFactory = new ObjectWrapperFactory(this);
    }

    public SerializationContext(IEnumerable<Type> types)
      : this()
    {
      foreach (Type type in types)
        this.Register(type);
    }

    public SerializationContext(FallbackStrategy fallbackStrategy)
      : this()
    {
      this._fallbackStrategy = fallbackStrategy;
    }

    public SerializationContext(FallbackStrategy fallbackStrategy, IEnumerable<Type> types)
      : this(fallbackStrategy)
    {
      foreach (Type type in types)
        this.Register(type);
    }

    public void Register(Type type)
    {
      this._serializerObjectFactory.Register(type);
    }

    public void RegisterAlias(Type type, string alias, bool canonical)
    {
      this._serializerObjectFactory.RegisterAlias(type, alias, canonical);
    }

    public string GetAlias(string typeName)
    {
      return this._serializerObjectFactory.GetAlias(typeName);
    }

    internal bool CanCreate(string typeName)
    {
      return this._serializerObjectFactory.CanCreate(typeName);
    }

    internal bool CanCreate(Type type)
    {
      return this._serializerObjectFactory.CanCreate(type);
    }

    internal object Create(string typeName)
    {
      return this._serializerObjectFactory.Create(typeName);
    }

    internal object Create(Type type)
    {
      return this._serializerObjectFactory.Create(type);
    }

    internal ClassDescription GetClassDescription(object obj)
    {
      return this._objectWrapperFactory.GetClassDescription(obj);
    }

    internal ClassDescription GetClassDescription(Type type, object obj)
    {
      return this._objectWrapperFactory.GetClassDescription(type, obj);
    }

    internal DeserializationStrategy GetDeserializationStrategy(string typeName)
    {
      if (!this.CanCreate(typeName))
        return this.GetFallbackDeserializationStrategy();
      return DeserializationStrategy.TypedObject;
    }

    internal DeserializationStrategy GetDeserializationStrategy(Type type)
    {
      if (!this.CanCreate(type))
        return this.GetFallbackDeserializationStrategy();
      return DeserializationStrategy.TypedObject;
    }

    private DeserializationStrategy GetFallbackDeserializationStrategy()
    {
      return this._fallbackStrategy != FallbackStrategy.DynamicObject ? DeserializationStrategy.Exception : DeserializationStrategy.DynamicObject;
    }
  }
}
