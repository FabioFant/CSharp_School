using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppAlbero
{
    internal class Program
    {
        static void Main(string[] args)
        {
            bool inputOK;
            int N;
            //Lettura N
            do
            {
                Console.Write("Inserisci N: ");
                inputOK = int.TryParse(Console.ReadLine(), out N);
                if (!inputOK) Console.WriteLine("Input Invalido!");
                else if (N < 1)
                {
                    inputOK = false;
                    Console.WriteLine("Errore: N < 1");
                }
            } while (!inputOK);
            Console.WriteLine();
            //Determina lunghezza spazi iniziali
            int sxSpaz = 0;
            int dxSpaz = 1;
            for (int i = 1; i < N; i++)
            {
                sxSpaz = dxSpaz;
                dxSpaz = (sxSpaz * 2) + 1;
            }
            //Stampa
            int sxNum = 0;
            int dxNum = 0;
            string spazioSx = "";
            string spazioDx = "";
            for (int i = 0; i < N; i++)
            {
                //Crea spazi
                for (int j = 0; j < sxSpaz; j++) spazioSx += " ";
                spazioDx = (spazioSx + spazioSx) + " ";
                //Stampa primo spazio e numero
                    Console.Write(spazioSx + sxNum);
                sxNum++;
                //Stampa spazi lunghi (spazioDx) e numeri
                while (sxNum <= dxNum)
                {
                    Console.Write(spazioDx + sxNum);
                    sxNum++;
                }
                Console.WriteLine();
                //Cambia confini numeri
                sxNum = dxNum + 1;
                dxNum = sxNum * 2;
                //Cambia spazi
                dxSpaz = sxSpaz;
                sxSpaz = sxSpaz / 2;
                spazioDx = "";
                spazioSx = "";
            }

            Console.ReadKey();
        }
    }
}
