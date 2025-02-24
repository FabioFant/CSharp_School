// Fabio Fantini 4H 2025-02-24
// Applicazione per caricamento immagini e applicazione di un filtro personalizzato su di esse

using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Drawing;
using System.ComponentModel;
using System.Drawing.Configuration;
using System.Drawing.Printing;

namespace WpfAppFiltriImmagini
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string _fileName = "";  // Percorso file immagine

        static WriteableBitmap Convoluzione(WriteableBitmap img, int[,] matrice)
        {
            WriteableBitmap imgRis = new WriteableBitmap(img.Width, img.Height);
            for(int i = 0; i < img.Width - 1; i++)
                for(int j = i; j < img.Height - 1; j++)
                    imgRis.SetPixel(i, j, CalcolaConvoluzione(img, i, j, matrice));

            return imgRis;
        }

        static System.Drawing.Color CalcolaConvoluzione(WriteableBitmap img, int x, int y, int[,] matrice)
        {
            int R, G, B;
            R = G = B = 0;

            for(int i = 0; i < 3; i++)
                for(int j = 0; j < 3; j++)
                {
                    System.Drawing.Color pixel = img.GetPixel(x + i - 1, y + j - 1);
                    R += pixel.R * matrice[i, j];
                    G += pixel.G * matrice[i, j];
                    B += pixel.B * matrice[i, j];
                }
        }

        static BitmapImage BitmapToBitmapSource(WriteableBitmap img)
        {
            
        }

        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnCaricaFoto_Click(object sender, RoutedEventArgs e)
        {
            // Finestra scelta file
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.DefaultExt = ".png";
            dlg.Filter = "Image files (*.jpg; *.jpeg; *.png; *.gif; *.bmp; *.tiff; *.webp) |*.jpg;*.jpeg;*.png;*.gif;*.bmp;*.tiff;*.webp";

            // Aperta finestra con esito
            Nullable<bool> result = dlg.ShowDialog();

            if(result == true)
            {
                // Caricamento foto nell'applicazione
                _fileName = dlg.FileName;
                BitmapImage bitmap = new BitmapImage(new Uri(_fileName));
                lblNomeFile.Content = _fileName;
                imgFoto.Source = bitmap;
            }
            else
            {
                MessageBox.Show("File non recuperato", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnTrasforma_Click(object sender, RoutedEventArgs e)
        {
            int[,] matControl = new int[3, 3];
            matControl[0, 0] = int.Parse(txt00.Text);
            matControl[0, 1] = int.Parse(txt01.Text);
            matControl[0, 2] = int.Parse(txt02.Text);
            matControl[1, 0] = int.Parse(txt10.Text);
            matControl[1, 1] = int.Parse(txt11.Text);
            matControl[1, 2] = int.Parse(txt12.Text);
            matControl[2, 0] = int.Parse(txt20.Text);
            matControl[2, 1] = int.Parse(txt21.Text);
            matControl[2, 2] = int.Parse(txt22.Text);

            WriteableBitmap imgOriginale = new WriteableBitmap(source:_fileName);

            WriteableBitmap imgRisultato = new WriteableBitmap(imgOriginale.Width - 2, imgOriginale.Height - 2, 0, 0, );

            imgRisultato = Convoluzione(imgOriginale, matControl);

            imgFoto.Source = BitmapToBitmapSource(imgRisultato);
        }
    }
}