using System;
using System.Collections.Generic;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace LibreriaClassi
{
    
    internal class AnimatoPilotato : AnimatoInAcqua
    {
        protected bool dx = true;
        public Window Finestra {  get; protected set; }
        public Canvas Canvas { get; protected set; }
        public AnimatoPilotato(Image immagine, bool flippato, int step, Window finestra, int ms = 0, Thickness target = new Thickness())
            :base(immagine, ms, step, target, flippato)
        {
            Finestra = finestra;
        }

        private void Movimento(object sender, KeyEventArgs e)
        {
            // Posizione X e Y corrente
            double currentX = Canvas.GetLeft(Immagine);
            double currentY = Canvas.GetTop(Immagine);
            if (double.IsNaN(currentX)) currentX = 0;
            if (double.IsNaN(currentY)) currentY = 0;

            // Cambio della posizione in base al tasto premuto
            switch (e.Key)
            {
                case Key.Up:
                    currentY -= NumeroDiStep;
                    break;
                case Key.Down:
                    currentY += NumeroDiStep;
                    break;
                case Key.Left:
                    currentX -= NumeroDiStep;
                    if (dx) { FlipImmagine(); dx = false; }
                    break;
                case Key.Right:
                    currentX += NumeroDiStep;
                    if (!dx) { FlipImmagine(); dx = true; }
                    break;
            }

            // Imposta il movimento
            Canvas.SetLeft(Immagine, currentX);
            Canvas.SetTop(Immagine, currentY);
        }

        public override void IniziaAnimazione(Canvas canvas)
        {
            // Aggiunta immagine al canvas e connessione dell'evento al tasto premuto
            Immagine.Name = "AnimatoPilotato";
            canvas.Children.Add(Immagine);
            Finestra.KeyDown += Movimento;
            Canvas = canvas;
        }
    }
}
