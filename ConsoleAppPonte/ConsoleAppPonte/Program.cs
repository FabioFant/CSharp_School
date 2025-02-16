// Fabio Fantini 4H 2025-01-20
// Ponte con apertura / chiusura per far passare le auto.
// Svolgimento con semafori binari e di dijkstra

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static System.Console;
using static System.Net.Mime.MediaTypeNames;

namespace ConsoleAppPonte
{
    internal class Program
    {

        #region Variabili Globali

        const int NUM_AUTO_SUL_PONTE = 4; // Portata massima del ponte in numero di autoù

        const int MAX_AUTO_CONSOLE = 8; // Max auto nel parcheggio stampabili su console

        const int MAX_PAUSA = 40;   // Sleep max per auto
        const int MIN_PAUSA = 20;   // Sleep min per auto

        static Random rnd = new Random(); // Definizione istanza random

        // Mutua Esclusione
        static Object lockConsole    = new Object();
        static Object lockParcheggio = new Object();
        static Object lockCorsia     = new Object();
        static Object lockPonte = new Object();

        static SemaphoreSlim semaphore = new SemaphoreSlim(NUM_AUTO_SUL_PONTE);

        // Variabili per i thread
        static List<Thread> passa = new List<Thread>(NUM_AUTO_SUL_PONTE); // Lista auto in transito
        static bool levatoio = true;                                      // Levatoio alto/basso
        static List<string> parcheggio = new List<string>();              // Auto nel parcheggio
        static bool[] corsia = new bool[NUM_AUTO_SUL_PONTE];              // Stato delle corsie libero
        static int n_auto_totali = 0;                                     // Numero di auto totali, per l'identificazione

        static bool exit = false; // Per terminare il programma

        #endregion

        #region Metodi per Console

        /// <summary>
        /// Stampa la mappa del programma
        /// </summary>
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

        /// <summary>
        /// Permette all'utente di inviare un input ed effettuare un'azione per il programma
        /// </summary>
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
                    AggiungiAutoDaParcheggio();
                    break;

                case 'C':
                    ChiudiPonte();
                    break;

                case 'O':
                    ApriPonte();
                    break;

                case 'E':
                    Scrivi(86, 6, "- Richiesta uscita", 0, ConsoleColor.DarkRed);
                    Environment.Exit(0);
                    exit = true;
                    break;

                default:
                    return;
            }
        }

        /// <summary>
        /// Stampa le auto nel parcheggio
        /// </summary>
        static void AggiornaParcheggio()
        {
            // Stampa delle auto che sono nel parcheggio
            for (int i = 0; i < MAX_AUTO_CONSOLE + 1; i++)
            {
                if (i < parcheggio.Count) Scrivi(10, 3 + i, parcheggio[i], 0); // Stampa l'auto
                else Scrivi(10, 3 + i, "                 ", 0); // Se le auto sono finite, stampa vuoto
            }
            if (parcheggio.Count > MAX_AUTO_CONSOLE) Scrivi(10, MAX_AUTO_CONSOLE + 3, "...              ", 0); // Se le auto sono troppe, stampa puntini
        }
        #endregion

        #region Metodi per Thread

        /// <summary>
        /// Apre il ponte lasciando le auto passare
        /// </summary>
        static void ApriPonte()
        {
            if (!levatoio) return;

            // Apri il ponte
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

        /// <summary>
        /// Una volta che tutte le macchine che stanno transitando sono passate, il ponte si chiude e ulteriori auto non possono passare
        /// </summary>
        static void ChiudiPonte()
        {
            if (levatoio) return;

            Scrivi(93, 4, "- Richiesta chiusura", 0, ConsoleColor.DarkRed);
            lock (lockPonte)
            {
                // Aspetta che le auto transitino
                for (int i = 0; i < passa.Count; i++)
                    passa[i].Join();

                // Chiudi il ponte
                levatoio = true;
            }
            Scrivi(93, 4, "                    ", 0);

            
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

        /// <summary>
        /// Aggiunge un auto al parcheggio attendendo di passare per il ponte
        /// </summary>
        static void AggiungiAutoDaParcheggio()
        {
            n_auto_totali++; // Aggiorna il numero di macchine ricevute
            lock (lockParcheggio)
            {
                // Aggiunta dell'auto
                string nome = $"Auto {n_auto_totali}";
                parcheggio.Add(nome);
                AggiornaParcheggio();

                // Creazione del thread
                Thread thAuto = new Thread(Auto);
                thAuto.Name = nome;
                thAuto.Start();
            }
        }

        /// <summary>
        /// Rimuove un auto dal parcheggio per passare poi dal ponte
        /// </summary>
        /// <param name="auto">Il nome dell'auto da rimuovere</param>
        static void RimuoviAutoDaParcheggio(string auto)
        {
            // Check se la macchina è nel parcheggio
            if (!parcheggio.Contains(auto))
                new Exception($"ERRORE : Impossibile trovare l'auto ''{auto}'' nel parcheggio.");

            // Rimozione
            lock (lockParcheggio)
            {
                parcheggio.Remove(auto);
                AggiornaParcheggio();
            }
        }

        /// <summary>
        /// Metodo per i thread auto. Attende che ci sia una corsia libera e che il ponte sia aperto, poi transita per il ponte
        /// </summary>
        static void Auto()
        {
            // Init
            int posAuto = 0;
            string auto = Thread.CurrentThread.Name;
            int pausa = rnd.Next(MIN_PAUSA, MAX_PAUSA);

            semaphore.Wait(); // Aspetta che si liberi una corsia

            lock (lockPonte) passa.Add(Thread.CurrentThread); // Informa il ponte che il thread deve transitare
            while (levatoio) { /* Aspettando che si apra il ponte */ }

            RimuoviAutoDaParcheggio(auto); // Rimuovi auto dal parcheggio

            // Cerca la corsia libera
            int rig = -1;
            lock (lockCorsia)
            {
                for (int i = 0; i < NUM_AUTO_SUL_PONTE; i++)
                    if (corsia[i])
                    {
                        rig = i;
                        corsia[i] = false; // Blocca la corsia
                        break;
                    }
            }

            do
            {
                // Posizione successiva animazione
                posAuto++;
                Scrivi(posAuto, rig + 13, ' ' + auto, pausa);

            } while (posAuto < 110); // Finchè non si raggiunge la destinazione (fine console)

            lock (lockCorsia) corsia[rig] = true; // Sblocca la corsia
            passa.Remove(Thread.CurrentThread); // Avvisa il ponte che il thread non deve transitare (Non bisogna mettere il lock!)
            semaphore.Release(); // Libera il posto
        }
        #endregion

        static void Main(string[] args)
        {
            // Init del programma
            OutputEncoding = Encoding.Unicode;
            CursorVisible = false;
            WriteLine("Fabio Fantini 4H 2024-11-04\n");
            StampaMappa();

            // Init delle corsie
            for (int i = 0; i < corsia.Length; i++)
                corsia[i] = true;

            // Accetta input in qualsiasi momento
            while(!exit)
            {
                if(KeyAvailable) AccettaComandi();
            }
        }
    }
}
