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
        // Data
        private static int numero_di_matricola = 0;

        private string      _nome;
        private string      _cognome;
        private char        _genere;
        private DateOnly    _dataDiNascita;
        private string      _classe;
        private Indirizzo   _indirizzo;
        private int         _matricola;

        #region Proprietà
        public static int NumeroDiMatricola { get { return numero_di_matricola; } private set { numero_di_matricola = value; } }

        public string Nome
        {
            get { return _nome; }
            private set
            {
                // Controlla se il nome è presente
                if (value.Length <= 0) throw new Exception("Nome non presente");
                _nome = value;
            }
        }
        public string Cognome
        {
            get { return _cognome; }
            private set
            {
                // Controlla se cognome esiste
                if (value.Length <= 0) throw new Exception("Cognome non presente");
                _cognome = value;
            }
        }
        public char Genere
        {
            get { return _genere; }
            private set
            {
                // Controllo del genere
                string temp = value.ToString().ToUpper();
                if (temp != "M" && temp != "F") throw new Exception("Genere non valido.");
                _genere = temp[0];
            }
        }
        public DateOnly DataDiNascita
        {
            get { return _dataDiNascita; }
            private set
            {
                // Controlla che la data di nascita non provenga prima dell'anno 2000
                if (value.DayNumber < DateOnly.Parse("1/1/2000").DayNumber)
                    throw new Exception("Data di nascita troppo passata.");
                _dataDiNascita = value;
            }
        }
        public string Classe
        {
            get { return _classe; }
            private set
            {
                // Controlla che la classe sia stata assegnata o che non superi 4 caratteri
                if (value.Length == 0 || value.Length > 4) throw new Exception("Classe non presente o troppo lunga");

                // Controlla se il primo char è un numero
                if ((value[0] < 49 || value[0] > 53))
                    throw new Exception("Anno di scuola non valido");

                // Controlla che dopo l'anno sia presente una sezione, cioè una lettera maiuscola
                string sezione = value.ToUpper();
                for (int i = 1; i < sezione.Length; i++)
                {
                    if (sezione[i] < 65 || sezione[i] > 90)
                        throw new Exception("Sezione non valida");
                }

                _classe = sezione;
            }
        }
        public Indirizzo Indirizzo {
            get { return _indirizzo; }
            private set {
                // Controlla che l'indirizzo combaci con i componenti dell'enum
                if ((int)value < 0 || (int)value > 2) throw new Exception("Indirizzo non valido.");
                _indirizzo = value;
            }
        }
        public int Matricola { get { return _matricola; } private set { _matricola = value; } }
        #endregion

        /// <summary>
        /// Crea L'alunno
        /// </summary>
        /// <param name="nome">Nome dell'alunno</param>
        /// <param name="cognome">Cognome dell'alunno</param>
        /// <param name="genere">Genere dell'alunno (M o F)</param>
        /// <param name="dataDiNascita">Data di nascita dopo l'anno 2000</param>
        /// <param name="classe">Classe dell'alunno</param>
        /// <param name="indirizzo">Indirizzo scolastico (Informatica, Automazione, Biotecnologie</param>
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

        // Incrementa matricola all'aggiunta di un alunno
        private static int AggiungiMatricola()
        {
            return ++numero_di_matricola;
        }
    }
}
