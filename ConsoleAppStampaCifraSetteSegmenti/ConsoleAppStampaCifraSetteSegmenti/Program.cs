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

        static string[] TrovaCifra(char num)
        {
            switch (num)
            {
                case '0':
                    return cifra_0;
                case '1':
                    return cifra_1;
                case '2':
                    return cifra_2;
                case '3':
                    return cifra_3;
                case '4':
                    return cifra_4;
                case '5':
                    return cifra_5;
                case '6':
                    return cifra_6;
                case '7':
                    return cifra_7;
                case '8':
                    return cifra_8;
                case '9':
                    return cifra_9;
            }

            return cifra_0;
        }

        static void Main(string[] args)
        {
            Console.Title = "Stampa cifra a 7 segmenti con posizionamento by Fabio Fantini.";

            //Dichiarazione variabili
            bool inputOK;
            int trash;
            string num;

            do
            {//Lettura numero in stringa
                Console.Write("Inserisci il numero: ");
                num = Console.ReadLine();
                inputOK = int.TryParse(num, out trash);
                if (trash < 0) inputOK = false;
            } while (!inputOK);
            //Lettura riga e colonna
            int riga = LetturaInput("Inserisci la riga: ");
            int col = LetturaInput("Inserisci la colonna: ");

            Console.WriteLine("\nPremi un tasto per incominciare.");
            Console.ReadKey();
            Console.Clear();

            //Stampa del numero
            for (int i = 0; i < num.Length; i++)
            {
                StampaCifra(TrovaCifra(num[i]), riga, col);
                riga += 9;
            }

            Console.ReadKey();
        }
    }
}


