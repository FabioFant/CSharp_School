using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ProgettoCondiviso
{
    internal class AnimatoInAcqua : AnimatoSulFondo
    {
        public double DistYZero { get; private set; }
        public AnimatoInAcqua(string immagine, Thickness posInizio, Thickness posFine, int altezza, int lunghezza, Size grandezza, int intervallo, int cambioPos)
            :base(immagine, posInizio, posFine, altezza, lunghezza, grandezza, intervallo, cambioPos)
        {
            
        }

        private void TickAnimazione(object sender, EventArgs e) // Animazione per ogni tick
        {
            
        }

        public override void IniziaAnimazione(Canvas canvas) // Aggiunge l'immagine al canvas
        {
            timer.Tick += new EventHandler(TickAnimazione);
            canvas.Children.Add(Immagine);
            timer.Start();
        }
    }
}
