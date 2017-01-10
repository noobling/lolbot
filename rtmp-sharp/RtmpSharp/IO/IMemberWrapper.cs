// Decompiled with JetBrains decompiler
// Type: RtmpSharp.IO.IMemberWrapper
// Assembly: rtmp-sharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8588136F-A4B9-4004-9712-4EA13AA4AF9D
// Assembly location: C:\Users\Hesa\Desktop\eZ_Source\bin\Debug\rtmp-sharp.dll

namespace RtmpSharp.IO
{
  internal interface IMemberWrapper
  {
    string Name { get; }

    string SerializedName { get; }

    object GetValue(object instance);

    void SetValue(object instance, object value);
  }
}
