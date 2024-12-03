using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interop;
using System.Windows.Media.Imaging;

namespace ProgettoCondiviso
{
    internal class Inanimato
    {
        public Image Immagine { get; private set; }
        public Inanimato(string immagine, Thickness posizione, int altezza, int lunghezza, Size grandezza)
        {
            Uri source; // Fornisce una rappresentazione in forma di oggetto
            source = new Uri(@$"pack://application:,,,/Immagini/{immagine}", UriKind.RelativeOrAbsolute);
            BitmapImage bitmap = new BitmapImage(source); // Elemento bitmap
            Immagine = new Image(); // Controllo per la visualizzazione di un'immagine
            Immagine.Source = bitmap;
            Immagine.Margin = posizione;
            Immagine.Height = altezza;
            Immagine.Width = lunghezza;
            Immagine.RenderSize = grandezza;
            //canvasAcquario.Children.Add(immagine);
        }

        public virtual void AggiungiImmagine(Canvas canvas) { canvas.Children.Add(Immagine); } // Aggiunge l'immagine al canvas
            
    }
}
