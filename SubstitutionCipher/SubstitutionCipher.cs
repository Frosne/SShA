using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubstitutionCipher
{
    class SubstitutionCipher
    {
        private int languageIStart;
        private int languageIEnd;

        private int languageOStart;
        private int languageOEnd;

        private List<TupleLetter> table;

        public SubstitutionCipher(char letterStart, char letterEnd)
            :this(letterStart,letterStart, letterEnd,letterEnd)
        {
        }
        public SubstitutionCipher(char letterIS, char letterOS, char letterIE, char letterOE)
        {
            if (((int)letterIE - (int)letterIS) != ((int)letterOE - (int)letterOS))
                throw new ArithmeticException("The ranges are different");                
            this.languageIStart = letterIS;
            this.languageOStart = letterOS;
            this.languageIEnd = letterIE;
            this.languageOEnd = letterOE;
            if (this.languageIStart > this.languageIEnd)
            {
                int temp = this.languageIStart;
                this.languageIStart = this.languageIEnd;
                this.languageIEnd = temp;
            }
            if (this.languageOStart > this.languageOEnd)
            {
                int temp = this.languageOStart;
                this.languageOStart = this.languageOEnd;
                this.languageOEnd = temp;
            }
        }
        public void GenerateTable()
        {
            table = new List<TupleLetter>();
            Random rnd = new Random(Guid.NewGuid().GetHashCode());
            for (int i = languageIStart; i <= languageIEnd; i++)
            {
                char newLetter = ' ';
                while(true)
                {
                    newLetter = (char)(languageOStart + rnd.Next() % (languageOEnd - languageOStart+1));
                    
                    if (table.Where(e => e.oletter == newLetter).Count() == 0)
                    {
                        table.Add(new TupleLetter((char)i,(char)newLetter));
                        break;
                    }
                }                
            }

            foreach (var elem in table)
                System.Console.WriteLine(    elem.ToString());
            
        }
        public string CipherMessage(string message)
        {
            if (!CheckMessage(message,true))
                throw new Exception("The string is not in the range of the table. The ciphering would be undeciphered");
            char [] letters = message.ToCharArray();
            for (int i = 0; i < message.Length; i++)
            {
                letters[i] = table.Where(e => e.iletter == letters[i]).First().oletter;
            }
            return new String(letters);
        }

        private bool CheckMessage(string message, bool p)
        {
            foreach (var elem in message.Distinct())
            {
                if (p)
                {
                    if (this.table.Where(e => e.iletter == elem).Count() == 0)
                        return false;
                }
                else
                    if (this.table.Where(e => e.oletter == elem).Count() == 0)
                        return false;
            }
            return true;
        }

        public string DecipherMessage(string message)
        {
            if (!CheckMessage(message, false))
                throw new Exception("The string is not in the range of the table. The deciphered text will not be possible");
            char[] letters = message.ToCharArray();
            for (int i = 0; i < message.Length; i++)
            {
                letters[i] = table.Where(e => e.oletter == letters[i]).First().iletter;
            }

            return new String(letters);
        }

    }

    class TupleLetter
    {
        public char iletter;
        public char oletter;

        public TupleLetter()
        {
            
        }

        public TupleLetter(char iletter, char oletter)
        {
            this.iletter = iletter;
            this.oletter = oletter;
        }
        
        public override string ToString()
{
    String result = iletter.ToString() + "->" + oletter.ToString();
    return result;
}
    }
}
