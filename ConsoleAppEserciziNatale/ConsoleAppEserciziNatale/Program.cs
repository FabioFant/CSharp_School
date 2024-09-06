using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppEserciziNatale
{
    internal class Program
    {
        static int IndexSottoStringa(string stringa, string sotto_stringa)
        {
            int pointer = 0;
            int index = -1;
            for (int i = 0; i < stringa.Length; i++)
            {
                //Controllo per passi se trovo i caratteri della sottostringa
                if (stringa[i] == sotto_stringa[pointer]) pointer++;
                else pointer = 0; //Azzero sppena trovo un carattere diverso

                //Se trovo l'intera sottostringa memorizzo l'indice iniziale ed esco dal for
                if (pointer == sotto_stringa.Length)
                {
                    index = i - pointer + 1;
                    break;
                }
            }
            return index;
        }

        static void Main(string[] args)
        {
            //Input
            Console.Write("Inserisci una stringa: ");
            string stringa = Console.ReadLine();
            Console.Write("Inserisci una sottostringa: ");
            string sotto_stringa = Console.ReadLine();

            int index = IndexSottoStringa(stringa, sotto_stringa);

            //Stampa
            if (index == -1) Console.WriteLine("\nSottostringa non presente nella stringa.");
            else Console.WriteLine("\nSottostringa presente nella stringa.");
            Console.WriteLine($"Indice: {index}");

            Console.ReadKey();
        }
    }
}
