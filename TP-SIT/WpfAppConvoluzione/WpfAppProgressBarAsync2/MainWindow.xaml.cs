// Fabio Fantini 4H 2025-03-10
// ProgressBar Asincrono con un'altra risoluzione a quella precedenza

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

namespace WpfAppProgressBarAsync2
{
    /// <summary>
    /// Logica di interazione per MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            btnStart.IsEnabled = false; // Evita più chiamate

            // Avvio dei progressbar e memorizzazione delle task
            Task TaskStartProgressBarAsync01 = StartProgressBarAsync01(700);
            Task TaskStartProgressBarAsync02 = StartProgressBarAsync02(500);

            await Task.Delay(800);      // Simulate long operation

            // Istruzioni bloccanti, si attende che le due task siano terminate
            await TaskStartProgressBarAsync01;
            await TaskStartProgressBarAsync02;

            btnStart.IsEnabled = true; // Riattiva il bottone, è assicurato che le due progressbar siano terminate
        }

        private async Task StartProgressBarAsync01(int tempo)
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
        }

        private async Task StartProgressBarAsync02(int tempo)
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
        }
    }
}
