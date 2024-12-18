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
        public Image Immagine { get; protected set; }
        public Inanimato(Image immagine)
        {
            Immagine = immagine;
        }

        protected void RimuoviImmagine() { Immagine.Source = null; }
        public virtual void IniziaAnimazione(Canvas canvas) { canvas.Children.Add(Immagine); } // Aggiunge l'immagine al canvas

    }
}
