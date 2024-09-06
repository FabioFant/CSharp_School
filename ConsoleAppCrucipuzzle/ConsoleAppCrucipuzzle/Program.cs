//Fabio Fantini 3H 2024-03-12
//Risolvitore di Crucipuzzle da file su console

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppCrucipuzzle
{
    internal class Program
    {
        const string file_input = @"..\..\..\input.txt";
        const string file_output = @"..\..\..\output.txt";

        static List<List<char>> tab = new List<List<char>>();           //tabellone dei caratteri
        static List<List<bool>> ver = new List<List<bool>>();           //tabellone dei caratteri trovati
        static Stack<string> parole = new Stack<string>();              //stack contenente le parole ancora da trovare
        static List<(int, int)> just_found = new List<(int, int)>();   //lista che memorizza le coordinate delle lettere della parola appena trovata (step-by-step)

        static void Stampa()
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            for (int i = 0; i < tab.Count; i++)
            {
                for (int j = 0; j < tab[i].Count; j++)
                {
                    if (ver[i][j] == true) //giallo se trovato
                        Console.ForegroundColor = ConsoleColor.Yellow;

                    if (just_found.Contains((i, j))) //azzurro se lo ho appena trovato (step-by-step)
                        Console.ForegroundColor = ConsoleColor.Cyan;

                    Console.Write(tab[i][j] + " ");
                    Console.ForegroundColor = ConsoleColor.DarkYellow; //giallo scuro di default
                }
                Console.WriteLine();
            }
            Console.ForegroundColor = ConsoleColor.White;
        }

        #region Metodi Ricerche
        //i metodi ritornano 'true' se la parola è stata trovata, altrimenti 'false'
        static bool Basso(string par, int in_x, int in_y)
        {
            int y = in_y;
            //controllo ogni lettera della parola
            for (int i = 0; i < par.Length; i++)
            {
                //lettera diversa mi fermo
                if (tab[y][in_x] != par[i])
                    return false;
                //fuori dalla tabella mi fermo
                if (i != par.Length - 1 && (y + 1) >= tab.Count)
                    return false;
                //vado in basso
                y++;
            }
            //salvo i char verificati
            for (int i = in_y; i < y; i++)
            {
                ver[i][in_x] = true;
                just_found.Add((i, in_x));
            }

            return true;
        }
        static bool Alto(string par, int in_x, int in_y)
        {
            int y = in_y;
            //controllo ogni lettera della parola
            for (int i = 0; i < par.Length; i++)
            {
                //lettera diversa mi fermo
                if (tab[y][in_x] != par[i])
                    return false;
                //fuori dalla tabella mi fermo
                if (i != par.Length - 1 && (y - 1) < 0)
                    return false;
                //vado in alto
                y--;
            }
            y++;
            //salvo i char verificati
            for (int i = in_y; i >= y; i--)
            {
                ver[i][in_x] = true;
                just_found.Add((i, in_x));
            }

            return true;
        }
        static bool Destra(string par, int in_x, int in_y)
        {
            int x = in_x;
            //controllo ogni lettera della parola
            for (int i = 0; i < par.Length; i++)
            {
                //lettera diversa mi fermo
                if (tab[in_y][x] != par[i])
                    return false;
                //fuori dalla tabella mi fermo
                if (i != par.Length - 1 && (x + 1) >= tab[in_y].Count)
                    return false;
                //vado in basso
                x++;
            }
            //salvo i char verificati
            for (int i = in_x; i < x; i++)
            {
                ver[in_y][i] = true;
                just_found.Add((in_y, i));
            }

            return true;
        }
        static bool Sinistra(string par, int in_x, int in_y)
        {
            int x = in_x;
            //controllo ogni lettera della parola
            for (int i = 0; i < par.Length; i++)
            {
                //lettera diversa mi fermo
                if (tab[in_y][x] != par[i])
                    return false;
                //fuori dalla tabella mi fermo
                if (i != par.Length - 1 && (x - 1) < 0)
                    return false;
                //vado in basso
                x--;
            }
            x++;
            //salvo i char verificati
            for (int i = in_x; i >= x; i--)
            {
                ver[in_y][i] = true;
                just_found.Add((in_y, i));
            }

            return true;
        }
        static bool AltoSinistra(string par, int in_x, int in_y)
        {
            int x = in_x;
            int y = in_y;
            //controllo ogni lettera della parola
            for (int i = 0; i < par.Length; i++)
            {
                //lettera diversa mi fermo
                if (tab[y][x] != par[i])
                    return false;
                //fuori dalla tabella mi fermo
                if (i != par.Length - 1 && ((x - 1) < 0 || (y - 1) < 0))
                    return false;
                //vado in alto a sinistra
                x--; y--;
            }
            //salvo i char verificati
            x = in_x;
            y = in_y;
            for (int i = 0; i < par.Length; i++)
            {
                ver[y][x] = true;
                just_found.Add((y, x));
                x--; y--;
            }

            return true;
        }
        static bool BassoSinistra(string par, int in_x, int in_y)
        {
            int x = in_x;
            int y = in_y;
            //controllo ogni lettera della parola
            for (int i = 0; i < par.Length; i++)
            {
                //lettera diversa mi fermo
                if (tab[y][x] != par[i])
                    return false;
                //fuori dalla tabella mi fermo
                if (i != par.Length - 1 && ((x - 1) < 0 || (y + 1) >= tab.Count))
                    return false;
                //vado in alto a sinistra
                x--; y++;
            }
            //salvo i char verificati
            x = in_x;
            y = in_y;
            for (int i = 0; i < par.Length; i++)
            {
                ver[y][x] = true;
                just_found.Add((y, x));
                x--; y++;
            }

            return true;
        }
        static bool AltoDestra(string par, int in_x, int in_y)
        {
            int x = in_x;
            int y = in_y;
            //controllo ogni lettera della parola
            for (int i = 0; i < par.Length; i++)
            {
                //lettera diversa mi fermo
                if (tab[y][x] != par[i])
                    return false;
                //fuori dalla tabella mi fermo
                if (i != par.Length - 1 && ((x + 1) >= tab[y].Count || (y - 1) < 0))
                    return false;
                //vado in alto a sinistra
                x++; y--;
            }
            //salvo i char verificati
            x = in_x;
            y = in_y;
            for (int i = 0; i < par.Length; i++)
            {
                ver[y][x] = true;
                just_found.Add((y, x));
                x++; y--;
            }

            return true;
        }
        static bool BassoDestra(string par, int in_x, int in_y)
        {
            int x = in_x;
            int y = in_y;
            //controllo ogni lettera della parola
            for (int i = 0; i < par.Length; i++)
            {
                //lettera diversa mi fermo
                if (tab[y][x] != par[i])
                    return false;
                //fuori dalla tabella mi fermo
                if (i != par.Length - 1 && ((x + 1) >= tab[y].Count || (y + 1) >= tab.Count))
                    return false;
                //vado in alto a sinistra
                x++; y++;
            }
            //salvo i char verificati
            x = in_x;
            y = in_y;
            for (int i = 0; i < par.Length; i++)
            {
                ver[y][x] = true;
                just_found.Add((y, x));
                x++; y++;
            }

            return true;
        }
        #endregion

        static void Main(string[] args)
        {
            Console.Title = "Risolvitore di Crucipuzzle by Fabio Fantini 3H.";

            #region Scelta della risoluzione
            bool step_by_step = false;
            while (true)
            {
                Console.Clear();
                //stampa menù
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("CRUCIPUZZLE RESOLVER\n");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("[R]isolvi");
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("[S]tep-by-step\n");
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("--> ");
                //input
                string input = Console.ReadLine();
                input = input.ToUpper();
                //scelta
                if (input == "R") break;

                else if (input == "S")
                {
                    step_by_step = true;
                    break;
                }
                //se non valido, cicla
                else continue;
            }
            Console.Clear();
            #endregion

            //Controlli di lettura
            bool no_par = false; //non ci sono parole
            bool no_tab = false; //non c'è il tabellone

            #region Lettura da file
            using (StreamReader sr = new StreamReader(file_input))
            {
                int count = 0;   //contatore delle righe
                string line = sr.ReadLine();
                //lettura della tabella
                while (line != "" && line != null)
                {
                    //creo una riga
                    tab.Add(new List<char>());
                    ver.Add(new List<bool>());
                    //riempo la riga di caratteri
                    for (int i = 0; i < line.Length; i++)
                    {
                        tab[count].Add(line[i]);
                        ver[count].Add(false);
                    }
                    //passo alla riga dopo
                    count++;
                    line = sr.ReadLine();
                }
                //riempimento stack di parole da trovare
                while (!sr.EndOfStream)
                {
                    string par = sr.ReadLine();

                    if (par != "" && par != null)
                        parole.Push(par);

                }

                sr.Close();
            }
            #endregion

            if (tab.Count == 0)
                no_tab = true;
            if (parole.Count == 0)
                no_par = true;

#if false
            //stampa la matrice
            for (int i = 0; i < tab.Count; i++)
            {
                for (int j = 0; j < tab[i].Count; j++)
                {
                    Console.Write(tab[i][j] + " ");
                }
                Console.WriteLine();
            }
            Console.ReadKey();
#endif
            if (!no_tab && !no_par)
            {
                #region Ricerche delle parole
                while (parole.Count > 0)
                {
                    //trov mi permettere di fare un break di tutti i cicli, per passare direttamente alla prossima parola
                    bool trov = false;
                    //prendo e tolgo la parola dallo stack
                    string par = parole.Pop();
                    //passo per i char
                    for (int i = 0; i < tab.Count; i++)
                    {
                        if (trov) break;

                        for (int j = 0; j < tab[i].Count; j++)
                        {
                            if (trov) break;

                            //se la prima lettera è uguale al char, incomincio i controlli
                            if (tab[i][j] == par[0])
                            {
                                #region Controlli
                                trov = true;

                                if (Alto(par, j, i))
                                    continue;
                                if (Basso(par, j, i))
                                    continue;
                                if (Sinistra(par, j, i))
                                    continue;
                                if (Destra(par, j, i))
                                    continue;
                                if (AltoDestra(par, j, i))
                                    continue;
                                if (AltoSinistra(par, j, i))
                                    continue;
                                if (BassoDestra(par, j, i))
                                    continue;
                                if (BassoSinistra(par, j, i))
                                    continue;

                                trov = false;
                                #endregion
                            }
                        }
                    }
                    if (step_by_step && trov)
                    {//Stampa tabella e parola per il step-by-step
                        Stampa();
                        just_found.Clear();
                        Console.WriteLine("\n\n" + par);
                        Console.ReadKey();
                        Console.Clear();
                    }
                }
                #endregion

                #region Stampa parola misteriosa 😦
                just_found.Clear();
                //stampa interfaccia
                Stampa();
                Console.WriteLine("\nParola Nascosta:");
                //stampa parola + scrittura su file
                using (StreamWriter sw = new StreamWriter(file_output))
                {
                    for (int i = 0; i < ver.Count; i++)
                    {
                        for (int j = 0; j < ver[i].Count; j++)
                        {
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            if (!ver[i][j])
                            {
                                Console.Write(tab[i][j] + " ");
                                sw.Write(tab[i][j] + " ");
                            }
                        }
                    }
                }
                Console.ForegroundColor = ConsoleColor.White;
                #endregion
            }
            else
            {
                //Avvisi di errore
                if (no_par)
                    Console.WriteLine("Non ci sono parole nel file.");
                if (no_tab)
                    Console.WriteLine("Non c'è il tabellone delle lettere nel file.");
            }
            Console.ReadKey();
        }
    }
}