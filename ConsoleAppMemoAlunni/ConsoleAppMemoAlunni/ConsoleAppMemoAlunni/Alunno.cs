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
        private static int numero_di_matricola = 0;

        private string      _nome;
        private string      _cognome;
        private char        _genere;
        private DateOnly    _dataDiNascita;
        private string      _classe;
        private Indirizzo   _indirizzo;
        private int         _matricola;

        public Alunno(string nome, string cognome, char genere, DateOnly dataDiNascita, string classe, Indirizzo indirizzo)
        {
            Nome = nome;
            Cognome = cognome;
            Genere = genere;
            DataDiNascita = dataDiNascita;
            Classe = classe;
            Indirizzo = indirizzo;
            Matricola = AggiungiMatricola();
        }

        private static int AggiungiMatricola()
        {
            return ++numero_di_matricola;
        }

        public static int NumeroDiMatricola { get { return numero_di_matricola; } private set { numero_di_matricola = value; } }

        public string Nome            { get { return _nome; }          private set { _nome = value; } }
        public string Cognome         { get { return _cognome; }       private set { _cognome = value; } }
        public char Genere            { get { return _genere; }        private set { _genere = value; } }
        public DateOnly DataDiNascita { get { return _dataDiNascita; } private set { _dataDiNascita = value; } }
        public string Classe          { get { return _classe; }        private set { _classe = value; } }
        public Indirizzo Indirizzo    { get { return _indirizzo; }     private set { _indirizzo = value; } }
        public int Matricola          { get { return _matricola; }     private set { _matricola = value; } }
    }
}
