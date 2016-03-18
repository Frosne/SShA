using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntXLib;
using System.Xml;
using System.Diagnostics;

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

       public static void XMLResponse(Process[] toWrite, Type type)
       {
           XmlWriterSettings settings = new XmlWriterSettings();
           
           settings.Async = true;
           settings.CheckCharacters = false;
           string path = @"D:/Alt/Cat/xml";
           System.IO.DirectoryInfo dr= new System.IO.DirectoryInfo(path.Remove(path.LastIndexOf('/')));
           if (!dr.Exists)
               dr.Create();
            
           XmlWriter xml = XmlWriter.Create(path, settings);
           xml.WriteStartDocument();
           foreach (var elem in toWrite)
           {
               xml.WriteValue(elem.
           }
           xml.WriteEndDocument();
           xml.Close();
           
       }

       static void Main(string[] args)
       {
           LoggingExtensions.Logging.Log.InitializeWith<LoggingExtensions.log4net.Log4NetLog>();

       }

    }
}
