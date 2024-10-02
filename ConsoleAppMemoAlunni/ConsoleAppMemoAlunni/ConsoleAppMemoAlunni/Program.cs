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
                    if (dati.Length < 6)
                    {
                        Console.WriteLine($"ATTENZIONE : Rilevata riga mancante di dati dopo il {numeroAlunno}° alunno.");
                        continue;
                    }
                    numeroAlunno++;

                    // Conversione dell'input
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
                    Alunno alunno = new Alunno(dati[0], dati[1], dati[2][0], dataDiNascita, dati[4], indirizzo);
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
                        int anno = alunno.Classe[0] - 48;
                        istituto.Add(new Classe(anno, alunno.Classe.Substring(1), alunno.Indirizzo));
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

#if true
            Console.WriteLine("\nCLASSI");
            foreach (Classe classe in istituto)
            {
                Console.WriteLine("{0}\t{1}"
                       ,classe.NomeClasse, classe.Indirizzo);
            }
#endif

#if true
            Console.WriteLine("\nALUNNI");
            // Stampa alunni
            for (int i = 0; i < Alunno.NumeroDiMatricola; i++)
                foreach (Classe classe in istituto)
                {
                    (string, string) info = classe[i];
                    if (info != ("", ""))
                        Console.WriteLine("{0}\t{1}\t{2}\t{3}"
                            ,info.Item1, info.Item2, classe.NomeClasse, classe.Indirizzo);
                }
                    
#endif  
        }
    }
}
