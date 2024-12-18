using ProgettoCondiviso;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;

namespace LibreriaClassi
{
    internal class AnimatoInAcqua : AnimatoSulFondo
    {
        protected bool reverseY = false; // Gira la direzione di movimento verticale

        public double LunghezzaPassoY { get { return Math.Abs((Target.Top - Immagine.Margin.Top) / NumeroDiStep); } }

        public AnimatoInAcqua(Image immagine, int ms, int NStep, Thickness target, bool flippato)
            : base(immagine, ms, NStep, target, flippato)
        {
            if (Immagine.Margin.Top > target.Top) reverseY = true;
        }
        private void TickAnimazione(object sender, EventArgs e) // Animazione per ogni tick
        {
            // Posizione X e Y corrente
            double currentX = Canvas.GetLeft(Immagine);
            if (double.IsNaN(currentX)) currentX = 0;
            double currentY = Canvas.GetTop(Immagine);
            if (double.IsNaN(currentY)) currentY = 0;

            // Incremento opposto se la direzione è inversa
            double incrX = LunghezzaPassoX;
            if (reverseX) incrX *= -1;
            double incrY = LunghezzaPassoY;
            if (reverseY) incrY *= -1;

            // Cambio X e Y in base al numero di step fatti
            double newX = currentX;
            double newY = currentY;
            if (currSteps < NumeroDiStep)
            {
                newX += incrX;
                newY += incrY;
            }
            else if (currSteps < NumeroDiStep * 2)
            {
                newX -= incrX;
                newY -= incrY;
            }

            // Effettuo uno step
            Canvas.SetLeft(Immagine, newX);
            Canvas.SetTop(Immagine, newY);
            currSteps++;

            // Flip e reset step una volta raggiunte le destinazioni
            if (currSteps == NumeroDiStep) FlipImmagine();
            else if (currSteps == NumeroDiStep * 2)
            {
                FlipImmagine();
                currSteps = 0;
            }
        }

        public override void IniziaAnimazione(Canvas canvas)
        {
            // Set del motore e inizio
            motor.Interval = TimeSpan.FromMilliseconds(Intervallo);
            motor.Tick += new EventHandler(TickAnimazione);
            canvas.Children.Add(Immagine);
            motor.Start();
        }
    }
}
