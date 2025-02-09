// Fabio Fantini 4H 2025-01-20
// Ponte con apertura / chiusura per far passare le auto.
// Svolgimento con semafori

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static System.Console;

namespace ConsoleAppPonte
{
    internal class Program
    {

        #region Variabili Globali

        const int NUM_AUTO_SUL_PONTE = 4; // Portata massima del ponte in numero di autoù

        const int MAX_PAUSA = 30;
        const int MIN_PAUSA = 10;

        static Random rnd = new Random();

        // Mutua Esclusione
        static Object lockConsole    = new Object();
        static Object lockParcheggio = new Object();
        static Object lockCorsia     = new Object();

        // Variabili per i thread
        static List<Thread> passa;          // Lista auto in transito
        static bool levatoio = true;        // Levatoio alto/basso
        static List<string> parcheggio = new List<string>();    // Auto nel parcheggio
        static bool[] corsia = new bool[NUM_AUTO_SUL_PONTE];    // Stato delle corsie libero
        static int n_auto_totali = 0;

        static bool exit = false;

        #endregion

        #region Metodi per Console

        static void StampaMappa()
        {
            lock (lockConsole)
            {
                ForegroundColor = ConsoleColor.DarkYellow;

                //         1234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345
                WriteLine("                                           ║░░░░░░░░░░░░░░░░░░░░░░░░░░░║                                           "); // 3
                WriteLine("                                           ║░░░░░░░░░░░░░░░░░░░░░░░░░░░║       [A]dd Auto                          "); // 4
                WriteLine("                                           ║░░░░░░░░░░░░░░░░░░░░░░░░░░░║       [C]lose Ponte                       "); // 5
                WriteLine("                                           ║░░░░░░░░░░░░░░░░░░░░░░░░░░░║       [O]pen Ponte                        "); // 6
                WriteLine("                                           ║░░░░░░░░░░░░░░░░░░░░░░░░░░░║       [E]xit                              "); // 7
                WriteLine("                                           ║░░░░░░░░░░░░░░░░░░░░░░░░░░░║                                           "); // 8
                WriteLine("                                           ║░░░░░░░░░░░░░░░░░░░░░░░░░░░║                                           "); // 9
                WriteLine("                                           ║░░░░░░░░░░░░░░░░░░░░░░░░░░░║                                           "); // 10
                WriteLine("                                           ║░░░░░░░░░░░░░░░░░░░░░░░░░░░║                                           "); // 11
                WriteLine("                                           ║░░░░░░░░░░░░░░░░░░░░░░░░░░░║                                           "); // 12
                WriteLine("                                           ║░░░░░░░░░░░░░░░░░░░░░░░░░░░║                                           "); // 13
                WriteLine("                                           ║░░░░░░░░░░░░░░░░░░░░░░░░░░░║                                           "); // 14
                WriteLine("                                           ║░░░░░░░░░░░░░░░░░░░░░░░░░░░║                                           "); // 15
                WriteLine("                                           ║░░░░░░░░░░░░░░░░░░░░░░░░░░░║                                           "); // 16
                WriteLine("                                           ║░░░░░░░░░░░░░░░░░░░░░░░░░░░║                                           "); // 17
                WriteLine("                                           ║░░░░░░░░░░░░░░░░░░░░░░░░░░░║                                           "); // 18
                WriteLine("                                           ║░░░░░░░░░░░░░░░░░░░░░░░░░░░║                                           "); // 19
                WriteLine("                                           ║░░░░░░░░░░░░░░░░░░░░░░░░░░░║                                           "); // 20
                WriteLine("                                           ║░░░░░░░░░░░░░░░░░░░░░░░░░░░║                                           "); // 21
                WriteLine("                                           ║░░░░░░░░░░░░░░░░░░░░░░░░░░░║                                           "); // 22
                WriteLine("                                           ║░░░░░░░░░░░░░░░░░░░░░░░░░░░║                                           "); // 23
                WriteLine("                                           ║░░░░░░░░░░░░░░░░░░░░░░░░░░░║                                           "); // 24
                WriteLine("                                           ║░░░░░░░░░░░░░░░░░░░░░░░░░░░║                                           "); // 25
                WriteLine("                                           ║░░░░░░░░░░░░░░░░░░░░░░░░░░░║                                           "); // 26
                WriteLine("                                           ║░░░░░░░░░░░░░░░░░░░░░░░░░░░║                                           "); // 27
                WriteLine("                                           ║░░░░░░░░░░░░░░░░░░░░░░░░░░░║                                           "); // 28
                WriteLine("                                           ║░░░░░░░░░░░░░░░░░░░░░░░░░░░║                                           "); // 29
                ForegroundColor = ConsoleColor.White;
            }
        }
        /// <summary>
        /// Scrive un messaggio in una posizione precisa della console utilizzando la mutua esclusione.
        /// </summary>
        /// <param name="col">Colonna della console.</param>
        /// <param name="rig">Riga della console.</param>
        /// <param name="mess">Messaggio da stampare</param>
        /// <param name="sleep">Pausa prima di stampare in millisecondi.</param>
        /// <param name="colore">Colore del messaggio.</param>
        static void Scrivi(int col, int rig, string mess, int sleep, ConsoleColor colore = ConsoleColor.White)
        {
            // Attesa
            Thread.Sleep(sleep);
            lock (lockConsole)
            {
                // Posizione cursore, colore e scrittura
                SetCursorPosition(col, rig);
                ForegroundColor = colore;
                Write(mess);
            }
            // Colore di default
            ForegroundColor = ConsoleColor.White;
        }
        static void ApriPonte()
        {
            levatoio = false;

            lock (lockConsole)
            {
                Scrivi(43, 12, "╚═══════════════════════════╝", 0, ConsoleColor.DarkYellow);
                Scrivi(43, 13, "                             ", 0, ConsoleColor.DarkYellow);
                Scrivi(43, 14, "                             ", 0, ConsoleColor.DarkYellow);
                Scrivi(43, 15, "                             ", 0, ConsoleColor.DarkYellow);
                Scrivi(43, 16, "                             ", 0, ConsoleColor.DarkYellow);
                Scrivi(43, 17, "╔═══════════════════════════╗", 0, ConsoleColor.DarkYellow);
            }
        }
        static void ChiudiPonte()
        {
            levatoio = true;

            lock (lockConsole)
            {
                Scrivi(43, 12, "║░░░░░░░░░░░░░░░░░░░░░░░░░░░║", 0, ConsoleColor.DarkYellow);
                Scrivi(43, 13, "║░░░░░░░░░░░░░░░░░░░░░░░░░░░║", 0, ConsoleColor.DarkYellow);
                Scrivi(43, 14, "║░░░░░░░░░░░░░░░░░░░░░░░░░░░║", 0, ConsoleColor.DarkYellow);
                Scrivi(43, 15, "║░░░░░░░░░░░░░░░░░░░░░░░░░░░║", 0, ConsoleColor.DarkYellow);
                Scrivi(43, 16, "║░░░░░░░░░░░░░░░░░░░░░░░░░░░║", 0, ConsoleColor.DarkYellow);
                Scrivi(43, 17, "║░░░░░░░░░░░░░░░░░░░░░░░░░░░║", 0, ConsoleColor.DarkYellow);
            }
        }
        static void AccettaComandi()
        {
            char choice = ' ';

            // Legge il comando utente
            choice = ReadKey(true).KeyChar;
            choice = char.ToUpper(choice);

            // In base al comando, un'azione viene svolta
            switch (choice)
            {
                case 'A':
                    break;

                case 'C':
                    ChiudiPonte();
                    break;

                case 'O':
                    ApriPonte();
                    break;

                case 'E':
                    exit = true;
                    break;

                default:
                    return;
            }
        }
        static void AggiornaParcheggio()
        {
            for (int i = 0; i < parcheggio.Count; i++)
            {
                Scrivi(10, 3 + i, parcheggio[i], 0);
            }
        }

        #endregion

        #region Metodi per Thread

        static void AggiungiAuto()
        {
            n_auto_totali++;
            lock (lockParcheggio)
            {
                parcheggio.Add($"Auto {n_auto_totali}");
                AggiornaParcheggio();
            }
        }
        static void Auto(object obj)
        {
            string[] input = (string[])obj;

            int posAuto = 0;
            string auto = input[0];
            int col = int.Parse(input[1]);
            int pausa = rnd.Next(MIN_PAUSA, MAX_PAUSA);

            lock(lockCorsia) corsia[col - 1] = true;
            do
            {
                // Posizione successiva animazione
                posAuto++;
                Scrivi(posAuto, col, auto, pausa);

            } while (posAuto < 115); // Finchè non si raggiunge la destinazione (fine console)
            lock (lockCorsia) corsia[col - 1] = false;
        }

        #endregion

        static void Main(string[] args)
        {
            // Init del programma
            OutputEncoding = Encoding.Unicode;
            CursorVisible = false;
            WriteLine("Fabio Fantini 4H 2024-11-04\n");
            StampaMappa();

            while(!exit)
            {
                if(KeyAvailable) AccettaComandi();
            }
        }
    }
}
