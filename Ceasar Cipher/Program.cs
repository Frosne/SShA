using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ceasar_Cipher
{
    class Program
    {
        static void Main(string[] args)
        {

            Ceasar c = new Ceasar("VJGTG CTG UGXGTCN JKFFGP OGUUCIGU KP VJKU KOCIG. VTA VQ HKPF VJGO CPF UGPF AQWT UQNWVKQP VQ IGQTI QT OCTM. JCXG HWP!");
            c.CeasarAlgorithm();
            System.Console.Read();
        }
    }

    class Ceasar
    {
        public String InputText;
        public List<String> PossibleText;
        public int LengthOfAlphabet = 26;

        public Ceasar(String text)
        {
            this.InputText = text;
            PossibleText = new List<string>();
        }

        public void CeasarAlgorithm()
        {
            char[] array = new char[InputText.Length];
            for (int i = 0; i < this.LengthOfAlphabet; i++)
            {                
                for (int j = 0; j < InputText.Length; j++)
                {
                    array[j] = (InputText[j] >='A' && InputText[j]<='Z' ? IntToChar(CharToInt(InputText[j]) + i):InputText[j]);
                }                    
                PossibleText.Add(new String(array));   
            }

            foreach (var elem in PossibleText)
                System.Console.WriteLine(elem);

        }

        //to do - add check
        private int CharToInt(char sym)
        {
            return (sym - (int)'A' + 1);
        }

        private char IntToChar(int sym)
        {
            return Convert.ToChar(((int)'A' + sym)>(int)'Z' ? (int)'A' + (sym-26) : (int)'A' + (sym));
        }
    }
}
                                                                                           