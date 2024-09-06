using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppQuozienteResto
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
                if (!inputOK) Console.WriteLine("Input Invalido! Riprova.");

            } while (!inputOK);

            return numInput;
        }

        static bool QuozientePositivo(int primoNumero, int secondoNumero)
        {
            if ((primoNumero >= 0 && secondoNumero >= 0) || (primoNumero < 0 && secondoNumero < 0)) return true;
            else return false;
        }

        static void Main(string[] args)
        {
            int dividendo, divisore;

            dividendo = LeggiInput("Inserisci il dividendo: ");

            do
            {//Controllo divisore != 0
                divisore = LeggiInput("Inserisci il divisore: ");
                if (divisore == 0) Console.WriteLine("Il divisore non può essere zero. Riprova.");
            } while (divisore == 0);

            int quoziente = 0, resto = 0;
            if (QuozientePositivo(dividendo, divisore))
            {
                dividendo = Math.Abs(dividendo);
                divisore = Math.Abs(divisore);

                while (dividendo >= divisore)
                {//Calcolo quoziente
                    quoziente++;
                    dividendo -= divisore;
                }

                resto = dividendo;
            }
            else
            {
                dividendo = Math.Abs(dividendo);
                divisore = Math.Abs(divisore);

                while (dividendo >= divisore)
                {//Calcolo quoziente
                    quoziente--;
                    dividendo -= divisore;
                }

                resto = -dividendo;
            }

            Console.WriteLine($"\nIl quoziente è {quoziente} con resto {resto}.");
            Console.WriteLine("Clicca un tasto per terminare il programma.");
            Console.ReadKey();
        }
    }
}
