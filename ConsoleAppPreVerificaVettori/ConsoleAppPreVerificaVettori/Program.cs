using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppPreVerificaVettori
{
    internal class Program
    {
        static bool isFull(int[] vett, int vett_count) { return vett_count == vett.Length; }

        static int[] Realloc(int[] vett)
        {
            int[] new_vett = new int[vett.Length*2];
            for(int i = 0; i < vett.Length; i++)
                new_vett[i] = vett[i];

            return new_vett;
        }

        static void Insert(ref int[] vett, ref int vett_count , int index, int value)
        {
            if (isFull(vett, vett_count))
                vett = Realloc(vett);

            int move_count = vett_count - index;
            for (int i = move_count; i > 0; i--)
                vett[index + i] = vett[index + i - 1];

            vett[index] = value;

            vett_count++;
        }

        static void Remove(ref int[] vett, ref int vett_count, int index)
        {
            int move_count = vett_count - index - 1;
            for (int i = 0; i < move_count; i++)
                vett[index + i] = vett[index + i + 1];
            
            vett_count--;
        }

        static void Main(string[] args)
        {
            int[] vett = {0, 1, 2, 3, 4, 5, 6, 7, 8, 9};
            int vett_count = 10;

            while (true)
            {
                for(int i = 0; i < vett_count; i++)
                    Console.Write(vett[i] + " ");
                Console.WriteLine();

                Console.WriteLine("1: Aggiungi");
                Console.WriteLine("0: Rimuovi");

                string s = Console.ReadLine();

                if (s == "1")
                {
                    Console.Write("Inserisci l'indice: ");
                    int index = int.Parse(Console.ReadLine());
                    Console.Write("Inserisci il valore: ");
                    int value = int.Parse(Console.ReadLine());
                    Insert(ref vett, ref vett_count, index, value);
                }
                else if (s == "0")
                {
                    Console.Write("Inserisci l'indice: ");
                    int index = int.Parse(Console.ReadLine());
                    Remove(ref vett, ref vett_count, index);
                }
                else
                    continue;
            }
        }
    }
}
