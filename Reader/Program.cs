using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reader
{
    class Program
    {
        static void Main(string[] args)
        {
            var chars =  Reader.ReturnASCIIFromFileCTF(@"D:\Работать\CTF\write-ups-2016-master\write-ups-2016-master\breakin-ctf-2016\steganography\you-cant-see-me-100\color.bin");
            System.Console.Write(chars);
            System.Console.Read();
        }
    }

   static class Reader
    {
       public static string[] ReturnBinaryStringAFromFile(string path)
       {
           var binary = ReturnBinaryByteFromFile(path);
           string[] arr = new string[binary.Length];
           for (int i = 0; i < arr.Length; i++)
           {
               arr[i] = Convert.ToString(binary[i], 2);
           }
           return arr;

       }
       public static byte[] ReturnBinaryByteFromFile(string path)
       {
           var stream = System.IO.File.OpenRead(path);
           System.IO.BinaryReader br = new System.IO.BinaryReader(stream);
           byte[] binary = new byte[br.BaseStream.Length];
           for (int i = 0; i < binary.Length; i++)
           {
               binary[i] = br.ReadByte();
           }
           return binary;

       }
       public static string ReturnASCIIFromFileCTF(string path)
       {
           String str = String.Empty;
           var byteArr = ReturnBinaryStringAFromFile(path);
           for (int i = 0; i < byteArr.Length; i++)
           {
               if (byteArr[i].Length == 7)
                     str = byteArr[i] + str;
           }
          var length =  str.Length;
           /*
           for (int i = 0; i < charArr.Length; i++)
           {
               byte b=0;
               int value = (int)Math.Pow(2,i%7);
               b +=charArr[i%7] == true ? Convert.ToByte(value):(byte)0;

               ASCIIbyte[((int)i / 7)] = b;
           }
           */
           return null;
       }

       public static String ASCII(byte[] arrayBinary)
       {
           char[] arr = new char[arrayBinary.Length];
           Decoder dr = Encoding.ASCII.GetDecoder();
           dr.GetChars(arrayBinary, 0, arrayBinary.Length, arr, 0);
           return new String(arr);
       }
    }
}
