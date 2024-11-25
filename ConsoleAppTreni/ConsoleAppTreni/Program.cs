// Fabio Fantini 4H 2024-11-04
// Transito treni e blocco del personaggio, utilizzanbdo thread

// Librerie utilizzate
using System;
//using System.Diagnostics;
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

        // Intervallo min/max fra un transito e l'altro dei treni
        const int MINIMO_INTERVALLO = 5000;
        const int MASSIMO_INTERVALLO = 12000;
        static int pausaTransito1, pausaTransito2;

        // Lunghezza delle pause
        const int PAUSA_TRENI = 5;
        const int PAUSA_PEDONE = 50;

        // I thread
        static Thread thPedone;
        static Thread thTreno1;
        static Thread thTreno2;

        // Il lock per i vari thread
        static Object _lock = new Object();

        // Partenza 0, Arrivo 115
        static int posPedone = 0;

        // Personaggi
        static string[] pedone =
        {
            "  ☻",
            @" /▓\ ",
            @" / \",
        };

        //Input
        static string comando = "";
        #endregion

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
        static void Interfaccia()
        {
            //          1234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345
            WriteLine(@"                             |     |State treno 1                                             |     |State treno 2 "); // 1 
            WriteLine(@"                             |     |                                                          |     |              "); // 2
            WriteLine(@"                             |     |Is alive =                                                |     |Is alive      "); // 3
            WriteLine(@"                             |     |                                                          |     |              "); // 4
            WriteLine(@"                             |     |                                                          |     |              "); // 5
            WriteLine(@"                             |     |                                                          |     |              "); // 6
            WriteLine(@"                             1     |                                                          2     |              "); // 7
            WriteLine(@"                            ┌─┐   ┌─┐                                                        ┌─┐   ┌─┐             "); // 8
            WriteLine(@"                            └─┘   └─┘                                                        └─┘   └─┘             "); // 9
            WriteLine(@" ☻                                                                                                                 "); // 10
            WriteLine(@"/▓\                                                                                                                "); // 11
            WriteLine(@"/ \                                                                                                                "); // 12
            WriteLine(@"                            ┌─┐   ┌─┐                                                        ┌─┐   ┌─┐             "); // 13
            WriteLine(@"                            └─┘   └─┘                                                        └─┘   └─┘             "); // 14
            WriteLine(@"                             |     |                                                          |     |              "); // 15
            WriteLine(@"                             |     |                                                          |     |              "); // 16
            WriteLine(@"                             |     |                                                          |     |              "); // 17
            WriteLine(@"                             |     |                                                          |     |              "); // 18
            WriteLine(@"                             |     |                                                          |     |              "); // 19
            WriteLine(@"   (S) Suspend pedone        |     |                                                          |     |              "); // 20
            WriteLine(@"   (R) Resume pedone         |     |                                                          |     |              "); // 21
            WriteLine(@"   (A) Abort pedone          |     |                                                          |     |              "); // 22
            WriteLine(@"                             |     |                                                          |     |              "); // 23
            WriteLine(@"                             |     |                                                          |     |  State pedone"); // 24
            WriteLine(@"                             |     |                                                          |     |              "); // 25
            WriteLine(@"                             |     |                                                          |     |              "); // 26
            WriteLine(@"                             |     |                                                          |     |              "); // 27
        }
        static void Main(string[] args)
        {
            OutputEncoding = Encoding.Unicode;
            CursorVisible = false;
            WriteLine("Fabio Fantini 4H 2024-11-04\n");
            Interfaccia();

            // Calcola casualmente gli intervalli di transito dei 2 treni
            pausaTransito1 = rnd.Next(MINIMO_INTERVALLO, MASSIMO_INTERVALLO);
            pausaTransito2 = rnd.Next(MINIMO_INTERVALLO, MASSIMO_INTERVALLO);

            //TODO

            ReadKey(true);
        }
    }
}
