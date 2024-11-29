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
        private Image _immagine;
        public Inanimato(string immagine, Thickness posizione, int altezza, int lunghezza, Size grandezza)
        {
            Uri source; // Fornisce una rappresentazione in forma di oggetto
            source = new Uri(@$"pack://application:,,,/Immagini/{immagine}", UriKind.RelativeOrAbsolute);
            BitmapImage bitmap = new BitmapImage(source); // Elemento bitmap
            _immagine = new Image(); // Controllo per la visualizzazione di un'immagine
            _immagine.Source = bitmap;
            _immagine.Margin = posizione;
            _immagine.Height = altezza;
            _immagine.Width = lunghezza;
            _immagine.RenderSize = grandezza;
            //canvasAcquario.Children.Add(immagine);
        }
    }
}
