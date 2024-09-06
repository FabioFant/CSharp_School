using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppPensare
{
    internal class Program
    {
        const int MINIMO = 0;
        const int MASSIMO = 100;

        static void Main(string[] args)
        {
            char risposta;
            int numero = (MASSIMO + MINIMO) / 2;
            int tempMax = MASSIMO;
            int tempMin = MINIMO;

            Console.WriteLine($"Pensa un numero tra {MINIMO} e {MASSIMO} e cercherò di indovinarlo.\n");
            while (true)
            { 
                Console.WriteLine($"Il tuo numero è {numero}? Risposte: > < =.");
                risposta = char.Parse(Console.ReadLine());

                if(risposta == '>')
                {
                    tempMin = numero;
                    numero += ((tempMax - tempMin) / 2);
                }
                else if(risposta == '<')
                {
                    tempMax = numero;
                    numero -= ((tempMax - tempMin) / 2);
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
