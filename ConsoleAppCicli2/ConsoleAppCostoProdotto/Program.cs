using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppCostoProdotto
{
    internal class Program
    {
        static double LeggiInput(string messaggio)
        {
            bool InputOK; double numInput;

            do
            {
                Console.Write(messaggio);
                InputOK = double.TryParse(Console.ReadLine(), out numInput);
                //Validazione Input
                if (!InputOK) Console.WriteLine("Input Invalido! Riprova.");
                else if (numInput < 0)
                {
                    Console.WriteLine("Input Invalido! L'input non può essere negativo.");
                    InputOK = false; 
                }

            } while (!InputOK);

            return numInput;
        }
        static void Main(string[] args)
        {
            string nome_prodotto; double prezzo;
            string nomeMax = ""; double prezzoMax = -1;

            do
            {
                Console.Write("Inserisci il nome del prodotto: ");
                nome_prodotto = Console.ReadLine();
                //Stringa vuota = Fine
                if (nome_prodotto == "") break;

                prezzo = LeggiInput("Inserisci il prezzo: ");

                if (prezzo > prezzoMax)
                {
                    prezzoMax = prezzo; nomeMax = nome_prodotto;
                } 
            } while (true);

            Console.WriteLine($"\nProdotto più costoso\nnome: {nomeMax}\nprezzo: {prezzoMax}");

            Console.WriteLine("\nClicca un tasto per terminare il programma.");
            Console.ReadKey();
        }
    }
}
