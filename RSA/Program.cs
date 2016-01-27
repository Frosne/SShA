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
            RSA rsa = new RSA("34", 20);
            rsa.CipherFullGeneration();

            rsa.CRTAlgorithmPrecomputing(rsa.d, rsa.p, rsa.q);
            
           
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

        public void CipherFullGeneration(string message = null)
        {
            this.CDMessage = IntX.Parse(this.CMessage);
            this.GeneratePQ();
            this.GenerateN();
            this.GeneratePhi();
            this.GenerateE();
            this.CipherMessage(message);
        }

        public void CipherMessage(string message = null)
        {
           this.CDMessage =  IntX.Parse(this.CMessage);
          // this.DDMessage= MathAlgs.Multiplication(this.CDMessage, this.e, this.n);
           this.DDMessage = MathAlgs.MultiplicationSq(this.CDMessage, this.e, this.n);
        }


        public void CipherMessageDebug(int mode)
        {
            this.CDMessage = IntX.Parse(this.CMessage);
            if (mode == 1)
                this.DDMessage= MathAlgs.Multiplication(this.CDMessage, this.e, this.n);
            else if (mode == 2)
                 this.DDMessage = MathAlgs.MultiplicationSq(this.CDMessage, this.e, this.n);
        }

        public void DecipherMessage(string message = null)
        {
           this.DDMessage =  this.DDMessage != null ? this.DDMessage : IntX.Parse(this.DMessage);
           this.Result = MathAlgs.MultiplicationSq(this.DDMessage, this.d, this.n);
        }

        public void DecipherMessageDebug(int mode)
        {
            this.DDMessage = this.DDMessage != null ? this.DDMessage : IntX.Parse(this.DMessage);
            if (mode == 1)
                this.Result = MathAlgs.Multiplication(this.DDMessage, this.d, this.n);
            else if (mode == 2)
                this.Result = MathAlgs.MultiplicationSq(this.DDMessage, this.d, this.n);

        }


        internal void GetCMessage(string message)
        {
            this.CMessage = message;
        }

        internal void GetDMessage(string message)
        {
            this.DMessage = message;
        }

        public void CRTAlgorithm(IntX Ciphered, IntX d, IntX N, IntX p, IntX q)
        {
            this.CRTAlgorithmPrecomputing(this.d, this.p, this.q);
        }

        public List<IntX> CRTAlgorithmPrecomputing(IntX d, IntX p, IntX q)
        {
            IntX dp = IntX.Modulo(d, new IntX(p - 1), DivideMode.Classic);
            IntX dq = IntX.Modulo(d, new IntX(q - 1), DivideMode.Classic);
            IntX a = 1;
            IntX b = 1;

            while (!((a % q) == 0))            
                a += p;   
            
            while (!(b % p == 0))
                b += q;
           

            List<IntX> lst = new List<IntX>();
            lst.Add(dp);
            lst.Add(dq);
            lst.Add(a);
            lst.Add(b);

            return lst;

        }
    }

    
}