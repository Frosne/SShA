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
            RSA rsa = new RSA("34",10);

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
                System.Console.WriteLine(entity.d);
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
                System.Console.WriteLine("The message is {0}", entity.Result);
            }

            else if (command.Contains("show phi"))
            {
                System.Console.WriteLine("phi - {0}", entity.Phi != null ? entity.Phi.ToString() : "null");
            }

            else if (command.Contains("show p") || command.Contains("show q"))
            {
                System.Console.WriteLine("p - {0}, q - {1}", entity.p!=null ? entity.p.ToString() : "null", entity.q!=null ? entity.q.ToString() : "null" );
            }
            else if (command.Contains("show n"))
            {
                System.Console.WriteLine("n - {0}", entity.n != null ? entity.n.ToString() : "null");
            }

          

            else if (command.Contains("show e"))
            {
                System.Console.WriteLine("e - {0}, d - {1}", entity.e != null ? entity.e.ToString() : "null", entity.d != null ? entity.d.ToString() : "null");
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
                    this.CommandParse("show p",ref entity);
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
                        this.CommandParse("phi", ref entity);
                        return false;
                    }

                    if (entity.n == null)
                    {
                        System.Console.WriteLine("N is not done");
                        entity.GenerateN();
                        this.CommandParse("show n", ref entity);
                        return false;
                    }

                    return true;
                }

            }

            if (command.Contains("d"))
            {
                if (!CheckParameters(ref entity, "compute e"))
                    CheckParameters(ref entity, command);
                else
                {
                    if (entity.e == null)
                    {
                        System.Console.WriteLine("E is not done");
                        entity.GenerateE();
                        this.CommandParse("show e", ref entity);
                        return false;
                    }

                    return true;
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

                        entity.CipherMessage();
                        System.Console.WriteLine("Ciphered message  - {0} ", entity.DDMessage);
                    
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

                    entity.DecipherMessage();
                    System.Console.WriteLine("Decipher message - {0}", entity.Result);

                    
                }
            }

            return true;
        }
    }
}
