//Fabio Fantini 3H 2024-03-12
//Risolvitore di Crucipuzzle da file su xml

using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfAppCrucipuzzle
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static bool step_by_step = false;                               //bool per metodo di risoluzione
        static Button[,] buttons = null;                                //tabella bottoni
        static List<List<char>> tab = new List<List<char>>();           //tabellone dei caratteri
        static List<List<bool>> ver = new List<List<bool>>();           //tabellone dei caratteri trovati
        static Stack<string> parole = new Stack<string>();              //stack contenente le parole ancora da trovare
        static List<(int, int)> just_found = new List<(int, int)>();    //lista che memorizza le coordinate delle lettere della parola appena trovata (step-by-step)

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

        public MainWindow()
        {
            InitializeComponent();
        }

        static void ModifyGrid()
        {
            ;
            for (int i = 0; i < tab.Count; i++)
            {
                for (int j = 0; j < tab[i].Count; j++)
                {
                    if (ver[i][j] == true) //giallo se trovato
                    {
                        buttons[i, j].Background = Brushes.Gold;
                    }


                    if (step_by_step && just_found.Contains((i, j))) //(step-by-step)
                        buttons[i, j].Background = Brushes.DarkCyan;
                }
            }
        }

        private void file_btn_Click(object sender, RoutedEventArgs e)
        {
            //Reset variabili
            tab.Clear();
            ver.Clear();
            parole.Clear();
            just_found.Clear();
            //Apertura file
            var dialog = new OpenFileDialog();
            dialog.FileName = "Document";
            dialog.DefaultExt = ".txt";
            dialog.Filter = "Text documents (.txt)|*.txt";
            dialog.InitialDirectory = System.IO.Path.GetFullPath(@"..\..\..\..\");
            bool? result = dialog.ShowDialog();
            if (result == null || result == false) return;
            string file_input = dialog.FileName;

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

            //Eccezioni da lettura
            if (tab.Count == 0)
                MessageBox.Show("Tabella mancante dal file.");
            if (parole.Count == 0)
                MessageBox.Show("Parole mancanti dal file.");

            CreateGrid();
        }

        private void CreateGrid()
        {
            //Crea la gmatrice di bottoni
            parolaNascosta_txt.Text = "";
            buttonGrid.Children.Clear();
            int h = tab.Count;
            int w = tab[0].Count;
            buttons = new Button[h, w];
            buttonGrid.Height = h * 30;
            buttonGrid.Width = w * 30;
            for (int i = 0; i < h; i++)
            {
                for (int j = 0; j < w; j++)
                {//Creo ogni singolo bottone
                    Button b = new Button();
                    b.Content = tab[i][j];
                    b.BorderBrush = Brushes.DarkGoldenrod;
                    b.BorderThickness = new Thickness(3, 3, 3, 3);
                    b.Background = Brushes.DarkGoldenrod;
                    b.Width = 29;
                    b.Height = 29;
                    b.HorizontalAlignment = HorizontalAlignment.Left;
                    b.VerticalAlignment = VerticalAlignment.Top;
                    b.Margin = new Thickness(j * 30, i * 30, 0, 0);
                    b.Tag = i + "," + j;
                    buttons[i, j] = b;
                    buttonGrid.Children.Add(b);
                }
            }
        }

        private void Resolve_btn_Click(object sender, RoutedEventArgs e)
        {
            step_by_step = false;

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
            }
            #endregion

            //Fine
            ModifyGrid();
            just_found.Clear();
            //stampa parola + scrittura su file
            parolaNascosta_txt.Text = "";
            for (int i = 0; i < ver.Count; i++)
            {
                for (int j = 0; j < ver[i].Count; j++)
                {
                    if (!ver[i][j])
                    {
                        parolaNascosta_txt.Text += tab[i][j];
                    }
                }
            }
        }

        private void trovaParola_btn_Click(object sender, RoutedEventArgs e)
        {
            if (parole.Count == 0)
            {
                parolaNascosta_txt.Text = "";
                //stampa parola + scrittura su file
                for (int i = 0; i < ver.Count; i++)
                {
                    for (int j = 0; j < ver[i].Count; j++)
                    {
                        if (!ver[i][j])
                        {
                            parolaNascosta_txt.Text += tab[i][j];
                        }
                    }
                }
                return;
            }

            #region Ricerca singola parola
            step_by_step = true;
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
            #endregion

            ModifyGrid();
            just_found.Clear();
        }
    }
}