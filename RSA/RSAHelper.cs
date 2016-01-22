using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSA
{
    class Helper
    {
        public void HelperMain()
        {
            RSA rsa = new RSA("sfsdf",30);

            while (true)
            {
                System.Console.WriteLine(">...");
                string command = System.Console.ReadLine();
                CommandParse(command, ref rsa);

            }

        }

        private void CommandParse(string command, ref RSA entity)
        {
            if (command.Contains("generate p"))
            {
                System.Console.WriteLine(entity.bitsize);
                entity.GeneratePQ();
                System.Console.WriteLine(entity.p);
                System.Console.WriteLine(entity.q);
            }

            else if (command.Contains("compute modulo n"))
            {
                if (!CheckParameters(ref entity, command))
                {
                    CommandParse(command, ref entity);
                }
                entity.GenerateN();
                System.Console.WriteLine(entity.n);
            }

            else if (command.Contains("compute phi"))
            {
                if (!CheckParameters(ref entity, command))
                {
                    CommandParse(command, ref entity);
                }

                entity.GeneratePhi();
                System.Console.WriteLine(entity.Phi);
            }

            else if (command.Contains("compute e"))
            {
                if (!CheckParameters(ref entity, command))
                {
                    CommandParse(command, ref entity);
                }

                entity.GenerateE();
                System.Console.WriteLine(entity.e);
            }


            else if (command.Contains("!cipher"))
            {
                if (!CheckParameters(ref entity, command))
                {
                    CommandParse(command, ref entity);
                }

                entity.CipherMessage();
                System.Console.WriteLine(entity.DDMessage);

            }

            else if (command.Contains("decipher"))
            {
                if (!CheckParameters(ref entity, command))
                {
                    CommandParse(command, ref entity);
                }

                entity.DecipherMessage();
                System.Console.WriteLine(entity.Result);
            }


        }

        private bool CheckParameters(ref RSA entity, string command)
        {
            if (command.Contains("modulo") || command.Contains("phi"))
            {
                if (entity.p == null)
                {
                    System.Console.WriteLine("p and q are not done");
                    entity.GeneratePQ();
                    return false;
                }
                return true;
            }

            if (command.Contains("e"))
            {
                if (!CheckParameters(ref entity, "modulo"))
                    CheckParameters(ref entity, command);
                else
                {
                    if (entity.Phi == null)
                    {
                        System.Console.WriteLine("Phi is not done");
                        entity.GeneratePhi();
                        return false;
                    }
                }

            }

            if (command.Contains("d"))
            {
                if (!CheckParameters(ref entity, "e"))
                    CheckParameters(ref entity, command);
                else
                {
                    if (entity.e == null)
                    {
                        System.Console.WriteLine("E is not done");
                        entity.GenerateE();
                        return false;
                    }
                }
            }

            if (command.Contains("!cipher"))
            {
                if (!CheckParameters(ref entity, "d"))
                    CheckParameters(ref entity, command);
                else
                {
                    if (entity.CMessage == null)
                    {
                        System.Console.WriteLine("insert the message");
                        string message = System.Console.ReadLine();
                        entity.GetCMessage(message);
                    }
                }
            }

            if (command.Contains("decipher"))
            {
                if (!CheckParameters(ref entity, "d"))
                    CheckParameters(ref entity, command);
                else
                {
                    if (entity.DMessage == null || entity.DDMessage == null)
                    {
                        System.Console.WriteLine("insert the message");
                        string message = System.Console.ReadLine();
                        entity.GetCMessage(message);
                        entity.GetDMessage(message);

                    }
                }
            }

            return true;
        }
    }
}
