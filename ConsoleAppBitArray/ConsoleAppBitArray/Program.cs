namespace ConsoleAppBitArray
{
    internal class Program
    {
        class BitArray
        {
            private const int BITS_PER_VALUE = 32;
            private uint[] bits;
            private long n_bits;

            public BitArray(long n_bits)
            {
                this.n_bits = n_bits;
                bits = new uint[n_bits / BITS_PER_VALUE];
            }
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
        }
    }
}
