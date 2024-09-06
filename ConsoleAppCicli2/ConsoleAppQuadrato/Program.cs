using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppQuadrato
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int inputNum, quad = 0, dis = 1; bool inputOK;

            #region Lettura Intero
            do
            {
                Console.Write("Inserisci il numero intero: ");
                inputOK = int.TryParse(Console.ReadLine(), out inputNum);
                if (!inputOK) Console.WriteLine("Input Invalido! Riprova.");
                else if(inputNum < 0)
                {
                    Console.WriteLine("Input Invalido! Il numero deve essere positivo.");
                    inputOK = false;
                }
            } while (!inputOK);
            #endregion

            for(int i = 0; i < inputNum ; i++)
            {
                quad += dis;
                dis += 2;
            }

            Console.WriteLine($"Il quadrato del numero è {quad}.");

            Console.WriteLine("Clicca un tasto per terminare il programma.");
            Console.ReadKey();
        }
    }
}
