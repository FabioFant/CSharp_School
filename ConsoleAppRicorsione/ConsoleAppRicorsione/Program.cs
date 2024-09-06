using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppRicorsione
{
    internal class Program
    {
        static int Potenza(int baseNum, int esp)
        {
            if (esp == 0)
                return 1;

            return baseNum * Potenza(baseNum, esp-1);
        }

        static void Main(string[] args)
        {
            Console.WriteLine(Potenza(2, 5));

            Console.ReadKey();
        }
    }
}
