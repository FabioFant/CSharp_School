using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppSottoStringa
{
    internal class Program
    {
        static string LetturaStringa(string messaggio)
        {
            string str;
            while (true)
            {
                Console.Write(messaggio);
                str = Console.ReadLine();
                if (str == "") continue;
                break;
            }
            return str;
        }

        static int RicercaSottostringa(string str, string sotStr)
        {
            //Memorizza a che punto si hanno trovato i caratteri della sottostringa e a che indice si trova la stringa; output
            int pointer = -1;
            for(int i = 0; i < str.Length; i++)
            {
                //Trovato il carattere
                if (str[i] == sotStr[pointer + 1]) pointer++;
                //Non trovato
                else pointer = -1;
                //Trovata la sottostringa
                if (pointer + 1 == sotStr.Length)
                {
                    pointer = i - pointer;
                    break;
                }
                else if (i == str.Length - 1) pointer = -1;
            }
            return pointer;
        }

        static void Main(string[] args)
        {
            //Input
            string str = LetturaStringa("Inserisci la stringa: ");
            string sotStr = LetturaStringa("Inserisci la sottostringa: ");
            //Output
            Console.WriteLine("");
            if (RicercaSottostringa(str, sotStr) == -1)
                Console.WriteLine("Sottostringa non trovata nella stringa.");
            else
                Console.WriteLine($"Sottostringa trovata nell'indice {RicercaSottostringa(str, sotStr)}");
            Console.ReadKey();
        }
    }
}
