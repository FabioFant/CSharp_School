// Fabio Fantini 4H 2024-09-20
// Classe Alunno

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppMemoAlunni
{
    internal class Alunno
    {
        private string      _nome;
        private string      _cognome;
        private char        _genere;
        private DateOnly    _dataDiNascita;
        private string      _classe;
        private Indirizzo   _indirizzo;

        public Alunno(string nome, string cognome, char genere, DateOnly dataDiNascita, string classe, Indirizzo indirizzo)
        {
            Nome = nome;
            Cognome = cognome;
            Genere = genere;
            DataDiNascita = dataDiNascita;
            Classe = classe;
            Indirizzo= indirizzo;
        }
        public string Nome            { get { return _nome; }          private set { _nome = value; } }
        public string Cognome         { get { return _cognome; }       private set { _cognome = value; } }
        public char Genere            { get { return _genere; }        private set { _genere = value; } }
        public DateOnly DataDiNascita { get { return _dataDiNascita; } private set { _dataDiNascita = value; } }
        public string Classe          { get { return _classe; }        private set { _classe = value; } }
        public Indirizzo Indirizzo    { get { return _indirizzo; }     private set { _indirizzo = value; } }
    }
}
