using System;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using static System.Console;

namespace ConsoleAppRipasso
{
    internal class Program
    {
        static Random rnd = new Random();
        const int VELOCITA_MAX = 50;
        const int VELOCITA_MIN = 40;
        const int MEDIA_VEL = VELOCITA_MAX + VELOCITA_MIN / 2;

        static Thread thAndrea;
        static Thread thBaldo;
        static Thread thCarlo;

        static int velAndrea;
        static int velBaldo;
        static int velCarlo;

        static Object _lock = new Object();

        static int posAndrea = 0;
        static int posBaldo = 0;
        static int posCarlo = 0;
        static int classifica = 0;

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

        static ConsoleColor colAndrea = ConsoleColor.Red;
        static ConsoleColor colBaldo = ConsoleColor.Green;
        static ConsoleColor colCarlo = ConsoleColor.Blue;

        static string comando = "";

        static void Scrivi(int col, int rig, string mess, int sleep, ConsoleColor colore)
        {
            Thread.Sleep(sleep);
            lock (_lock)
            {
                SetCursorPosition(col, rig);
                ForegroundColor = colore;
                Write(mess);
            }
            ForegroundColor = ConsoleColor.White;
        }
        static void Stato()
        {
            Scrivi(1, 2, "Andrea -> " + thAndrea.ThreadState + "                      ", MEDIA_VEL, colAndrea);
            Scrivi(50, 2, "Is alive -> " + thAndrea.IsAlive + "                      ", MEDIA_VEL, colAndrea);

            Scrivi(1, 6, "Baldo -> " + thBaldo.ThreadState + "                      ", MEDIA_VEL, colBaldo);
            Scrivi(50, 6, "Is alive -> " + thBaldo.IsAlive + "                      ", MEDIA_VEL, colBaldo);

            Scrivi(1, 10, "Carlo -> " + thCarlo.ThreadState + "                      ", MEDIA_VEL, colCarlo);
            Scrivi(50, 10, "Is alive -> " + thCarlo.IsAlive + "                      ", MEDIA_VEL, colCarlo);
        }
        static void Menu(string titolo, int col)
        {
            Scrivi(col, 20, titolo + "                                                                                           ", 0, ConsoleColor.White);
            Scrivi(col, 22, "Andrea (A)                                                                                          ", 0, ConsoleColor.White);
            Scrivi(col, 23, "Baldo (B)                                                                                           ", 0, ConsoleColor.White);
            Scrivi(col, 24, "Carlo (C)                                                                                           ", 0, ConsoleColor.White);
            Scrivi(col, 25, "                                                                                                    ", 0, ConsoleColor.White);
        }
        static char MenuAzioni(string titolo, int col)
        {
            char c;
            Scrivi(col, 20, titolo, 0, ConsoleColor.White);
            Scrivi(col, 22, "Sospendere (S)  ", 0, ConsoleColor.White);
            Scrivi(col, 23, "Riprendere (R)  ", 0, ConsoleColor.White);
            Scrivi(col, 24, "Abort (A)       ", 0, ConsoleColor.White);
            Scrivi(col, 25, "Aspetta (J)     ", 0, ConsoleColor.White);
            c = char.ToUpper(ReadKey(true).KeyChar);
            return c;
        }
        static void AccettaComandi()
        {
            Thread thAzione = null;
            comando = "";
            char choice = ' ';

            choice = ReadKey(true).KeyChar;
            choice = char.ToUpper(choice);

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
                    break;
            }
            comando += choice;

            choice = MenuAzioni("AZIONE SU " + thAzione.Name + "          ", 33);

            switch (choice)
            {
                case 'S':
                    lock(_lock)
                    {
                        if (thAzione.ThreadState == ThreadState.Running || thAzione.ThreadState == ThreadState.WaitSleepJoin)
                            thAzione.Suspend();
                    }
                    break;
                case 'R':
                    if (thAzione.ThreadState == ThreadState.Suspended)
                        thAzione.Resume();
                    break;
                case 'A':
                    lock (_lock)
                    {
                        if (thAzione.ThreadState == ThreadState.Running || thAzione.ThreadState == ThreadState.WaitSleepJoin)
                            thAzione.Abort();
                    }
                    break;
                case 'J':
                    comando += choice;
                    Menu("CHI ASPETTA " + thAzione.Name + " ?", 63);
                    choice = char.ToUpper(ReadKey(true).KeyChar);
                    comando += choice;
                    break;

                default:
                    break;
            }
        }
        static void Pronti()
        {
            Scrivi(posAndrea, 3, andrea[0], 0, colAndrea);
            Scrivi(posAndrea, 4, andrea[1], 0, colAndrea);
            Scrivi(posAndrea, 5, andrea[2], 0, colAndrea);

            Scrivi(posBaldo, 7, baldo[0], 0, colBaldo);
            Scrivi(posBaldo, 8, baldo[1], 0, colBaldo);
            Scrivi(posBaldo, 9, baldo[2], 0, colBaldo);

            Scrivi(posCarlo, 11, carlo[0], 0, colCarlo);
            Scrivi(posCarlo, 12, carlo[1], 0, colCarlo);
            Scrivi(posCarlo, 13, carlo[2], 0, colCarlo);
        }
        static void Andrea()
        {
            int posAndrea = 0;
            do
            {
                if(comando.Length == 3)
                    if (comando[1] == 'J' && comando[0] == 'A')
                        switch (comando[2])
                        {
                            case 'B':
                                thBaldo.Join();
                                break;
                            case 'C':
                                thCarlo.Join();
                                break;

                            default:
                                break;
                        }

                posAndrea++;
                Scrivi(posAndrea, 3, andrea[0], velAndrea, colAndrea);
                Scrivi(posAndrea, 4, andrea[1], velAndrea, colAndrea);
                Scrivi(posAndrea, 5, andrea[2], velAndrea, colAndrea);
            } while (posAndrea < 115);
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
                if (comando.Length == 3)
                    if (comando[1] == 'J' && comando[0] == 'B')
                        switch (comando[2])
                        {
                            case 'A':
                                thAndrea.Join();
                                break;
                            case 'C':
                                thCarlo.Join();
                                break;

                            default:
                                break;
                        }

                posBaldo++;
                Scrivi(posBaldo, 7, baldo[0], velBaldo, colBaldo);
                Scrivi(posBaldo, 8, baldo[1], velBaldo, colBaldo);
                Scrivi(posBaldo, 9, baldo[2], velBaldo, colBaldo);
            } while (posBaldo < 115);
            lock (_lock)
            {
                classifica++;
                Scrivi(posBaldo, 6, $"{classifica}", 0, ConsoleColor.White);
            }
        }
        static void Carlo()
        {
            int posCarlo = 0;
            do
            {
                if (comando.Length == 3)
                    if (comando[1] == 'J' && comando[0] == 'C')
                        switch (comando[2])
                        {
                            case 'B':
                                thBaldo.Join();
                                break;
                            case 'A':
                                thAndrea.Join();
                                break;

                            default:
                                break;
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

        static void Main(string[] args)
        {
            OutputEncoding = Encoding.Unicode;
            CursorVisible = false;
            WriteLine("Fabio Fantini 4H 2024-11-04");

            velAndrea = rnd.Next(VELOCITA_MIN, VELOCITA_MAX);
            velBaldo = rnd.Next(VELOCITA_MIN, VELOCITA_MAX);
            velCarlo = rnd.Next(VELOCITA_MIN, VELOCITA_MAX);

            thAndrea = new Thread(Andrea);
            thAndrea.Name = "Andrea";
            thBaldo = new Thread(Baldo);
            thBaldo.Name = "Baldo";
            thCarlo = new Thread(Carlo);
            thCarlo.Name = "Carlo";

            Pronti();
            Stato();

            Scrivi(27, 16, "Premi un tasto per iniziare!", 0, ConsoleColor.DarkYellow);
            ReadKey(true);
            Scrivi(27, 16, "Gara in corso...            ", 0, ConsoleColor.DarkYellow);
            Menu("MENU'", 3);

            thAndrea.Start();
            thBaldo.Start();
            thCarlo.Start();

            do
            {
                Menu("MENU'", 3);
                Stato();
                if(KeyAvailable) AccettaComandi();
            } while (thAndrea.IsAlive || thBaldo.IsAlive || thCarlo.IsAlive);
            Stato();

            Scrivi(27, 16, "Premi un tasto per uscire! ", 0, ConsoleColor.DarkYellow);
            ReadKey(true);
        }
    }
}
