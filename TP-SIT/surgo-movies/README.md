# Surgo Movies 🎬

Un'applicazione Vue.js per la scoperta di film che permette agli utenti di esplorare i film di tendenza e cercare pellicole utilizzando le **API di The Movie Database (TMDB)**.

## 📖 Panoramica del Progetto

Surgo Movies è un'applicazione frontend costruita con **Vue 3**, **TypeScript** e **Bootstrap**. Fornisce agli utenti un'interfaccia elegante per esplorare i film di tendenza e visualizzare informazioni dettagliate su ogni pellicola.

---

## 🛣️ Routing e Pagine

L'applicazione utilizza **Vue Router** per la navigazione con le seguenti rotte:

### Home Page (`/`)
- **Componente**: `HomePage.vue`
- **Comportamento predefinito**: Visualizza una lista dei **film di tendenza** della settimana
- **Funzionalità di ricerca**: Gli utenti possono cercare film per titolo utilizzando l'input di ricerca. Premere `Invio` per cercare
- **Visualizzazione dinamica**: Mostra "Trending Movies" o "Search Results" a seconda della vista corrente
- **Griglia film**: Visualizza i film in una griglia Bootstrap responsiva a 3 colonne

### Pagina Dettagli Film (`/movie/:id`)
- **Componente**: `MovieDetailsPage.vue`
- **Rotta dinamica**: Accetta un ID film come parametro URL
- **Contenuto visualizzato**:
  - Sezione hero con immagine di sfondo del film
  - Titolo del film con valutazione a stelle
  - Data di uscita, lingua e durata
  - Lista dei generi
  - Sinossi/panoramica
  - Stato di produzione
  - Informazioni sul regista

---

## 🧩 Componenti

### `AppHeader.vue`
Un componente header sticky che visualizza il logo del brand Surgo Movies. Cliccando sul logo si torna alla home page.

### `AppFooter.vue`
Un componente footer con:
- Logo del brand
- Icone social media (X/Twitter, Instagram, YouTube, LinkedIn)
- Informazioni di contatto (email, telefono, indirizzo)

### `HeroBanner.vue`
Un banner hero visualizzato nella home page con l'immagine promozionale di Surgo Movies e un effetto vignetta cinematografico.

### `MovieCard.vue`
Un componente card interattivo che visualizza:
- Poster del film con effetto design pellicola cinematografica
- Titolo del film
- Valutazione a stelle (convertita in scala da 0 a 5)
- Estratto della sinossi (troncato)
- Funzionalità click per navigare ai dettagli del film

### `HelloWorld.vue`
Componente boilerplate predefinito di Vue (non utilizzato attivamente nell'applicazione).

---

## 📁 Models (Modelli)

Le interfacce TypeScript sono utilizzate per garantire la **type safety** in tutta l'applicazione:

### `Movie.ts`
Interfaccia base per i dati del film:
```typescript
interface Movie {
    title: string;       // Titolo del film
    overview: string;    // Sinossi
    image: string;       // URL del poster
    stars: number;       // Valutazione (scala 0-5)
    id: string;          // ID univoco del film
}
```

### `MovieDetails.ts`
Interfaccia estesa che eredita da `Movie` con dettagli aggiuntivi:
```typescript
interface MovieDetails extends Movie {
    vote_count: number;         // Numero di voti
    release_date: string;       // Data di uscita
    original_language: string;  // Lingua originale
    runtime: number;            // Durata in minuti
    genres: string[];           // Lista dei generi
    status: string;             // Stato di produzione
    backdrop_path?: string;     // Immagine di sfondo
}
```

### `CrewMembers.ts`
Interfaccia per le informazioni sulla troupe del film:
```typescript
interface CrewMembers {
    crew: {
        name: string;   // Nome del membro
        job: string;    // Ruolo (es. Director, Writer)
    }[];
}
```

---

## 🔧 Service Layer (Livello Servizi)

### `MovieService.ts`

Una classe service che gestisce tutte le comunicazioni API con TMDB. Trasforma le risposte API grezze in oggetti model tipizzati.

#### Metodo Privato
- **`getApiData(endpoint, queryParams)`**: Metodo generico per effettuare richieste API autenticate a TMDB

#### Metodi Pubblici

| Metodo | Endpoint TMDB | Ritorna | Descrizione |
|--------|---------------|---------|-------------|
| `getTrendingMovies()` | `/trending/movie/week` | `Movie[]` | Recupera i film di tendenza settimanali |
| `searchMovie(query)` | `/search/movie` | `Movie[]` | Cerca film per titolo |
| `getMovieDetails(id)` | `/movie/{id}` | `MovieDetails` | Ottiene info dettagliate per un film specifico |
| `getMovieCrew(movieId)` | `/movie/{id}/credits` | `CrewMembers` | Ottiene i crediti di cast e troupe |

#### Trasformazione Dati
Il service trasforma le risposte API di TMDB:
- Costruisce URL completi delle immagini usando `https://image.tmdb.org/t/p/w500`
- Converte la scala di valutazione da 10 punti a 5 stelle
- Mappa gli oggetti genere in stringhe con i nomi dei generi

---

## 🌐 Chiamate API (TMDB)

L'applicazione si integra con **4 endpoint API di TMDB**:

### 1. Film di Tendenza
```
GET https://api.themoviedb.org/3/trending/movie/week?api_key={API_KEY}
```
Recupera la lista dei film di tendenza nella settimana corrente.

### 2. Ricerca Film
```
GET https://api.themoviedb.org/3/search/movie?api_key={API_KEY}&query={TERMINE_RICERCA}
```
Cerca film corrispondenti alla stringa di query fornita.

### 3. Dettagli Film
```
GET https://api.themoviedb.org/3/movie/{MOVIE_ID}?api_key={API_KEY}
```
Recupera i dettagli completi per un film specifico inclusi durata, generi e stato di uscita.

### 4. Crediti Film
```
GET https://api.themoviedb.org/3/movie/{MOVIE_ID}/credits?api_key={API_KEY}
```
Recupera le informazioni su cast e troupe per un film specifico.

---

## 🛠️ Tecnologie Utilizzate

| Tecnologia | Scopo |
|------------|-------|
| **Vue 3** | Framework frontend con Composition API |
| **TypeScript** | Tipizzazione statica e type safety |
| **Vue Router 4** | Routing lato client |
| **Bootstrap 4** | Framework CSS per styling responsivo |
| **Vite** | Tool di build e server di sviluppo |

---

## 🚀 Come Iniziare

### Prerequisiti
- **Node.js** (versione 18 o superiore consigliata)
- **npm** package manager

### Installazione

1. Clona il repository:
   ```bash
   git clone <repository-url>
   cd progetto_fe_api/surgo-movies
   ```

2. Installa le dipendenze:
   ```bash
   npm install
   ```

3. Avvia il server di sviluppo:
   ```bash
   npm run dev
   ```

4. Apri il browser e naviga all'URL mostrato nel terminale (tipicamente `http://localhost:5173`)

### Build per Produzione

```bash
npm run build
```

### Anteprima Build di Produzione

```bash
npm run preview
```

---

## 📂 Struttura del Progetto

```
surgo-movies/
├── src/
│   ├── components/        # Componenti Vue riutilizzabili
│   │   ├── AppHeader.vue
│   │   ├── AppFooter.vue
│   │   ├── HeroBanner.vue
│   │   └── MovieCard.vue
│   ├── views/             # Componenti pagina
│   │   ├── HomePage.vue
│   │   └── MovieDetailsPage.vue
│   ├── models/            # Interfacce TypeScript
│   │   ├── Movie.ts
│   │   ├── MovieDetails.ts
│   │   └── CrewMembers.ts
│   ├── services/          # Livello servizi API
│   │   └── MovieService.ts
│   ├── enviroment/        # Configurazione ambiente
│   │   └── enviroment.ts
│   ├── router/            # Configurazione Vue Router
│   │   └── index.ts
│   ├── App.vue            # Componente root
│   └── main.ts            # Entry point applicazione
├── public/                # Asset statici
└── package.json
```
