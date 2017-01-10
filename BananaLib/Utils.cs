// Decompiled with JetBrains decompiler
// Type: BananaLib.Utils
// Assembly: BananaLib, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 75213AF3-E339-4AEB-B3FE-095F85BC5F53
// Assembly location: C:\Users\Hesa\Desktop\eZ\BananaLib.dll

using System;
using System.IO;

namespace BananaLib
{
  public class Utils
  {
    public static string ByteArrToHexStr(byte[] byteArr)
    {
      char[] chArray = new char[byteArr.Length * 2];
      for (int index = 0; index < byteArr.Length; ++index)
      {
        int num1 = (int) byteArr[index] >> 4;
        chArray[index * 2] = (char) (87 + num1 + (num1 - 10 >> 31 & -39));
        int num2 = (int) byteArr[index] & 15;
        chArray[index * 2 + 1] = (char) (87 + num2 + (num2 - 10 >> 31 & -39));
      }
      return new string(chArray);
    }

    public static byte[] HexStrToByteArr(string str)
    {
      int length = str.Length / 2;
      byte[] numArray = new byte[length];
      using (StringReader stringReader = new StringReader(str))
      {
        for (int index = 0; index < length; ++index)
          numArray[index] = Convert.ToByte(new string(new char[2]
          {
            (char) stringReader.Read(),
            (char) stringReader.Read()
          }), 16);
      }
      return numArray;
    }
  }
}
