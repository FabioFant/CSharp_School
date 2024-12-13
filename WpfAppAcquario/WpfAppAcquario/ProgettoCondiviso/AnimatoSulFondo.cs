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

        public double DistanzaRispettoZero {  get; private set; }
        public Thickness PosizioneIniziale { get; private set; }
        public Thickness PosizioneFinale { get; private set; }
        public int CambioPosizione { get; private set; }

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
            
            // Init delle variabili
            PosizioneIniziale = posInizio;
            PosizioneFinale = posFine;
            CambioPosizione = cambioPos;
            DistanzaRispettoZero = Immagine.Margin.Left;
        }
        private void TickAnimazione(object sender, EventArgs e) // Animazione per ogni tick
        {
            Debug.WriteLine(DistanzaRispettoZero);

            // TODO FIX flip
            // Cambio del lato se si superano i limiti
            if (DistanzaRispettoZero < PosizioneIniziale.Left)
            {
                //Canvas.SetLeft(Immagine, PosizioneIniziale.Left);
                flag = true;
                flip.ScaleX = Math.Abs(flip.ScaleX);
                translate.X = PosizioneIniziale.Left;
                Debug.Write("Sinistra, ");
            }
            else if (DistanzaRispettoZero >= PosizioneFinale.Left)
            {
                //Canvas.SetLeft(Immagine, PosizioneFinale.Left);
                flag = false;
                flip.ScaleX = -Math.Abs(flip.ScaleX);
                translate.X = PosizioneFinale.Left;
                Debug.Write("Destra, ");
            }
            // Cambio della posizione
            if (flag)
            {
                DistanzaRispettoZero += CambioPosizione;
                translate.X += CambioPosizione;
                Debug.WriteLine("Avanti"); 
            }
            else
            {
                DistanzaRispettoZero -= CambioPosizione;
                translate.X -= CambioPosizione;
                Debug.WriteLine("Indietro");
            }
            
            // Aggiunta dei trasform alla immagine
            transformGroup = new TransformGroup();
            transformGroup.Children.Add(translate);
            transformGroup.Children.Add(flip);
            Immagine.RenderTransform = transformGroup;
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
