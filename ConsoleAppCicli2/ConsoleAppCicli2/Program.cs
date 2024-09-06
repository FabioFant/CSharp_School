using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppCicli2
{
    internal class Program
    {

        static int LeggiInput(string messaggio)
        {
            bool inputOK; int numInput;

            do
            {
                Console.Write(messaggio);
                inputOK = int.TryParse(Console.ReadLine(), out numInput);
                if(!inputOK) Console.WriteLine("Input Invalido! Riprova.");

            } while (!inputOK);

            return numInput;
        }

        static bool ProdottoPositivo(int primoFattore, int secondoFattore)
        {
            if ((primoFattore >= 0 && secondoFattore >= 0) || (primoFattore < 0 && secondoFattore < 0)) return true;
            else return false;
        }

        static void Main(string[] args)
        {
            int a, b;

            a = LeggiInput("Inserisci il primo numero: ");
            b = LeggiInput("Inserisci il secondo numero: ");

            int count = 0;
            int prodotto = 0;

            if(ProdottoPositivo(a, b))
            {
                while(count < Math.Abs(b))
                {
                    prodotto += Math.Abs(a);
                    count++;
                }
            }
            else
            {
                while (count < Math.Abs(b))
                {
                    prodotto -= Math.Abs(a);
                    count++;
                }
            }

            Console.WriteLine($"\nIl prodotto è {prodotto}");
            Console.WriteLine("Clicca un tasto per terminare il programma.");
            Console.ReadKey();
        }
    }
}
