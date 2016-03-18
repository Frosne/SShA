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
            return MathAlgs.MultiplicationSq(number, pow, mod);
        }

        public static IntX MultiplicationBruteForce(IntX number, IntX pow, IntX mod)
        {
            IntX temp = number;
            IntX _pow = pow - 1;
            while (_pow != 0)
            {
                temp = IntX.Multiply(temp, number, MultiplyMode.Classic);
                temp = IntX.Modulo(temp, mod, DivideMode.Classic);
                _pow -= 1;
            }
            return temp;
        }

        public static IntX MultiplicationSq(IntX number, IntX pow, IntX mod)
        {
            if (pow == 0)
                return 1;
            BitArray br = GenerateBitArray(pow);
            IntX s = number;
            for (int i = 1; i <= br.Length - 1; i++)
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

        public static IntX Modulo(IntX number, IntX mod)
        {
            IntX div = number / mod;
            return number - mod * div;
        }

        public static void Test()
        {
            MontgomeryForm(99934, 23423497, 1938491);
        }

        public static Tuple<IntX, IntX> MontgomeryForm(IntX a, IntX b, IntX mod)
        {
            IntX R = 1;
            int log = FindClosestPow(mod)-1;
            do
            {
                log += 1;
                R = IntX.Pow(new IntX(2), (uint)(log));

            } while (GCD(R, mod) != 1);

            return ExtendedEuclidianAlgorithm(R, mod);

        }

        public static Tuple<IntX, IntX> ExtendedEuclidianAlgorithm(IntX A, IntX B)
        {
            IntX[] result = new IntX[3];
            if (A < B) //if A less than B, switch them
            {
                IntX temp = A;
                A = B;
                B = temp;
            }
            IntX r = B;
            IntX q = 0;
            IntX x0 = 1;
            IntX y0 = 0;
            IntX x1 = 0;
            IntX y1 = 1;
            IntX x = 0, y = 0;
            while (r > 1)
            {
                r = A % B;
                q = A / B;
                x = x0 - q * x1;
                y = y0 - q * y1;
                x0 = x1;
                y0 = y1;
                x1 = x;
                y1 = y;
                A = B;
                B = r;
            }
           /* result[0] = r;
            result[1] = x;
            result[2] = y;-*/
            return new Tuple<IntX, IntX>(x, y);
        }

        public static IntX CRTAlgorithm(IntX Ciphered, IntX d, IntX n, IntX p, IntX q)
        {
            List<IntX> lst = MathAlgs.CRTAlgorithmPrecomputing(d, p, q);
            IntX dp = 1, dq = 1, a = 1, b = 1;
            try
            {
                dp = lst[0];
                dq = lst[1];
                a = lst[2];
                b = lst[3];
            }
            catch (Exception exp)
            {
                System.Console.WriteLine("Some problems during generation pre parameters");
            }

            IntX Cp = IntX.Modulo(Ciphered, p, DivideMode.Classic);
            IntX Cq = IntX.Modulo(Ciphered, q, DivideMode.Classic);

            IntX Xp = MathAlgs.Multiplication(Cp, dp, p);
            IntX Xq = MathAlgs.Multiplication(Cq, dq, q);

            IntX result = IntX.Modulo((a * Xp + b * Xq), n, DivideMode.Classic);
            return result;
        }

        /// <summary>
        /// Compute dp, dq, a and b for CRT algorithm
        /// </summary>
        /// <param name="d">private key</param>
        /// <param name="p">prime p</param>
        /// <param name="q">prime q</param>
        /// <returns>List<IntX> where the first element in list is dp, the second element is dq, followed by a and b</returns>
        public static List<IntX> CRTAlgorithmPrecomputing(IntX d, IntX p, IntX q)
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
