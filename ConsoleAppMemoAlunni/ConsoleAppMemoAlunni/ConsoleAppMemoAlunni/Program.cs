// Fabio Fantini 4H 2024-09-20
// Memorizzazione di 200 Alunni con l'utilizzo di una classe

using System.Runtime.InteropServices;

namespace ConsoleAppMemoAlunni
{
    internal class Program
    {
        const string        file_input = @"..\..\..\input.txt";
        static List<Classe> istituto = new List<Classe>();
        static int          numeroAlunno = 0; // Usato per individuare l'alunno durante la lettura dell'input

        static void LeggiClassiAlunni()
        {
            using (StreamReader sr = new StreamReader(file_input))
            {
                while (!sr.EndOfStream)
                {
                    // Lettura riga e aggiornamento numero alunno
                    string[] dati = sr.ReadLine().Split("\t");
                    numeroAlunno++;

                    char genere;
                    if (!char.TryParse(dati[2], out genere))
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
                    Indirizzo indirizzo;
                    if (!Indirizzo.TryParse(dati[5], out indirizzo))
                    {
                        Console.WriteLine($"ERRORE : Indirizzo non valido per il {numeroAlunno}° alunno.");
                        continue;
                    }
                    // Crea alunno e assegnalo ad una classe
                    Alunno alunno = new Alunno(dati[0], dati[1], genere, dataDiNascita, dati[4], indirizzo);
                    bool flag = false;
                    foreach(Classe classe in istituto)
                    {
                        if(classe.NomeClasse == alunno.Classe)
                        {
                            classe.AggiungiAlunno(alunno);
                            flag = true;
                        }
                    }
                    // Se la classe non è stata trovata allora la creo e poi aggiungo l'alunno
                    if (!flag)
                    {
                        Console.WriteLine(alunno.Classe[0] + alunno.Classe.Substring(1));
                        istituto.Add(new Classe(alunno.Classe[0], alunno.Classe.Substring(1), alunno.Indirizzo));
                        istituto[istituto.Count - 1].AggiungiAlunno(alunno);
                    }
                }
            }
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Fabio Fantini 4H 2024-09-20");

            // Lettura input
            LeggiClassiAlunni();
            Console.WriteLine();

#if true
            // Stampa dati salvati
            foreach (Classe classe in istituto)
                Console.WriteLine("{0}\t{1}",
                    classe.NomeClasse,
                    classe.Indirizzo);
            #endif  
        }
    }
}
