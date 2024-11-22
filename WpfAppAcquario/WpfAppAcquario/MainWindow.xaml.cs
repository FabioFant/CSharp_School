// Fabio Fantini 4H 2024-11-22
// WpfApp che presenta un'animazione di un acquario

using System.Text;
using System.Windows;
using System.Windows.Threading;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfAppAcquario
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        const int MILLISECONDI = 300; // Numero di ms per fra un tick e l'altro
        DispatcherTimer dispatcherTimer; // Motore per le animazioni

        public MainWindow()
        {
            InitializeComponent();
            SetupTimer();
            AggiungiOggetti();
        }

        void SetupTimer()
        {
            txtMillSec.Content = $"Ms: {MILLISECONDI}";

            dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Interval = TimeSpan.FromMilliseconds(MILLISECONDI);
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Start();
        }

        int i = 0;
        private void dispatcherTimer_Tick(object sender, EventArgs e) // Istruzioni per le animazioni
        {
            txtContatore.Content = $"N.Tick: {++i}";
        }

        Image immagine;
        private void AggiungiOggetti()
        {
            Uri source; // Fornisce una rappresentazione in forma di oggetto
            source = new Uri(@"/Immagini/pescepalla.png", UriKind.RelativeOrAbsolute);
            BitmapImage bitmap = new BitmapImage(source); // Elemento bitmap
            immagine = new Image(); // Controllo per la visualizzazione di un'immagine
            immagine.Source = bitmap;
            immagine.Margin = new Thickness(300, 50, 0, 0);
            immagine.Height = 50;
            immagine.Width = 50;
            immagine.RenderSize = new Size(50, 50);
            canvasAcquario.Children.Add(immagine);
        }
    }
}