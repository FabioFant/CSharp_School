using ProgettoCondiviso;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace LibreriaClassi
{
    internal class AnimatoPilotatoSilurato : AnimatoPilotato
    {
        static int velocità_siluro = 5;

        Image immSiluro;
        List<Inanimato> siluri;
        public AnimatoPilotatoSilurato(Image immagine, Image proiettile, bool flippato, int step, Window finestra)
            : base(immagine, flippato, step, finestra)
        {
            immSiluro = proiettile;
            siluri = new List<Inanimato>();

            motor.Interval = TimeSpan.FromMilliseconds(Intervallo);
            motor.Tick += new EventHandler(TickProiettili);
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
                case Key.W:
                    currentY -= NumeroDiStep;
                    break;
                case Key.S:
                    currentY += NumeroDiStep;
                    break;
                case Key.A:
                    currentX -= NumeroDiStep;
                    if (dx) { FlipImmagine(); dx = false; }
                    break;
                case Key.D:
                    currentX += NumeroDiStep;
                    if (!dx) { FlipImmagine(); dx = true; }
                    break;
                case Key.Space:
                    CreaProiettile();
                    break;
            }

            // Imposta il movimento
            Canvas.SetLeft(Immagine, currentX);
            Canvas.SetTop(Immagine, currentY);
        }

        private void CreaProiettile()
        {
            Inanimato siluro = new Inanimato(immSiluro);
            Image immIstanza = siluro.Immagine;

            double variazione = dx ? immIstanza.Width : -immIstanza.Width;
            immIstanza.Margin = new Thickness(immIstanza.Margin.Left + variazione, immIstanza.Margin.Top, immIstanza.Margin.Right, immIstanza.Margin.Bottom);

            siluro.IniziaAnimazione(Canvas);
            siluri.Add(siluro);
        }

        private void TickProiettili(object sender, EventArgs e)
        {
            foreach (Inanimato sil in siluri)
            {
                // Posizione X corrente
                double currentX = Canvas.GetLeft(sil.Immagine);
                if (double.IsNaN(currentX)) currentX = 0;

                // Incremento opposto se la direzione è inversa
                double incr = velocità_siluro;
                if (!dx) incr *= -1;

                // Effettuo uno step
                Canvas.SetLeft(sil.Immagine, currentX + incr);
            }
        }

        public override void IniziaAnimazione(Canvas canvas)
        {
            // Aggiunta immagine al canvas e connessione dell'evento al tasto premuto
            canvas.Children.Add(Immagine);
            Finestra.KeyDown += Movimento;
            Canvas = canvas;
        }
    }
}
