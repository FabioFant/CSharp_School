using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;

namespace ProgettoCondiviso
{
    internal class AnimatoSulPosto : Inanimato
    {
        protected DispatcherTimer timer; // Motore delle animazioni
        protected ScaleTransform flip; // Transform per ruotare l'immagine

        public AnimatoSulPosto(string immagine, Thickness posizione, int altezza, int lunghezza, Size grandezza, int intervallo)
            :base(immagine, posizione, altezza, lunghezza, grandezza) // Viene creata l'immagine
        {
            flip = new ScaleTransform();
            Immagine.RenderTransformOrigin = new Point(0.5, 0.5); // Serve per ruotare l'immagine dal centro

            // Setup del motore
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(intervallo);
        }
        private void TickAnimazione(object sender, EventArgs e) // Animazione per ogni tick
        {
            flip.ScaleX *= -1;
            Immagine.RenderTransform = flip;
        }

        public override void IniziaAnimazione(Canvas canvas)
        {
            timer.Tick += new EventHandler(TickAnimazione);
            base.IniziaAnimazione(canvas);
            timer.Start();
        }
    }
}
