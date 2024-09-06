using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppAlbero
{
    internal class Program
    {
        static void Main(string[] args)
        {
            bool inputOK;
            int N;
            //Lettura N
            do
            {
                Console.Write("Inserisci N: ");
                inputOK = int.TryParse(Console.ReadLine(), out N);
                if (!inputOK) Console.WriteLine("Input Invalido!");
                else if(N < 1)
                {
                    inputOK = false;
                    Console.WriteLine("Errore: N < 1");
                }
            }while (!inputOK);

            //Stampa       
            {
                int sx = 0;
                int dx = 0;
                for (int i = 0; i < N; i++)
                {
                    while (sx <= dx)
                    {
                        Console.Write($"{sx} ");
                        sx++;
                    }
                    Console.WriteLine();
                    sx = dx + 1;
                    dx = sx * 2;
                }
            }

            Console.ReadKey();
        }
    }
}
