using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace CRC
{
    class Program
    {
        static void Main(string[] args)
        {
            string text ="11010011101100";
            string key = "1011";
            string tr;
            CRC crc = new CRC();
            crc.CRCAlg(text, key, out tr);
            System.Console.WriteLine(tr);
            CRCBruteForce br = new CRCBruteForce("100", key, 14);
            br.Search("100", key);

            var test = br.found[0];
            string res;
            crc.CRCAlg(ref test, CRC.ToBitArray(key), out tr);


            System.Console.Read();
        }
    }

    class CRC
    {
        public BitArray ExtendedText;
        public BitArray Key;
        public BitArray KeyExtended;

        public CRC(string Text, string Key)
        {
           ExtendToSizeLeft(ref this.ExtendedText, ToBitArray(Text), Text.Length + Key.Length - 1);
           this.Key = ToBitArray(Key);
           this.KeyExtended = new BitArray(Text.Length + Key.Length - 1);
        }

        public CRC()
        {
           
        }

        
        public void ExtendToSizeRight(ref BitArray to, BitArray bitArray, int p)
        {
            if (to == null)
                to = new BitArray(p, false);
            for (int i = 0; i < bitArray.Length; i++)
                to[i] = bitArray[i];
        }

        public BitArray ExtendToSizeLeft(ref BitArray to, BitArray bitArray, int p, int counter = 0)
        {
           // if (to == null)
                to = new BitArray(p, false);
            for (int i = 0; i < bitArray.Length; i++)
            {
                to[to.Length - bitArray.Length + i-counter] = bitArray[i];
            }
            return to;
        }

        public static BitArray ToBitArray(string Text)
        {
            BitArray br = new BitArray(Text.Length);
            for (int i = 0; i < Text.Length; i++)
            {
                try
                {
                    br[i] = Text[Text.Length-1-i] == '0' ? false : true;
                }
                catch (Exception ex)
                {
                    System.Console.WriteLine(ex.Data);
                }
            }
            return br;
        }

        public static void PrintBitArray(BitArray br)
        {
            for (int i = br.Length - 1; i >= 0; i--)
                System.Console.Write(br[i] ==  true ?  "1" : "0" );
            System.Console.WriteLine();
        }

        public void GenerateOneRound(ref BitArray to, BitArray key, int counter)
        {
            this.ExtendToSizeLeft(ref this.KeyExtended, key, to.Length, counter);
            to = to.Xor(this.KeyExtended);


        }

        public BitArray CRCAlg(ref BitArray to, BitArray key , out string Truncated)
        {
            this.Key = key;
            this.ExtendedText = to;
            Truncated = String.Empty;
            int size = to.Length;
            PrintBitArray(to);
            PrintBitArray(key);
            for (int i = to.Length-1; i > key.Length; i--)
            {
                if (to[i] == true)
                {
                    GenerateOneRound(ref to, key, to.Length - i - 1);
                   // PrintBitArray(to);
                }
            }

            char[] temp = new char[this.Key.Length - 1];
            for (int i = 0; i < temp.Length; i++)
                temp[temp.Length - 1 - i] = to[i] == true ? '1' : '0';
            Truncated = new String(temp);

            return to;
        }

        public BitArray CRCAlg(out string Truncated)
        { 
            Truncated = String.Empty;
            try
            {
               
                return this.CRCAlg(ref this.ExtendedText, this.Key, out Truncated);
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.Data);
            }
            return null;
        }
        public BitArray CRCAlg(string Text, string Key, out string Truncated)
        {
            Truncated = String.Empty;
            ExtendToSizeLeft(ref this.ExtendedText, ToBitArray(Text), Text.Length + Key.Length - 1);
            this.Key = ToBitArray(Key);
            this.KeyExtended = new BitArray(Text.Length + Key.Length - 1);
            var result = this.CRCAlg(ref this.ExtendedText, this.Key, out Truncated);

            char[] temp = new char[this.Key.Length - 1];
            for (int i = 0; i < temp.Length; i++)
                temp[temp.Length - 1 - i] = result[i] == true ? '1' : '0';
            Truncated = new String(temp);
            return  result;
        }

    }
}
