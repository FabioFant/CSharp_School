// Fabio Fantini 4H 2024-10-09
// Classe BitArray con test

namespace BitArrays
{
    // Questa classe funziona come un:
    //
    //     bool[] vect = new bool[N];
    //
    // con N molto grande
    class BitArray
    {
        private const int BITS_PER_VALUE = 32;  // ogni (unit) ha 32 bit
        private uint[] bits;
        private long n_bits;

        public BitArray(long n_bits, bool initial_value)
        {
            long n_uints = n_bits / BITS_PER_VALUE;
            if (n_bits % BITS_PER_VALUE != 0)
                ++n_uints;  // serve un (uint) in più per i bits residui

            if (n_uints > int.MaxValue - 60)  // il valore massimo è stato trovato per tentativi
                throw new OverflowException("Too many bits");

            this.bits = new uint[n_uints];
            this.n_bits = n_bits;
            SetAllBits(initial_value);
        }

        public bool GetBit(long bit_index)
        {
            int uint_index = (int)bit_index / BITS_PER_VALUE;
            int bit_index_in_uint = (int)(bit_index % BITS_PER_VALUE);
            int mask = (1 << bit_index_in_uint); // Creo una mask dove di tutti 0, dove l'1 si trova nella posizione del bit
            return (bits[uint_index] & mask) != 0; // Operazione di AND fra l'uint e la mask.
        }
        public void SetBit(long bit_index, bool value)
        {
            int uint_index = (int)bit_index / BITS_PER_VALUE;
            int bit_index_in_uint = (int)(bit_index % BITS_PER_VALUE);
            int mask = (int)(1 << bit_index_in_uint);
            if (value) // SET: Se bisogna portare il bit a 1, OR
                bits[bit_index_in_uint] |= (uint)mask; // Mask con 0 quasi ovunque

            else      // RESET: Se bisogna portare il bit a 0, AND
                bits[bit_index_in_uint] &= ~(uint)mask; // Mask con 1 quasi ovunque
        }
        public void SetAllBits(bool value)  // imposta tutti i bit a true o false
        {
            uint _value = 0;
            if (value)
                _value = ~_value;

            for (int i = 0; i < bits.Length; ++i)
                bits[i] = _value;
        }
        public bool this[long bit_index]
        {
            get { return GetBit(bit_index); } 
            set { SetBit(bit_index, value); } 
        }
    }
    internal class Program
    {
        static List<long> EratosthenesSieve(long max_value)
        {
            List<long> primes = new List<long>();

            BitArray sieve = new BitArray(max_value + 1, true);
            for (long n = 2; n <= max_value; ++n)
            {
                if (sieve[n])  // se true, allora n è primo
                {
                    primes.Add(n);
                    for (long mult_n = (n << 1); mult_n <= max_value; mult_n += n)  // genera tutti i multipli di n, che vanno marcati come non-primi
                    {
                        sieve[mult_n] = false;
                    }
                }
            }

            return primes;
        }

        static void Main(string[] args)
        {
            List<long> primes = EratosthenesSieve(100);

            foreach (long n in primes)
            {
                Console.Write($"{n}, ");
            }
                
            Console.WriteLine();
        }
    }
}