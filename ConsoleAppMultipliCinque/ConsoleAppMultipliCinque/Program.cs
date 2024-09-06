//Fabio Fantini 3H
//Somma multipli di 5 [10; N]

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppMultipliCinque
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int N;
            bool inputOK;

            #region Lettura Input
            do
            {
                Console.Write("Inserisci N --> ");
                inputOK = int.TryParse(Console.ReadLine(), out N);
                if (!inputOK) Console.WriteLine("Input Invalido! Riprova.\n");
                else if (N < 0)
                {
                    Console.WriteLine("Input invalido! N deve essere > 0.\n");
                    inputOK = false;
                }
            } while (!inputOK);
            #endregion

            int somma = 0;
            for(int i = 0; i <= N; i++)
            {
                if(i >= 10 && i % 5 == 0) somma += i;
            }

            Console.WriteLine($"Somma dei multipli di 5 [10 ; N] : {somma}");
            Console.ReadKey();
        }
    }
}
