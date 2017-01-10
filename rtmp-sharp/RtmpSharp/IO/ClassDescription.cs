// Decompiled with JetBrains decompiler
// Type: RtmpSharp.IO.ClassDescription
// Assembly: rtmp-sharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8588136F-A4B9-4004-9712-4EA13AA4AF9D
// Assembly location: C:\Users\Hesa\Desktop\eZ_Source\bin\Debug\rtmp-sharp.dll

using System;

namespace RtmpSharp.IO
{
  internal class ClassDescription
  {
    public string Name { get; }

    public IMemberWrapper[] Members { get; private set; }

    public bool IsExternalizable { get; private set; }

    public bool IsDynamic { get; private set; }

    public bool IsTyped
    {
      get
      {
        return !string.IsNullOrEmpty(this.Name);
      }
    }

    internal ClassDescription(string name, IMemberWrapper[] members, bool externalizable, bool dynamic)
    {
      this.Name = name;
      this.Members = members;
      this.IsExternalizable = externalizable;
      this.IsDynamic = dynamic;
    }

    public virtual bool TryGetMember(string name, out IMemberWrapper memberWrapper)
    {
      throw new NotImplementedException();
    }
  }
}
