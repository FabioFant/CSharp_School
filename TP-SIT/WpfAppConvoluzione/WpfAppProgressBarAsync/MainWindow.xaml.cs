// Fabio Fantini 4H 2025-03-10
// ProgressBar Asincrono

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

namespace WpfAppProgressBarAsync
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

        // async significa che viene avviato un nuovo processo in parallelo all'interfaccia, evitando che si blocchi
        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            btnStart.IsEnabled = false; // Evita più chiamate

            // Start dei ProgressBar
            StartProgressBarAsync01(700);
            await Task.Delay(800);      // Simulate long operation
            StartProgressBarAsync02(500);
        }

        private async void StartProgressBarAsync01(int tempo)
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
            if (txtAsync02.Text == "End ProgressBarAsync") btnStart.IsEnabled = true;
        }

        private async void StartProgressBarAsync02(int tempo)
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
            if (txtAsync01.Text == "End ProgressBarAsync") btnStart.IsEnabled = true;
        }
    }
}
