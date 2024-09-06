using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppDataValida
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

            } while (!inputOK);

            return numInput;
        }

        static bool ControlloBisestile(int anno)
        {
            if (anno % 4 != 0) return false;
            if (anno % 1000 == 0) return true;
            if (anno % 100 == 0) return false;
            return true;
        }

        static bool ControlloMese30Giorni(int mese)
        {
            if (mese == 4 || mese == 6 || mese == 9 || mese == 10) return true;
            return false;
        }

        static bool ValidaData(int giorno, int mese, int anno)
        {
            //Valori fuori dal range valido
            if(anno < 0 || mese <= 0 || mese > 12 || giorno <= 0) return false;

            //Mese con 30 giorni con più di 30 giorni
            if(ControlloMese30Giorni(mese)) if (giorno > 30) return false;

            //Mese con 31 giorni con più di 31 giorni
            if(!ControlloMese30Giorni(mese) && mese != 2) if (giorno > 31) return false;

            //Febbraio + Anno bisestile con più di 29 giorni
            if(ControlloBisestile(anno) && mese == 2) if (giorno > 29) return false;

            //Febbraio + Anno NON bisestile con più di 28 giorni
            if(!ControlloBisestile(anno) && mese == 2) if (giorno > 28) return false;

            //Tutt'ok ☻
            return true;
        }

        static void Main(string[] args)
        {
            int giorno, mese, anno;

            giorno = LeggiInput("Inserisci il giorno: ");
            mese = LeggiInput("Inserisci il mese: ");
            anno = LeggiInput("Inserisci l'anno: ");

            if (ValidaData(giorno, mese, anno)) Console.WriteLine("\nLa data è valida.");
            else Console.WriteLine("\nLa data non è valida.");

            Console.WriteLine("Premi un tasto per terminare il programma.");
            Console.ReadKey();
        }
    }
}



