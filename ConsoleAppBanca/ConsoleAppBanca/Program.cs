// Fabio Fantini 4H 2024-10-29
// Classe Banca contenente N istanze di Cliente e ogni cliente contiene N istanze di PrestitoSemplice
// O PrestitoComposto, quest'ultimo eredita dalla superclasse PrestitoSemplice

namespace ConsoleAppBanca
{
    internal class Program
    {

        class Banca
        {
            // La banca contiene N clienti
            private List<Cliente> _clienti;
            public Banca()
            {
                _clienti = new List<Cliente>();
            }

            // Indicizzatore, consegnando alla banca il codice fiscale, il cliente viene ritornato
            public Cliente this[string codiceFiscale] 
            {
                get 
                { 
                    for (int i = 0; i < _clienti.Count; i++)
                        if (_clienti[i].CodiceFiscale == codiceFiscale)
                            return _clienti[i];

                    return new Cliente("", "", "", 0.0);
                }
            }

            // Uso i metodi di List per organizzare i clienti
            public void AddCliente(Cliente cliente) { _clienti.Add(cliente); }
            public void RemoveCliente(Cliente cliente) { _clienti.Remove(cliente); }

            // Utilizzo l'indicizzatore per trovare il cliente
            public Cliente SearchCliente(string codiceFiscale) { return this[codiceFiscale]; }
            public void AddPrestito(PrestitoSemplice prestito)
            {
                // Aggiunge il prestito al cliente cercandolo con il codice fiscale
                Cliente cliente = this[prestito.CodiceFiscale];
                if (cliente.Nome != "" && cliente.Cognome != "" && cliente.CodiceFiscale != "" && cliente.Stipendio != 0.0)
                    cliente.RichiediPrestito(prestito);
                else
                    throw new Exception("Cliente non trovato con il codice fiscale dato.");
            }
            public List<PrestitoSemplice> SearchPrestiti(string codiceFiscale)
            {
                // Ritorna tutti i prestiti di un cliente, cercandolo tramite il cod. fiscale
                List<PrestitoSemplice> prestiti = new List<PrestitoSemplice>();
                Cliente cliente = this[codiceFiscale];
                for (int i = 0; i < cliente.NumeroPrestiti; i++)
                    prestiti.Add(cliente[i]);

                return prestiti;
            }
            // Utilizzo l'indicizzatore per trovare il cliente
            public double TotalePrestiti(string codiceFiscale) { return this[codiceFiscale].NumeroPrestiti; }
        }
        class Cliente
        {
            // Dati privati
            private string _nome;
            private string _cognome;
            private string _codiceFiscale;
            private double _stipendio;
            private List<PrestitoSemplice> _prestiti;

            // Proprietà con solo get pubblico
            public string Nome { get { return _nome; } private set { _nome = value; } }
            public string Cognome { get { return _cognome; } private set { _cognome = value; } }
            public string CodiceFiscale { get { return _codiceFiscale; } private set { _codiceFiscale = value; } }
            public double Stipendio { get { return _stipendio; } private set { _stipendio = value; } }
            public int NumeroPrestiti { get { return _prestiti.Count; } }

            // Indicizzatore, ritorna il prestito consegnato l'indice all'instanza
            public PrestitoSemplice this[int indice] { get { return _prestiti[indice]; } } 

            // Costruttore
            public Cliente(string nome, string cognome, string codiceFiscale, double stipendio)
            {
                _nome = nome;
                _cognome = cognome;
                _codiceFiscale = codiceFiscale;
                _stipendio = stipendio;
                _prestiti = new List<PrestitoSemplice>();
            }

            // Stampa i dati
            public string StampaCliente() { return $"{Nome} {Cognome} {CodiceFiscale} {Stipendio}"; }

            // Aggiungi un prestito alla lista
            public void RichiediPrestito(PrestitoSemplice prestito) 
            {
                if (prestito.CodiceFiscale != CodiceFiscale) throw new Exception("Codice fiscale non valido.");
                _prestiti.Add(prestito); 
            }
        }
        class PrestitoSemplice
        {
            // Dati privati
            private double _capitale;
            private double _interesse;
            private DateOnly _dataInizio;
            private DateOnly _dataFine;
            private string _codiceFiscale;

            // Proprietà con solo get pubblico
            public double Capitale { get { return _capitale; } private set { _capitale = value; } }
            public double Interesse { get { return _interesse; } private set { _interesse = value; } }
            public DateOnly DataInizio
            {
                get { return _dataInizio; }
                private set
                {
                    _dataInizio = value;

                    if (_dataInizio.DayNumber > _dataFine.DayNumber) // swap se inizio > fine
                    {
                        DateOnly temp = _dataInizio;
                        _dataInizio = _dataFine;
                        _dataFine = temp;
                    }
                }
            }
            public DateOnly DataFine
            {
                get { return _dataFine; }
                private set
                {
                    _dataFine = value;

                    if (_dataInizio.DayNumber > _dataFine.DayNumber) // swap se inizio > fine
                    {
                        DateOnly temp = _dataInizio;
                        _dataInizio = _dataFine;
                        _dataFine = temp;
                    }
                }
            }
            public string CodiceFiscale { get { return _codiceFiscale; } private set { _codiceFiscale = value; } }

            // Proprietà calcolate con solo get
            public double Rata { get { return (Montante / Durata) / 12; } }
            public virtual double Montante { get { return Capitale * (1 + Durata * Interesse); } }
            public double Durata { get { return (DataInizio.DayNumber - DataFine.DayNumber) % 365; } }

            // Costruttore
            public PrestitoSemplice(double capitale, double interesse, DateOnly dataInizio, DateOnly dataFine, string codiceFiscale)
            {
                Capitale = capitale;
                Interesse = interesse;
                DataInizio = dataInizio;
                DataFine = dataFine;
                CodiceFiscale = codiceFiscale;
            }

            // Stampa dei dati del prestito
            public string StampaPrestito() { return $"{Capitale} {Interesse} {DataInizio} {DataFine} {CodiceFiscale}"; }
        }
        class PrestitoComposto : PrestitoSemplice // Prestito Composto eredita da PrestitoSemplice
        {
            // Montante viene sovrascritto dalla nuova formula, il resto rimane invariato
            public override double Montante { get { return Math.Pow(Capitale * (1 + Interesse), Durata); } }

            // Il costruttore chiama quello del PrestitoSemplice tramite 'base'
            public PrestitoComposto(double capitale, double interesse, DateOnly dataInizio, DateOnly dataFine, string codiceFiscale)
                : base(capitale, interesse, dataInizio, dataFine, codiceFiscale) { }
        }
        static void Main(string[] args)
        {
            Console.WriteLine("Fabio Fantini 4H 2024-10-29");
        }
    }
}
