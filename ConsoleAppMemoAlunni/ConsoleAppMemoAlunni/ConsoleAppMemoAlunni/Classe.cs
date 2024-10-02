// Fabio Fantini 4H 2024-09-27
// Classe Classe

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppMemoAlunni
{
    internal class Classe
    {
        // Data
        private int          _anno;
        private string       _sezione;
        private Indirizzo    _indirizzo;
        private List<Alunno> _alunni;

        #region Proprietà e indicizzatori
        public int Anno
        {
            get { return _anno; }
            private set
            {
                // Controllo se l'anno si trova fra 1 e 5
                if (value < 1 || value > 5) throw new Exception("Anno non valido.");
                _anno = value;
            }
        }
        public string Sezione
        {
            get { return _sezione; }
            private set
            {
                // Controllo se la sezione non inesistente o troppo lunga
                if (value.Length == 0 || value.Length > 3) throw new Exception("Sezione non presente o troppo lunga.");

                // Controlla che dopo l'anno sia presente una sezione, cioè una lettera maiuscola
                string sezione = value.ToUpper();
                for (int i = 1; i < sezione.Length; i++)
                {
                    if (sezione[i] < 65 || sezione[i] > 90)
                        throw new Exception("Sezione non valida");
                }

                _sezione = sezione;
            }
        }
        public Indirizzo Indirizzo
        {
            get { return _indirizzo; }
            private set
            {
                // Controlla che l'indirizzo combaci con i componenti dell'enum
                if ((int)value < 0 || (int)value > 2) throw new Exception("Indirizzo non valido.");
                _indirizzo = value;
            }
        }
        public string NomeClasse { get { return $"{Anno}{Sezione}"; } }
        public (string nome, string cognome) this[int matricola] // Indicizzatore matricola --> nome e cognome
        {
            get
            {
                for(int i = 0; i < _alunni.Count; i++)
                {
                    if (_alunni[i].Matricola == matricola)
                        return (_alunni[i].Nome, _alunni[i].Cognome);
                }
                return ("", "");
            }
        }
        #endregion

        /// <summary>
        /// Creazione classe contenente gli alunni
        /// </summary>
        /// <param name="anno">L'anno della classe</param>
        /// <param name="sezione">La sezione della classe</param>
        /// <param name="indirizzo">L'indirizzo della classe</param>
        public Classe(int anno, string sezione, Indirizzo indirizzo)
        {
            Anno = anno;
            Sezione = sezione;
            Indirizzo = indirizzo;
            _alunni = new List<Alunno>(); 
        }

        /// <summary>
        /// Aggiunge l'alunno alla classe
        /// </summary>
        /// <param name="alunno">L'alunno da aggiungere</param>
        public void AggiungiAlunno(Alunno alunno)
        {
            _alunni.Add(alunno);
        }
    }
}
