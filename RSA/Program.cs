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
            RSA rsa = new RSA("nothinghere");
            // rsa.GeneratePQ();
           /*long temp = 32452834762618213;
            Stopwatch stw = new Stopwatch();
            stw.Start();
            System.Console.WriteLine(temp + ((MathAlgs.PrimarilyTestBruteForce(new IntX(temp))) == true ? "is prime" : "isnot prime"));
            stw.Stop();
            System.Console.WriteLine(stw.ElapsedTicks);

            stw.Restart();
            System.Console.WriteLine(temp + ((MathAlgs.PrimarilyTestBruteForceIncreased(new IntX(temp))) == true ? "is prime" : "isnot prime"));
            stw.Stop();
            System.Console.WriteLine(stw.ElapsedTicks);
            */
            System.Console.WriteLine(MathAlgs.GCD(new IntX("284612834525939"), new IntX("161529830970143030971931")));
            System.Console.Read();
        }
    }

    class RSA
    {
        //how crucial it will be to leave it public ?
        private int bitsize;
        public int securityLevel;
        private IntX p;
        private IntX q;
        private IntX Phi;
        public IntX e;

        public IntX n;

        private string PMessage;
        public string CMessage;

        public RSA(string message)
            : this(message, 2048)
        { }

        public RSA(string message, int bitsize)
            : this(message, 2048, 0)
        { }

        public RSA(string message, int bitsize, int securityLevel)
        {
            this.PMessage = message;
            this.bitsize = bitsize;
            this.securityLevel = securityLevel;
        }

        public void GeneratePQ()
        {
            p = GeneratePrime();
            q = GeneratePrime();
        }

        /*Secured?*/
        public IntX GeneratePrime(IntX maximum)
        {
            return this.GeneratePrime(maximum.ToString(2).Length - 1);
        }

        public IntX GeneratePrime(int bitsize = 0)
        {
            Random rnd = new Random(Guid.NewGuid().GetHashCode());
            int sizeCurr = sizeof(Int32);
            IntX temp = new IntX(rnd.Next(Int32.MaxValue));
            int _bitsize = bitsize == 0 ? this.bitsize : bitsize;
            while (sizeCurr <= _bitsize / 8)
            {
                temp = (temp << 32) + rnd.Next(Int32.MaxValue);
                sizeCurr += sizeof(Int32);
                System.Console.WriteLine(sizeCurr);
            }

            while (!ifNumberPrime(temp))
            {
                temp += 2;
            }
            //what's better to regenerate prime if it's appropiate or just like add 2?
            return temp;
        }

        //move all math to different class?

        /**/
        public IntX GenerateCoprime(IntX mod)
        {
            /*  Random rnd = new Random(Guid.NewGuid().GetHashCode());
              int sizeCurr = sizeof(Int32);
              IntX temp = new IntX(rnd.Next(Int32.MaxValue));
              while (sizeCurr <= _bitsize / 8)
              {
                  temp = (temp << 32) + rnd.Next(Int32.MaxValue);
                  sizeCurr += sizeof(Int32);
                  System.Console.WriteLine(sizeCurr);
              }
              */
            return null;
        }

      

        /// <summary>
        /// Takes as input securitylevel and generated number and checks if it's appropriate 
        /// according to security level
        /// the idea is to distinguish strong primes/rsa primes
        /// + the difference in generation
        /// and write documentation
        /// </summary>
        /// <param name="temp"></param>
        /// <returns></returns>
        private bool ifNumberPrime(IntX temp)
        {
            throw new NotImplementedException();
        }

        private void GenerateN()
        {
            this.n = this.p * this.q;
        }

        private void GeneratePhi()
        {
            this.Phi = (this.p - 1) * (this.q - 1);
        }

        private void ComputeE()
        {

        }

        public void CipherMessage(string message = null)
        {

        }

        public void DecipherMessage(string message = null)
        {

        }

    }

    
}