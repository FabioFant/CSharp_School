// Fabio Fantini 4H 2024-10-18
// Classi figure geometriche con eredietarità

namespace ConsoleAppFigureGeometriche
{
    internal class Program
    {
        class FiguraGeometrica
        {
            public FiguraGeometrica() { }

            public virtual double Area() { return 0; }
            public virtual double Perimetro() { return 0; }

            // TODO : Fare ToString
        }
        class Cerchio : FiguraGeometrica
        {
            private double _raggio;
            public double Raggio { get { return _raggio; } private set { _raggio = value; } }
            public Cerchio(double raggio) { _raggio = raggio; }

            public override double Area() { return Math.PI * (Raggio * Raggio); }
            public override double Perimetro() { return Raggio * 2 * Math.PI; }
        }
        // TODO
        class Triangolo : FiguraGeometrica { }
        class Quadrato : FiguraGeometrica
        {
            private double _lato;
            public double Lato { get { return _lato; } private set { _lato = value; } }
            public Quadrato(double lato) { Lato = lato; }

            public override double Area() { return Lato * Lato; }
            public override double Perimetro() { return Lato * 4; }
        }
        class Rettangolo : Quadrato
        {
            private double _altezza;
            public double Altezza { get { return _altezza; } private set { _altezza = value; } }
            public Rettangolo(double lato, double altezza) : base(lato) { _altezza = altezza; }

            public override double Area() { return Lato * Altezza; }
            public override double Perimetro() { return (Lato + Altezza) * 2; }
        }
        class Rombo : Quadrato
        {
            private double _diagonaleMaggiore;
            private double _diagonaleMinore;
            public double DiagonaleMaggiore { get { return _diagonaleMaggiore; } private set { _diagonaleMaggiore = value; } }
            public double DiagonaleMinore { get { return _diagonaleMinore; } private set { _diagonaleMinore = value; } }
            public Rombo(double lato, double diagonaleMaggiore, double diagonaleMinore)
                : base(lato)
            { 
                DiagonaleMaggiore = diagonaleMaggiore; 
                DiagonaleMinore = diagonaleMinore;
            }

            public override double Area() { return (DiagonaleMaggiore * DiagonaleMinore) / 2; }
            public override double Perimetro() { return Lato * 4; }
        }
        // TODO
        // class Trapezio : Rettangolo { }
        static void Main(string[] args)
        {

        }
    }
}
