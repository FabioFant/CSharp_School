using System;
using System.Text;
using System.Threading;
using static System.Console;

namespace ConsoleAppRipasso
{
    internal class Program
    {
        static Random rnd = new Random();

        const int MIN_INTERVALLO = 2;
        const int MAX_INTERVALLO = 4;

        const int PUASA_TRENI = 10;
        const int PUASA_PEDONE = 50;

        static Thread thPedone;
        static Thread thTreno1;
        static Thread thTreno2;
        static Thread thManager;

        static object _lock = new object();

        static int posPedone = 0;

        static bool finito1;
        static bool finito2;

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

        static void Pedone()
        {
            do
            {

                if (thTreno1.IsAlive && posPedone == 24) thTreno1.Join();
                else if (thTreno2.IsAlive && posPedone == 89) thTreno2.Join();

                posPedone++;
                Scrivi(posPedone, 11, pedone[0], PUASA_PEDONE);
                Scrivi(posPedone, 12, pedone[1], PUASA_PEDONE);
                Scrivi(posPedone, 13, pedone[2], PUASA_PEDONE);


            } while (posPedone < 115);
        }

        static void Treno()
        {
            int col;
            switch (Thread.CurrentThread.Name)
            {
                case "Treno1":
                    col = 31;
                    break;

                case "Treno2":
                    col = 96;
                    break;
            }

            
        }

        static void Scrivi(int col, int rig, string mess, int sleep, ConsoleColor colore = ConsoleColor.White)
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

        static void Main(string[] args)
        {
            
        }
    }
}
