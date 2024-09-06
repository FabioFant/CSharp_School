using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Esercizio6
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Input
            int N; bool inputOk;
            do
            {
                Console.Write("Inserisci N: ");
                inputOk = int.TryParse(Console.ReadLine(), out N);
                if (!inputOk) Console.WriteLine("Input Invalido!");
            } while (!inputOk);
            N++;
            //Creo l'array
            bool[] values = new bool[N];
            for(int i = 0; i < N; i++)
                values[i] = true;
            values[0] = false;
            values[1] = false;
            //Definisco i valori primi
            bool continueOk;
            for (int i = 2; i < N; i++)
            {
                int count = 1;
                for (int j = i / 2; j > 0; j--)
                    if (i % j == 0)
                        count++;

                continueOk = false;
                if (count == 2)
                {
                    for (int j = 2; i * j < N; j++)
                        if (values[i * j] == true)
                        {
                            values[i * j] = false;
                            continueOk = true;
                        }
                }
                else
                    continueOk = true;

                if(!continueOk) break;
            }
            //Stampa
            Console.WriteLine("Numeri Primi in N (Crivello di Eratostene):");
            for (int i = 0; i < N; i++)
                if (values[i] == true)
                    Console.Write($"{i} ");
                
            Console.ReadKey();
        }
    }
}
