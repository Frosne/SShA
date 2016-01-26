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
            MathAlgs.Test();

        }
    }

    

    class RSA
    {
        //how crucial it will be to leave it public ?
        public int bitsize;
        public int securityLevel;
        public IntX p;
        public IntX q;
        public IntX Phi;
        public IntX e;
        public IntX d;


        public IntX n;

        public string CMessage;
        public IntX CDMessage;
        public string DMessage;
        public IntX DDMessage;
        public IntX Result;


        public RSA(string message)
            : this(message, 2048)
        { }

        public RSA(string message, int bitsize)
            : this(message, bitsize, 0)
        { }

        public RSA(string message, int bitsize, int securityLevel)
        {
            this.CMessage = message;
            this.bitsize = bitsize;
            this.securityLevel = securityLevel;
        }

        

        public void GeneratePQ()
        {
            p = MathAlgs.GeneratePrime(this.bitsize);
            q = MathAlgs.GeneratePrime(this.bitsize);

        }


        public void GenerateN()
        {
            this.n = this.p * this.q;
        }

        public void GeneratePhi()
        {
            this.Phi = (this.p - 1) * (this.q - 1);
        }

        public void GenerateE()
        {
            this.e = MathAlgs.GenerateCoprime(this.Phi,this.bitsize/2);
            this.d = MathAlgs.GenerateInverse(this.e, this.Phi);

        }

        public void CipherMessage(string message = null)
        {
          

           this.CDMessage =  IntX.Parse(this.CMessage);
           this.DDMessage= MathAlgs.Multiplication(this.CDMessage, this.e, this.n);

        }

        public void DecipherMessage(string message = null)
        {
           this.DDMessage =  this.DDMessage != null ? this.DDMessage : IntX.Parse(this.DMessage);
            this.Result = MathAlgs.Multiplication(this.DDMessage, this.d, this.n);
        }


        internal void GetCMessage(string message)
        {
            this.CMessage = message;
        }

        internal void GetDMessage(string message)
        {
            this.DMessage = message;
        }
    }

    
}