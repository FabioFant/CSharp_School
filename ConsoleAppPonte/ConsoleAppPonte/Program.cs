// Fabio Fantini 4H 2025-01-20
// Ponte con apertura / chiusura per far passare le auto.
// Svolgimento con semafori

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleAppPonte
{
    internal class Program
    {
        const int NUM_AUTO_SUL_PONTE = 4; // Portata massima del ponte in numero di auto

        // Mutua Esclusione
        static Object lockConsole    = new Object();
        static Object lockParcheggio = new Object();
        static Object lockPassa      = new Object();
        static Object lockCorsia     = new Object();

        static List<Thread> passa;          // Lista auto in transito
        static List<string> parcheggio;     // Lista auto nel parcheggio
        static bool levatoio = true;        // Levatoio alto/basso
        static bool[] corsia = new bool[NUM_AUTO_SUL_PONTE];    // Stato delle corsie libero

        static void Main(string[] args)
        {

        }
    }
}
