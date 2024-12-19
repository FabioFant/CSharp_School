using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows;

namespace LibreriaClassi
{
    internal static class ImageManager
    {
        public static Image CreaImmagine(string nome, Thickness pos, double altezza, double lunghezza, Size grandezza)
        {
            Image immagine;
            Uri source; // Fornisce una rappresentazione in forma di oggetto
            source = new Uri(@$"pack://application:,,,/Immagini/{nome}", UriKind.RelativeOrAbsolute);
            BitmapImage bitmap = new BitmapImage(source); // Elemento bitmap
            immagine = new Image(); // Controllo per la visualizzazione di un'immagine
            immagine.Margin = pos;
            immagine.Source = bitmap;
            immagine.Height = altezza;
            immagine.Width = lunghezza;
            immagine.RenderSize = grandezza;

            return immagine;
        }
        public static bool Collisione(Image imm1, Image imm2)
        {
            Rect rect1 = new Rect(
                new Point(Convert.ToDouble(imm1.GetValue(Canvas.LeftProperty)),
                          Convert.ToDouble(imm1.GetValue(Canvas.TopProperty))),
                new Point((Convert.ToDouble(imm1.GetValue(Canvas.LeftProperty)) + imm1.ActualWidth),
                          (Convert.ToDouble(imm1.GetValue(Canvas.TopProperty)) + imm1.ActualHeight)));

            Rect rect2 = new Rect(
                new Point(Convert.ToDouble(imm2.GetValue(Canvas.LeftProperty)),
                          Convert.ToDouble(imm2.GetValue(Canvas.TopProperty))),
                new Point((Convert.ToDouble(imm2.GetValue(Canvas.LeftProperty)) + imm2.ActualWidth),
                          (Convert.ToDouble(imm2.GetValue(Canvas.TopProperty)) + imm2.ActualHeight)));

            rect1.Intersect(rect2);
            return !(rect1 == Rect.Empty);
        }
    }
}
