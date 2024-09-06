using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppPensareRandom
{
    internal class Program
    {
        const int MAX = 100;
        const int MIN = 0;
        static void Main(string[] args)
        {
            Random rnd = new Random();
            int num;
            char risposta;
            int tempMin = MIN, tempMax = MAX;

            Console.WriteLine($"Pensa un numero tra {MIN} e {MAX} e cercherò di indovinarlo.\n");
            while (true)
            {
                num = rnd.Next(tempMin, tempMax+1);
                Console.WriteLine($"Il tuo numero è {num}? Risposte: < > =");
                risposta = char.Parse(Console.ReadLine());

                if (risposta == '<')
                {
                    tempMax = num;
                }
                else if (risposta == '>')
                {
                    tempMin = num;
                }
                else if (risposta == '=')
                {
                    break;
                }
            }

            Console.WriteLine("Ho indovinato! Yohoo :D");
            Console.ReadKey();
        }
    }
}
