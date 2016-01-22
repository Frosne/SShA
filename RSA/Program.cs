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
           /* RSA rsa = new RSA("nothinghere",20);
            rsa.GeneratePQ();
            rsa.GenerateN();
            rsa.GeneratePhi();
            rsa.GenerateE();
            System.Console.WriteLine(rsa.p + "\n" + rsa.q + "\n" + rsa.n + "\n" +rsa.Phi + "\n" + rsa.e);
            * */

            MathAlgs.Test();
            System.Console.Read();
            

        }
    }

    class RSA
    {
        //how crucial it will be to leave it public ?
        private int bitsize;
        public int securityLevel;
        public IntX p;
        public IntX q;
        public IntX Phi;
        public IntX e;

        public IntX n;

        private string PMessage;
        public string CMessage;

        public RSA(string message)
            : this(message, 2048)
        { }

        public RSA(string message, int bitsize)
            : this(message, bitsize, 0)
        { }

        public RSA(string message, int bitsize, int securityLevel)
        {
            this.PMessage = message;
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

        }

        public void CipherMessage(string message = null)
        {
            int test = 34;
        }

        public void DecipherMessage(string message = null)
        {

        }

    }

    
}