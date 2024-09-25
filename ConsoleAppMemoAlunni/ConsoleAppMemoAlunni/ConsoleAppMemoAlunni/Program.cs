// Fabio Fantini 4H 2024-09-20
// Memorizzazione di 200 Alunni con l'utilizzo di una classe

namespace ConsoleAppMemoAlunni
{
    internal class Program
    {
        const string        file_input = @"..\..\..\input.txt";
        static List<Alunno> alunni = new List<Alunno>();
        static int          numeroAlunno = 0; // Usato per individuare l'alunno durante la lettura dell'input

        static void LeggiAlunni()
        {
            using (StreamReader sr = new StreamReader(file_input))
            {
                while (!sr.EndOfStream)
                {
                    // Lettura riga e aggiornamento numero alunno
                    string[] dati = sr.ReadLine().Split("\t");
                    numeroAlunno++;

                    // Controlli input
                    if (dati.Length != 6)
                    {
                        Console.WriteLine($"ERRORE : Non sono presenti un totale di 6 dati nel {numeroAlunno}° alunno.");
                        continue;
                    }
                    char genere;
                    if (!char.TryParse(dati[2], out genere) || (genere != 'F' && genere != 'M'))
                    {
                        Console.WriteLine($"ERRORE : Genere non valido per il {numeroAlunno}° alunno.");
                        continue;
                    }
                    DateOnly dataDiNascita;
                    if (!DateOnly.TryParse(dati[3], out dataDiNascita))
                    {
                        Console.WriteLine($"ERRORE : Data di nascita non valida per il {numeroAlunno}° alunno.");
                        continue;
                    }

                    // Creazione e aggiunta alunno
                    alunni.Add(new Alunno(dati[0], dati[1], genere, dataDiNascita, dati[4], dati[5]));
                }
            }
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Fabio Fantini 4H 2024-09-20");

            // Lettura input
            LeggiAlunni();
            Console.WriteLine();

            // Stampa dati salvati
            foreach(Alunno alunno in alunni)
                Console.WriteLine("{0}\t{1}\t{2}\t{3}\t{4}\t{5}", 
                    alunno.Nome,   alunno.Cognome,
                    alunno.Genere, alunno.DataDiNascita,
                    alunno.Classe, alunno.Indirizzo);
        }
    }
}
