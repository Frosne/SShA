using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntXLib;

namespace TextParsing
{
  public static class TextParsing
    {
       public static List<IntX> StringToNumericArray(string word)
       {
           List<IntX> result = new List<IntX>(word.Length);
           foreach (var elem in word)
                result.Add(new IntX((int)elem));
           return result;
       }

       public static string NumericIntXArrayToString(List<IntX> word)
       {
           String result = String.Empty;
           foreach (var elem in word)
           {
               try
               {
                   char elemChar = (char)((int)elem);
                   result = result + elemChar;
               }
               catch (Exception exp)
               {
                   System.Console.WriteLine("Excepring");
               }
           }
           return result;
       }

       static void Main(string[] args)
       {
          var lst =  TextParsing.StringToNumericArray("hello world");
          var result = TextParsing.NumericIntXArrayToString(lst);
       }

    }
}
