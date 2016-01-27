using IntXLib;
using System;
using System.Collections;
using System.Collections.Generic;


namespace RSA
{
    public static class MathAlgs
    {
        public static bool PrimarilyTestBruteForce(IntX number)
        {
            if (number <= 2)
                return false;
            IntX temp = 3;
            if (number % 2 == 0)
                return false;
            while (temp * temp <= number)
            {
                if (number % temp == 0)
                {
                    return false;
                }
                else
                    temp += 2;
            }

            return true;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="lst"></param>
        /// <param name="elem"></param>
        /// <returns>false if the one of the elements is divisible by the elements existing in array; else true</returns>
        public static bool ListSearch(List<IntX> lst, IntX elem)
        {
            foreach (var _elemLst in lst)
            {
                if (elem % _elemLst == 0)
                    return false;
            }
            return true;
        }

        public static bool PrimarilyTestBruteForceIncreased(IntX number)
        {
            if (number <= 2)
                return false;
            List<IntX> _tempList = new List<IntX>();
            IntX temp = new IntX(3);
            if (number % 2 == 0)
                return false;
            while (temp * temp <= number)
            {
                if (number % temp == 0)
                    return false;
                else
                {
                    _tempList.Add(temp);
                    do
                    {
                        temp += 2;
                    }
                    while (!ListSearch(_tempList, temp));
                }
            }
            return true;
        }

        public static IntX GeneratePrime(IntX maximum)
        {
            return MathAlgs.GeneratePrime(maximum.ToString(2).Length - 1);
        }

        public static bool IsPrime(IntX number)
        {
            /*check the size and choose the appropriate algorithm*/
            return PrimarilyTestBruteForceIncreased(number);
        }

        public static IntX GeneratePrime(int bitsize)
        {
            Random rnd = new Random(Guid.NewGuid().GetHashCode());
            int sizeCurr = sizeof(Int32);
            IntX temp = new IntX(rnd.Next(bitsize <= 32 ? ((int)Math.Pow(2, bitsize)) : Int32.MaxValue));
            if (!temp.IsOdd)
                temp += 1;
            while (sizeCurr <= bitsize / 8)
            {
                temp = (temp << 32) + rnd.Next(Int32.MaxValue);
                sizeCurr += sizeof(Int32);
            }

            while (!IsPrime(temp))
            {
                temp += 2;
            }
            //what's better to regenerate prime if it's appropiate or just like add 2?
            return temp;
        }

        public static IntX GCD(IntX a, IntX b)
        {
            IntX _a = a;
            IntX _b = b;

            if (_a > _b)
            {
                IntX temp = _a;
                _a = _b;
                _b = temp;
            }

            while (_b != 0)
            {
                IntX temp = _a % _b;
                _a = _b;
                _b = temp;
            }

            return _a;
        }

        public static IntX GenerateCoprime(IntX mod, int bitsize)
        {
            Random rnd = new Random(Guid.NewGuid().GetHashCode());
            int sizeCurr = sizeof(Int32);
            IntX _temp = new IntX(rnd.Next(bitsize <= 32 ? ((int)Math.Pow(2, bitsize)) : Int32.MaxValue));
            if (!_temp.IsOdd)
                _temp += 1;
            while (sizeCurr <= bitsize / 8)
            {
                _temp = (_temp << 32) + rnd.Next(Int32.MaxValue);
                sizeCurr += sizeof(Int32);
                System.Console.WriteLine(sizeCurr);
            }

            //  IntX _temp = MathAlgs.GeneratePrime(bitsize);
            IntX.Modulo(_temp, mod, DivideMode.Classic);

            while (MathAlgs.GCD(_temp, mod) != 1)
                _temp += 2;

            return _temp;
        }

        public static IntX GenerateInverse(IntX number, IntX mod)
        {

            IntX new_ = 1, old = 0, q = mod, r, h, a = number;
            bool pos = false;
            while (a > 0)
            {
                r = q % a;
                q = q / a;
                h = q * new_ + old;
                old = new_;
                new_ = h;
                q = a;
                a = r;
                pos = !pos;
            }
            return pos ? old : (mod - old);
        }

        public static IntX Multiplication(IntX number, IntX pow, IntX mod)
        {
            IntX temp = number;
            IntX _pow = pow-1;
            while (_pow != 0)
            {
                temp = IntX.Multiply(temp, number, MultiplyMode.Classic);
                temp =  IntX.Modulo(temp, mod, DivideMode.Classic);
                _pow-=1;
            }
            return temp;
        }

        public static IntX MultiplicationSq(IntX number, IntX pow, IntX mod)
        {
            if (pow == 0)
                return 1; 
            BitArray br = GenerateBitArray(pow);
            IntX s = number;
            for (int i = 1; i <= br.Length-1; i++)
            {
                s = IntX.Multiply(s, s, MultiplyMode.Classic);
                s = IntX.Modulo(s, mod, DivideMode.Classic);
                if (br[i])
                {
                    s = IntX.Multiply(s, number, MultiplyMode.Classic);
                    s = IntX.Modulo(s, mod, DivideMode.Classic);
                }
            }
            return s;
        }

        public static BitArray GenerateBitArray(IntX number)
        {
            string numberBinary = number.ToString(2);
            BitArray br = new BitArray(numberBinary.Length);
            for (int i = 0; i < br.Length; i++)
            {
                if (numberBinary[i] == '1')
                    br[i] = true;
            }
            return br;
        }

        public static int FindClosestPow(IntX number)
        {
            if (number == 1)
                return 0;                
            IntX _temp = 2;
            int power = 1;
            while (_temp <= number)
            {
                _temp = IntX.Multiply(_temp, 2, MultiplyMode.Classic);
                power++;
            }

            return power; 
        }

        public static void Test()
        {
            // System.Console.WriteLine(MathAlgs.GCD(new IntX("284612834525939"), new IntX("161529830970143030971931")));
            //System.Console.WriteLine(MathAlgs.GenerateCoprime(1257957, 13));
            System.Console.WriteLine(MathAlgs.GenerateInverse(447, 235716571963));
            //System.Console.WriteLine(FindClosestPow(1025));
            //System.Console.WriteLine(GenerateBitArray(1024));


            //for (int i = 0; i<256; i++)
            //    System.Console.WriteLine("2^{0} = {1}",i,MathAlgs.MultiplicationSq(2, i, 23232));
            //System.Console.Read();
        }
    }
}
