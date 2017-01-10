// Decompiled with JetBrains decompiler
// Type: RtmpSharp.IO.Reflection
// Assembly: rtmp-sharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8588136F-A4B9-4004-9712-4EA13AA4AF9D
// Assembly location: C:\Users\Hesa\Desktop\eZ_Source\bin\Debug\rtmp-sharp.dll

using System;

namespace RtmpSharp.IO
{
  internal static class Reflection
  {
    public static bool IsNullable(this Type type)
    {
      if (type.IsGenericType)
        return type.GetGenericTypeDefinition() == typeof (Nullable<>);
      return false;
    }

    public static bool IsConvertible(this Type type)
    {
      return typeof (IConvertible).IsAssignableFrom(type);
    }
  }
}
