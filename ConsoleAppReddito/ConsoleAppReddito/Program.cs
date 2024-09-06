using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppRedditoLordo
{
    internal class Program
    {
        static void Main(string[] args)
        {

            Console.OutputEncoding = System.Text.Encoding.UTF8;

            #region Dichiarazioni
            //Dichiarazione variabili
            string stInput;
            bool inputOK;
            double redditoLordo;
            double imposta;

            //Dichiarazione Costanti
            const int SOGLIA1 = 10000;
            const int SOGLIA2 = 15000;
            const double PERCENTUALE_SOGLIA1 = 15.0 / 100.0;
            const double PERCENTUALE_SOGLIA2 = 20.0 / 100.0;
            const double PERCENTUALE_SOPRASOGLIA = 23.0 / 100.0;
            #endregion

            #region Lettura Input
            do
            {
                Console.WriteLine("Inserire l'Input.");
                stInput = Console.ReadLine();
                //Convertire l'Input in Double
                inputOK = double.TryParse(stInput, out redditoLordo);
                if (!inputOK)
                {
                    Console.WriteLine("Input Invalido! Riprovare.");
                }
                else if (redditoLordo < 0.00)
                {
                    Console.WriteLine("Input Invalido! Inserire un valore positivo.");
                    inputOK = false;
                }
            } while (!inputOK);//Ricicla se invalido
            #endregion

            #region Calcolo dell'importo
            if (redditoLordo <= SOGLIA1)
            {
                imposta = redditoLordo * PERCENTUALE_SOGLIA1;
            }
            else if(redditoLordo <= SOGLIA2)
            {
                imposta = SOGLIA1 * PERCENTUALE_SOGLIA1 + (redditoLordo - SOGLIA1) * PERCENTUALE_SOGLIA2;
            }
            else
            {
                imposta = SOGLIA1 * PERCENTUALE_SOGLIA1 + (SOGLIA2 - SOGLIA1) * PERCENTUALE_SOGLIA2 + (redditoLordo - SOGLIA2) * PERCENTUALE_SOPRASOGLIA;
            }
            #endregion

            Console.WriteLine("L'imposta è di {0:C}", imposta);

            Console.WriteLine("Clicca un tasto per terminare il programma.");
            Console.ReadKey();
        }
    }
}



