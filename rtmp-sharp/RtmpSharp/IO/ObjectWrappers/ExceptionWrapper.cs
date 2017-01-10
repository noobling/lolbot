// Decompiled with JetBrains decompiler
// Type: RtmpSharp.IO.ObjectWrappers.ExceptionWrapper
// Assembly: rtmp-sharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8588136F-A4B9-4004-9712-4EA13AA4AF9D
// Assembly location: C:\Users\Hesa\Desktop\eZ_Source\bin\Debug\rtmp-sharp.dll

using System;
using System.Collections.Generic;
using System.Linq;

namespace RtmpSharp.IO.ObjectWrappers
{
  internal class ExceptionWrapper : BasicObjectWrapper
  {
    private static readonly HashSet<string> ExcludedMembers = new HashSet<string>() { "HelpLink", "HResult", "Source", "StackTrace", "TargetSite" };

    public ExceptionWrapper(SerializationContext serializationContext)
      : base(serializationContext)
    {
    }

    public override ClassDescription GetClassDescription(object obj)
    {
      ClassDescription classDescription = base.GetClassDescription(obj);
      return new ClassDescription(classDescription.Name, ((IEnumerable<IMemberWrapper>) classDescription.Members).Where<IMemberWrapper>((Func<IMemberWrapper, bool>) (x => !ExceptionWrapper.ExcludedMembers.Contains(x.Name))).ToArray<IMemberWrapper>(), classDescription.IsExternalizable, classDescription.IsDynamic);
    }
  }
}
