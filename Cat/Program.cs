using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextParsing;

namespace Cat
{
    class Program
    {
        static void Main(string[] args)
        {
            Cat cat = new Cat();
            cat.GetAllProcesses();
        }
    }

    class Cat
    {
        public void GetAllProcesses()
        {
            Process[] procList = Process.GetProcesses();
            foreach (var elem in procList)
                System.Console.WriteLine(elem);
            if (procList.Length > 0)
            {
                Type tp = procList[0].GetType();
                TextParsing.TextParsing.XMLResponse(procList);
            }
            

        }
       

    }
}
