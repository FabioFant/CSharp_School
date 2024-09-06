//Fabio Fantini 3H 28/11/2023
//Gestione tombola con estrazioni ecc..

using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppTombola
{
    internal class Program
    {
        static bool[] tabellone = new bool[90];
        static Random rnd = new Random();

        static bool TuttoEstratto()
        {
            for (int i = 0; i < 90; i++)
            { //Passa per tutti i numeri
                if (!tabellone[i]) return false;
            }
            //Se non trova estratti ritorno true
            return true;
        }
        static void Estrazione()
        {
            int num;
            while (true) //Ricicla se non torva un numero estratto
            {
                num = rnd.Next(0, 90);
                if (!tabellone[num])
                { //Numero non estratto
                    tabellone[num] = true;
                    break;
                }
            }
            Console.WriteLine($"\n ESTRAZIONE: {num+1}");
        }
        static void GeneraCartella()
        {
            int spazio;
            int count = 0;
            int[] numeri = new int[27];
            //Definizione posizione numeri
            for (int i = 0; i < 3; i++)
            {//Passo per le righe
                for (int j = 0; j < 9; j++)
                { // Passo per le colonne
                    spazio = rnd.Next(0, 2);
                    if (spazio == 0 && numeri[j + (i * 9)] == 0)
                    { //Prob. del 50% nel mettere un valore in una posizione vuota
                        numeri[j + (i * 9)] = -1; 
                        count++;
                    }
                    if (count == 5)
                    { //Se la riga raggiunge cinque valori
                        count = 0;
                        break;
                    }
                    else if (count < 5 && j == 8) j = 0; //Restarti se non raggiungo 5 valori
                }
            }
            //Assegnamento valori nelle posizioni date
            int sx = 1;
            int dx = 11;
            for (int i = 0; i < 27; i += 9) //Passo per le colonne
            {
                while (numeri[i] == -1)
                { //Per i valori dati prima, assegno un valore che sta nel confine
                    int num = rnd.Next(sx, dx);
                    if (!numeri.Contains(num))
                    { //Se il valore non è già stato messo, lo metto
                        numeri[i] = num;
                        break;
                    }
                }
                if (i >= 18 && i != 26)
                { //Se l'indice si trova nell'ultima riga cambio i confini e la colonna
                    sx = dx;
                    dx += 10;
                    i = i - 18 - 9 + 1;
                }
            }
            Console.WriteLine();
            Console.WriteLine("           C A R T E L L A");
            //Stampa Cartella
            for (int i = 0; i < 27; i++)
            {
                if (numeri[i] > 0) //Serve per capire se ha un valore
                    if (numeri[i] < 10) Console.Write("  0" + numeri[i]); //Stampa per numeri < 10 (quindi con lo 0 davanti)
                    else Console.Write("  " + numeri[i]); //Stampa numeri > 10
                else //Numero non estratto
                    Console.Write("  ##");
                if (((i + 1) % 9) == 0) Console.WriteLine(); //A capo ogni 9 numeri
            }
        }
        static bool Cinquina()
        {
            Console.WriteLine();
            int[] estrazioni = new int[5];
            //Riempo l'array con valori fuori dal range
            for (int i = 0; i < estrazioni.Length; i++) estrazioni[i] = 91;

            for (int i = 0; i < estrazioni.Length; i++)
            { //Inserisco i numeri
                bool inputOK;
                do
                {
                    Console.Write($"Numero {i + 1}: ");
                    inputOK = int.TryParse(Console.ReadLine(), out estrazioni[i]);
                    //Controlli
                    if (!inputOK) Console.WriteLine("Errore: Input Invalido.");
                    else if (estrazioni[i] < 1 || estrazioni[i] > 90)
                    { //Se il numero non rispetta il range
                        Console.WriteLine("Errore: Numero non esistente.");
                        inputOK = false;
                    }
                    for (int j = 0; j <= i; j++)
                    { //Controllo che il numero non sià già stato inserito
                        if (estrazioni[i] == estrazioni[j] && j != i)
                        { //Se trovato
                            Console.WriteLine("Errore: Numero già inserito.");
                            inputOK = false;
                            break;
                        }
                    }
                } while (!inputOK); //Ricicla se invalido
            }

            for (int i = 0; i < estrazioni.Length; i++)
            { //Passo per tutti i valori e controllo se siano stati estratti
                if (!tabellone[estrazioni[i] - 1]) return false;
            }
            return true;
        }
        static bool Decina()
        {
            Console.WriteLine();
            int[] estrazioni = new int[10];
            //Riempo l'array con valori fuori dal range
            for (int i = 0; i < estrazioni.Length; i++) estrazioni[i] = 91;

            for (int i = 0; i < estrazioni.Length; i++)
            { //Inserisco i numeri
                bool inputOK;
                do
                {
                    Console.Write($"Numero {i + 1}: ");
                    inputOK = int.TryParse(Console.ReadLine(), out estrazioni[i]);
                    //Controlli
                    if (!inputOK) Console.WriteLine("Errore: Input Invalido.");
                    else if (estrazioni[i] < 1 || estrazioni[i] > 90)
                    { //Se il numero non rispetta il range
                        Console.WriteLine("Errore: Numero non esistente.");
                        inputOK = false;
                    }
                    for (int j = 0; j <= i; j++)
                    { //Controllo che il numero non sià già stato inserito
                        if (estrazioni[i] == estrazioni[j] && j != i)
                        { //Se trovato
                            Console.WriteLine("Errore: Numero già inserito.");
                            inputOK = false;
                            break;
                        }
                    }
                } while (!inputOK); //Ricicla se invalido
            }

            for (int i = 0; i < estrazioni.Length; i++)
            { //Passo per tutti i valori e controllo se siano stati estratti
                if (!tabellone[estrazioni[i] - 1]) return false;
            }
            return true;
        }
        static bool Tombola()
        {
            Console.WriteLine();
            int[] estrazioni = new int[15];
            //Riempo l'array con valori fuori dal range
            for (int i = 0; i < estrazioni.Length; i++) estrazioni[i] = 91;

            for (int i = 0; i < estrazioni.Length; i++)
            { //Inserisco i numeri
                bool inputOK;
                do
                {
                    Console.Write($"Numero {i + 1}: ");
                    inputOK = int.TryParse(Console.ReadLine(), out estrazioni[i]);
                    //Controlli
                    if (!inputOK) Console.WriteLine("Errore: Input Invalido.");
                    else if (estrazioni[i] < 1 || estrazioni[i] > 90)
                    { //Se il numero non rispetta il range
                        Console.WriteLine("Errore: Numero non esistente.");
                        inputOK = false;
                    }
                    for (int j = 0; j <= i; j++)
                    { //Controllo che il numero non sià già stato inserito
                        if (estrazioni[i] == estrazioni[j] && j != i)
                        { //Se trovato
                            Console.WriteLine("Errore: Numero già inserito.");
                            inputOK = false;
                            break;
                        }
                    }
                } while (!inputOK); //Ricicla se invalido
            }

            for (int i = 0; i < estrazioni.Length; i++)
            { //Passo per tutti i valori e controllo se siano stati estratti
                if (!tabellone[estrazioni[i] - 1]) return false;
            }
            return true;
        }

        static void VisualizzaInterfaccia()
        {
            Console.WriteLine("\n");
            for (int i = 0; i < 90; i++)
            { //Stampa Tebellone
                if (tabellone[i]) //Numero estratto
                    if ((i + 1) < 10) Console.Write("  0" + (i + 1)); //Stampa per numeri < 10 (quindi con lo 0 davanti)
                    else Console.Write("  " + (i + 1)); //Stampa numeri > 10
                else //Numero non estratto
                    Console.Write("  ##");
                if (((i + 1) % 10) == 0) Console.WriteLine(); //A capo ogni 10 numeri
            }
            //Stampa comandi
            Console.WriteLine("\n");
            Console.WriteLine("  --[E]strazione--");
            Console.WriteLine("  --[G]enera Cartella--\n");
            Console.WriteLine("    VERIFICHE  ");
            Console.WriteLine("  --[C]inquina--");
            Console.WriteLine("  --[D]ecina  --");
            Console.WriteLine("  --[T]ombola --");
            Console.WriteLine("\n  --[U]scita--");

        }
        static void Main(string[] args)
        {
            Console.Title = "TOMBOLA by Fabio Fantini 3H.";

            for (int i = 0; i < 90; i++)
            { //Riempimento tombola
                tabellone[i] = false;
            }

            Console.WriteLine("\n    P R O G R A M M A   T O M B O L A");

            bool continuoOK = true;
            while (continuoOK)
            { //Comando eseguito
                VisualizzaInterfaccia();
                ConsoleKeyInfo key = Console.ReadKey(true);
                switch (key.KeyChar)
                {
                    case 'e': //Estrazione
                        // Controllo se tutto estratto
                        if (!TuttoEstratto()) Estrazione();
                        else
                            Console.WriteLine("\n       T U T T O   E S T R A T T O");
                        break;
                    case 'g': //Genera cartella
                        GeneraCartella();
                        break;
                    case 'c': //Cinquina
                        if (Cinquina()) Console.WriteLine("\n    C I N Q U I N A   A P P R O V A T A");
                        else Console.WriteLine("\n C I N Q U I N A   N O N   A P P R O V A T A");
                        break;
                    case 'd': //Decina
                        if (Decina()) Console.WriteLine("\n      D E C I N A   A P P R O V A T A");
                        else Console.WriteLine("\n  D E C I N A   N O N   A P P R O V A T A");
                        break;
                    case 't': //Tombola
                        if (Tombola()) Console.WriteLine("\n     T O M B O L A   A P P R O V A T A");
                        else Console.WriteLine("\n T O M B O L A   N O N   A P P R O V A T A");
                        break;
                    case 'u': //Uscita
                        continuoOK = false;
                        break;

                }
            }
        }
    }
}




