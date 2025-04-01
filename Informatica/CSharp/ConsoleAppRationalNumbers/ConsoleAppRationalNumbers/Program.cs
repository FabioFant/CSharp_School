// Fabio Fantini 4H 2024-10-07
// Classe numeri razionali

using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.IO.IsolatedStorage;

namespace RationalNumbers
{
    class RationalNumber
    {
        public static int Euclid(int a, int b)
        {
            //Debug.Assert(b < a && b >= 0);
            while (b > 0)
            {
                int r = a % b;
                a = b;
                b = r;
            }
            return a;
        }

        private int num, den;  // Dovrà sempre essere (den > 0)

        // Costruttori
        public RationalNumber(int num, int den)
        {
            if (den == 0)
                throw new Exception("Invalid denominator value");

            this.num = num;
            this.den = den;

            Normalize();
        }
        public RationalNumber(int number) : this(number, 1) { }
        public RationalNumber() : this(0, 1) { }
        public RationalNumber(RationalNumber r) : this(r.num, r.den) { }

        private void Normalize()
        {
            if (den < 0)
            {
                den = -den;
                num = -num;
            }

            int MCD = Euclid(Math.Abs(num), den);
            Debug.Assert(num % MCD == 0 && den % MCD == 0);
            num /= MCD;
            den /= MCD;
        }

        #region Operatori
        // Operazioni
        public static RationalNumber operator +(RationalNumber a, RationalNumber b)
        {
            return new RationalNumber(a.num * b.den + b.num * a.den, a.den * b.den);
        }
        public static RationalNumber operator -(RationalNumber a, RationalNumber b)
        {
            return new RationalNumber(a.num * b.den - b.num * a.den, a.den * b.den);
        }
        public static RationalNumber operator *(RationalNumber a, RationalNumber b)
        {
            return new RationalNumber(a.num * b.num, a.den * b.den);
        }
        public static RationalNumber operator /(RationalNumber a, RationalNumber b)
        {
            if (b.num == 0)
            {
                throw new DivideByZeroException();
            }
            return new RationalNumber(a.num * b.den, a.den * b.num);
        }

        // Confronti
        public static bool operator <(RationalNumber a, RationalNumber b)
        {
            return a.num * b.den < b.num * a.den;
        }
        public static bool operator >(RationalNumber a, RationalNumber b)
        {
            return a.num * b.den > b.num * a.den;
        }
        public static bool operator <=(RationalNumber a, RationalNumber b)
        {
            return a.num * b.den <= b.num * a.den;
        }
        public static bool operator >=(RationalNumber a, RationalNumber b)
        {
            return a.num * b.den >= b.num * a.den;
        }
        public static bool operator ==(RationalNumber a, RationalNumber b)
        {
            return a.num * b.den == b.num * a.den;
        }
        public static bool operator !=(RationalNumber a, RationalNumber b)
        {
            return a.num * b.den != b.num * a.den;
        }

        // Unari
        public static RationalNumber operator +(RationalNumber a)
        {
            return a;
        }
        public static RationalNumber operator -(RationalNumber a)
        {
            return new RationalNumber(-a.num, a.den);
        }
        #endregion

        public override string ToString()
        {
            if (den == 1)
                return $"{num}";
            if (num < 0)
                return $"({num}/{den})";
            return $"{num}/{den}";
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            RationalNumber rA = new RationalNumber(2, 4);
            RationalNumber rB = new RationalNumber(1, 2);

            // Operazioni
            RationalNumber rSum = rA + rB;
            Console.WriteLine($"{rA} + {rB} = {rSum}");

            RationalNumber rDif = rA - rB;
            Console.WriteLine($"{rA} - {rB} = {rDif}");

            RationalNumber rMul = rA * rB;
            Console.WriteLine($"{rA} * {rB} = {rMul}");

            RationalNumber rDiv = rA / rB;
            Console.WriteLine($"{rA} / {rB} = {rDiv}");

            // Confronti
            Console.WriteLine($"{rA} <  {rB} = {rA <  rB}");
            Console.WriteLine($"{rA} >  {rB} = {rA >  rB}");

            Console.WriteLine($"{rA} <= {rB} = {rA <= rB}");
            Console.WriteLine($"{rA} >= {rB} = {rA >= rB}");

            Console.WriteLine($"{rA} == {rB} = {rA == rB}");
            Console.WriteLine($"{rA} != {rB} = {rA != rB}");

            // Unari
            Console.WriteLine($"(+{rA}) = {+rA}");
            Console.WriteLine($"(+{rB}) = {+rB}");

            Console.WriteLine($"(-{rA}) = {-rA}");
            Console.WriteLine($"(-{rB}) = {-rB}");
        }
    }
}
