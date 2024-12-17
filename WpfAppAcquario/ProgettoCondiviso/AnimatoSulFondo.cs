using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Media;
using System.Windows.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Diagnostics;

namespace ProgettoCondiviso
{
    internal class AnimatoSulFondo : AnimatoSulPosto
    {
        private TransformGroup transformGroup;
        private TranslateTransform translate; // Transform per spostare l'immagine
        private double currLeft = 0;
        private bool flag = true;

        public double DistXZero {  get; private set; }
        public Thickness PosizioneIniziale { get; private set; }
        public Thickness PosizioneFinale { get; private set; }
        public int CambioPosizione { get; private set; }

        // TODO, settare N.Step anzichè cambioPos
        public AnimatoSulFondo(string immagine, Thickness posInizio, Thickness posFine, int altezza, int lunghezza, Size grandezza, int intervallo, int cambioPos)
            : base(immagine, posInizio, altezza, lunghezza, grandezza, intervallo) // Viene creata l'immagine
        {
            translate = new TranslateTransform();

            // Se la fine si trova più a sinistra dell'inizio ==> swap
            if (posInizio.Left > posFine.Left)
            {
                Thickness temp = new Thickness();
                temp = posInizio;
                posInizio = posFine;
                posFine = temp;
            }
            
            // Imposta pos. Iniziale e Finale in base ai valori
            if (posInizio.Left < posFine.Left)
            {
                PosizioneIniziale = posInizio;
                PosizioneFinale = posFine;
            }
            else
            {
                PosizioneIniziale = posFine;
                PosizioneFinale = posInizio;
            }

            CambioPosizione = cambioPos;
            DistXZero = Immagine.Margin.Left;
        }
        private void TickAnimazione(object sender, EventArgs e) // Animazione per ogni tick
        {
            //Canvas.SetLeft(Immagine, PosizioneFinale.Left);

            // TODO Flip
            // Cambio della direzione se si superano i limiti
            if (DistXZero <= PosizioneIniziale.Left)
                flag = true;
            else if (DistXZero >= PosizioneFinale.Left)
                flag = false;

            // Cambio della posizione
            if (flag)
            {
                DistXZero += CambioPosizione;
                translate.X += CambioPosizione; 
            }
            else
            {
                DistXZero -= CambioPosizione;
                translate.X -= CambioPosizione;
            }
            
            // Aggiunta dei trasform alla immagine
            transformGroup = new TransformGroup();
            transformGroup.Children.Add(translate);
            transformGroup.Children.Add(flip);
            Immagine.RenderTransform = transformGroup;
        }

        public override void IniziaAnimazione(Canvas canvas) // Aggiunge l'immagine al canvas
        {
            Immagine.Margin = new Thickness(Immagine.Margin.Left + .1, canvas.Height - Immagine.RenderSize.Height, Immagine.Margin.Right, 0);
            timer.Tick += new EventHandler(TickAnimazione);
            canvas.Children.Add(Immagine);
            timer.Start();
        }
    }
}
