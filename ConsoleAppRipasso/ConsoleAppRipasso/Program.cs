using System.Runtime.CompilerServices;

namespace ConsoleAppRipasso
{
    internal class Program
    {
        //INCAPSULAMENTO
         class Dipendente
        {
            protected string nome;
            public string Nome { get { return nome; } protected set { nome = value; } }
            // Overload
            public Dipendente(string nome) { this.nome = nome; }
            public Dipendente()
                : this("") { }
            public int Ritorna(int num) { return num; }
            public int Ritorna() { return 0; }

            public void Saluta() { Console.WriteLine($"Ciao, il mio nome è {Nome}"); }
            public virtual void Override() { Console.WriteLine("Virtual!"); }
        }
        //EREDIETARITA'
        class Docente : Dipendente
        {
            protected string materia;
            public string Materia { get { return materia; } protected set { materia = value; } }
            // Overload
            public Docente(string nome, string materia)
                : base(nome)
            {
                this.materia = materia;
            }
            public Docente(string materia)
                 : this("", materia) { }
            public Docente()
                 : this("", "") { }
            // Override
            public override void Override() { Console.WriteLine("Override!"); }
        }

        static void Main(string[] args)
        {
            //POLIMORFISMO
            Dipendente runtime = new Docente("Dynamic", "Runtime");          // Docente --> Runtime
            Docente compile_time = new Docente("Static", "Compile Time");    // Docente --> Compile-time

            Console.WriteLine(((Docente)runtime).Materia);
            Console.WriteLine(compile_time.Materia);

            runtime.Saluta();
            compile_time.Saluta();

            Dipendente virtu = new Dipendente();

            //Console.WriteLine(((Docente)virtu).Materia); NO! non si può fare il contrario, da superclasse a sottoclasse

            virtu.Override();
            runtime.Override();
            compile_time.Override();
        }
    }
}
