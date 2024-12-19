using ProgettoCondiviso;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace LibreriaClassi
{
    internal class AnimatoPilotatoSilurato : AnimatoPilotato
    {
        static int velocità_siluro = 5;
        List<(Inanimato, bool)> siluri;
        string nomeImm;

        public AnimatoPilotatoSilurato(Image immagine, string nome, bool flippato, int step, Window finestra, int ms = 10)
            : base(immagine, flippato, step, finestra, ms)
        {
            siluri = new List<(Inanimato, bool)>();
            nomeImm = nome;

            motor.Interval = TimeSpan.FromMilliseconds(Ms);
            motor.Tick += new EventHandler(TickProiettili);

            Immagine.Name = "AnimatoPilotatoSilurato";
        }

        public override void IniziaAnimazione(Canvas canvas)
        {
            // Aggiunta immagine al canvas e connessione dell'evento al tasto premuto
            canvas.Children.Add(Immagine);
            Finestra.KeyDown += Movimento;
            Canvas = canvas;
            motor.Start();
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
                    CreaProiettile(currentX, currentY, dx);
                    break;
            }

            // Imposta il movimento
            Canvas.SetLeft(Immagine, currentX);
            Canvas.SetTop(Immagine, currentY);
        }

        private void CreaProiettile(double x, double y, bool dx)
        {
            Inanimato siluro = new Inanimato(ImageManager.CreaImmagine(nomeImm, Immagine.Margin, 25, 25, new Size(25, 25)));

            Canvas.SetTop(siluro.Immagine, y);
            Canvas.SetLeft(siluro.Immagine, x);

            siluro.IniziaAnimazione(Canvas);
            siluri.Add((siluro, dx));
        }

        private void TickProiettili(object sender, EventArgs e)
        {
            List<Image> image_garbage = new List<Image>();
            List<(Inanimato, bool)> sil_garbage = new List<(Inanimato, bool)>();

            foreach ((Inanimato sil, bool isDx) in siluri)
            {
                // Posizione X corrente
                double currentX = Canvas.GetLeft(sil.Immagine);
                if (double.IsNaN(currentX)) currentX = 0;

                // Incremento opposto se la direzione è inversa
                double incr = velocità_siluro;
                if (!isDx) incr *= -1;
                double newX = currentX + incr;

                // Effettuo uno step
                Canvas.SetLeft(sil.Immagine, newX);

                foreach(Image imm in Canvas.Children.OfType<Image>())
                {
                    if (imm.Name == "AnimatoInAcqua" || imm.Name == "AnimatoSulFondo" || imm.Name == "AnimatoSulPosto")
                    {
                        if (ImageManager.Collisione(sil.Immagine, imm))
                        {
                            image_garbage.Add(imm);
                            sil_garbage.Add((sil, isDx));
                        }
                    } 
                }

                if ((newX + sil.Immagine.Margin.Left < 0 || newX > Canvas.ActualWidth - sil.Immagine.Margin.Left) && !sil_garbage.Contains((sil, isDx)))
                    sil_garbage.Add((sil, isDx));
            }

            foreach (Image imm in image_garbage)
                Canvas.Children.Remove(imm);

            foreach((Inanimato sil, bool isDx) in sil_garbage)
            {
                Canvas.Children.Remove(sil.Immagine);
                siluri.Remove((sil, isDx));
            }
        }
    }
}
