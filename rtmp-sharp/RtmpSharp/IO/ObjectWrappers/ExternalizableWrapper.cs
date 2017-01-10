// Decompiled with JetBrains decompiler
// Type: RtmpSharp.IO.ObjectWrappers.ExternalizableWrapper
// Assembly: rtmp-sharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8588136F-A4B9-4004-9712-4EA13AA4AF9D
// Assembly location: C:\Users\Hesa\Desktop\eZ_Source\bin\Debug\rtmp-sharp.dll

using System;

namespace RtmpSharp.IO.ObjectWrappers
{
  internal class ExternalizableWrapper : IObjectWrapper
  {
    private readonly SerializationContext _serializationContext;

    public ExternalizableWrapper(SerializationContext serializationContext)
    {
      this._serializationContext = serializationContext;
    }

    public bool GetIsDynamic(object instance)
    {
      return false;
    }

    public bool GetIsExternalizable(object instance)
    {
      return false;
    }

    public ClassDescription GetClassDescription(object obj)
    {
      return (ClassDescription) new ExternalizableWrapper.ExternalizableClassDescription(this._serializationContext.GetAlias(obj.GetType().FullName), new IMemberWrapper[0], true, false);
    }

    private class ExternalizableClassDescription : ClassDescription
    {
      internal ExternalizableClassDescription(string name, IMemberWrapper[] members, bool externalizable, bool dynamic)
        : base(name, members, externalizable, dynamic)
      {
      }

      public override bool TryGetMember(string name, out IMemberWrapper memberWrapper)
      {
        throw new NotSupportedException();
      }
    }
  }
}
