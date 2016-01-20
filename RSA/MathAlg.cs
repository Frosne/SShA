using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntXLib;


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
    }
}
