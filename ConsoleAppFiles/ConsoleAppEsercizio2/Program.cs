using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppEsercizio2
{
    internal class Program
    {
        static string fi = @"..\..\..\dati.txt";

        static void Main(string[] args)
        {
            int num;
            while (true)
            {
                Console.Write("Inserisci il numero da trovare nel file: ");
                if (!int.TryParse(Console.ReadLine(), out num))
                {
                    Console.Clear();
                    continue;
                }
                   
                break;
            }
            Console.WriteLine();

            using(StreamReader sr = new StreamReader(fi))
            {
                bool found = false;
                int curr, rig = 0;
                while(!sr.EndOfStream)
                {
                    rig++;
                    if(int.TryParse(sr.ReadLine(), out curr) && curr == num)
                    {
                        if (!found)
                        {
                            found = true;
                            Console.WriteLine("RIGHE:");
                        }

                        Console.WriteLine(rig);
                    }
                }
                if (!found)
                    Console.WriteLine("Non presente nel file.");
            }

            Console.ReadKey();
        }
    }
}
