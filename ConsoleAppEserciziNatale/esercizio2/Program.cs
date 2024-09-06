using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Esercizio2
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

        static int[] IncreaseLenght(int[] vect)
        {
            //Raddoppio la lunghezza del vettore
            int lenght = vect.Length * 2;
            int[] newVect = new int[lenght];
            //Reinserisco i valori nel nuovo vettore
            for (int i = 0; i < vect.Length; i++)
                newVect[i] = vect[i];
            
            return newVect;
        }

        static int[] InsertSorted(int[] vect, int num, int pointer)
        {
            //Trovo il massimo minore rispetto al numero
            int maxMin = -1;
            int maxMinIndex = -1;
            for (int i = 0; i < pointer; i++)
            {
                if (vect[i] < num && vect[i] >= maxMin)
                {
                    maxMin = vect[i];
                    maxMinIndex = i;
                }
            }
            //Sposto i valori dopo al massimo minore trovato di un indice a destra
            for (int i = pointer; i > maxMinIndex + 1; i--)
                vect[i] = vect[i - 1];
            //Inserisco il numero nel posto giusto
            vect[maxMinIndex + 1] = num;

            return vect;
        }

        static void Main(string[] args)
        {
            int[] valori = new int[100];
            int pointer = -1; //Indica a che indice ci sono i valori, -1 perché parte vuoto

            Console.WriteLine("Inserisci una serie di valori, per fermarti inserirne uno negativo (non considerato nella lista)");
            while (true)
            {
                //Input
                int num = Input($"Valore indice {pointer + 1}: ");
                if (num < 0) break;
                //Aggiorna Pointer e Lunghezza
                pointer++;
                if (pointer == valori.Length) valori = IncreaseLenght(valori);
                //Inserisci valore in modo ordinato
                valori = InsertSorted(valori, num, pointer);
                //Stampa valori
                Console.WriteLine("Serie di valori: ");
                for (int i = 0; i <= pointer; i++) Console.Write($"{valori[i]} ");
                Console.WriteLine("\n");
            }
            //All'uscita stampa dei valori finali
            Console.WriteLine("\nSerie di numeri finale: ");
            for (int i = 0; i <= pointer; i++) Console.Write($"{valori[i]} ");
            Console.ReadKey();
        }
    }
}
