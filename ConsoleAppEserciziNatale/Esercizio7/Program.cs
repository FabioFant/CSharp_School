using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Esercizio7
{
    internal class Program
    {
        static double CheckEratosteneAlgorithm(int N)
        {
            System.Diagnostics.Stopwatch cronometro = new System.Diagnostics.Stopwatch();

            N++;
            //Creo l'array
            bool[] values = new bool[N];
            for (int i = 0; i < N; i++)
                values[i] = true;
            values[0] = false;
            values[1] = false;

            cronometro.Restart();
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

                if (!continueOk) break;
            }
            cronometro.Stop();

            double time = (double)cronometro.ElapsedMilliseconds / 1000;

            return time;//Math.Round(time, 2);
        }

        static double CheckClassicAlgorithm(int N, out int lastN)
        {
            System.Diagnostics.Stopwatch cronometro = new System.Diagnostics.Stopwatch();

            cronometro.Restart();
            int num = 2;
            while (N != 0)
            {
                //Conto quante volte è divisibile
                int count = 1;
                for (int i = num / 2; i >= 1; i--)
                    if (num % i == 0)
                        count++;
                //Se rispetta la regola stampo e aggiorno N
                if (count == 2)
                    N--;

                num++;
            }
            cronometro.Stop();

            lastN = num-1; //Utilizato per Eratostene

            double time = (double)cronometro.ElapsedMilliseconds / 1000;

            return time;//Math.Round(time, 2);
        }

        static void Main(string[] args)
        {
            int[] N = { 100, 500, 1000, 2500, 5000, 7500, 10000, 20000, 30000, 40000, 50000}; //Quantità di numeri primi

            Console.WriteLine("{0, 10}\t{1, 10}\t{2, 10}", "N", "CLASSIC", "ERATOSTENE");
            for (int i = 0; i < N.Length; i++)
            {
                Console.Write("{0,10}", N[i]);
                int eratosteneN;

                double timeCl = CheckClassicAlgorithm(N[i], out eratosteneN);
                Console.Write("\t{0,10:0.000}", timeCl);

                double timeEr = CheckEratosteneAlgorithm(eratosteneN);
                Console.Write("\t{0,10:0.000}", timeEr);

                Console.WriteLine();
            }

            Console.ReadKey();
        }
    }
}
