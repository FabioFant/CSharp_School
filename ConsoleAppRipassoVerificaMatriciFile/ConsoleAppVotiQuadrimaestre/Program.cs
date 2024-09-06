using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppVotiQuadrimaestre
{
    internal class Program
    {
        static int LeggiIntero(string messaggio)
        {
            int num;
            while (true)
            {
                Console.Write(messaggio);
                if (!int.TryParse(Console.ReadLine(), out num) || num <= 0) continue;
                else break;
            }
            return num;
        }

        static void LeggiVettoreString(ref string[] vett, string messaggio)
        {
            Console.WriteLine(messaggio);
            for (int i = 0; i < vett.Length; i++)
            {
                while (true)
                {
                    string parola = Console.ReadLine();
                    if (parola.Length == 0) continue;
                    else break;
                }
            }
        }

        static double MediaQuadrimaestre(int[,] matrice)
        {
            int sum = 0;
            for(int i = 0; i < matrice.GetLength(0); i++)
            {
                for(int j = 0; j < matrice.GetLength(1); j++)
                {
                    sum += matrice[i, j];
                }
            }
            return (double)sum / matrice.Length;
        }

        static string AlunnoMigliore(int[,] matrice, string[] alunni)
        {
            double best_media = 0.0;
            int sum = 0, best_index = 0;
            for (int i = 0; i < matrice.GetLength(0); i++)
            {
                for (int j = 0; j < matrice.GetLength(1); j++)
                {
                    sum += matrice[i, j];
                }
                if((double)sum / matrice.GetLength(1) > best_media)
                {
                    best_media = sum / matrice.GetLength(1);
                    best_index = i;
                }
            }
            return alunni[best_index];
        }

        static void Main(string[] args)
        {
            int N = LeggiIntero("Numero di Alunni: ");
            int M = LeggiIntero("Numero di Materie: ");

            int[,] T1 = new int[N, M];
            int[,] T2 = new int[N, M];

            string[] alunni = new string[N];
            string[] materie = new string[M];

            LeggiVettoreString(ref alunni, "Inserisci gli Alunni:");
            LeggiVettoreString(ref materie, "Inserisci le Materie:");

            Console.WriteLine($"Media 1° Quadrimaestre: {MediaQuadrimaestre(T1)}");
            Console.WriteLine($"Media 1° Quadrimaestre: {MediaQuadrimaestre(T2)}");

            Console.WriteLine("Alunno con migliore media");
            Console.WriteLine($"1° Quad.: {AlunnoMigliore(T1, alunni)}");
            Console.WriteLine($"2° Quad.: {AlunnoMigliore(T2, alunni)}");
        }
    }
}
