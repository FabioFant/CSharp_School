//Conversione personalizzata
//Fabio Fantini 3H 2023-10-31
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppConversionePersonalizzata
{
    internal class Program
    {
        static int BaseToInt(string valore, int b)
        {
            int res = 0;

            for (int i = 0; i < valore.Length; i++)
            {
                char ch = valore[i];
                int cifra = CharToInt(ch);
                res *= b;
                res += cifra;
            }

            return res;
        }

        static string IntToBase(int valore, int b)
        {
            string res = "";
            while (valore > 0)
            {
                int resto = valore % b;
                char ch = IntToChar(resto);
                res = ch + res;
                valore /= b;
            }

            return res;
        }

        static int CharToInt(char digit)
        {
            switch (digit)
            {
                case '1': return 1;
                case '2': return 2;
                case '3': return 3;
                case '4': return 4;
                case '5': return 5;
                case '6': return 6;
                case '7': return 7;
                case '8': return 8;
                case '9': return 9;
                case 'A': return 10;
                case 'B': return 11;
                case 'C': return 12;
                case 'D': return 13;
                case 'E': return 14;
                case 'F': return 15;
                case 'G': return 16;
                case 'H': return 17;
                case 'I': return 18;
                case 'J': return 19;
                case 'K': return 20;
                case 'L': return 21;
                case 'M': return 22;
                case 'N': return 23;
                case 'O': return 24;
                case 'P': return 25;
                case 'Q': return 26;
                case 'R': return 27;
                case 'S': return 28;
                case 'T': return 29;
                case 'U': return 30;
                case 'V': return 31;
                case 'W': return 32;
                case 'X': return 33;
                case 'Y': return 34;
                case 'Z': return 35;
            }
            return 0;
        }

        static char IntToChar(int num)
        {
            switch (num)
            {
                case 0: return '0';
                case 1: return '1';
                case 2: return '2';
                case 3: return '3';
                case 4: return '4';
                case 5: return '5';
                case 6: return '6';
                case 7: return '7';
                case 8: return '8';
                case 9: return '9';
                case 10: return 'A';
                case 11: return 'B';
                case 12: return 'C';
                case 13: return 'D';
                case 14: return 'E';
                case 15: return 'F';
                case 16: return 'G';
                case 17: return 'H';
                case 18: return 'I';
                case 19: return 'J';
                case 20: return 'K';
                case 21: return 'L';
                case 22: return 'M';
                case 23: return 'N';
                case 24: return 'O';
                case 25: return 'P';
                case 26: return 'Q';
                case 27: return 'R';
                case 28: return 'S';
                case 29: return 'T';
                case 30: return 'U';
                case 31: return 'V';
                case 32: return 'W';
                case 33: return 'X';
                case 34: return 'Y';
                case 35: return 'Z';
            }
            return ' ';
        }

        static int LetturaBase(string messaggio)
        {
            bool inputOK; int numInput;

            do
            {
                Console.Write(messaggio);
                inputOK = int.TryParse(Console.ReadLine(), out numInput);

                if (!inputOK) Console.WriteLine("Input Invalido! Riprova.");
                else if (numInput < 2 || numInput > 36)
                {
                    Console.WriteLine("Input Invalido! La base deve essere > 1 e < 37.");
                    inputOK = false;
                }
            } while (!inputOK);

            return numInput;
        }
        static void Main(string[] args)
        {
            Console.Title = "Conversioni personalizzate by Fabio Fantini 3H.";

            bool inputOK;

            //IntToBase
            int b; int num;
            string conversione;

            #region Lettura Int
            do
            {
                Console.Write("Inserisci il numero intero da convertire: ");
                inputOK = int.TryParse(Console.ReadLine(), out num);
                if (!inputOK) Console.WriteLine("Input Invalido! Riprova.");
                else if (num < 0)
                {
                    Console.WriteLine("Input Invalido! Il numero deve essere positivo.");
                    inputOK = false;
                }
            } while (!inputOK);
            #endregion

            b = LetturaBase("Inserisci la base: ");

            conversione = IntToBase(num, b);

            Console.WriteLine($"\nLa conversione è: {conversione}\n");

            //BaseToInt
            string valore; int risultato;

            b = LetturaBase("Inserisci la base del numero: ");

            #region Lettura Valore
            do
            {
                inputOK = true;
                Console.Write("Inserisci il valore da convertire in intero: ");
                valore = Console.ReadLine().ToUpper();
                //Controlli
                if (valore == "")
                {
                    Console.WriteLine("Input Invalido! Riprova.");
                    inputOK = false;
                }
                for (int i = 0; i < valore.Length; i++)
                {
                    int cifra = CharToInt(valore[i]);
                    if ((cifra + 1) > b)
                    {
                        Console.WriteLine("Input Invalido! La base non corrisponde.");
                        inputOK = false;
                        break;
                    }
                }
            } while (!inputOK);
            #endregion

            risultato = BaseToInt(valore, b);

            Console.WriteLine($"\nLa conversione in intero è: {risultato}\n");

            //Termine programma
            Console.WriteLine("Premi un tasto per terminare.");
            Console.ReadKey();
        }
    }
}


