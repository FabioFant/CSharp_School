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
using ProgettoCondiviso;

namespace WpfAppAcquario
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //SetupTimer();
            //AggiungiOggetti();

            Inanimato corallo = new Inanimato("corallo.png", new Thickness(500, canvasAcquario.Height - 50, 0, 0), 50, 50, new Size(50, 50));
            AnimatoSulPosto alga = new AnimatoSulPosto("alga.png", new Thickness(200, canvasAcquario.Height - 50, 0, 0), 50, 50, new Size(50, 50), 500);
            AnimatoSulFondo tartaruga = new AnimatoSulFondo("tartaruga.png", new Thickness(200, 0, 0, 0), new Thickness(500, 0, 0, 0), 50, 50, new Size(50, 50), 100, 10);
            corallo.IniziaAnimazione(canvasAcquario);
            alga.IniziaAnimazione(canvasAcquario);
            tartaruga.IniziaAnimazione(canvasAcquario);
        }

        #region Metodi di riferimento
        const int MILLISECONDI = 300; // Numero di ms per fra un tick e l'altro
        DispatcherTimer dispatcherTimer; // Motore per le animazioni
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
            source = new Uri(@"/Immagini/cavalluccio.png", UriKind.RelativeOrAbsolute);
            BitmapImage bitmap = new BitmapImage(source); // Elemento bitmap
            immagine = new Image(); // Controllo per la visualizzazione di un'immagine
            immagine.Source = bitmap;
            immagine.Margin = new Thickness(300, 50, 0, 0);
            immagine.Height = 50;
            immagine.Width = 50;
            immagine.RenderSize = new Size(50, 50);
            canvasAcquario.Children.Add(immagine);
        }

        int x = 0;
        int y = 0;
        double scalax = 1.0;
        double scalay = 1.0;
        int gradi = 0;
        private void btnTranslate_Click(object sender, RoutedEventArgs e)
        {
            TranslateTransform translateTransform;
            translateTransform = new TranslateTransform(--x, ++y);
            immagine.RenderTransform = translateTransform;
        }
        private void btn_Rotate_Click(object sender, RoutedEventArgs e)
        {
            RotateTransform rotateTransform;
            rotateTransform = new RotateTransform(++gradi, 100, 100);
            immagine.RenderTransform = rotateTransform;
        }
        private void btn_Scale_Click(object sender, RoutedEventArgs e)
        {
            ScaleTransform scaleTransform;
            scalax += 0.1;
            scalay += 0.1;
            scaleTransform = new ScaleTransform(scalax, scalay, 100, 100);
            immagine.RenderTransform = scaleTransform;
        }

        private void btn_Transform_Click(object sender, RoutedEventArgs e)
        {
            TransformGroup transfromGroup = new TransformGroup();
            ScaleTransform scaleTransform;
            TranslateTransform translateTransform;
            RotateTransform rotateTransform;

            scaleTransform = new ScaleTransform(0.5, 0.5, immagine.Width / 2, immagine.Height / 2);
            scaleTransform = new ScaleTransform(-0.5, -0.5);
            scaleTransform = new ScaleTransform(scalax, scalay);

            translateTransform = new TranslateTransform(++x, ++y);
            rotateTransform = new RotateTransform(45, immagine.Width / 2, immagine.Height / 2);
            rotateTransform = new RotateTransform(++gradi);

            transfromGroup.Children.Add(scaleTransform);
            transfromGroup.Children.Add(rotateTransform);
            transfromGroup.Children.Add(translateTransform);

            immagine.RenderTransform = transfromGroup;
        }
        #endregion
    }
}