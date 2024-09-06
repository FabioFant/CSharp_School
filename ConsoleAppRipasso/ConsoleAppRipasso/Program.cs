using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppRipasso
{
    internal class Program
    {
        static int mcd(int a, int b)
        {
            //PASSO BASE
            if(b == 0)
            {
                return a;
            }
            //PASSO RICORSIVO
            return mcd(b, a % b);
        }
        static void Main(string[] args)
        {
            Console.WriteLine(mcd(21, 18));
            Console.ReadKey();  
        }
    }
}
