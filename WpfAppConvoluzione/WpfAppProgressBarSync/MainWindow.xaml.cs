// Fabio Fantini 4H 2025-03-10
// ProgressBar Sincrono

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

namespace WpfAppProgressBarSync
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Chimata metodi
            StartProgressBarSync01(1000);
            StartProgressBarSync02(1000);
        }

        private void StartProgressBarSync01(int tempo) // Velocità di progressione
        {
            txtSync01.Text = "Start ProgressBar";

            // Definizione range
            ProgressBarSync01.Minimum = 0;
            ProgressBarSync01.Maximum = tempo;

            // Progressione
            for (int i = 0; i <= tempo; i++)
            {
                ProgressBarSync01.Value = i; // Simulate long operation
                Thread.Sleep(1);
            }
            txtSync01.Text = "End ProgressBarSync";
        }

        private void StartProgressBarSync02(int tempo)
        {
            txtSync02.Text = "Start ProgressBar";

            // Definizione Range
            ProgressBarSync02.Minimum = 0;
            ProgressBarSync02.Maximum = tempo;

            // Progressione
            for(int i = 0; i <= tempo; i++)
            {
                ProgressBarSync02.Value = i; // Simulate long operation
                Thread.Sleep(1);
            }
            txtSync02.Text = "End ProgressBarSync";
        }
    }
}
