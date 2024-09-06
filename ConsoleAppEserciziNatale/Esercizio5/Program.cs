using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Esercizio5
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
                inputOk = int.TryParse(Console.ReadLine(), out N );
                if (!inputOk) Console.WriteLine("Input Invalido!");
            } while (!inputOk);
            //Stampa
            Console.WriteLine("N Numeri Primi:");

            int num = 2;
            while(N != 0)
            {
                //Conto quante volte è divisibile
                int count = 1;
                for (int i = num / 2; i >= 1; i--)
                    if (num % i == 0)
                        count++;
                //Se rispetta la regola stampo e aggiorno N
                if (count == 2)
                {
                    Console.Write($"{num} ");
                    N--; 
                }
                num++;
            }

            Console.ReadKey();
        }
    }
}
