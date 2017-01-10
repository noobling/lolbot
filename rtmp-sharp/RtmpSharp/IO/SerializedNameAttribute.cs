// Decompiled with JetBrains decompiler
// Type: RtmpSharp.IO.SerializedNameAttribute
// Assembly: rtmp-sharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8588136F-A4B9-4004-9712-4EA13AA4AF9D
// Assembly location: C:\Users\Hesa\Desktop\eZ_Source\bin\Debug\rtmp-sharp.dll

using System;

namespace RtmpSharp.IO
{
  [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Interface, AllowMultiple = true)]
  public sealed class SerializedNameAttribute : Attribute
  {
    public string SerializedName { get; set; }

    public bool Canonical { get; set; }

    public SerializedNameAttribute(string serializedName)
    {
      this.SerializedName = serializedName;
      this.Canonical = true;
    }
  }
}
