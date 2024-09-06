using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppClass
{
    enum Materia
    {
        Italiano,
        Matematica,
        Inglese,
        Storia,
        Informatica,
        SistemiReti,
        TpSit,
        Telecomunicazioni,
        Motoria,
        EducazioneCivica
    }
    internal class Program
    {

        public class Alunno
        {
            private string nome;
            private string cognome;
            private int anno_di_nascita;
            private List<double>[] voti = new List<double>[Enum.GetValues(typeof(Materia)).Length];

            public Alunno(string nome, string cognome, int anno_di_nascita)
            {
                this.nome = nome;
                this.cognome = cognome;
                this.anno_di_nascita = anno_di_nascita;

                // Inizializza ogni lista nell'array
                for (int i = 0; i < voti.Length; i++)
                {
                    voti[i] = new List<double>();
                }
            }

            public void AggiungiVoto(Materia materia, double voto)
            {
                if(voto < 0.0 || voto > 10.0)
                {
                    throw new Exception("Voto fuori dal range.");
                }

                voti[(int)materia].Add(voto);
            }

            public double MediaMateria(Materia materia)
            {
                if(voti[(int)materia].Count == 0)
                {
                    throw new Exception("Impossibile fare una media con nessun voto.");
                }

                return voti[(int)materia].Average();
            }
        }
        static void Main(string[] args)
        {
            Alunno nuovo_alunno = new Alunno("Fabio", "Fantini", 2007);
            nuovo_alunno.AggiungiVoto(Materia.Informatica, 8.0);
            nuovo_alunno.AggiungiVoto(Materia.Informatica, 9.0);
            nuovo_alunno.AggiungiVoto(Materia.Informatica, 8.5);

            Console.WriteLine(nuovo_alunno.MediaMateria(Materia.Informatica));

            Console.ReadKey();
        }
    }
}
