using System;
using IntXLib;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSA
{
    class RSA
    {
        private int bitsize;
        private int securityLevel;
        private IntX p;
        private IntX q;
        private IntX phi;
        public IntX e;
        private IntX d;
        public IntX n;

        public string MessageToCipherString;
        public IntX MessageToCipherNumeric;
        public string MessageToDecipherString;
        public IntX MessageToDecipherNumeric;
        public IntX MessageDeciphered;
        public IntX MessageCiphered;



        public RSA() : this(null) { }
        /// <summary>
        /// Default constructor with bitkey equals to 2048
        /// </summary>
        /// <param name="message">Message to cipher</param>
        public RSA(string message)
            : this(message, 2048)
        { }

        public RSA(string message, int bitsize)
            : this(message, bitsize, 0)
        { }

        public RSA(string message, int bitsize, int securityLevel)
        {
            this.MessageToCipherString = message;
            this.bitsize = bitsize;
            this.securityLevel = securityLevel;
        }

        //Generation prime numbers p and q
        private void GeneratePQ()
        {
            p = MathAlgs.GeneratePrime(this.bitsize);
            q = MathAlgs.GeneratePrime(this.bitsize);
        }

        private void GenerateN()
        {
            if (this.p != null && this.q != null)
                this.n = this.p * this.q;
        }

        private void GeneratePhi()
        {
            if (this.p != null && this.q != null)
                this.phi = (this.p - 1) * (this.q - 1);
        }

        private void GenerateE()
        {
            if (this.phi != null)
            {
                this.e = MathAlgs.GenerateCoprime(this.phi, this.bitsize / 2);
                this.d = MathAlgs.GenerateInverse(this.e, this.phi);
            }
        }

        public void CipherFullCycle()
        {
            this.GeneratePQ();
            this.GenerateN();
            this.GeneratePhi();
            this.GenerateE();
            this.CipherMessage();
        }

        public IntX CipherMessage()
        {
            if (this.MessageToCipherNumeric != null)
                this.MessageCiphered = MathAlgs.MultiplicationSq(this.MessageToCipherNumeric, this.e, this.n);
            return this.MessageCiphered;
        }


        public void CipherMessageDebug()
        {
            CipherFullCycle();
            System.Console.WriteLine("Message to cipher string", this.MessageToCipherString);
            System.Console.WriteLine("Message to cipher", this.MessageToCipherNumeric);
            System.Console.WriteLine("p", this.p);
            System.Console.WriteLine("q", this.q);
            System.Console.WriteLine("n", this.n);
            System.Console.WriteLine("phi", this.phi);
            System.Console.WriteLine("e", this.e);
            System.Console.WriteLine("d", this.d);
            System.Console.WriteLine("Ciphered message", this.MessageCiphered);
        }

        public IntX DecipherMessage()
        {
            this.MessageDeciphered = MathAlgs.MultiplicationSq(this.MessageToDecipherNumeric, this.d, this.n);
            return this.MessageDeciphered;
        }

        public IntX DecipherMessageCRT()
        {
            return MathAlgs.CRTAlgorithm(this.MessageToDecipherNumeric, this.d, this.n, this.p, this.q);
        }

        public void ProvideNewMessageToCipherWithoutRenewal(string message)
        {
            this.MessageToCipherString = message;
        }

        public void ProvideNewMessageToDecipherWithoutRenewal(string message)
        {
            this.MessageToDecipherString = message;
        }


    }
}
