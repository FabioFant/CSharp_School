using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Media;
using System.Windows.Threading;
using System.Windows;
using System.Windows.Controls;

namespace ProgettoCondiviso
{
    internal class AnimatoSulFondo : AnimatoSulPosto
    {
        private TransformGroup transformGroup;
        private TranslateTransform translate; // Transform per spostare l'immagine
        private double currLeft = 0;
        private bool flag = true;

        public Thickness PosizioneIniziale { get; private set; }
        public Thickness PosizioneFinale { get; private set; }
        public int CambioPosizione { get; private set; }

        public AnimatoSulFondo(string immagine, Thickness posInizio, Thickness posFine, int altezza, int lunghezza, Size grandezza, int intervallo, int cambioPos)
            : base(immagine, posInizio, altezza, lunghezza, grandezza, intervallo) // Viene creata l'immagine
        {
            transformGroup = new TransformGroup();
            translate = new TranslateTransform();

            // Se la fine si trova più a sinistra dell'inizio ==> swap
            if (posInizio.Left > posFine.Left)
            {
                Thickness temp = new Thickness();
                temp = posInizio;
                posInizio = posFine;
                posFine = temp;
            }
            
            // Init delle variabili
            PosizioneIniziale = posInizio;
            PosizioneFinale = posFine;
            CambioPosizione = cambioPos;
        }
        private void TickAnimazione(object sender, EventArgs e) // Animazione per ogni tick
        {
            // TODO : FIX : Non va il flip
            // Cambio del lato se si superano i limiti
            if(Immagine.Margin.Left <= PosizioneIniziale.Left)
            {
                flag = true;
                flip.ScaleX *= -1;
            }
            else if(Immagine.Margin.Left >= PosizioneFinale.Left)
            {
                flag = false;
                flip.ScaleX *= -1;
            }
            
            
            // Cambio della posizione
            if (flag) currLeft += CambioPosizione;
            else currLeft -= CambioPosizione;
            translate = new TranslateTransform(currLeft, 0);
   
            // Aggiunta dei trasform alla immagine
            transformGroup.Children.Add(translate);
            transformGroup.Children.Add(flip);
            Immagine.RenderTransform = translate;
        }

        public override void IniziaAnimazione(Canvas canvas) // Aggiunge l'immagine al canvas
        {
            Immagine.Margin = new Thickness(Immagine.Margin.Left, canvas.Height - Immagine.RenderSize.Height, Immagine.Margin.Right, 0);
            timer.Tick += new EventHandler(TickAnimazione);
            canvas.Children.Add(Immagine);
            timer.Start();
        }
    }
}
