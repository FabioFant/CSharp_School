using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Esercizio4
{
    internal class Program
    {
        static int Input(string messaggio)
        {
            int num;
            bool inputOk;
            do
            {
                Console.Write(messaggio);
                inputOk = int.TryParse(Console.ReadLine(), out num);
                if (!inputOk || num < 0) 
                {
                    Console.WriteLine("Input Invalido!");
                    inputOk = false;
                } 
            } while (!inputOk);
            return num;
        }

        static void SwapInt(ref int val1, ref int val2)
        {
            int tmp = val1;
            val1 = val2;
            val2 = tmp;
        }

        static void SwapString(ref string val1, ref string val2)
        {
            string tmp = val1;
            val1 = val2;
            val2 = tmp;
        }

        static void Main(string[] args)
        {
            int num;
            while (true)
            {
                num = Input("Inserisci il numero di persone: ");
                if (num > 0) break;
                Console.WriteLine("Input Invalido!");
            }

            string[] nomi = new string[num];
            int[] età = new int[num];

            //Input
            for(int i = 0; i < num; i++)
            {
                Console.WriteLine($"\nPersona {i + 1}");
                Console.Write("Nome: "); nomi[i] = Console.ReadLine();
                età[i] = Input("Età: ");
            }

            //Selection sort
            for(int i = num-1; i >= 0; i--)
            {
                int maxVal = -1;
                int maxIndex = 0;
                for(int j = 0; j <= i; j++)
                {
                    if (età[j] >= maxVal)
                    {
                        maxVal = età[j];
                        maxIndex = j;
                    }
                }
                SwapInt(ref età[i], ref età[maxIndex]);
                SwapString(ref nomi[i], ref nomi[maxIndex]);
            }

            //Stampa
            Console.WriteLine("\nLista Ordinata per Età: ");
            Console.WriteLine("ETA'\tNOMI");
            for (int i = 0; i < num; i++)
                Console.WriteLine($"{età[i]}\t{nomi[i]}");

            Console.ReadKey();
        }
    }
}
