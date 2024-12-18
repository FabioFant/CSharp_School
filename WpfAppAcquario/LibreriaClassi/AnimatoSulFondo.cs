using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Media;
using System.Windows.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Diagnostics;
using System.Windows.Interop;

namespace ProgettoCondiviso
{
    internal class AnimatoSulFondo : AnimatoSulPosto
    {
        protected TransformGroup transformGroup; // Transform per impostare tutte le modifiche
        protected TranslateTransform translate; // Transform per spostare l'immagine
        protected int currSteps = 0; // Step fatti da inizio a fine
        protected bool reverseX = false; // Gira la direzione del movimento orizzontale

        public virtual int NumeroDiStep { get; protected set; }
        public Thickness Target { get; protected set; }

        public double LunghezzaPassoX { get { return Math.Abs((Target.Left - Immagine.Margin.Left) / NumeroDiStep); } }
        public int Intervallo {  get { return Ms / NumeroDiStep; } }

        public AnimatoSulFondo(Image immagine, int ms, int NStep, Thickness target, bool flippato)
            : base(immagine, ms, flippato)
        {
            // Init
            translate = new TranslateTransform();
            NumeroDiStep = NStep;
            Target = target;

            // Direzione di movimento opposta se il target si trova prima
            if (Immagine.Margin.Left > target.Left) reverseX = true;
        }
        private void TickAnimazione(object sender, EventArgs e) // Animazione per ogni tick
        {
            // Posizione X corrente
            double currentX = Canvas.GetLeft(Immagine);
            if (double.IsNaN(currentX)) currentX = 0;

            // Incremento opposto se la direzione è inversa
            double incr = LunghezzaPassoX;
            if (reverseX) incr *= -1;

            // Cambio X in base al numero di step fatti
            double newX = currentX;
            if (currSteps < NumeroDiStep)   newX += incr;
            else if(currSteps < NumeroDiStep * 2)    newX -= incr;
            
            // Effettuo uno step
            Canvas.SetLeft(Immagine, newX);
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
            // Sposta l'immagine sul fondo nella posizione iniziale
            Immagine.Margin = new Thickness(Immagine.Margin.Left, canvas.Height - Immagine.RenderSize.Height, Immagine.Margin.Right, 0);

            // Set del motore e inizio
            motor.Interval = TimeSpan.FromMilliseconds(Intervallo);
            motor.Tick += new EventHandler(TickAnimazione);
            canvas.Children.Add(Immagine);
            motor.Start();
        }
    }
}
