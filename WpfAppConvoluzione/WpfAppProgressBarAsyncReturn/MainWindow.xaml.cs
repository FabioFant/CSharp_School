// Fabio Fantini 4H 2025-03-10
// ProgressBar Asincrono con valori di ritorno

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace WpfAppProgressBarAsyncReturn
{
    /// <summary>
    /// Logica di interazione per MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Random _random = new Random(); // Valori di ritorno dei progressbar

        public MainWindow()
        {
            InitializeComponent();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            btnStart.IsEnabled = false; // Evita più chiamate

            // Avvio dei progressbar e memorizzazione delle task
            Task<int> TaskStartProgressBarAsync01 = StartProgressBarAsync01(700);
            Task<int> TaskStartProgressBarAsync02 = StartProgressBarAsync02(500);

            await Task.Delay(2000); // Simulate long operation

            int cas2 = await TaskStartProgressBarAsync02; // Raccoglie il risultato quando il progressbar ha terminato
            txtAsync02.Text = "Restituito " + cas2.ToString(); // Stampa del valore

            await Task.Delay(3000); // Simulate long operation

            int cas1 = await TaskStartProgressBarAsync01; // Raccoglie il risultato quando il progressbar ha terminato
            txtAsync01.Text = "Restituito " + cas1.ToString(); // Stampa del valore

            btnStart.IsEnabled = true; // Riattiva il bottone, è assicurato che le due progressbar siano terminate
        }

        // Task<int> singifica che il task ritorna un intero
        private async Task<int> StartProgressBarAsync01(int tempo)
        {
            txtAsync01.Text = "Start ProgressBar";

            // Definizione Range
            ProgressBarAsync01.Minimum = 0;
            ProgressBarAsync01.Maximum = tempo;

            // Progressione
            for (int i = 0; i <= tempo; i++)
            {
                ProgressBarAsync01.Value = i; // Simulate long operation
                await Task.Delay(1);
            }

            // Finisce e controlla che anche l'altra ProgressBar termini
            txtAsync01.Text = "End ProgressBarAsync";
            return (_random.Next(1, 101));
        }

        private async Task<int> StartProgressBarAsync02(int tempo)
        {
            txtAsync02.Text = "Start ProgressBar";

            // Definizione Range
            ProgressBarAsync02.Minimum = 0;
            ProgressBarAsync02.Maximum = tempo;

            // Progressione
            for (int i = 0; i <= tempo; i++)
            {
                ProgressBarAsync02.Value = i; // Simulate long operation
                await Task.Delay(1);
            }

            // Finisce e controlla che anche l'altra ProgressBar termini
            txtAsync02.Text = "End ProgressBarAsync";
            return (_random.Next(1, 101));
        }
    }
}
