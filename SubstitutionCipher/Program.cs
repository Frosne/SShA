using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubstitutionCipher
{
    class Program
    {
        static void Main(string[] args)
        {
            SubstitutionCipher sc = new SubstitutionCipher('a', 'z');
            sc.GenerateTable();
            var message =  sc.CipherMessage("hello");
            System.Console.WriteLine(message);
            System.Console.WriteLine(sc.DecipherMessage(message));
            sc.GenerateTable();
            System.Console.WriteLine(sc.DecipherMessage(message));
            System.Console.ReadLine();
        }
    }
}
