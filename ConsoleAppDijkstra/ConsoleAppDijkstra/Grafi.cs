// Fabio Fantini 4H 2024-11-15
// Implementazione classi GrafoDiretto e GrafoIndiretto con metodi di aggiunta archi e nodi

namespace ConsoleAppDijkstra
{
    /// <summary>
    /// Grafo Diretto; appartenente archi uni-direzionali
    /// </summary>
    public class GrafoDiretto
    {
        public struct Arco
        {
            public int nodo_partenza;
            public int nodo_arrivo;
            public int costo;
        }
        List<Arco> archi;
        List<string> nodi;
        int max_nodi;

        /// <summary>
        /// Costruisce la classe, è garantito che non si supererà numero_max_nodi
        /// </summary>
        /// <param name="numero_max_nodi">Numero massimo di nodi</param>
        public GrafoDiretto(int numero_max_nodi = 1000) // è garantito che non si supererà numero_max_nodi

        {
            nodi = new List<string>();
            archi = new List<Arco>();
            max_nodi = numero_max_nodi;
        }
        /// <summary>
        /// Numero attuale di nodi inseriti
        /// </summary>
        public int NumeroNodi { get { return nodi.Count; } } // proprietà che torna il numero attuale di nodi inseriti

        /// <summary>
        /// Aggiunge il nodo specificandone il nome
        /// </summary>
        /// <param name="nome">Nome del nodo</param>
        /// <exception cref="Exception">Non si può superare il numero massimo di nodi</exception>
        public void AggiungiNodo(string nome) // aggiunge un nodo
        {
            if (NumeroNodi >= max_nodi)
                throw new Exception("Impossibile aggiungere il nodo, raggiunta la massima quntità.");

            nodi.Add(nome);
        }

        /// <summary>
        /// Aggiunge l'arco specificando il nome del nodo di partenza, arrivo e costo
        /// </summary>
        /// <param name="nome_nodo_partenza">Nome del nodo di partenza</param>
        /// <param name="nome_nodo_arrivo">Nome del nodo di arrivo</param>
        /// <param name="costo">Costo per passare da un nodo all'altro</param>
        public virtual void AggiungiArco(string nome_nodo_partenza, string nome_nodo_arrivo, int costo) // aggiunge un arco fra i due nodi dati, con il costo indicato
        {
            // Crea l'arco
            Arco arco;
            arco.costo = costo;

            // Trova gli indici
            int idx_partenza = this[nome_nodo_partenza];
            int idx_arrivo = this[nome_nodo_arrivo];

            // Assegna direzioni e aggiugni artco alla lista
            arco.nodo_partenza = idx_partenza;
            arco.nodo_arrivo = idx_arrivo;
            archi.Add(arco);
        }
        /// <summary>
        /// Aggiunge l'arco
        /// </summary>
        /// <param name="arco">L'arco da aggiungere</param>
        public virtual void AggiungiArco(Arco arco)  // aggiunge un arco fra i due nodi dati, con il costo indicato
        {
            archi.Add(arco);
        }
        /// <summary>
        /// Verifica se il nodo esiste
        /// </summary>
        /// <param name="idx_nodo">Indice del nodo</param>
        /// <returns>True se esiste, false se non esiste</returns>
        public bool ControllaIndiceNodo(int idx_nodo)
        {
            // Se cercando il nodo, avviene un'eccezione ritorno false, altrimenti true
            try { string nodo = this[idx_nodo]; }
            catch(Exception e) { return false; }
            return true;
        }
        /// <summary>
        /// Indicizzatore dato l'indice del nodo, ritorna il nome
        /// </summary>
        /// <param name="nodo">Indice del nodo</param>
        /// <returns>Nome del nodo</returns>
        public string this[int nodo]  // indicizzatore per i nodi
        {
            get 
            {
                return nodi[nodo];
            }
        }
        /// <summary>
        /// Indicizzatore dato il nome del nodo, ritorna l'indice
        /// </summary>
        /// <param name="nome_nodo">Nome del nodo</param>
        /// <returns>Indice del nodo</returns>
        /// <exception cref="IndexOutOfRangeException">Eccezione se il nodo non viene trovato</exception>
        public int this[string nome_nodo]  // indicizzatore per i nodi
        {
            get
            {
                // Passo per i nodi e cerco quello specifico
                for (int i = 0; i < NumeroNodi; i++)
                    if (nodi[i] == nome_nodo)
                        return i;

                throw new IndexOutOfRangeException();
            }
        }

        /// <summary>
        /// Dato il nodo, ritorna gli archi che lo hanno come partenza
        /// </summary>
        /// <param name="nodo">Nodo di partenza</param>
        /// <returns>Archi con quel nodo di partenza</returns>
        public IEnumerable<Arco> ArchiUscenti(int nodo)  // enumeratore per gli archi uscenti dal nodo dato
        {
            // Passo per gli archi e cerco il nodo ritornandolo
            foreach (Arco arco in archi)
                if (arco.nodo_partenza == nodo)
                    yield return arco;
        }

        /// <summary>
        /// Dato il nodo, ritorna gli archi che lo hanno come arrivo
        /// </summary>
        /// <param name="nodo">Nodo di arrivo</param>
        /// <returns>Archi di quel nodo di arrivo</returns>
        public IEnumerable<Arco> ArchiEntranti(int nodo)  // enumeratore per gli archi entranti nell nodo dato
        {
            // Passo per gli archi e cerco il nodo ritornandolo
            foreach (Arco arco in archi)
                if (arco.nodo_arrivo == nodo)
                    yield return arco;
        }
    }


    /// <summary>
    /// Grafo non diretto, appartenente archi bi-direzionali
    /// </summary>
    public class GrafoNonDiretto : GrafoDiretto  // specializzazione di GrafoDiretto per grafi "non diretti" (se si può andare da A a B con costo 10, allora si va anche da B ad A con lo stesso costo)
    {
        /// <summary>
        /// Costruisce la classe, è garantito che non si supererà numero_max_nodi
        /// </summary>
        /// <param name="numero_max_nodi">Numero massimo di nodi</param>
        public GrafoNonDiretto(int numero_max_nodi = 1000)
            : base(numero_max_nodi) { }

        /// <summary>
        /// Aggiunge l'arco al grafo specificando nome del nodo di partenza, arrivo e costo. Viene aggiunto anche un arco con direzioni contrarie
        /// </summary>
        /// <param name="nome_nodo_partenza">Nome del nodo di partenza</param>
        /// <param name="nome_nodo_arrivo">Nome del nodo di arrivo</param>
        /// <param name="costo">Costo per passare da un nodo all'altro</param>
        public override void AggiungiArco(string nome_nodo_partenza, string nome_nodo_arrivo, int costo) // aggiunge un arco fra i due nodi dati, con il costo indicato
        {
            // Crea l'arco
            Arco arco;
            Arco contrario;
            arco.costo = costo;
            contrario.costo = costo;

            // Trova gli indici
            int idx_partenza = this[nome_nodo_partenza];
            int idx_arrivo = this[nome_nodo_arrivo];

            // Assegna le direzioni degli archi
            arco.nodo_partenza = idx_partenza;
            arco.nodo_arrivo = idx_arrivo;
            contrario.nodo_partenza = idx_arrivo;
            contrario.nodo_arrivo = idx_partenza;

            // Aggiungi gli archi alla lista
            base.AggiungiArco(arco);
            base.AggiungiArco(contrario);
        }

        /// <summary>
        /// Aggiunge l'arco al grafo. Viene aggiunto anche un arco con direzioni contrarie
        /// </summary>
        /// <param name="arco">L'arco da aggiungere</param>
        public override void AggiungiArco(Arco arco)  // aggiunge un arco fra i due nodi dati, con il costo indicato
        {
            // Aggiungi l'arco
            base.AggiungiArco(arco);

            // Aggiungi l'arco contrario
            Arco contrario;
            contrario.nodo_arrivo = arco.nodo_partenza;
            contrario.nodo_partenza = arco.nodo_partenza;
            contrario.costo = arco.costo;
            base.AggiungiArco(contrario);
        }
    }
}