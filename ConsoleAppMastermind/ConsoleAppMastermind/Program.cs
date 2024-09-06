//Fabio Fantini 3H 2023-12-19
//Mastermind

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleAppMasterMind
{
    internal class Program
    {
        static Random rnd = new Random(); //Usato per la creazione della cartella
        static int tentativi = 0;         //Quante volte si può riprovare
        static int[] codice = new int[4]; //Codice da indovinare


        //Tutte le difficoltà esistenti
        enum Difficoltà
        {
            Relax,
            Facile,
            Medio,
            Difficile,
            Hardcore,
        }

        static void CambiaColoreTentativi(int tentativi)
        {
            //Cambia colore in base al numero di tentativi
            switch (tentativi)
            {
                case 10:
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    return;
                case 9:
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    return;
                case 8:
                    Console.ForegroundColor = ConsoleColor.Blue;
                    return;
                case 7:
                    Console.ForegroundColor = ConsoleColor.DarkBlue;
                    return;
                case 6:
                    Console.ForegroundColor = ConsoleColor.Green;
                    return;
                case 5:
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    return;
                case 4:
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    return;
                case 3:
                    Console.ForegroundColor = ConsoleColor.Red;
                    return;
                case 2:
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    return;
                case 1:
                    Console.ForegroundColor = ConsoleColor.DarkMagenta;
                    return;
            }
        }

        static int[] GeneraCodice()
        {
            int[] codiceRnd = new int[4];
            for (int i = 0; i < codiceRnd.Length; i++)
            {//Passo per ogni indice
                while (true)
                {//Ciclo finchè riesco a riempire l'indice
                    int cifra = rnd.Next(0, 10);
                    if (!codiceRnd.Contains(cifra))
                    {//Se il numero non è già contenuto lo inserisco ed esco
                        codiceRnd[i] = cifra;
                        break;
                    }

                }
            }
            return codiceRnd;
        }

        static int IntInput(string messaggio)
        {//Lettura di un input con il solo controllo che sia un int, gli altri sono effettuati direttamente sul main
            bool inputOk;
            int num;
            do
            {//Inserimento di un numero con controlli
                Console.Write(messaggio);
                inputOk = int.TryParse(Console.ReadLine(), out num);
                if (!inputOk)
                    Console.WriteLine("Input Invalido. Riprova.");
            } while (!inputOk);//Ricicla se invalido
            return num;
        }

        static void DefinisciTentativi(Difficoltà difficoltà)
        {
            switch (difficoltà)
            {//In base alla difficoltà, definisco i tentativi
                case Difficoltà.Relax:
                    tentativi = 10;
                    return;
                case Difficoltà.Facile:
                    tentativi = 8;
                    return;
                case Difficoltà.Medio:
                    tentativi = 6;
                    return;
                case Difficoltà.Difficile:
                    tentativi = 4;
                    return;
                case Difficoltà.Hardcore:
                    tentativi = 2;
                    return;
            }
        }

        static int CifreGiusteNelPosto(int[] codiceScelto)
        {
            int count = 0;
            for (int i = 0; i < codice.Length; i++)
            {//Passo per il codice dell'utente e conto quante volto le cifre sono nel posto giusto
                if (codiceScelto[i] == codice[i])
                    count++;
            }
            return count;
        }

        static int CifreGiusteFuori(int[] codiceScelto)
        {
            int esc = CifreGiusteNelPosto(codiceScelto);
            int count = 0;
            for (int i = 0; i < codice.Length; i++)
            {//Passo per il codice dell'utente e conto quante volte ritrovo le cifre anche nel codice da indovinare a prescindere dalla posizione
                if (codice.Contains(codiceScelto[i]))
                    count++;
            }
            //Ritorno il num. di cifre trovate escludendo quelle nelle che si trovano nella stessa posizione
            return count - esc;
        }

        static void Main(string[] args)
        {
            Console.Title = "MasterMind by Fabio Fantini 3H.";

            //Variabile che definisce se continuare, inizializzata a true all'inizio
            bool riprovaOk = true;
            do
            {
                Console.CursorVisible = true;
                //Genero il codice da indovinare
                codice = GeneraCodice();

                //Codice per sapere il codice prima di iniziare
#if false
                Console.Write("Codice:");
                for (int i = 0; i < codice.Length; i++)
                    Console.Write(codice[i] + " ");
                Console.WriteLine("\n");
#endif

                #region Menù iniziale
                //Presento un menù all'utente per scegliere la difficoltà
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(" __  __           _                      _           _ ");
                Console.WriteLine("|  \\/  |         | |                    (_)         | |");
                Console.WriteLine("| \\  / | __ _ ___| |_ ___ _ __ _ __ ___  _ _ __   __| |");
                Console.WriteLine("| |\\/| |/ _` / __| __/ _ \\ '__| '_ ` _ \\| | '_ \\ / _` |");
                Console.WriteLine("| |  | | (_| \\__ \\ ||  __/ |  | | | | | | | | | | (_| |");
                Console.WriteLine("|_|  |_|\\__,_|___/\\__\\___|_|  |_| |_| |_|_|_| |_|\\__,_|");
                Console.WriteLine("                                                       ");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Benvenuto nel gioco! Inserisci una difficoltà");
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.WriteLine("0: Relax     - 10 Tentativi");
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine("1: Facile    - 8 Tentativi");
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("2: Medio     - 6 Tentativi");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("3: Difficile - 4 Tentativi");
                Console.ForegroundColor = ConsoleColor.DarkMagenta;
                Console.WriteLine("4: Hardcore  - 2 Tentativi");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine();
                #endregion

                #region Inserimento Difficoltà
                bool inputOk = true;
                int scelta;
                do
                {//Inserimento della difficoltà come scritto nel menù
                    scelta = IntInput("Difficoltà: ");
                    if (scelta < 0 || scelta > 4)
                    {//Fuori range --> Riciclo
                        Console.WriteLine("Scelta non valida. Inserisci un numero fra 0 e 4");
                        inputOk = false;
                    }
                    else
                        inputOk = true;
                } while (!inputOk);
                //Se tutto andato bene, converto il numero inserito come nell'enum Difficoltà e definisco i tentativi
                Difficoltà diff = (Difficoltà)scelta;
                DefinisciTentativi(diff);
                #endregion

                Console.Clear();

                bool vitt = false;
                while (tentativi != 0 && !vitt)
                {//Serie di gioco
                 //Comunico i tentativi e POI ne tolgo uno
                    CambiaColoreTentativi(tentativi);
                    Console.WriteLine($"\n{tentativi--} TENTATIVI\n");
                    Console.ForegroundColor = ConsoleColor.White;

                    #region Inserimento Codice
                    int[] codiceScelto = new int[4];
                    string stInput;
                    int valore;
                    do
                    {
                        //Inserimento Input
                        inputOk = true;
                        Console.Write("Inserisci il tuo codice: ");
                        stInput = Console.ReadLine();
                        //Ricicla se:
                        if (stInput.Length != 4)
                        {//Il codice non ha 4 cifre
                            Console.WriteLine("Errore: Il codice deve essere di 4 cifre.");
                            inputOk = false;
                        }
                        else if (!int.TryParse(stInput, out valore))
                        {//Il codice non ha cifre valide
                            inputOk = false;
                            Console.WriteLine("Errore: Cifre non valide.");
                        }
                    } while (!inputOk);

                    //Cast da stringa ad intero
                    codiceScelto[0] = stInput[0] - '0';
                    codiceScelto[1] = stInput[1] - '0';
                    codiceScelto[2] = stInput[2] - '0';
                    codiceScelto[3] = stInput[3] - '0';
                    #endregion

                    //Definisco le cifre giuste che si trovano nello stesso posto e quelle fuori posto
                    int cifrPosto = CifreGiusteNelPosto(codiceScelto);
                    int cifrFuori = CifreGiusteFuori(codiceScelto);

                    Thread.Sleep(500);

                    #region Esito
                    if (cifrPosto == 4)
                    {//Tutte le cifre sono state indovinate
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.WriteLine("\nHai VINTO WAHOOO! Il codice è stato indovinato correttamente!");
                        Console.ForegroundColor = ConsoleColor.White;
                        vitt = true;
                    }
                    else
                    {
                        Console.Write("\nDel tuo codice: ");
                        //Stampa i parametri delle cifre
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine($"\n{cifrPosto} cifre sono nel posto giusto.");
                        Console.ForegroundColor = ConsoleColor.DarkCyan;
                        Console.WriteLine($"{cifrFuori} cifre sono giuste ma nel posto sbagliato.");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine("Cliccare per continuare.");
                        Console.ReadKey(true);
                    }
                    #endregion

                }

                if (!vitt)
                {//Perdita con tentativi finiti e rilevamento del codice
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.Write("\n\nHai perso... Il codice era: ");
                    for (int i = 0; i < codice.Length; i++)
                        Console.Write($"{codice[i]}");
                    Console.ForegroundColor = ConsoleColor.White;
                }

                //Stampa menù di riprova
                Console.CursorVisible = false;
                Console.WriteLine("\n");
                Console.WriteLine("1: Riprova");
                Console.WriteLine("2: Uscita");
                //Se non si riprova si esce
                ConsoleKeyInfo key = Console.ReadKey(true);
                if (key.KeyChar != '1') riprovaOk = false;
                Console.Clear();
            } while (riprovaOk);
        }
    }
}


