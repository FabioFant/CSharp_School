namespace ConsoleAppBanca
{
    internal class Program
    {
        class Banca
        {
            private class Cliente
            {
                private string _nome;
                private string _cognome;
                private string _codiceFiscale;
                private double _stipendio;
                private List<PrestitoSemplice> _prestiti;

                public Cliente(string nome, string cognome, string codiceFiscale, double stipendio)
                {

                }

                //public string StampaCliente() { }
            }
            private class PrestitoSemplice
            {
                private double _capitale;
                private double _interesse;
                private DateOnly _dataInizio;
                private DateOnly _dataFine;
                private string _codiceFiscale;
                private double _rata;

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
                public double Rata { get { return _rata; } private set { _rata = value; } }
                public virtual double Montante { get { return Capitale * ( 1 + Durata * Interesse ); } }
                public double Durata { get { return ( DataInizio.DayNumber - DataFine.DayNumber ) % 365; } }

                public PrestitoSemplice(double capitale, double interesse, DateOnly dataInizio, DateOnly dataFine, string codiceFiscale, double rata)
                {
                    Capitale = capitale;
                    Interesse = interesse;
                    DataInizio = dataInizio;
                    DataFine = dataFine;
                    CodiceFiscale = codiceFiscale;
                    Rata = rata;
                }

                public string StampaPrestito()
                {
                    return $"Capitale: {Capitale}; Interesse: {Interesse}; DataInzio: {DataInizio}; DataFine: {DataFine}; CodiceFiscale: {CodiceFiscale}, Rata: {Rata}.";
                }
            }
            private class PrestitoComposto : PrestitoSemplice
            {
                public override double Montante { get { return Math.Pow(Capitale * (1 + Interesse), Durata); } }

                public PrestitoComposto(double capitale, double interesse, DateOnly dataInizio, DateOnly dataFine, string codiceFiscale, double rata)
                    : base(capitale, interesse, dataInizio, dataFine, codiceFiscale, rata) { }
            }

            private List<Cliente> _clienti;
            public Banca()
            {

            }

            //public void AddCliente(Cliente cliente);
            //public void RemoveCliente(Cliente cliente);
            //public Cliente SearchCliente(Cliente cliente);
            //public void AddPrestito(PrestitoSemplice cliente);
           // public List<PrestitoSemplice> SearchPrestiti(string codiceFiscale);
            //public double TotalePrestiti(string codiceFiscale);
        }
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
        }
    }
}
