using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppStampa____
{
    internal class Program
    {
        static int LeggiInput(string messaggio)
        {
            bool inputOK; int numInput;

            do
            {
                Console.Write(messaggio);
                inputOK = int.TryParse(Console.ReadLine(), out numInput);
                if (!inputOK) Console.WriteLine("Input Invalido! Riprova.");
                else if(numInput < 0)
                {
                    Console.WriteLine("Input Invalido! L'Input deve essere positivo.");
                    inputOK = false;
                }
            } while (!inputOK);

            return numInput;
        }

        static void Main(string[] args)
        {
            //M = Righe, N = Num.Caratteri
            int M, N;

            M = LeggiInput("Inserisci il numero di Righe: ");
            N = LeggiInput("Inserisci il numero di Caratteri: ");

            Console.WriteLine("");
            for (int i = 0; i < M; i++)
            {
                for(int j = 0; j < N; j++)
                {
                    Console.Write("#");
                }
                Console.WriteLine("");
            }

            Console.WriteLine("\nClicca un tasto per terminare il programma.");
            Console.ReadKey();
        }
    }
}
