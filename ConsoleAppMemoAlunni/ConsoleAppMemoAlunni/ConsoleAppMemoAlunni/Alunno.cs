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
        private string   nome;
        private string   cognome;
        private char     genere;
        private DateOnly dataDiNascita;
        private string   classe;
        private string   indirizzo;

        public Alunno(string nome, string cognome, char genere, DateOnly dataDiNascita, string classe, string indirizzo)
        {
            this.nome = nome;
            this.cognome = cognome;
            this.genere = genere;
            this.dataDiNascita = dataDiNascita;
            this.classe = classe;
            this.indirizzo = indirizzo;
        }
        public string   GetNome()          { return nome; }
        public string   GetCognome()       { return cognome; }
        public char   GetGenere()          { return genere; }
        public DateOnly GetDataDiNascita() { return dataDiNascita; }
        public string   GetClasse()        { return classe; }
        public string   GetIndirizzo()     { return indirizzo; }
    }
}
