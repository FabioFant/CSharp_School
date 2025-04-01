// Fabio Fantini 4H 2024-11-04
// Introduzione ai thread: gara fra Andrea, Baldo e Carlo dove tutti e 3 si muovono contemporaneamente

// Librerie utilizzate
using System;
//using System.Diagnostics;
using System.Text;
using System.Threading;
using static System.Console;

namespace ConsoleAppThreadCorsa
{
    internal class Program
    {
        #region Variabili globali
        // Random e limiti del random
        static Random rnd = new Random();
        const int VELOCITA_MIN = 40;
        const int VELOCITA_MAX = 50;
        const int MEDIA_VEL = VELOCITA_MIN + VELOCITA_MAX / 2;

        // I thread
        static Thread thAndrea;
        static Thread thBaldo;
        static Thread thCarlo;

        // Memo delle velocità di ogni persona
        static int velAndrea;
        static int velBaldo;
        static int velCarlo;

        // Testimone, il thread può utilizzare le risorse solo se possiede il lock.
        // All'interno del lock ci devono essere meno istruzioni possibili, altrimenti
        // si perde troppo tempo
        static Object _lock = new Object();

        // Partenza 0, Traguardo 115
        static int posAndrea = 0;
        static int posBaldo = 0;
        static int posCarlo = 0;
        static int classifica = 0;

        // Personaggi
        static string[] andrea =
        {
            "  ▼",
            @" /▓\ ",
            @" / \",
        };
        static string[] baldo =
{
            "  ♥",
            @" /▓\ ",
            @" / \",
        };
        static string[] carlo =
{
            "  ☺",
            @" /▓\ ",
            @" / \",
        };

        // Colore per ogni persona
        static ConsoleColor colAndrea = ConsoleColor.Red;
        static ConsoleColor colBaldo = ConsoleColor.Green;
        static ConsoleColor colCarlo = ConsoleColor.Blue;

        //Input
        static string comando = "";
        #endregion

        #region Metodi per Console e Input
        static void Scrivi(int col, int rig, string mess, int sleep, ConsoleColor colore)
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
            // Andrea
            Scrivi(1, 2, "Andrea -> " + thAndrea.ThreadState + "                      ", MEDIA_VEL, colAndrea);
            Scrivi(50, 2, "Is alive = " + thAndrea.IsAlive + "                      ", MEDIA_VEL, colAndrea);

            // Baldo
            Scrivi(1, 6, "Baldo -> " + thBaldo.ThreadState + "                      ", MEDIA_VEL, colBaldo);
            Scrivi(50, 6, "Is Alive = " + thBaldo.IsAlive + "                      ", MEDIA_VEL, colBaldo);

            // Carlo
            Scrivi(1, 10, "Carlo -> " + thCarlo.ThreadState + "                      ", MEDIA_VEL, colCarlo);
            Scrivi(50, 10, "Is alive = " + thCarlo.IsAlive + "                      ", MEDIA_VEL, colCarlo);
        }
        static void Menu(string titolo, int col)
        {
            Scrivi(col, 20, titolo + "                                                                                          ", 0, ConsoleColor.White);
            Scrivi(col, 22, "Andrea (A)                                                                                         ", 0, ConsoleColor.White);
            Scrivi(col, 23, "Baldo (B)                                                                                          ", 0, ConsoleColor.White);
            Scrivi(col, 24, "Carlo (C)                                                                                          ", 0, ConsoleColor.White);
            Scrivi(col, 25, "                                                                                                   ", 0, ConsoleColor.White);
        }
        static char MenuAzioni(string titolo, int col)
        {
            char c;
            Scrivi(col, 20, titolo, 0, ConsoleColor.White);
            Scrivi(col, 22, "Sospendere (S)  ", 0, ConsoleColor.White);
            Scrivi(col, 23, "Riprendere (R)  ", 0, ConsoleColor.White);
            Scrivi(col, 24, "Abort      (A)  ", 0, ConsoleColor.White);
            Scrivi(col, 25, "Aspetta    (J)  ", 0, ConsoleColor.White);
            c = ReadKey(true).KeyChar;
            return c = char.ToUpper(c);
        }
        static void AccettaComandi()
        {
            Thread thAzione;
            comando = "";
            char choice = ' ';

            // Legge il comando utente
            choice = ReadKey(true).KeyChar;
            choice = char.ToUpper(choice);

            // Memorizza il primo thread coinvolto nel comando
            switch (choice)
            {
                case 'A':
                    thAzione = thAndrea;
                    break;
                case 'B':
                    thAzione = thBaldo;
                    break;
                case 'C':
                    thAzione = thCarlo;
                    break;

                default:
                    return;
            }
            comando += choice;

            // Legge l'azione da intraprendere
            choice = MenuAzioni("AZIONE SU " + thAzione.Name + "          ", 33); // 33

            //Compie l'azione richiesta
            switch (choice)
            {
                case 'S':
                    lock (_lock)
                    {
                        if(thAzione.ThreadState.Equals(ThreadState.Running) || thAzione.ThreadState.Equals(ThreadState.WaitSleepJoin))
                            thAzione.Suspend();
                    }
                    break;
                case 'R':
                    if(thAzione.ThreadState.Equals(ThreadState.Suspended))
                        thAzione.Resume();
                    break;
                case 'A':
                    lock (_lock)
                    {
                        if(thAzione.ThreadState.Equals(ThreadState.Running) || thAzione.ThreadState.Equals(ThreadState.WaitSleepJoin))
                            thAzione.Abort();
                    }
                    break;
                case 'J': // Termina e prepara il comando per i thread
                    comando += choice;
                    Menu("CHI ASPETTA " + thAzione.Name + " ?", 63); // 63
                    choice = char.ToUpper(ReadKey(true).KeyChar);
                    comando += choice;
                    break;

                default:
                    return;
            }
        }
        static void Pronti()
        {
            // Andrea
            Scrivi(posAndrea, 3, andrea[0], 0, colAndrea);
            Scrivi(posAndrea, 4, andrea[1], 0, colAndrea);
            Scrivi(posAndrea, 5, andrea[2], 0, colAndrea);

            // Baldo
            Scrivi(posBaldo, 7, andrea[0], 0, colBaldo);
            Scrivi(posBaldo, 8, andrea[1], 0, colBaldo);
            Scrivi(posBaldo, 9, andrea[2], 0, colBaldo);

            // Carlo
            Scrivi(posCarlo, 11, carlo[0], 0, colCarlo);
            Scrivi(posCarlo, 12, carlo[1], 0, colCarlo);
            Scrivi(posCarlo, 13, carlo[2], 0, colCarlo);
        }
        #endregion

        #region Metodi per i Thread
        static void Andrea()
        {
            int posAndrea = 0;
            do
            { 
                //Ritardo(1)
                if (comando.Length == 3) // Processa ed esegue il comando
                    if (comando[1] == 'J' && comando[0] == 'A') // non serve &&
                        switch (comando[2])
                        {
                            case 'B':
                                thBaldo.Join(); // Bloccante
                                break;
                            case 'C':
                                thCarlo.Join();
                                break;

                            default:
                                return;
                        }
                // Posizione successiva animazione
                posAndrea++;
                Scrivi(posAndrea, 3, andrea[0], velAndrea, colAndrea);
                Scrivi(posAndrea, 4, andrea[1], velAndrea, colAndrea);
                Scrivi(posAndrea, 5, andrea[2], velAndrea, colAndrea);

            } while (posAndrea < 115);
            // Stampa della classifica alla fine
            lock (_lock)
            {
                classifica++;
                Scrivi(posAndrea, 2, $"{classifica}", 0, ConsoleColor.White);
            }
        }
        static void Baldo()
        {
            int posBaldo = 0;
            do
            {
                //Ritardo(1)
                if (comando.Length == 3) // Processa ed esegue il comando
                    if (comando[1] == 'J' && comando[0] == 'B') // non serve &&
                        switch (comando[2])
                        {
                            case 'A':
                                thAndrea.Join(); // Bloccante
                                break;
                            case 'C':
                                thCarlo.Join();
                                break;

                            default:
                                return;
                        }
                // Posizione successiva animazione
                posBaldo++;
                Scrivi(posBaldo, 7, baldo[0], velBaldo, colBaldo);
                Scrivi(posBaldo, 8, baldo[1], velBaldo, colBaldo);
                Scrivi(posBaldo, 9, baldo[2], velBaldo, colBaldo);

            } while (posBaldo < 115);
            // Stampa della classifica alla fine
            lock (_lock)
            {
                classifica++;
                Scrivi(posBaldo, 6, $"{classifica}", 0, ConsoleColor.White);
            }
        }
        static void Carlo()
        {
            int posCarlo = 0;
            do // Cambio di posizione con stampa rallentata da una piccola pausa fino ad arrivare al traguardo
            {
                //Ritardo(1)
                if (comando.Length == 3) // Processa ed esegue il comando
                    if (comando[1] == 'J' && comando[0] == 'C') // non serve &&
                        switch (comando[2])
                        {
                            case 'A':
                                thAndrea.Join(); // Bloccante
                                break;
                            case 'B':
                                thBaldo.Join();
                                break;

                            default:
                                return;
                        }
                posCarlo++;
                Scrivi(posCarlo, 11, carlo[0], velCarlo, colCarlo);
                Scrivi(posCarlo, 12, carlo[1], velCarlo, colCarlo);
                Scrivi(posCarlo, 13, carlo[2], velCarlo, colCarlo);

            } while (posCarlo < 115);
            lock (_lock)
            {
                classifica++;
                Scrivi(posCarlo, 10, $"{classifica}", 0, ConsoleColor.White);
            }
        }
        #endregion

        static void Main(string[] args)
        {
            OutputEncoding = Encoding.Unicode;
            CursorVisible = false;
            WriteLine("Fabio Fantini 4H 2024-11-04");

            // Determina le velocità
            velAndrea = rnd.Next(VELOCITA_MIN, VELOCITA_MAX);
            velBaldo = rnd.Next(VELOCITA_MIN, VELOCITA_MAX);
            velCarlo = rnd.Next(VELOCITA_MIN, VELOCITA_MAX);

            // Creazione dei thread con nome
            thAndrea = new Thread(Andrea);
            thAndrea.Name = "Andrea";
            thBaldo = new Thread(Baldo);
            thBaldo.Name = "Baldo";
            thCarlo = new Thread(Carlo);
            thCarlo.Name = "Carlo";

            // Visualizza la grafica di partenza
            Pronti();
            Stato();

            Scrivi(27, 16, "Premi un tasto per iniziare!", 0, ConsoleColor.DarkYellow);
            ReadKey(true);
            Scrivi(27, 16, "Gara in corso...            ", 0, ConsoleColor.DarkYellow);
            Menu("MENU'", 3);

            // Inizio di esecuzione dei thread, codice non bloccante
            // I thread vanno in conflitto perchè usano risorse comuni
            // Quindi utilizzaranno un testimone con lock
            thAndrea.Start();
            thCarlo.Start();
            thBaldo.Start();

            // Aggiornamento stato finchè i thread sono alive
            do
            {
                Menu("MENU'", 3);
                Stato();
                if (KeyAvailable) AccettaComandi();
            } while (thAndrea.IsAlive || thBaldo.IsAlive || thCarlo.IsAlive);
            Stato();

            Scrivi(27, 16, "Premi un tasto per uscire!", 0, ConsoleColor.DarkYellow);
            ReadKey(true);
        }
    }
}
