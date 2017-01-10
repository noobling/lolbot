// Decompiled with JetBrains decompiler
// Type: RtmpSharp.IO.AMF3.ArrayCollection
// Assembly: rtmp-sharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8588136F-A4B9-4004-9712-4EA13AA4AF9D
// Assembly location: C:\Users\Hesa\Desktop\eZ_Source\bin\Debug\rtmp-sharp.dll

using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace RtmpSharp.IO.AMF3
{
  [TypeConverter(typeof (ArrayCollectionConverter))]
  [SerializedName("flex.messaging.io.ArrayCollection")]
  [Serializable]
  public class ArrayCollection : List<object>, IExternalizable
  {
    public void ReadExternal(IDataInput input)
    {
      object[] objArray = input.ReadObject() as object[];
      if (objArray == null)
        return;
      this.AddRange((IEnumerable<object>) objArray);
    }

    public void WriteExternal(IDataOutput output)
    {
      output.WriteObject((object) this.ToArray());
    }
  }
}
