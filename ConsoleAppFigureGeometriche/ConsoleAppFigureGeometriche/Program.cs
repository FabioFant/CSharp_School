// Fabio Fantini 4H 2024-10-18
// Classi figure geometriche con eredietarità

using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;

namespace ConsoleAppFigureGeometriche
{
    internal class Program
    {
        #region Figure Geometriche
        class FiguraGeometrica
        {
            public FiguraGeometrica() { }

            public virtual double Area() { return 0; }
            public virtual double Perimetro() { return 0; }
            public override string ToString() { return $"Figura Geometrica"; }
            protected double Radianti(double gradi) { return gradi * (Math.PI / 180); }
        }
        class Cerchio : FiguraGeometrica
        {
            private double _raggio;
            public double Raggio { get { return _raggio; } private set { _raggio = value; } }
            public Cerchio(double raggio) { _raggio = raggio; }

            public override double Area() { return Math.PI * (Raggio * Raggio); }
            public override double Perimetro() { return Raggio * 2 * Math.PI; }
            public override string ToString() { return $"{base.ToString()} > Cerchio: Raggio = {Raggio}"; }
        }
        class Triangolo : FiguraGeometrica 
        {
            private double _lato1;
            private double _lato2;
            private double _angoloCompreso;
            public double Lato1  { get { return _lato1; } private set { _lato1 = value; } }
            public double Lato2 { get { return _lato2; } private set { _lato2 = value; } }
            public double AngoloCompreso 
            { 
                get { return _angoloCompreso; } 
                private set 
                {
                    // Controlla i gradi
                    if (value < 0 || value >= 360 || value == 180)
                        throw new ArgumentException();

                    if (value < 180)
                        _angoloCompreso = value;
                    else // Se l'angolo è > 180, lo si trasforma in uno di < 180
                        _angoloCompreso = 360 - value;
                } 
            }
            public Triangolo(double lato1, double lato2, double angoloCompreso) 
            { 
                Lato1 = lato1;
                Lato2 = lato2;
                AngoloCompreso = angoloCompreso;
            }

            public double CalcolaTerzoLato() { return Math.Sqrt((Lato1 * Lato1) + (Lato2 * Lato2) - (2 * Lato1 * Lato2 * Math.Cos(Radianti(AngoloCompreso)))); }
            public override double Area() { return 0.5 * Lato1 * Lato2 * Math.Sin(Radianti(AngoloCompreso)); }
            public override double Perimetro() { return Lato1 + Lato2 + CalcolaTerzoLato(); }
            public override string ToString() 
                { return $"{base.ToString()} > Triangolo: Lato 1 = {Lato1}, Lato 2 = {Lato2}, Angolo Compreso = {AngoloCompreso}"; }
        }

        #region Quadrati
        class Quadrato : FiguraGeometrica
        {
            private double _lato;
            public double Lato { get { return _lato; } protected set { _lato = value; } }
            public Quadrato(double lato) { Lato = lato; }

            public override double Area() { return Lato * Lato; }
            public override double Perimetro() { return Lato * 4; }
            public override string ToString() { return $"{base.ToString()} > Quadrato: Lato = {Lato}"; }
        }
        class Rombo : Quadrato
        {
            private double _angoloInterno;
            public double AngoloInterno 
            { 
                get { return _angoloInterno; }
                private set
                {
                    // Controlla i gradi
                    if (value < 0 || value >= 360 || value == 180)
                        throw new ArgumentException();

                    if (value < 180)
                        _angoloInterno = value;
                    else // Se l'angolo è > 180, lo si trasforma in uno di < 180
                        _angoloInterno = 360 - value;
                }
            }
            public Rombo(double lato, double angoloInterno) : base(lato) { AngoloInterno = angoloInterno; }

            public override double Area() { return (Lato * Lato) * Math.Sin(AngoloInterno); }
            public override double Perimetro() { return Lato * 4; }
            public override string ToString() { return $"{base.ToString()} > Rombo: Lato = {Lato}, Angolo Interno = {AngoloInterno}"; }
        }

        #region Rettangoli
        class Rettangolo : Quadrato
        {
            private double _altezza;
            public double Altezza { get { return _altezza; } private set { _altezza = value; } }
            public Rettangolo(double lato, double altezza) : base(lato) { _altezza = altezza; }

            public override double Area() { return Lato * Altezza; }
            public override double Perimetro() { return (Lato + Altezza) * 2; }
            public override string ToString() { return $"{base.ToString()} > Rettangolo: Base = {Lato}, Altezza = {Altezza}"; }
        }
        class Trapezio : Rettangolo 
        {
            private double _baseMinore; // Lato = BaseMaggiore
            public double BaseMinore { 
                get { return _baseMinore; } 
                private set 
                { 
                    _baseMinore = value;
                    if (_baseMinore > Lato) // Se BaseMin > BaseMax --> Swap
                    {
                        double temp = _baseMinore;
                        _baseMinore = Lato;
                        Lato = temp;
                    }
                } 
            }
            public Trapezio(double baseMinore, double baseMaggiore, double altezza) 
                : base(baseMaggiore, altezza) { BaseMinore = baseMinore; }

            public override double Area() { return ((BaseMinore + Lato) * Altezza) / 2; }
            public override double Perimetro() 
            {
                double c = (Lato - BaseMinore) / 2; // Cateto
                double latoObliquo = Math.Sqrt((c * c) + (Altezza * Altezza));

                return Lato + BaseMinore + (latoObliquo * 2);
            }
            public override string ToString() { return $"{base.ToString()} > Trapezio: Base Maggiore = {Lato}, Base Minore = {BaseMinore}, Altezza = {Altezza}"; }
        }
        #endregion

        #endregion

        #endregion

        static void Main(string[] args)
        {
            #region Test
            FiguraGeometrica figuraGeometrica = new FiguraGeometrica();
            FiguraGeometrica cerchio = new Cerchio(5.0);
            FiguraGeometrica triangolo = new Triangolo(4.0, 5.0, 315);
            FiguraGeometrica quadrato = new Quadrato(5.0);
            FiguraGeometrica rombo = new Rombo(5.0, 315);
            FiguraGeometrica rettangolo = new Rettangolo(5.0, 4.0);
            FiguraGeometrica trapezio = new Trapezio(4.0, 5.0, 3.0);

            Console.WriteLine($"{figuraGeometrica}\nArea: {figuraGeometrica.Area()} cm^2\nPerimetro: {figuraGeometrica.Perimetro()} cm^2\n");
            Console.WriteLine($"{cerchio}\nArea: {cerchio.Area()} cm^2\nPerimetro: {cerchio.Perimetro()} cm^2\n");
            Console.WriteLine($"{triangolo}\nArea: {triangolo.Area()} cm^2Perimetro: {triangolo.Perimetro()} cm^2\n");
            Console.WriteLine($"{quadrato}\nArea: {quadrato.Area()} cm^2\nPerimetro: {quadrato.Perimetro()} cm^2\n");
            Console.WriteLine($"{rombo}\nArea: {rombo.Area()} cm^2\nPerimetro:  {rombo.Perimetro()} cm^2\n");
            Console.WriteLine($"{rettangolo}\nArea: {rettangolo.Area()} cm^2\nPerimetro:  {rettangolo.Perimetro()} cm^2\n");
            Console.WriteLine($"{trapezio}\nArea: {trapezio.Area()} cm^2\nPerimetro:  {trapezio.Perimetro()} cm^2\n");
            #endregion
        }
    }
}
