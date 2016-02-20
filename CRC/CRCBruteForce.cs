using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using IntXLib;
namespace CRC
{
    class CRCBruteForce
    {
        CRC module;
        BitArray toFind;
        BitArray key;
      public  List<BitArray> found;
        int size;
        CRC crc;

        public CRCBruteForce(string element, string key, int size)
        {
            this.size = size;
            crc = new CRC();
            found = new List<BitArray>();
            
        }

        public void Search(string toFind, string key)
        {
            List<BitArray> lst = GenerateListBitArrays();
            for (int i = 0; i<lst.Count; i++)
            {
                string tru;
                var temp = lst[i];
                crc.CRCAlg(ref temp, CRC.ToBitArray(key), out tru);
                if (tru == toFind)
                   this.found.Add(lst[i]);

            }
                    
        }

      

        public String GenerateBinaryString(IntX number)
        {
            Char[] str = new  Char[this.size];
            for (int i = 0; i < str.Length; i++)
                str[i] = '0';

            var temp = number.ToString(2);
            for (int i = 0; i < temp.Length; i++)
                str[i] = temp[i];
            return new String(str);
        }

        public BitArray GenerateBinaryBitArray(IntX number)
        {
           return CRC.ToBitArray(this.GenerateBinaryString(number));
        }

        public List<BitArray> GenerateListBitArrays()
        {
            List<BitArray> lst = new List<BitArray>();
            IntX max = IntX.Pow(2, (uint)this.size) - 1;
            for (IntX i = 0; i <= max; i++)
            {
                lst.Add(this.GenerateBinaryBitArray(i));
            }
            return lst;
        }
    }
}
