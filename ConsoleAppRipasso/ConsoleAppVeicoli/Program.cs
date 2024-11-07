namespace ConsoleAppVeicoli
{
    internal class Program
    {
        interface IMotorizzato
        {
            public int NumeroDiRuote { get; }
            public void AvviaMotore();
        }

        abstract class Veicolo
        {
            public string Marca { get; }
            public Veicolo(string marca)
            {
                Marca = marca;
            }
            public abstract void Guida();
            public virtual void MostraDettagli() { Console.WriteLine(Marca); }
        }

        class Auto : Veicolo, IMotorizzato
        {
            public int NumeroDiRuote { get; }
            public Auto(string marca)
                : base(marca)
            {
                NumeroDiRuote = 4;
            }
            public void AvviaMotore() { Console.WriteLine("Motore dell'auto avviato!"); }
            public override void Guida() { Console.WriteLine("Stai guidando l'auto."); }
            public override void MostraDettagli() { Console.WriteLine(Marca + " " + NumeroDiRuote); }
        }

        class Bicicletta : Veicolo
        {
            public Bicicletta(string marca)
                : base(marca) { }

            public override void Guida() { Console.WriteLine("Stai pedalando la bicicletta"); }
        }

        static void Main(string[] args)
        {
            List<Veicolo> veicoli = new List<Veicolo>() { new Bicicletta("Argon 18"), new Auto("Fiat") };
            for (int i = 0; i < veicoli.Count; i++)
                if (veicoli[i] is IMotorizzato)
                {
                    IMotorizzato motorizzato = veicoli[i] as IMotorizzato;
                    if (motorizzato != null)
                        motorizzato.AvviaMotore();
                }
                else if (veicoli[i] is Bicicletta)
                {
                    Bicicletta bicicletta = veicoli[i] as Bicicletta;
                    if (bicicletta != null)
                        bicicletta.Guida();
                }

        }
    }
}
