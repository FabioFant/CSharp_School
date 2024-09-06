using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Esercizio8
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
                if (!inputOk)
                {
                    Console.WriteLine("Input Invalido!");
                    inputOk = false;
                }
            } while (!inputOk);
            return num;
        }

        static bool IsSorted(int[] vect)
        {
            int[] sortedVect = new int[vect.Length];
            //Copia valori
            for (int i = 0; i < sortedVect.Length; i++)
                sortedVect[i] = vect[i];
            //Controllo
            for (int i = 0; i < sortedVect.Length; i++)
                for (int j = i; j < sortedVect.Length; j++)
                    if (sortedVect[j] < vect[i])
                        return false;

            return true;
        }

        static void Swap(ref int val1, ref int val2)
        {
            int tmp = val1;
            val1 = val2;
            val2 = tmp;
        }

        static int[] BubbleSort(int[] vett)
        {
            for (int i = 0; i < vett.Length; i++)
                for (int j = 1; j < vett.Length - i; j++)
                    if (vett[j - 1] > vett[j])
                        Swap(ref vett[j - 1], ref vett[j]);

            return vett;
        }

        static int[] Fusione(int[] vett_A, int[] vett_B)
        {
            if (!IsSorted(vett_A) && !IsSorted(vett_B))
                throw new Exception("Vettori non ordinati.");

            int[] risultato = new int[vett_A.Length + vett_B.Length];

            //Riempi risultato con i valori dei due vettori non in modo ordinato
            for(int i = 0; i < vett_A.Length; i++)
                risultato[i] = vett_A[i];

            for (int i = 0; i < vett_B.Length; i++)
                risultato[i+vett_A.Length] = vett_B[i];

            risultato = BubbleSort(risultato);

            return risultato;
        }

        static void Main(string[] args)
        {
            //Input primo vettore
            int num_A;
            while (true)
            {
                num_A = Input("Inserisci il numero di valori per il primo vettore: ");
                if (num_A > 0) break;
                Console.WriteLine("Input Invalido!");
            }

            int[] vett_A = new int[num_A];

            for (int i = 0; i < vett_A.Length; i++)
                vett_A[i] = Input($"Valore {i+1}: ");
            Console.WriteLine();

            //Input secondo vettore
            int num_B;
            while (true)
            {
                num_B = Input("Inserisci il numero di valori per il secondo vettore: ");
                if (num_B > 0) break;
                Console.WriteLine("Input Invalido!");
            }

            int[] vett_B = new int[num_B];

            for (int i = 0; i < vett_B.Length; i++)
                vett_B[i] = Input($"Valore {i+1}: ");
            Console.WriteLine();

            //Ordina i vettori
            vett_A = BubbleSort(vett_A);
            vett_B = BubbleSort(vett_B);

            int[] fusione = Fusione(vett_A, vett_B);

            Console.WriteLine("Fusione:");
            for(int i = 0;i < fusione.Length;i++)
                Console.Write(fusione[i] + " ");

            Console.ReadKey();
        }
    }
}
