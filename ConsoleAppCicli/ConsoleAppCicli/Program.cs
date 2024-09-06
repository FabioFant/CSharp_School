using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppCicli
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int a, b;
            bool ok = false;

            Console.WriteLine("Inserisci due numeri per fare l' MCD.");
            a = int.Parse(Console.ReadLine());
            b = int.Parse(Console.ReadLine());

            int mcd = a;
            if(b < a)
            {
                mcd = b;
            }

            mcd++;
            while(ok == false)
            {
                mcd--;
                if (a % mcd == 0 && b % mcd == 0) ok = true;
            }

            Console.WriteLine($"L'MCD è {mcd}");
            Console.ReadKey();
        }
    }
}
