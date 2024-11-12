namespace ConsoleAppDijkstra
{
    public class GrafoDiretto
    {
        public struct Arco
        {
            public int nodo_partenza;
            public int nodo_arrivo;
            public int costo;
        }

        public GrafoDiretto(int numero_max_nodi = 1000) // TODO : costruisce la classe,
                                                        //        è garantito che non si supererà numero_max_nodi
        {
            
        }
        public int NumeroNodi; // proprietà che torna il numero attuale di nodi inseriti
        public void AggiungiNodo(string nome) // aggiunge un nodo
        { 

        }
        public virtual void AggiungiArco(string nome_nodo_partenza, string nome_nodo_arrivo, int costo) // aggiunge un arco fra i due nodi dati, con il costo indicato
        {

        }
        public virtual void AggiungiArco(Arco arco)  // aggiunge un arco fra i due nodi dati, con il costo indicato
        {

        }
        //public string this[int nodo]  // indicizzatore per i nodi
        //public int this[string nome_nodo]  // indicizzatore per i nodi

        //public IEnumerable<Arco> ArchiUscenti(int nodo)  // enumeratore per gli archi uscenti dal nodo dato
        //public IEnumerable<Arco> ArchiEntranti(int nodo)  // enumeratore per gli archi entranti nell nodo dato
    }



public class GrafoNonDiretto : GrafoDiretto  // specializzazione di GrafoDiretto per grafi "non diretti" (se si può andare da A a B con costo 10, allora si va acnhe da B ad A con lo stesso costo)
{
    public GrafoNonDiretto(int numero_max_nodi = 1000) { 
        }
        public void override AggiungiArco(string nome_nodo_partenza, string nome_nodo_arrivo, int costo) // aggiunge un arco fra i due nodi dati, con il costo indicato
        public void override AggiungiArco(Arco arco)  // aggiunge un arco fra i due nodi dati, con il costo indicato
    }
}