using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppEnumString
{
    internal class Program
    {
        enum Ordinamento
        {
            Crescente = -1,
            Decrescente = +1,
            Nessuno = 0
        }

        static int LetturaInput(string messaggio)
        {
            bool inputOK;
            int num;
            do
            {
                Console.Write(messaggio);
                inputOK = int.TryParse(Console.ReadLine(), out num);
                if (!inputOK) Console.WriteLine("Input Invalido!\n");
            }while (!inputOK);

            return num;
        }

        static Ordinamento OrdineInteri(int[] interi)
        {
            //Creazione e riempimento del vettore di controllo
            int[] controllo = new int[interi.Length];
            for(int i = 0; i < controllo.Length; i++) controllo[i] = interi[i];

            //Crescente
            Array.Sort(controllo);
            if (controllo.SequenceEqual(interi)) return Ordinamento.Crescente;
            //Decrescente
            Array.Reverse(controllo);
            if (controllo.SequenceEqual(interi)) return Ordinamento.Decrescente;
            //Nessuno
            return Ordinamento.Nessuno;
        }
        static void Main(string[] args)
        {
            int Lenght;

            while(true)
            {//Lettura Lenght < 0
                Lenght = LetturaInput("Inserisci la lunghezza del vettore: ");
                if (Lenght <= 1)
                {
                    Console.WriteLine("La lunghezza deve essere > 1\n");
                    continue;
                }
                break;
            }

            //Creazione e riempimento del vettore
            int[] interi = new int[Lenght];
            for(int i = 0; i < Lenght; i++) interi[i] = LetturaInput($"Inserisci l'intero nell'indice {i}: ");

            Console.WriteLine("");
            //Crescente
            if (OrdineInteri(interi) == Ordinamento.Crescente) Console.WriteLine("Gli interi sono in ordine crescente.");
            //Decrescente
            if (OrdineInteri(interi) == Ordinamento.Decrescente) Console.WriteLine("Gli interi sono in ordine decrescente.");
            //Nessuno
            if (OrdineInteri(interi) == Ordinamento.Nessuno) Console.WriteLine("Gli interi non sono ne in ordine crescente ne decrescente");
            Console.ReadKey();
        }
    }
}
