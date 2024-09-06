//Fabio Fantini 2023/11/21
//Stampa cifra a 7 segmenti con posizionamento
//Esercizio 1

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConteggioAllaRovescia
{
    internal class Program
    {

        #region Cifre a 7 segmenti
        static string[] cifra_0 = {
            " ▓▓▓▓▓▓ ",
            "▓      ▓",
            "▓      ▓",
            "▓      ▓",
            "▓      ▓",
            "▓      ▓",
            " ▓▓▓▓▓▓ ",
        };

        static string[] cifra_1 = {
            "       ▓",
            "       ▓",
            "       ▓",
            "        ",
            "       ▓",
            "       ▓",
            "       ▓",
        };

        static string[] cifra_2 = {
            " ▓▓▓▓▓▓ ",
            "       ▓",
            "       ▓",
            " ▓▓▓▓▓▓ ",
            "▓       ",
            "▓       ",
            " ▓▓▓▓▓▓ ",
        };

        static string[] cifra_3 = {
            " ▓▓▓▓▓▓ ",
            "       ▓",
            "       ▓",
            " ▓▓▓▓▓▓ ",
            "       ▓",
            "       ▓",
            " ▓▓▓▓▓▓ ",
        };

        static string[] cifra_4 = {
            "▓      ▓",
            "▓      ▓",
            "▓      ▓",
            " ▓▓▓▓▓▓ ",
            "       ▓",
            "       ▓",
            "       ▓",
        };

        static string[] cifra_5 = {
            " ▓▓▓▓▓▓ ",
            "▓       ",
            "▓       ",
            " ▓▓▓▓▓▓ ",
            "       ▓",
            "       ▓",
            " ▓▓▓▓▓▓ ",
        };

        static string[] cifra_6 = {
            " ▓▓▓▓▓▓ ",
            "▓       ",
            "▓       ",
            " ▓▓▓▓▓▓ ",
            "▓      ▓",
            "▓      ▓",
            " ▓▓▓▓▓▓ ",
        };

        static string[] cifra_7 = {
            " ▓▓▓▓▓▓ ",
            "       ▓",
            "       ▓",
            "        ",
            "       ▓",
            "       ▓",
            "       ▓",
        };

        static string[] cifra_8 = {
            " ▓▓▓▓▓▓ ",
            "▓      ▓",
            "▓      ▓",
            " ▓▓▓▓▓▓ ",
            "▓      ▓",
            "▓      ▓",
            " ▓▓▓▓▓▓ ",
        };

        static string[] cifra_9 = {
            " ▓▓▓▓▓▓ ",
            "▓      ▓",
            "▓      ▓",
            " ▓▓▓▓▓▓ ",
            "       ▓",
            "       ▓",
            " ▓▓▓▓▓▓ ",
        };
        #endregion

        static int LetturaInput(string messaggio)
        {
            bool inputOK;
            int num;

            do
            {
                Console.Write(messaggio);
                //Lettura e conv. stringa
                inputOK = int.TryParse(Console.ReadLine(), out num);
                if (!inputOK) Console.WriteLine("Input Invalido!\n");
                else if (num < 0)
                {
                    Console.WriteLine("L'input non può essere negativo.\n");
                    inputOK = false;
                }
            } while (!inputOK);//Ricicla se invalido

            return num;
        }

        static void StampaCifra(string[] cifra, int riga, int col)
        {
            //Posizionamento colonna
            Console.SetCursorPosition(Console.CursorLeft, col);
            //Stampa cifra
            for (int i = 0; i < cifra_0.Length; i++)
            {
                Console.SetCursorPosition(riga, Console.CursorTop);
                Console.WriteLine(cifra[i]);
            }
        }

        static void Main(string[] args)
        {
            Console.Title = "Stampa cifra a 7 segmenti con posizionamento by Fabio Fantini.";

            //Inizializzazione
            int riga = LetturaInput("Inserisci la riga: ");
            int col = LetturaInput("Inserisci la colonna: ");

            Console.WriteLine("\nPremi un tasto per incominciare.");
            Console.ReadKey();

            #region Conto alla rovescia
            Console.Clear();
            StampaCifra(cifra_9, riga, col);
            Thread.Sleep(1000);
            Console.Clear();
            StampaCifra(cifra_8, riga, col);
            Thread.Sleep(1000);
            Console.Clear();
            StampaCifra(cifra_7, riga, col);
            Thread.Sleep(1000);
            Console.Clear();
            StampaCifra(cifra_6, riga, col);
            Thread.Sleep(1000);
            Console.Clear();
            StampaCifra(cifra_5, riga, col);
            Thread.Sleep(1000);
            Console.Clear();
            StampaCifra(cifra_4, riga, col);
            Thread.Sleep(1000);
            Console.Clear();
            StampaCifra(cifra_3, riga, col);
            Thread.Sleep(1000);
            Console.Clear();
            StampaCifra(cifra_2, riga, col);
            Thread.Sleep(1000);
            Console.Clear();
            StampaCifra(cifra_1, riga, col);
            Thread.Sleep(1000);
            Console.Clear();
            StampaCifra(cifra_0, riga, col);
            Thread.Sleep(1000);
            #endregion

            Console.Clear();
            Console.WriteLine("Fine.");
            Console.ReadKey();
        }
    }
}


