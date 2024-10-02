// Fabio Fantini 4H 2024-10-02
// Utilizzo della classe

using System.Text;

namespace ConsoleAppSymmetricCrypt
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Fabio Fantini 4H 2024-10-02\n");

            // Statico
            string frase = "Io sono un surgo.";
            string key = "Tu invece no.";

            string c_frase = SymmetricCrypt.Crypt(frase, key);
            string d_frase = SymmetricCrypt.Crypt(c_frase, key);

            Console.WriteLine($"Metodi statici:\n{frase} = {c_frase} = {d_frase}");

            // Istanza
            SymmetricCrypt crypt = new SymmetricCrypt(key);
            c_frase = crypt.Crypt(frase);
            d_frase = crypt.Crypt(c_frase);

            Console.WriteLine($"Metodi istanza:\n{frase} = {c_frase} = {d_frase}");
        }
    }
}
