// Fabio Fantini 4H 2024-11-22
// WpfApp che presenta un'animazione di un acquario

using System.Text;
using System.Windows;
using System.Windows.Threading;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using LibreriaClassi;
using ProgettoCondiviso;
using System.Windows.Interop;

namespace WpfAppAcquario
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            double p = 50; // Standard piccolo
            double g = 100; // Standard grande

            // Inanimato
            Inanimato corallo1 = new Inanimato(ImageManager.CreaImmagine("corallo.png", new Thickness(200, canvasAcquario.Height - p, 0 , 0) , p, p, new Size(p, p)));
            Inanimato corallo2 = new Inanimato(ImageManager.CreaImmagine("corallo.png", new Thickness(300, canvasAcquario.Height - p, 0, 0), p, p, new Size(p, p)));
            Inanimato corallo3 = new Inanimato(ImageManager.CreaImmagine("corallo.png", new Thickness(600, canvasAcquario.Height - p, 0, 0), p, p, new Size(p, p)));

            // AnimatoSulPosto
            AnimatoSulPosto alga1 = new AnimatoSulPosto(ImageManager.CreaImmagine("alga.png", new Thickness(0, canvasAcquario.Height - g, 0, 0), g, g, new Size(g, g)), 500, true);
            AnimatoSulPosto alga2 = new AnimatoSulPosto(ImageManager.CreaImmagine("alga.png", new Thickness(canvasAcquario.Width - g, canvasAcquario.Height - g, 0, 0), g, g, new Size(g, g)), 500, true);
            AnimatoSulPosto alga3 = new AnimatoSulPosto(ImageManager.CreaImmagine("alga.png", new Thickness(100, canvasAcquario.Height - p, 0, 0), p, p, new Size(p, p)), 500, false);
            AnimatoSulPosto alga4 = new AnimatoSulPosto(ImageManager.CreaImmagine("alga.png", new Thickness(canvasAcquario.Width - p - 100, canvasAcquario.Height - p, 0, 0), p, p, new Size(p, p)), 500, false);

            // AnimatoSulFondo
            AnimatoSulFondo tartaruga = new AnimatoSulFondo(ImageManager.CreaImmagine("tartaruga.png", new Thickness(50, canvasAcquario.Height - p, 0, 0), p, p, new Size(p, p)), 6000, 300, new Thickness(300, 0, 0, 0), false);
            AnimatoSulFondo cavalluccio = new AnimatoSulFondo(ImageManager.CreaImmagine("cavalluccio.png", new Thickness(200, canvasAcquario.Height - 50, 0, 0), p, p, new Size(p, p)), 3500, 175, new Thickness(600, 0, 0, 0), true);

            // AnimatoInAcqua
            AnimatoInAcqua pesce = new AnimatoInAcqua(ImageManager.CreaImmagine("pesce.png", new Thickness(100, 150, 0, 0), p, p, new Size(p, p)), 3000, 90, new Thickness(300, 100, 0, 0), false);
            AnimatoInAcqua pesce_pagliaccio = new AnimatoInAcqua(ImageManager.CreaImmagine("pesce_pagliaccio.png", new Thickness(650, 200, 0, 0), p, p, new Size(p, p)), 4000, 120, new Thickness(400, 150, 0, 0), false);
            AnimatoInAcqua polpo = new AnimatoInAcqua(ImageManager.CreaImmagine("polpo.png", new Thickness(300, 200, 0, 0), p*2, p, new Size(p*2, p)), 5000, 150, new Thickness(350, 300, 0, 0), false);
            AnimatoInAcqua banco1 = new AnimatoInAcqua(ImageManager.CreaImmagine("banco.png", new Thickness(0, 100, 0, 0), g, g, new Size(g, g)), 7000, 200, new Thickness(canvasAcquario.Width - g, 100, 0, 0), false);
            AnimatoInAcqua banco2 = new AnimatoInAcqua(ImageManager.CreaImmagine("banco.png", new Thickness(canvasAcquario.Width - g, 175, 0, 0), g, g, new Size(g, g)), 6000, 175, new Thickness(0, 175, 0, 0), true);
            AnimatoInAcqua banco3 = new AnimatoInAcqua(ImageManager.CreaImmagine("banco.png", new Thickness(0, 250, 0, 0), g, g, new Size(g, g)), 5000, 150, new Thickness(canvasAcquario.Width - g, 250, 0, 0), false);

            // AnimatoPilotato
            AnimatoPilotato pescepalla = new AnimatoPilotato(ImageManager.CreaImmagine("pescepalla.png", new Thickness(650, 100, 0, 0), p, p, new Size(p, p)), false, 5, this);

            // AnimatoPilotatoSilurante
            AnimatoPilotatoSilurato medusa = new AnimatoPilotatoSilurato(ImageManager.CreaImmagine("medusa.png", new Thickness(100, 300, 0, 0), p, p, new Size(p, p)), "bolla.png", false, 5, this);

            #region Init
            corallo1.IniziaAnimazione(canvasAcquario);
            corallo2.IniziaAnimazione(canvasAcquario);
            corallo3.IniziaAnimazione(canvasAcquario);

            alga1.IniziaAnimazione(canvasAcquario);
            alga2.IniziaAnimazione(canvasAcquario);
            alga3.IniziaAnimazione(canvasAcquario);
            alga4.IniziaAnimazione(canvasAcquario);

            tartaruga.IniziaAnimazione(canvasAcquario);
            cavalluccio.IniziaAnimazione(canvasAcquario);
            polpo.IniziaAnimazione(canvasAcquario);
            banco1.IniziaAnimazione(canvasAcquario);
            banco2.IniziaAnimazione(canvasAcquario);
            banco3.IniziaAnimazione(canvasAcquario);

            pesce.IniziaAnimazione(canvasAcquario);
            pesce_pagliaccio.IniziaAnimazione(canvasAcquario);

            pescepalla.IniziaAnimazione(canvasAcquario);

            medusa.IniziaAnimazione(canvasAcquario);
            #endregion
        }
    }
}