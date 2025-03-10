// Fabio Fantini 4H 2025-02-24
// Convoluzione asincrona con prograssbar

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
using System;
using System.IO;
using System.Threading.Tasks;

namespace WpfAppConvoluzioneAsync
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string _fileName = ""; // Nome del file

        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnCaricaFoto_Click(object sender, RoutedEventArgs e)
        {
            //Create OpenFile Dialog
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

            //Set filter for file extension and default file extension
            dlg.DefaultExt = ".png";
            dlg.Filter = "Image Files (*.jpg; *.jpeg; *.png; *.gif; *.bmp; *.tiff; *.webp)|*.jpg;*.jpeg;*.png;*.gif;*.bmp;*.tiff;*.webp"; ;

            //Display OpenFileDIalog by calling ShowDialog method
            Nullable<bool> result = dlg.ShowDialog();

            //Get the selected file name and display
            if (result == true)
            {
                _fileName = dlg.FileName;
                BitmapImage bitMap = new BitmapImage(new Uri(_fileName));
                lblNomeFile.Content = _fileName;
                imgFoto.Source = bitMap;
            }
            else
            {
                MessageBox.Show("File selezionato non esistente");
            }
        }

        private async void btnTrasforma_Click(object sender, RoutedEventArgs e)
        {
            btnTrasforma.IsEnabled = false;

            // Input della matrice
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

            Bitmap imgOriginale = new Bitmap(_fileName); // Creazione dell'immagine

            Bitmap imgRisultato = new Bitmap(imgOriginale.Width - 2, imgOriginale.Height - 2); // Nuova immagine

            // Avvio del task asincrono
            Task<Bitmap> ConvoluzioneImmagine = Convoluzione(imgOriginale, matControl);
            imgRisultato = await ConvoluzioneImmagine;

            imgFoto.Source = BitmapToBitmapSource(imgRisultato); // Visualizza l'immagine nuova

            btnTrasforma.IsEnabled = true;
        }

        private async Task<Bitmap> Convoluzione(Bitmap img, int[,] matrice)
        {
            Bitmap imgRis = new Bitmap(img.Width, img.Height); // Immagine vuota

            // Set progressbar
            lblEsito.Content = "Convoluzione in corso...";
            progressBar.Minimum = 0;
            progressBar.Maximum = (img.Width * img.Height) / 2; // Area dell'immagine, numero di pixel

            for (int i = 0; i < img.Width; i++)
            {
                for (int j = 0; j < img.Height; j++)
                {
                    imgRis.SetPixel(i, j, CalcolaConvoluzione(img, i, j, matrice)); // Set del pixel con convoluzione
                    progressBar.Value = img.Width * i + j + 1;
                    await Task.Delay(1); // TODO : fixare questo
                }
            }

            lblEsito.Content = "Convoluzione terminata";
            return imgRis;

        }

        private System.Drawing.Color CalcolaConvoluzione(Bitmap img, int x, int y, int[,] matrice)
        {
            int R, G, B;
            R = 0;
            G = 0;
            B = 0;

            //Calcolo il prodotto della matrice

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    // Calcolo della posizione da calcolare
                    int posX = x + i - 1;
                    int posY = y + j - 1;

                    // Gestisce la fuoriuscita dai margini
                    if (posX < 0) { posX = 0; }
                    else if (posX >= img.Width) { posX = img.Width - 1; }

                    if (posY < 0) { posY = 0; }
                    else if (posY >= img.Height) { posY = img.Height - 1; }

                    // Definizione del nuovo pixel
                    System.Drawing.Color pixel = img.GetPixel(posX, posY);
                    R += pixel.R * matrice[i, j];
                    G += pixel.G * matrice[i, j];
                    B += pixel.B * matrice[i, j];
                }

                //Controllo il valore del colore
                R = ControllaValore(R);
                G = ControllaValore(G);
                B = ControllaValore(B);
            }

            return System.Drawing.Color.FromArgb(R, G, B);
        }

        private static int ControllaValore(int subPixel)
        {
            // Evita che il valore del colore rimanga fra [0-255]
            if (subPixel < 0)
                return 0;
            else if (subPixel > 255)
                return 255;

            return subPixel;
        }

        static BitmapSource BitmapToBitmapSource(Bitmap img)
        {
            // Convert the System.Drawing.Bitmap to a MemoryStream
            using (MemoryStream memoryStream = new MemoryStream())
            {
                img.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Png);  // Save the Bitmap to the MemoryStream
                memoryStream.Position = 0;  // Reset the stream's position to the beginning

                // Create a BitmapImage from the MemoryStream
                BitmapImage bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = memoryStream;
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;  // Ensure the image is fully loaded
                bitmapImage.EndInit();

                return bitmapImage;  // Return the BitmapImage, which is a BitmapSource
            }
        }
    }
}