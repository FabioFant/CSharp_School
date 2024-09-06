//Fabio Fantini 3H 2023/11/07
//Test di scrittura su tastiera.

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Net;

namespace ConsoleAppTypeCheck
{
    internal class Program
    {
        const string nome_file = @"C:\Users\Utente\source\\repos\ConsoleAppTypeCheck\ConsoleAppTypeCheck\Frasi.txt";

        static void TypeCheck(string frase)
        {
            Console.CursorVisible = false;

            int errori = 0;
            int lenght = frase.Length;
            System.Diagnostics.Stopwatch cronometro = new System.Diagnostics.Stopwatch();

            #region Partenza con conto alla rovescia e beep
            Console.WriteLine("Sei pronto?");
            Thread.Sleep(2000);
            Console.Write("3 ");
            Console.Beep(2000, 250);
            Thread.Sleep(250);
            Console.Write("2 ");
            Console.Beep(2000, 250);
            Thread.Sleep(250);
            Console.Write("1\n\n");
            Console.Beep(2000, 250);
            Thread.Sleep(250);
            Console.Beep(4000, 250);
            #endregion
            
            Console.Write(frase);
            Console.ForegroundColor = ConsoleColor.Black;
            //All'inizio posiziono il cursore all'inizio della frase
            Console.SetCursorPosition(Console.CursorLeft - lenght, Console.CursorTop);

            cronometro.Restart();
            for (int i = 0; i < lenght; ++i)
            {
                //Leggo il tasto premuto
                ConsoleKeyInfo key = Console.ReadKey(true);
                //Correzione
                if (key.KeyChar == frase[i])
                    Console.BackgroundColor = ConsoleColor.DarkGreen;
                else
                {
                    Console.BackgroundColor = ConsoleColor.DarkRed;
                    errori++;
                }
                //Sostituisco il carattere colorandolo e muovendo il cursore
                Console.Write(frase[i]);
            }
            cronometro.Stop();
            //Reimposto i colori di default
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;

            //Stampo i valori

            //Tasso di errore
            double tasso_di_errore = (double)errori / lenght;
            tasso_di_errore = Math.Round(tasso_di_errore, 3);
            Console.WriteLine($"\n\n- Tasso di errore {tasso_di_errore * 100}%");

            //Tempo
            double secondi_trascorsi = (double)cronometro.ElapsedMilliseconds / 1000.0;
            Console.WriteLine($"- Tempo totale: {secondi_trascorsi}s.");

            //Battute al minuto
            double minuti_trascorsi = (double)cronometro.ElapsedMilliseconds / 60000.0;
            double battute_al_minuto = lenght / minuti_trascorsi;
            battute_al_minuto = Math.Round(battute_al_minuto, 3);
            Console.WriteLine($"- Battute al minuto: {battute_al_minuto}\n");
        }

        static void Main(string[] args)
        {
            StreamReader sr = new StreamReader(nome_file);
            Console.Title = "Test di Scrittura su Tastiera by Fabio Fantini 3H.";

            while (!sr.EndOfStream)
            {
                string riga = sr.ReadLine();

                TypeCheck(riga);
            }

            sr.Close();
            Console.ReadKey();
        }
    }
}



