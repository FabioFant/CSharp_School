// Fabio Fantini 4H 2024-11-04
// Transito treni e blocco del pedone, utilizzando i thread

// Librerie utilizzate
using System;
using System.Text;
using System.Threading;
using static System.Console;

namespace ConsoleAppTreni
{
    internal class Program
    {
        #region Variabili globali
        // Istanza random
        static Random rnd = new Random();

        // Intervallo min/max fra un transito e l'altro dei treni (sec)
        const int MINIMO_INTERVALLO = 2;
        const int MASSIMO_INTERVALLO = 4;

        // Lunghezza delle pause; velocità dei thread
        const int PAUSA_TRENI = 10;
        const int PAUSA_PEDONE = 50;

        // I thread
        static Thread thPedone;
        static Thread thTreno1;
        static Thread thTreno2;
        static Thread thManager;

        // Il lock per la mutua esclusione
        static Object _lock = new Object();

        // Posizioni del pedone e treni
        static int posPedone = 0;
        static int posTreno1;
        static int posTreno2;

        // Determinano se il transito dei treni è terminato 
        static bool finito1 = false;
        static bool finito2 = false;

        // Grafiche
        static string[] pedone =
        {
            "  ☻",
            @" /▓\ ",
            @" / \",
        };
        static string[] treno =
        {
            "╔═╗",
            "║ ║",
            "╚╦╝",
            "╔╩╗",
            "║ ║",
            "╚╦╝",
            "╔╩╗",
            "║ ║",
            "╚═╝",
        };
        #endregion

        #region Metodi per console
        static void Scrivi(int col, int rig, string mess, int sleep, ConsoleColor colore = ConsoleColor.White)
        {
            // Attesa
            Thread.Sleep(sleep);
            lock (_lock)
            {
                // Posizione cursore, colore e scrittura
                SetCursorPosition(col, rig);
                ForegroundColor = colore;
                Write(mess);
            }
            // Colore di default
            ForegroundColor = ConsoleColor.White;
        }
        static void Stato()
        {
            // Treno 1
            Scrivi(37, 3, thTreno1.ThreadState + "              ", PAUSA_TRENI);
            Scrivi(37, 5, thTreno1.IsAlive + "              ", PAUSA_TRENI);

            // Treno 2
            Scrivi(102, 3, thTreno2.ThreadState + "              ", PAUSA_TRENI);
            Scrivi(102, 5, thTreno2.IsAlive + "              ", PAUSA_TRENI);

            // Pedone
            Scrivi(102, 26, thPedone.ThreadState + "              ", PAUSA_PEDONE);
        }
        static void Interfaccia()
        {
            //          1234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345
            WriteLine(@"                             |     |State treno 1                                             |     |State treno 2 "); // 2 
            WriteLine(@"                             |     |                                                          |     |              "); // 3
            WriteLine(@"                             |     |Is alive =                                                |     |Is alive =    "); // 4
            WriteLine(@"                             |     |                                                          |     |              "); // 5
            WriteLine(@"                             |     |                                                          |     |              "); // 6
            WriteLine(@"                             |     |                                                          |     |              "); // 7
            WriteLine(@"                             1     ■                                                          2     ■              "); // 8
            WriteLine(@"                            ┌─┐   ┌─┐                                                        ┌─┐   ┌─┐             "); // 9
            WriteLine(@"                            └─┘   └─┘                                                        └─┘   └─┘             "); // 10
            WriteLine(@"                                                                                                                   "); // 11
            WriteLine(@"                                                                                                                   "); // 12
            WriteLine(@"                                                                                                                   "); // 13
            WriteLine(@"                            ┌─┐   ┌─┐                                                        ┌─┐   ┌─┐             "); // 14
            WriteLine(@"                            └─┘   └─┘                                                        └─┘   └─┘             "); // 15
            WriteLine(@"                             |     |                                                          |     |              "); // 16
            WriteLine(@"                             |     |                                                          |     |              "); // 17
            WriteLine(@"                             |     |                                                          |     |              "); // 18
            WriteLine(@"                             |     |                                                          |     |              "); // 19
            WriteLine(@"                             |     |                                                          |     |              "); // 20
            WriteLine(@"   (S) Suspend pedone        |     |                                                          |     |              "); // 21
            WriteLine(@"   (R) Resume pedone         |     |                                                          |     |              "); // 22
            WriteLine(@"   (A) Abort pedone          |     |                                                          |     |              "); // 23
            WriteLine(@"                             |     |                                                          |     |              "); // 24
            WriteLine(@"                             |     |                                                          |     |State pedone  "); // 25
            WriteLine(@"                             |     |                                                          |     |              "); // 26
            WriteLine(@"                             |     |                                                          |     |              "); // 27
            WriteLine(@"                             |     |                                                          |     |              "); // 28
            Scrivi(35, 8, "■", 0, ConsoleColor.Green);
            Scrivi(100, 8, "■", 0, ConsoleColor.Green);
        }
        #endregion

        #region Metodi per i Thread
        static void Pedone()
        {
            do
            {
                // Se il semaforo è rosso, il pedone resta fermo
                if ((thTreno1.IsAlive && posPedone == 24) || (thTreno2.IsAlive && posPedone == 89))
                    continue;

                // Posizione successiva animazione
                posPedone++;
                Scrivi(posPedone, 11, pedone[0], PAUSA_PEDONE);
                Scrivi(posPedone, 12, pedone[1], PAUSA_PEDONE);
                Scrivi(posPedone, 13, pedone[2], PAUSA_PEDONE);

            } while (posPedone < 115); // Finchè non si raggiunge la destinazione (fine console)
        }
        static void Treno1()
        {
            // Semaforo rosso
            Scrivi(35, 8, "■", 0, ConsoleColor.Red);
            Scrivi(29, 11, "|", 0);
            Scrivi(29, 12, "|", 0);
            Scrivi(29, 13, "|", 0);

            // Stampa entrata treno nella console
            for (int i = 1; i <= 9; i++) // i = numero di strati da stampare
            {
                for (int j = i; j > 0; j--) // j = numero di strati rimanenti da stampare
                {
                    Scrivi(31, j + 1, treno[treno.Length - (i - j) - 1], PAUSA_TRENI);
                }
            }

            // Transito del treno
            posTreno1 = 1;
            do
            {
                int currY = posTreno1;
                foreach(string strato in treno)
                {
                    if (currY == 28) // fine della console verticalmente
                        break;
                    Scrivi(31, ++currY, strato, PAUSA_TRENI); // scrittura della riga del treno
                }
                Scrivi(31, ++posTreno1, "   ", PAUSA_TRENI);

            } while (posTreno1 < 28);

            // Semaforo verde
            Scrivi(35, 8, "■", 0, ConsoleColor.Green);
            Scrivi(29, 11, " ", 0);
            Scrivi(29, 12, " ", 0);
            Scrivi(29, 13, " ", 0);

            finito1 = true; // Avviso il Main che ho terminato
        }
        static void Treno2()
        {
            // Semaforo rosso
            Scrivi(100, 8, "■", 0, ConsoleColor.Red);
            Scrivi(94, 11, "|", 0);
            Scrivi(94, 12, "|", 0);
            Scrivi(94, 13, "|", 0);
            
            // Stampa entrata del trano nella console
            for (int i = 1; i <= 9; i++) // i = numero di strati da stampare
            {
                for (int j = i; j > 0; j--) // j = numero di strati rimanenti da stampare
                {
                    Scrivi(96, j + 1, treno[treno.Length - (i - j) - 1], PAUSA_TRENI);
                }
            }

            // Transito del treno
            posTreno2 = 1;
            do
            {
                int currY = posTreno2;
                foreach (string strato in treno)
                {
                    if (currY == 28) // fine della console verticalmente
                        break;
                    Scrivi(96, ++currY, strato, PAUSA_TRENI); // scrittura della riga del treno
                }
                Scrivi(96, ++posTreno2, "   ", PAUSA_TRENI);

            } while (posTreno2 < 28);

            // Semaforo verde
            Scrivi(100, 8, "■", 0, ConsoleColor.Green);
            Scrivi(94, 11, " ", 0);
            Scrivi(94, 12, " ", 0);
            Scrivi(94, 13, " ", 0);

            finito2 = true; // Avviso il Main che ho terminato
        }
        static void Manager()
        {
            do
            {
                Stato(); // Aggiorna lo stato
                if (KeyAvailable) AccettaComandi(); // Leggi input

            } while (thPedone.IsAlive);
            Stato();
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
                case 'S': // Suspend
                    lock (_lock)
                    {
                        // Controllo dello stato
                        if (thPedone.ThreadState.Equals(ThreadState.Running) || thPedone.ThreadState.Equals(ThreadState.WaitSleepJoin))
                            thPedone.Suspend();
                    }
                    break;
                case 'R': // Resume
                    lock (_lock)
                    {
                        // Controllo dello stato
                        if (thPedone.ThreadState.Equals(ThreadState.Suspended))
                            thPedone.Resume();
                    }
                    break;
                case 'A': // Abort
                    lock (_lock)
                    {
                        // Controllo dello stato
                        if (thPedone.ThreadState.Equals(ThreadState.Running) || thPedone.ThreadState.Equals(ThreadState.WaitSleepJoin))
                            thPedone.Abort();
                    }
                    break;

                default:
                    return;
            }
        }
        #endregion

        static void Main(string[] args)
        {
            #region Init
            // Init del programma
            OutputEncoding = Encoding.Unicode;
            CursorVisible = false;
            WriteLine("Fabio Fantini 4H 2024-11-04\n");
            Interfaccia();

            // Init dei thread
            thPedone = new Thread(Pedone);
            thManager = new Thread(Manager);
            thTreno1 = new Thread(Treno1);
            thTreno2 = new Thread(Treno2);

            // Assegnazione nomi
            thPedone.Name = "Pedone";
            thTreno1.Name = "Treno1";
            thTreno2.Name = "Treno2";
            thManager.Name = "Manager";
            #endregion

            // Start
            Scrivi(50, 24, "Premi un tasto per iniziare!", 0, ConsoleColor.DarkYellow);
            ReadKey(true);
            Scrivi(50, 24, "                           ", 0, ConsoleColor.DarkYellow);

            // Avvio dei thread
            thManager.Start();
            thPedone.Start();

            int pausa1 = rnd.Next(MINIMO_INTERVALLO, MASSIMO_INTERVALLO + 1), pausa2 = rnd.Next(MINIMO_INTERVALLO, MASSIMO_INTERVALLO + 1); // Pausa di transito dei treni
            int count1 = 0, count2 = 0; // Contano i secondi passati dall'inizio della pausa
            do
            {
                // Conto un secondo passato
                count1++;
                count2++;

                #region Treno 1
                // Attivo il Treno1 quando finisce la pusa
                if (pausa1 == count1)
                {
                    thTreno1 = new Thread(Treno1);
                    thTreno1.Name = "Treno1";
                    thTreno1.Start();
                }
                // Quando il Treno1 ha transitato, determino una nuova pausa
                if (finito1)
                {
                    pausa1 = rnd.Next(MINIMO_INTERVALLO, MASSIMO_INTERVALLO + 1);
                    count1 = 0;
                    finito1 = false;
                }
                #endregion

                #region Treno2
                // Attivo il Treno2 quando finisce la pusa
                if (pausa2 == count2)
                {
                    thTreno2 = new Thread(Treno2);
                    thTreno2.Name = "Treno2";
                    thTreno2.Start();
                }
                // Quando il Treno2 ha transitato, determino una nuova pausa
                if (finito2)
                {
                    pausa2 = rnd.Next(MINIMO_INTERVALLO, MASSIMO_INTERVALLO + 1);
                    count2 = 0;
                    finito2 = false;
                }
                #endregion

                Thread.Sleep(1000); // Passa un secondo

            } while (thPedone.IsAlive); // Termina quando il pedone arriva a destinazione e tutti i treni hanno transitato

            // Fine
            Scrivi(50, 24, "Premi un tasto per terminare!", 0, ConsoleColor.DarkYellow);
            ReadKey(true);
            Scrivi(50, 24, "     Chiusura in corso...    ", 0, ConsoleColor.DarkYellow);
        }
    }
}
