//2048
//Fabio Fantini 3H 23/01/2024

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp2048
{
    internal class Program
    {
        static Random rnd = new Random();
        static int punti = 0; //I punti guadagnati mettendo assieme le celle
        static bool mosso = false; //Indica se con la mossa fatta qualcosa è effettivamente successo
        static int[,] tavola =
        { //La tabella contenente le celle
            {0, 0, 0, 0},
            {0, 0, 0, 0},
            {0, 0, 0, 0},
            {0, 0, 0, 0},
        };

        static bool Continua()
        {//Controllo se ci sono mosse possibili

            //1° Controllo che sia pieno
            for (int i = 0; i < tavola.GetLength(0); i++)
                for (int j = 0; j < tavola.GetLength(1); j++)
                    if (tavola[i, j] == 0)
                        return true;

            //2°Controllo se nelle righe ci sono 2 celle uguali
            for(int i = 0; i < tavola.GetLength(0); i++)
                for (int j = 1; j < tavola.GetLength(1)-1; j++)
                    if (tavola[i, j] == tavola[i, j - 1])
                        return true;

            //3°Controllo se nelle colonne ci sono 2 celle uguali
            for (int j = 0; j < tavola.GetLength(1); j++)
                for (int i = 1; i < tavola.GetLength(0) - 1; i++)
                    if (tavola[i, j] == tavola[i - 1, j])
                        return true;

            return false;

        }
        static void StampaInterfaccia()
        {
            Console.WriteLine($"\tPUNTI: {punti}\n");

            //Stampa della tabella
            int lastLeft;
            for (int i = 0; i < tavola.GetLength(0); i++)
            {
                for (int j = 0; j < tavola.GetLength(1); j++)
                {
                    if (j % 8 == 0)
                    { //A capo dopo 4 celle
                        lastLeft = 0;
                        Console.WriteLine();
                    }
                    else
                    { //Memorizzo la posizione del cursore a sinistra e torno su di 2
                        lastLeft = Console.CursorLeft;
                        Console.CursorTop -= 2;
                    }

                    #region Tavolozza Colori
                    switch (tavola[i, j])
                    {
                        case 0: //Vuoto
                            Console.BackgroundColor = ConsoleColor.Gray;
                            break;
                        case 1: //2
                            Console.BackgroundColor = ConsoleColor.Cyan;
                            break;
                        case 2: //4
                            Console.BackgroundColor = ConsoleColor.DarkCyan;
                            break;
                        case 3: //8
                            Console.BackgroundColor = ConsoleColor.Blue;
                            break;
                        case 4: //16
                            Console.BackgroundColor = ConsoleColor.DarkBlue;
                            break;
                        case 5: //32
                            Console.BackgroundColor = ConsoleColor.Green;
                            break;
                        case 6: //64
                            Console.BackgroundColor = ConsoleColor.DarkGreen;
                            break;
                        case 7: //128
                            Console.BackgroundColor = ConsoleColor.DarkYellow;
                            break;
                        case 8: //256
                            Console.BackgroundColor = ConsoleColor.Red;
                            break;
                        case 9: //512
                            Console.BackgroundColor = ConsoleColor.Magenta;
                            break;
                        case 10: //1024
                            Console.BackgroundColor = ConsoleColor.DarkMagenta;
                            break;
                        case 11: //2048
                            Console.BackgroundColor = ConsoleColor.White;
                            break;
                        default: //>= 4096
                            Console.BackgroundColor = ConsoleColor.Black;
                            break;
                    }
                    #endregion

                    for (int k = 1; k <= 13; k++)
                    { //Stampa celle 6x6 con 2 riga il numero
                        if (k != 7)
                        { //Stampa colori
                            Console.Write(" ");
                        }
                        else
                        { //Stampa numero
                            //Se il numero corrisponde ad un colore chiaro, lo scrivo in nero
                            if (Console.BackgroundColor == ConsoleColor.White || Console.BackgroundColor == ConsoleColor.Gray)
                                Console.ForegroundColor = ConsoleColor.Black;
                            //Stampo il numero, la matrice salva l'esponente di 2
                            if (tavola[i, j] != 0)
                                Console.Write("{0, 6}", (int)Math.Pow(2, tavola[i, j]));
                            else
                                Console.Write("      ");

                            Console.ForegroundColor = ConsoleColor.White;
                        }

                        if (k == 6 || k == 7)
                        { //A capo spostando il cursore a sinistra
                            Console.WriteLine();
                            Console.CursorLeft += lastLeft;
                        }
                    }
                }
            }
            Console.BackgroundColor = ConsoleColor.Black;
        }
        static void PiazzaCellaCasuale()
        {
            //80% --> 2 | 20% --> 4
            int num;
            if (rnd.Next(0, 11) <= 8)
                num = 1;
            else
                num = 2;
            //Determino casualmente dove piazzare il valore
            int riga, colonna;
            while (true)
            {
                //Esco quando trovo una casella libera
                riga = rnd.Next(0, 4);
                colonna = rnd.Next(0, 4);
                if (tavola[riga, colonna] == 0)
                    break;
            }
            tavola[riga, colonna] = num;
        }
        static ConsoleKey LeggiTasto()
        {
            while (true)
            {//Lettura del tasto
                ConsoleKeyInfo key = Console.ReadKey(true);
                //Se il tasto corrisponde ad una freccettina allora return char
                if (key.Key == ConsoleKey.UpArrow || key.Key == ConsoleKey.DownArrow
                    || key.Key == ConsoleKey.RightArrow || key.Key == ConsoleKey.LeftArrow)

                    return key.Key;
            }
        }
        static void Alto()
        {//Funziona come sinistra, ma le dimensioni sono invertite
            for (int j = 0; j < tavola.GetLength(1); j++)
            {
                for (int i = 1; i < tavola.GetLength(0); i++)
                {//Per ogni cella in ciascuna riga:
                    for (int k = 1; k <= 3 && (i - k) >= 0; k++)
                    {
                        if (tavola[i - k, j] == 0)
                        {//Se la cella a destra è vuota porto il valore lì
                            tavola[i - k, j] = tavola[i - k + 1, j];
                            tavola[i - k + 1, j] = 0;
                            mosso = true;
                        }
                        else if (tavola[i - k, j] == tavola[i - k + 1, j])
                        {//Se c'è lo stesso numero metto insieme le celle
                            tavola[i - k, j] += 1;
                            tavola[i - k + 1, j] = 0;
                            punti += (int)Math.Pow(2, tavola[i - k, j]);
                            mosso = true;
                            break;
                        }
                        else
                        {//Altrimenti mi fermo
                            break;
                        }
                    }
                }
            }
        }
        static void Basso()
        { //Funziona come destra, ma le dimensioni sono invertite
            for (int j = 0; j < tavola.GetLength(1); j++)
            {
                for (int i = tavola.GetLength(0) - 2; i >= 0; i--)
                {//Per ogni cella in ciascuna riga:
                    for (int k = 1; k <= 3 && (k + i) < tavola.GetLength(1); k++)
                    {
                        if (tavola[i + k, j] == 0)
                        {//Se la cella a destra è vuota porto il valore lì
                            tavola[i + k, j] = tavola[i + k - 1, j];
                            tavola[i + k - 1, j] = 0;
                            mosso = true;
                        }
                        else if (tavola[i + k, j] == tavola[i + k - 1, j])
                        {//Se c'è lo stesso numero metto insieme le celle
                            tavola[i + k, j] += 1;
                            tavola[i + k - 1, j] = 0;
                            mosso = true;
                            punti += (int)Math.Pow(2, tavola[i + k, j]);
                            break;
                        }
                        else
                        {//Altrimenti mi fermo
                            break;
                        }
                    }
                }
            }
        }
        static void Destra()
        {
            for (int i = 0; i < tavola.GetLength(0); i++) 
            { 
                for(int j = tavola.GetLength(1)-2; j >= 0; j--)
                {//Per ogni cella in ciascuna riga:
                    for(int k = 1; k <= 3 && (k+j) < tavola.GetLength(1); k++)
                    {
                        if (tavola[i, j + k] == 0)
                        {//Se la cella a destra è vuota porto il valore lì
                            tavola[i, j + k] = tavola[i, j + k - 1];
                            tavola[i, j + k - 1] = 0;
                            mosso = true;
                        }
                        else if (tavola[i, j + k] == tavola[i, j + k - 1])
                        {//Se c'è lo stesso numero metto insieme le celle
                            tavola[i, j + k] += 1;
                            tavola[i, j + k - 1] = 0;
                            mosso = true;
                            punti += (int)Math.Pow(2, tavola[i, j + k]);
                            break;
                        }
                        else
                        {//Altrimenti mi fermo
                            break;
                        }
                    }
                }
            }
        }
        static void Sinistra() 
        {
            for (int i = 0; i < tavola.GetLength(0); i++)
            {
                for (int j = 1; j < tavola.GetLength(1); j++)
                {//Per ogni cella in ciascuna riga:
                    for (int k = 1; k <= 3 && (j - k) >= 0; k++)
                    {
                        if (tavola[i, j - k] == 0)
                        {//Se la cella a destra è vuota porto il valore lì
                            tavola[i, j - k] = tavola[i, j - k + 1];
                            tavola[i, j - k + 1] = 0;
                            mosso = true;
                        }
                        else if (tavola[i, j - k] == tavola[i, j - k + 1])
                        {//Se c'è lo stesso numero metto insieme le celle
                            tavola[i, j - k] += 1;
                            tavola[i, j - k + 1] = 0;
                            punti += (int)Math.Pow(2, tavola[i, j - k]);
                            mosso = true;
                            break;
                        }
                        else
                        {//Altrimenti mi fermo
                            break;
                        }
                    }
                }
            }
        }
        static void Main(string[] args)
        {
            Console.Title = "2048 by Fabio Fantini 3H";
            Console.CursorVisible = false;

            PiazzaCellaCasuale();
            PiazzaCellaCasuale();
            StampaInterfaccia();
            while (true)
            {
                switch (LeggiTasto())
                {
                    case ConsoleKey.UpArrow:
                        Alto();
                        break;
                    case ConsoleKey.DownArrow:
                        Basso();
                        break;
                    case ConsoleKey.RightArrow:
                        Destra();
                        break;
                    case ConsoleKey.LeftArrow:
                        Sinistra();
                        break;
                }
                Console.Clear();
                StampaInterfaccia();

                if (mosso)
                    PiazzaCellaCasuale();
                else if (!Continua())
                    break;
                    
                mosso = false;

                Thread.Sleep(50);
                Console.Clear();
                StampaInterfaccia();
            }
            Console.WriteLine("\n\tGAME OVER");
            Thread.Sleep(3000);
            Console.ReadKey(true);
        }
    }
}
