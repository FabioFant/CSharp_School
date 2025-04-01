using LibreriaClassi;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Threading;

namespace ProgettoCondiviso
{
    internal class AnimatoSulPosto : Inanimato
    {
        protected DispatcherTimer motor; // Motore delle animazioni
        protected ScaleTransform flip; // Transform per ruotare l'immagine

        public int Ms { get; protected set; } // Ms fra ogni tick

        public AnimatoSulPosto(Image immagine, int ms, bool flippato)
            : base(immagine)
        {
            // Init
            flip = new ScaleTransform();
            motor = new DispatcherTimer();
            Ms = ms;

            Immagine.RenderTransformOrigin = new Point(0.5, 0.5); // Serve per ruotare l'immagine dal centro

            if (flippato) FlipImmagine();
        }
        private void TickAnimazione(object sender, EventArgs e) { FlipImmagine(); }

        protected void FlipImmagine()
        {
            flip.ScaleX *= -1;
            Immagine.RenderTransform = flip;
        }

        public override void IniziaAnimazione(Canvas canvas)
        {
            Immagine.Name = "AnimatoSulFondo";
            // Inserisce l'inizializza il motore e incomincia l'animazione
            motor.Tick += new EventHandler(TickAnimazione);
            motor.Interval = TimeSpan.FromMilliseconds(Ms);
            base.IniziaAnimazione(canvas);
            motor.Start();
        }
    }
}
