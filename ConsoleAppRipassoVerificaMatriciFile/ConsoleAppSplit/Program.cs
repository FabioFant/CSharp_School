using System.IO;
using System;

namespace AcquisizioneMatrice
{
    internal class Program
    {
        const string NOME_FILE = @"..\..\..\matrice.txt";
        const char SEPARATORE = '|';

        static bool IntTryParseFile(StreamReader file, out int num)
        {
            if (file.EndOfStream)
            {
                num = 0;
                return false;
            }

            return int.TryParse(file.ReadLine(), out num);
        }

        static int[,] LeggiMatriceDaFile(string percorso_file, char separator)
        {
            //Creo la matrice
            int[,] matrice = null;

            using(StreamReader file = new StreamReader(percorso_file))
            {
                //Definisco le dimensioni della matrice
                int N;
                if(!IntTryParseFile(file, out N) || N <= 0)
                {
                    Console.WriteLine("Errore riga 1: Numero righe errato.");
                    return null;
                }
                int M;
                if(!IntTryParseFile(file, out M) || M <= 0)
                {
                    Console.WriteLine("Errore riga 1: Numero colonne errato.");
                    return null;
                }
                //Alloco la matrice
                matrice = new int[N, M];
                //Passo per le righe
                int ir;
                for(ir = 0; ir < N && !file.EndOfStream; ir++)
                {
                    string riga = file.ReadLine();
                    string[] parti = riga.Split(separator);
                    //Controlli dei valori per ciascuna riga
                    if(parti.Length < M)
                    {
                        Console.WriteLine($"Errore riga {3 + ir}: I valori sono meno di {M}.");
                        return null;
                    }
                    if(parti.Length > M)
                    {
                        Console.WriteLine($"Avviso riga {3 + ir}: I valori sono più di {M}, quelli in più verranno ignorati.");
                    }
                    //Assegnazione valori alle righe
                    for(int ic = 0; ic < M; ic++)
                    {
                        //Controllo di valori validi
                        if (!int.TryParse(parti[ic], out matrice[ir, ic]))
                        {
                            Console.WriteLine($"Errore riga {3 + ir} colonna {1 + ic}: Valore non valido.");
                            return null;
                        }
                    }
                }
                //Controllo delle righe
                if(ir < N)
                {
                    Console.WriteLine($"Errore: Il file contiene solo {ir} righe di valori.");
                }

                file.Close();
            }

            return matrice;
        }

        static void Main(string[] args)
        {
            int[,] matrice = LeggiMatriceDaFile(NOME_FILE, SEPARATORE);

            if(matrice == null)
            {
                Console.ReadKey();
                return;
            }

            Console.WriteLine($"Matrice {matrice.GetLength(0)} x {matrice.GetLength(1)}, caricata da file '{NOME_FILE}'");
            Console.WriteLine("-------------------------------------------------------");
            for(int i = 0; i < matrice.GetLength(0); i++)
            {
                for(int j = 0; j < matrice.GetLength(1); j++)
                {
                    Console.Write($"{matrice[i, j],5} ");
                }
                Console.WriteLine();
            }
            Console.ReadKey();
        }
    }
}