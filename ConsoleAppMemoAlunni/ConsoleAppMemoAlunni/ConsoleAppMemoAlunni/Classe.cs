// Fabio Fantini 4H 2024-09-27
// Classe Classe

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppMemoAlunni
{
    internal class Classe
    {
        private int          _anno;
        private string       _sezione;
        private Indirizzo    _indirizzo;
        private List<Alunno> _alunni;

        public Classe(int anno, string sezione, Indirizzo indirizzo)
        {
            Anno = anno;
            Sezione = sezione;
            Indirizzo = indirizzo;
            _alunni = new List<Alunno>(); 
        }

        public int Anno            { get { return _anno; }      private set { _anno = value; } }
        public string Sezione      { get { return _sezione; }   private set { _sezione = value; } }
        public Indirizzo Indirizzo { get { return _indirizzo; } private set { _indirizzo = value; } }
        public string NomeClasse   { get { return $"{Anno}{Sezione}"; } }
        public string this [int indice] { get { return _alunni[indice].Cognome; } } // Indicizzatore

        public void AggiungiAlunno(Alunno alunno)
        {
            _alunni.Add(alunno);
        }
    }
}
