using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Esercizio2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string file_input = @"..\..\..\dati.txt";

            int value;
            while (true) 
            {
                Console.Write("Numero da cercare nel file: ");
                if (!int.TryParse(Console.ReadLine(), out value))
                {
                    Console.Clear();
                    continue;
                }
                break;
            }
            using(StreamReader sr = new StreamReader(file_input))
            {
                int count = 0;
                while (!sr.EndOfStream) 
                {
                    if (int.Parse(sr.ReadLine()) == value) count++;
                }

                if (count > 0)
                    Console.WriteLine($"{value} è presente nel file {count} volte.");
                else
                    Console.WriteLine("Non presente nel file.");

                sr.Close();
            }

            Console.ReadKey();
        }
    }
}
