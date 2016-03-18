using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntXLib;
using System.Timers;
using System.Diagnostics;


namespace RSA
{
    class Program
    {
        static void Main(string[] args)
        {
            RSA rsa = new RSA("10", 10, 0);
            rsa.CipherFullCycle();           
           
        }
    }

    

 

    
}