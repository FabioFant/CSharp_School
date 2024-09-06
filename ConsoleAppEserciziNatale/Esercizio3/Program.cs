using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Esercizio3
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
                if (!inputOk) Console.WriteLine("Input Invalido!");
            } while (!inputOk);
            return num;
        }

        static bool IsSorted(int[] vect)
        {
            int[] sortedVect = new int[vect.Length];
            //Copia valori
            for(int i = 0; i < sortedVect.Length; i++)
                sortedVect[i] = vect[i];
            //Controllo
            for (int i = 0; i < sortedVect.Length; i++)
                for (int j = i; j < sortedVect.Length; j++)
                    if (sortedVect[j] < vect[i])
                        return false;

            return true;
        }

        static void Main(string[] args)
        {
            //Input lunghezza vettore
            int lenght;
            while (true)
            {
                lenght = Input("Inserisci la lunghezza del vettore: ");
                if (lenght <= 0) Console.WriteLine("Errore: La lunghezza deve essere > 0");
                else break;
            }
            //Input valori vettore
            int[] values = new int[lenght];
            for (int i = 0; i < values.Length; i++)
                values[i] = Input($"Indice {i}: ");
            //Stampa risultato
            Console.WriteLine();
            if (IsSorted(values)) Console.WriteLine("Il vettore è ordinato.");
            else Console.WriteLine("Il vettore non è ordinato");

            Console.ReadKey();
        }
    }
}
