using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppOrologio
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Dichiarazione variabili
            string stInput;
            bool inputOK;

            int ore1, minuti1, secondi1; //Input 1
            int ore2, minuti2, secondi2; //Input 2

            int secondiTot1, secondiTot2, differenzaTot;
            int oreFin, minutiFin, secondiFin;

            do
            {
                #region Lettura Primo Orario
                Console.WriteLine("INSERISCI IL PRIMO ORARIO.");

                do//Lettura Ore
                {
                    Console.Write("Inserisci il numero di ORE --> ");
                    stInput = Console.ReadLine();
                    inputOK = int.TryParse(stInput, out ore1);
                    if (!inputOK)
                    {
                        Console.WriteLine("Input Invalido! Riprovare.\n");
                    }
                    else if (ore1 < 0)
                    {
                        Console.WriteLine("Input Invalido! Inserisci un valore positivo.\n");
                        inputOK = false;
                    }
                } while (!inputOK);

                do//Lettura Minuti
                {
                    Console.Write("Inserisci il numero di MINUTI --> ");
                    stInput = Console.ReadLine();
                    inputOK = int.TryParse(stInput, out minuti1);
                    if (!inputOK)
                    {
                        Console.WriteLine("Input Invalido! Riprovare.\n");
                    }
                    else if (minuti1 < 0 || minuti1 >= 60)
                    {
                        Console.WriteLine("Input Invalido! Inserisci un valore tra 0 e 59.\n");
                        inputOK = false;
                    }
                } while (!inputOK);

                do//Lettura Secondi
                {
                    Console.Write("Inserisci il numero di SECONDI --> ");
                    stInput = Console.ReadLine();
                    inputOK = int.TryParse(stInput, out secondi1);
                    if (!inputOK)
                    {
                        Console.WriteLine("Input Invalido! Riprovare.\n");
                    }
                    else if (secondi1 < 0 || secondi1 >= 60)
                    {
                        Console.WriteLine("Input Invalido! Inserisci un valore tra 0 e 59.\n");
                        inputOK = false;
                    }
                } while (!inputOK);
                #endregion

                #region Lettura Secondo Orario
                Console.WriteLine("\nINSERISCI IL SECONDO ORARIO.");

                do//Lettura Ore
                {
                    Console.Write("Inserisci il numero di ORE --> ");
                    stInput = Console.ReadLine();
                    inputOK = int.TryParse(stInput, out ore2);
                    if (!inputOK)
                    {
                        Console.WriteLine("Input Invalido! Riprovare.\n");
                    }
                    else if (ore2 < 0)
                    {
                        Console.WriteLine("Input Invalido! Inserisci un valore positivo.\n");
                        inputOK = false;
                    }
                } while (!inputOK);

                do//Lettura Minuti
                {
                    Console.Write("Inserisci il numero di MINUTI --> ");
                    stInput = Console.ReadLine();
                    inputOK = int.TryParse(stInput, out minuti2);
                    if (!inputOK)
                    {
                        Console.WriteLine("Input Invalido! Riprovare.\n");
                    }
                    else if (minuti2 < 0 || minuti2 >= 60)
                    {
                        Console.WriteLine("Input Invalido! Inserisci un valore tra 0 e 59.\n");
                        inputOK = false;
                    }
                } while (!inputOK);

                do//Lettura Secondi
                {
                    Console.Write("Inserisci il numero di SECONDI --> ");
                    stInput = Console.ReadLine();
                    inputOK = int.TryParse(stInput, out secondi2);
                    if (!inputOK)
                    {
                        Console.WriteLine("Input Invalido! Riprovare.\n");
                    }
                    else if (secondi2 < 0 || secondi2 >= 60)
                    {
                        Console.WriteLine("Input Invalido! Inserisci un valore tra 0 e 59.\n");
                        inputOK = false;
                    }
                } while (!inputOK);
                #endregion

                //Calcolo secondi totali
                secondiTot1 = ore1 * 3600 + minuti1 * 60 + secondi1;
                secondiTot2 = ore2 * 3600 + minuti2 * 60 + secondi2;

                if (secondiTot2 > secondiTot1) Console.WriteLine("\nIl secondo orario è più grande del primo! Riprova.\n");
            } while (secondiTot2 > secondiTot1);//Ricicla se input invalido

            differenzaTot = secondiTot1 - secondiTot2;
            //Calcolo Output
            oreFin = differenzaTot / 3600;
            minutiFin = (differenzaTot - (oreFin * 3600)) / 60;
            secondiFin = differenzaTot - (oreFin * 3600) - (minutiFin * 60);

            Console.WriteLine("\nLa differenza ha un valore di {0} ore, {1} minuti e {2} secondi.", oreFin, minutiFin, secondiFin);

            Console.WriteLine("\nClicca un tasto per terminare il programma.");
            Console.ReadKey();
        }
    }
}
