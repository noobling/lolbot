// Decompiled with JetBrains decompiler
// Type: RtmpSharp.IO.ObjectWrapperFactory
// Assembly: rtmp-sharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8588136F-A4B9-4004-9712-4EA13AA4AF9D
// Assembly location: C:\Users\Hesa\Desktop\eZ_Source\bin\Debug\rtmp-sharp.dll

using RtmpSharp.IO.AMF3;
using RtmpSharp.IO.ObjectWrappers;
using System;
using System.Collections.Generic;

namespace RtmpSharp.IO
{
  internal class ObjectWrapperFactory
  {
    private static readonly string ExternalizableTypeName = typeof (IExternalizable).FullName;
    private readonly Dictionary<Type, IObjectWrapper> _wrappers = new Dictionary<Type, IObjectWrapper>();
    private readonly IObjectWrapper _defaultWrapper;
    private readonly SerializationContext _serializationContext;

    public ObjectWrapperFactory(SerializationContext serializationContext)
    {
      this._serializationContext = serializationContext;
      this._defaultWrapper = (IObjectWrapper) new BasicObjectWrapper(serializationContext);
      this._wrappers[typeof (AsObject)] = (IObjectWrapper) new AsObjectWrapper(serializationContext);
      this._wrappers[typeof (IExternalizable)] = (IObjectWrapper) new ExternalizableWrapper(serializationContext);
      this._wrappers[typeof (Exception)] = (IObjectWrapper) new ExceptionWrapper(serializationContext);
    }

    public IObjectWrapper GetInstance(Type type)
    {
      if (type.GetInterface(ObjectWrapperFactory.ExternalizableTypeName, true) != (Type) null)
        return this._wrappers[typeof (IExternalizable)];
      IObjectWrapper objectWrapper;
      if (this._wrappers.TryGetValue(type, out objectWrapper))
        return objectWrapper;
      foreach (KeyValuePair<Type, IObjectWrapper> wrapper in this._wrappers)
      {
        if (type.IsSubclassOf(wrapper.Key))
          return wrapper.Value;
      }
      return this._defaultWrapper;
    }

    public ClassDescription GetClassDescription(object obj)
    {
      return this.GetInstance(obj.GetType()).GetClassDescription(obj);
    }

    public ClassDescription GetClassDescription(Type type, object obj)
    {
      return this.GetInstance(type).GetClassDescription(obj);
    }
  }
}
