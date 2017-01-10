// Decompiled with JetBrains decompiler
// Type: RtmpSharp.IO.ObjectWrappers.AsObjectWrapper
// Assembly: rtmp-sharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8588136F-A4B9-4004-9712-4EA13AA4AF9D
// Assembly location: C:\Users\Hesa\Desktop\eZ_Source\bin\Debug\rtmp-sharp.dll

using System;
using System.Collections.Generic;
using System.Linq;

namespace RtmpSharp.IO.ObjectWrappers
{
  internal class AsObjectWrapper : IObjectWrapper
  {
    private static readonly ClassDescription EmptyClassDescription = (ClassDescription) new AsObjectWrapper.AsObjectClassDescription(string.Empty, new IMemberWrapper[0], false, true);
    private readonly SerializationContext _serializationContext;

    public AsObjectWrapper(SerializationContext serializationContext)
    {
      this._serializationContext = serializationContext;
    }

    public bool GetIsDynamic(object instance)
    {
      return ((AsObject) instance).IsTyped;
    }

    public bool GetIsExternalizable(object instance)
    {
      return false;
    }

    public ClassDescription GetClassDescription(object obj)
    {
      AsObject source = (AsObject) obj;
      if (!source.IsTyped)
        return AsObjectWrapper.EmptyClassDescription;
      IMemberWrapper[] array = source.Select<KeyValuePair<string, object>, IMemberWrapper>((Func<KeyValuePair<string, object>, IMemberWrapper>) (x => (IMemberWrapper) new AsObjectWrapper.AsObjectMemberWrapper(x.Key))).ToArray<IMemberWrapper>();
      return (ClassDescription) new AsObjectWrapper.AsObjectClassDescription(this._serializationContext.GetAlias(source.TypeName), array, false, false);
    }

    private class AsObjectClassDescription : ClassDescription
    {
      internal AsObjectClassDescription(string name, IMemberWrapper[] members, bool externalizable, bool dynamic)
        : base(name, members, externalizable, dynamic)
      {
      }

      public override bool TryGetMember(string name, out IMemberWrapper memberWrapper)
      {
        memberWrapper = (IMemberWrapper) new AsObjectWrapper.AsObjectMemberWrapper(name);
        return true;
      }
    }

    private class AsObjectMemberWrapper : IMemberWrapper
    {
      public string Name { get; }

      public string SerializedName
      {
        get
        {
          return this.Name;
        }
      }

      public AsObjectMemberWrapper(string name)
      {
        this.Name = name;
      }

      public object GetValue(object instance)
      {
        return ((AsObject) instance)[this.Name];
      }

      public void SetValue(object instance, object value)
      {
        ((AsObject) instance)[this.Name] = value;
      }
    }
  }
}
