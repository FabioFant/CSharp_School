// Fabio Fantini 4H 2024-11-04
// Introduzione ai thread: gara fra Andrea, Baldo e Carlo dove tutti e 3 si muovono contemporaneamente

// Librerie utilizzate
using System;
using System.Diagnostics;
using System.Text;
using System.Threading;
using static System.Console;

namespace ConsoleAppThreadCorsa
{
    internal class Program
    {
        // Random e limiti del random
        static Random rnd = new Random();
        const int VELOCITA_MIN = 20;
        const int VELOCITA_MAX = 30;

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

        static void Pronti()
        {
            #region Andrea
            ForegroundColor = colAndrea;
            // Andrea     posizione in., colonna
            SetCursorPosition(posAndrea, 2);
            Write("Andrea");
            SetCursorPosition(posAndrea, 3);
            Write(andrea[0]);
            SetCursorPosition(posAndrea, 4);
            Write(andrea[1]); // @ per evitare problemi con \
            SetCursorPosition(posAndrea, 5);
            Write(andrea[2]);
            #endregion

            #region Baldo
            ForegroundColor = colBaldo;
            // Baldo     posizione in., colonna
            SetCursorPosition(posBaldo, 7);
            Write("Baldo");
            SetCursorPosition(posBaldo, 8);
            Write(baldo[0]);
            SetCursorPosition(posBaldo, 9);
            Write(baldo[1]); // @ per evitare problemi con \
            SetCursorPosition(posBaldo, 10);
            Write(baldo[2]);
            #endregion

            #region Carlo
            ForegroundColor = colCarlo;
            // Carlo     posizione in., colonna
            SetCursorPosition(posCarlo, 12);
            Write("Carlo");
            SetCursorPosition(posCarlo, 13);
            Write(carlo[0]);
            SetCursorPosition(posCarlo, 14);
            Write(carlo[1]); // @ per evitare problemi con \
            SetCursorPosition(posCarlo, 15);
            Write(carlo[2]);
            #endregion
        }
        static void Andrea()
        {
            do
            { // Cambio di posizione con stampa rallentata da una piccola pausa fino ad arrivare al traguardo
                posAndrea++;
                Thread.Sleep(velAndrea);
                lock(_lock)
                {
                    ForegroundColor = colAndrea;
                    SetCursorPosition(posAndrea, 3);
                    Write(andrea[0]);
                }
                Thread.Sleep(velAndrea);
                lock (_lock)
                {
                    ForegroundColor = colAndrea;
                    SetCursorPosition(posAndrea, 4);
                    Write(andrea[1]);
                }
                Thread.Sleep(velAndrea);
                lock (_lock)
                {
                    ForegroundColor = colAndrea;
                    SetCursorPosition(posAndrea, 5);
                    Write(andrea[2]);
                }
                Thread.Sleep(velAndrea);

            } while (posAndrea < 115);
            lock (_lock)
            {
                classifica++;
                ForegroundColor = ConsoleColor.White;
                SetCursorPosition(115, 2);
                Write(classifica);
            }
        }
        static void Baldo()
        {
            do // Cambio di posizione con stampa rallentata da una piccola pausa fino ad arrivare al traguardo
            {
                posBaldo++;
                Thread.Sleep(velBaldo);
                lock (_lock)
                {
                    ForegroundColor = colBaldo;
                    SetCursorPosition(posBaldo, 8);
                    Write(baldo[0]);
                }
                Thread.Sleep(velBaldo);
                lock (_lock)
                {
                    ForegroundColor = colBaldo;
                    SetCursorPosition(posBaldo, 9);
                    Write(baldo[1]);
                }
                Thread.Sleep(velBaldo);
                lock (_lock)
                {
                    ForegroundColor = colBaldo;
                    SetCursorPosition(posBaldo, 10);
                    Write(baldo[2]);
                }
                Thread.Sleep(velBaldo);

            } while (posBaldo < 115);
            lock (_lock)
            {
                classifica++;
                ForegroundColor = ConsoleColor.White;
                SetCursorPosition(115, 7);
                Write(classifica);
            }
        }
        static void Carlo()
        {

            do // Cambio di posizione con stampa rallentata da una piccola pausa fino ad arrivare al traguardo
            {
                posCarlo++;
                Thread.Sleep(velCarlo);
                lock (_lock)
                {
                    ForegroundColor = colCarlo;
                    SetCursorPosition(posCarlo, 13);
                    Write(carlo[0]);
                }
                Thread.Sleep(velCarlo);
                lock (_lock)
                {
                    ForegroundColor = colCarlo;
                    SetCursorPosition(posCarlo, 14);
                    Write(carlo[1]);
                }
                Thread.Sleep(velCarlo);
                lock (_lock)
                {
                    ForegroundColor = colCarlo;
                    SetCursorPosition(posCarlo, 15);
                    Write(carlo[2]);
                }
                Thread.Sleep(velCarlo);

            } while (posCarlo < 115);
            lock (_lock)
            {
                classifica++;
                ForegroundColor = ConsoleColor.White;
                SetCursorPosition(115, 12);
                Write(classifica);
            }
        }

        static void Main(string[] args)
        {
            OutputEncoding = Encoding.Unicode;
            CursorVisible = false;
            WriteLine("Fabio Fantini 4H 2024-11-04");

            Pronti(); // Visualizza la grafica di partenza (le 3 persone)

            // Determina le velocità
            velAndrea = rnd.Next(VELOCITA_MIN, VELOCITA_MAX);
            velBaldo = rnd.Next(VELOCITA_MIN, VELOCITA_MAX);
            velCarlo = rnd.Next(VELOCITA_MIN, VELOCITA_MAX);

            // Creazione dei thread
            Thread thAndrea = new Thread(Andrea);
            Thread thBaldo = new Thread(Baldo);
            Thread thCarlo = new Thread(Carlo);

            // Inizio di esecuzione dei thread, codice non bloccante
            // I thread vanno in conflitto perchè usano risorse comuni
            // Quindi utilizzaranno un testimone con lock
            thAndrea.Start();
            thCarlo.Start();
            thBaldo.Start();

            ReadKey();
        }
    }
}
