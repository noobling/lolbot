// Decompiled with JetBrains decompiler
// Type: RtmpSharp.IO.AMF3.ObjectProxy
// Assembly: rtmp-sharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8588136F-A4B9-4004-9712-4EA13AA4AF9D
// Assembly location: C:\Users\Hesa\Desktop\eZ_Source\bin\Debug\rtmp-sharp.dll

using System;
using System.Collections.Generic;

namespace RtmpSharp.IO.AMF3
{
  [SerializedName("flex.messaging.io.ObjectProxy")]
  [Serializable]
  internal class ObjectProxy : Dictionary<string, object>, IExternalizable
  {
    public void ReadExternal(IDataInput input)
    {
      IDictionary<string, object> dictionary = input.ReadObject() as IDictionary<string, object>;
      if (dictionary == null)
        return;
      foreach (KeyValuePair<string, object> keyValuePair in (IEnumerable<KeyValuePair<string, object>>) dictionary)
        this[keyValuePair.Key] = keyValuePair.Value;
    }

    public void WriteExternal(IDataOutput output)
    {
      output.WriteObject((object) new AsObject((Dictionary<string, object>) this));
    }
  }
}
