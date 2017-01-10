// Decompiled with JetBrains decompiler
// Type: RtmpSharp.IO.IObjectWrapper
// Assembly: rtmp-sharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8588136F-A4B9-4004-9712-4EA13AA4AF9D
// Assembly location: C:\Users\Hesa\Desktop\eZ_Source\bin\Debug\rtmp-sharp.dll

namespace RtmpSharp.IO
{
  internal interface IObjectWrapper
  {
    bool GetIsExternalizable(object instance);

    bool GetIsDynamic(object instance);

    ClassDescription GetClassDescription(object obj);
  }
}
