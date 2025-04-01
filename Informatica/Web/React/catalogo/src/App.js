import logo from './logo.svg';
import 'bootstrap/dist/css/bootstrap.css';
import 'bootstrap/dist/js/bootstrap.js';
import Navbar from './components/Header'
import Footer from './components/Footer';
import Book from './components/Book';
import { Component } from 'react';

const books = [
  {
    image: "9788807036521_0_0_536_0_75.jpg", 
    title: "La strada giovane", 
    text: "Nino, giovane panettiere siciliano, viene catturato dopo l'8 settembre. Dell'armistizio non ha capito granché, credeva che i tedeschi lo rispedissero a casa dalla sua famiglia, nelle Madonie, invece quel treno lo ha portato in un campo di prigionia in Austria, a patire fame, freddo e paura. Nino è un IMI, un internato militare, senza nemmeno i diritti di un prigioniero. Qualche conforto gli viene dall'amicizia con Lorenzo, un giovane toscano colto e spigliato, che con lui lavora nelle cucine governate dal Piemontese, un gigantesco macellaio. Insieme, i tre colgono l'occasione dello scompiglio per i festeggiamenti di capodanno del '44 per fuggire. Ma fuori il freddo, la fame e la paura non mordono meno: orientarsi non è semplice, trovare cibo e riparo è un'impresa, e la gente è terrorizzata e feroce. La Sicilia sembra irraggiungibile e Nino lascia sul terreno, chilometro dopo chilometro, innocenza e giovinezza. Eppure, a sorreggerlo nel suo interminabile viaggio attraverso i territori occupati dai nazisti, dove combattono le bande partigiane e continuano i bombardamenti, e poi nella devastazione di un Sud martoriato dall'avanzata degli Alleati, c'è il ricordo della bellezza, il calore degli affetti. Mentre si nutre con le lumache rosse che emergono dal terreno dopo la pioggia, emergono anche le sue memorie: la festa del Santo a Ferragosto, il profumo di burro e vaniglia dei biscotti preparati dal padre, il sapore dei babaluci in umido, l'emozione della Targa Florio, la celebre corsa automobilistica. E il calore dei baci di Maria Assunta che, forse, lo sta ancora aspettando e che lui desidera riabbracciare a ogni costo.", 
    link: "https://www.ibs.it/strada-giovane-libro-antonio-albanese/e/9788807036521"
  },
  {
    image: "9788831018913_0_0_536_0_75.jpg", 
    title: "Le parole fanno il solletico", 
    text: "Tutti sappiamo che ‘buttare un occhio' o ‘chiudere il becco' non significa necessariamente lanci di globi oculari che volano da una parte all'altra della stanza e che, anche volendo, non potremmo chiudere il becco, che sia con chiodi o supercolla, semplicemente perché non lo abbiamo. Sono ovviamente modi di dire. Ma cosa succederebbe se facessimo finta? Lollo, che ha sempre ‘la testa per aria' dovrebbe recuperarla dal soffitto con una scopa, e quando la zia Frignola (sì, si chiama proprio così, è proprio il suo nome di battesimo!) si scioglie in lacrime – e credetemi, succede spesso – tutta la famiglia dovrebbe accorrere con stracci, scopettoni e mocio e asciugare quella grande pozza dal pavimento... Tante storie surreali e divertentissime per ridere insieme alla famiglia più buffa del mondo, e per buttare – appunto – un occhio sui giochi che si possono fare con i modi di dire e la nostra lingua. Se poi chi ce le racconta sono un magistrale scrittore che si chiama Daniel Pennac e un genio delle parole come Stefano Bartezzaghi, non possiamo che farci coinvolgere dal loro gioco scanzonato, divertente e appassionante. Età di lettura: da 9 anni.", 
    link: "https://www.ibs.it/parole-fanno-solletico-libro-daniel-pennac-stefano-bartezzaghi/e/9788831018913"
  },
  {
    image: "2000000153681_0_0_536_0_75.jpg", 
    title: "Atalanta", 
    text: "Quando il sovrano di Arcadia vede nascere una figlia femmina, anziché l’erede maschio che tanto desiderava, la sua decisione è immediata: la bambina sarà abbandonata a morire sulle pendici di una montagna. Ma il destino ha in serbo ben altro per la piccola Atalanta, che prima viene salvata e allevata da un’orsa e poi, una volta cresciuta, viene scelta dalla dea Artemide per vivere nella foresta insieme alle sue ninfe. La giovane diventa così un’abilissima cacciatrice, forte e veloce quasi quanto Artemide stessa. Scoprendo che un equipaggio di eroi, gli Argonauti, sta per imbarcarsi in una missione impossibile alla ricerca del vello d’oro, la dea decide di inviare Atalanta in suo nome, con un unico avvertimento: se si sposerà, condannerà la sua esistenza alla rovina. Ma la ragazza non teme alcuna sfida, ed è pronta a lottare contro il disprezzo dei suoi compagni di viaggio e del loro capo, Giasone, per dimostrare che anche le donne meritano di diventare leggenda.", 
    link: "https://www.ibs.it/atalanta-libro-jennifer-saint/e/2000000153681"
  },
  {
    image: "2000000153360_0_0_536_0_75.jpg", 
    title: "L' uomo duplicato", 
    text: "Protagonista del romanzo è un professore di Storia di scuola media dal nome altisonante, Tertuliano Máximo Afonso. Separato dalla moglie senza ricordare né perché si fosse sposato né perché avesse divorziato, questi incontra grandi difficoltà nelle relazioni col prossimo. Lo si può definire un depresso. Conduce una vita solitaria e noiosa, fino al giorno in cui non scopre una cosa incredibile. Dietro consiglio di un collega, noleggia una commedia leggera in videocassetta, ed eccolo faccia a faccia con una comparsa che, ben più che somigliargli, è proprio lui. Un autentico doppio, la cui esistenza sconvolge quella di Tertuliano, che da quel momento fa di tutto per scoprire chi sia quell'attore, cosa faccia, che storia abbia, sprofondando così in una realtà parallela...", 
    link: "https://www.ibs.it/uomo-duplicato-libro-jose-saramago/e/2000000153360"
  },
  {
    image: "9788807036521_0_0_536_0_75.jpg", 
    title: "La strada giovane", 
    text: "Nino, giovane panettiere siciliano, viene catturato dopo l'8 settembre. Dell'armistizio non ha capito granché, credeva che i tedeschi lo rispedissero a casa dalla sua famiglia, nelle Madonie, invece quel treno lo ha portato in un campo di prigionia in Austria, a patire fame, freddo e paura. Nino è un IMI, un internato militare, senza nemmeno i diritti di un prigioniero. Qualche conforto gli viene dall'amicizia con Lorenzo, un giovane toscano colto e spigliato, che con lui lavora nelle cucine governate dal Piemontese, un gigantesco macellaio. Insieme, i tre colgono l'occasione dello scompiglio per i festeggiamenti di capodanno del '44 per fuggire. Ma fuori il freddo, la fame e la paura non mordono meno: orientarsi non è semplice, trovare cibo e riparo è un'impresa, e la gente è terrorizzata e feroce. La Sicilia sembra irraggiungibile e Nino lascia sul terreno, chilometro dopo chilometro, innocenza e giovinezza. Eppure, a sorreggerlo nel suo interminabile viaggio attraverso i territori occupati dai nazisti, dove combattono le bande partigiane e continuano i bombardamenti, e poi nella devastazione di un Sud martoriato dall'avanzata degli Alleati, c'è il ricordo della bellezza, il calore degli affetti. Mentre si nutre con le lumache rosse che emergono dal terreno dopo la pioggia, emergono anche le sue memorie: la festa del Santo a Ferragosto, il profumo di burro e vaniglia dei biscotti preparati dal padre, il sapore dei babaluci in umido, l'emozione della Targa Florio, la celebre corsa automobilistica. E il calore dei baci di Maria Assunta che, forse, lo sta ancora aspettando e che lui desidera riabbracciare a ogni costo.", 
    link: "https://www.ibs.it/strada-giovane-libro-antonio-albanese/e/9788807036521"
  },
  {
    image: "9788831018913_0_0_536_0_75.jpg", 
    title: "Le parole fanno il solletico", 
    text: "Tutti sappiamo che ‘buttare un occhio' o ‘chiudere il becco' non significa necessariamente lanci di globi oculari che volano da una parte all'altra della stanza e che, anche volendo, non potremmo chiudere il becco, che sia con chiodi o supercolla, semplicemente perché non lo abbiamo. Sono ovviamente modi di dire. Ma cosa succederebbe se facessimo finta? Lollo, che ha sempre ‘la testa per aria' dovrebbe recuperarla dal soffitto con una scopa, e quando la zia Frignola (sì, si chiama proprio così, è proprio il suo nome di battesimo!) si scioglie in lacrime – e credetemi, succede spesso – tutta la famiglia dovrebbe accorrere con stracci, scopettoni e mocio e asciugare quella grande pozza dal pavimento... Tante storie surreali e divertentissime per ridere insieme alla famiglia più buffa del mondo, e per buttare – appunto – un occhio sui giochi che si possono fare con i modi di dire e la nostra lingua. Se poi chi ce le racconta sono un magistrale scrittore che si chiama Daniel Pennac e un genio delle parole come Stefano Bartezzaghi, non possiamo che farci coinvolgere dal loro gioco scanzonato, divertente e appassionante. Età di lettura: da 9 anni.", 
    link: "https://www.ibs.it/parole-fanno-solletico-libro-daniel-pennac-stefano-bartezzaghi/e/9788831018913"
  },
  {
    image: "2000000153681_0_0_536_0_75.jpg", 
    title: "Atalanta", 
    text: "Quando il sovrano di Arcadia vede nascere una figlia femmina, anziché l’erede maschio che tanto desiderava, la sua decisione è immediata: la bambina sarà abbandonata a morire sulle pendici di una montagna. Ma il destino ha in serbo ben altro per la piccola Atalanta, che prima viene salvata e allevata da un’orsa e poi, una volta cresciuta, viene scelta dalla dea Artemide per vivere nella foresta insieme alle sue ninfe. La giovane diventa così un’abilissima cacciatrice, forte e veloce quasi quanto Artemide stessa. Scoprendo che un equipaggio di eroi, gli Argonauti, sta per imbarcarsi in una missione impossibile alla ricerca del vello d’oro, la dea decide di inviare Atalanta in suo nome, con un unico avvertimento: se si sposerà, condannerà la sua esistenza alla rovina. Ma la ragazza non teme alcuna sfida, ed è pronta a lottare contro il disprezzo dei suoi compagni di viaggio e del loro capo, Giasone, per dimostrare che anche le donne meritano di diventare leggenda.", 
    link: "https://www.ibs.it/atalanta-libro-jennifer-saint/e/2000000153681"
  },
  {
    image: "2000000153360_0_0_536_0_75.jpg", 
    title: "L' uomo duplicato", 
    text: "Protagonista del romanzo è un professore di Storia di scuola media dal nome altisonante, Tertuliano Máximo Afonso. Separato dalla moglie senza ricordare né perché si fosse sposato né perché avesse divorziato, questi incontra grandi difficoltà nelle relazioni col prossimo. Lo si può definire un depresso. Conduce una vita solitaria e noiosa, fino al giorno in cui non scopre una cosa incredibile. Dietro consiglio di un collega, noleggia una commedia leggera in videocassetta, ed eccolo faccia a faccia con una comparsa che, ben più che somigliargli, è proprio lui. Un autentico doppio, la cui esistenza sconvolge quella di Tertuliano, che da quel momento fa di tutto per scoprire chi sia quell'attore, cosa faccia, che storia abbia, sprofondando così in una realtà parallela...", 
    link: "https://www.ibs.it/uomo-duplicato-libro-jose-saramago/e/2000000153360"
  },
  {
    image: "9788807036521_0_0_536_0_75.jpg", 
    title: "La strada giovane", 
    text: "Nino, giovane panettiere siciliano, viene catturato dopo l'8 settembre. Dell'armistizio non ha capito granché, credeva che i tedeschi lo rispedissero a casa dalla sua famiglia, nelle Madonie, invece quel treno lo ha portato in un campo di prigionia in Austria, a patire fame, freddo e paura. Nino è un IMI, un internato militare, senza nemmeno i diritti di un prigioniero. Qualche conforto gli viene dall'amicizia con Lorenzo, un giovane toscano colto e spigliato, che con lui lavora nelle cucine governate dal Piemontese, un gigantesco macellaio. Insieme, i tre colgono l'occasione dello scompiglio per i festeggiamenti di capodanno del '44 per fuggire. Ma fuori il freddo, la fame e la paura non mordono meno: orientarsi non è semplice, trovare cibo e riparo è un'impresa, e la gente è terrorizzata e feroce. La Sicilia sembra irraggiungibile e Nino lascia sul terreno, chilometro dopo chilometro, innocenza e giovinezza. Eppure, a sorreggerlo nel suo interminabile viaggio attraverso i territori occupati dai nazisti, dove combattono le bande partigiane e continuano i bombardamenti, e poi nella devastazione di un Sud martoriato dall'avanzata degli Alleati, c'è il ricordo della bellezza, il calore degli affetti. Mentre si nutre con le lumache rosse che emergono dal terreno dopo la pioggia, emergono anche le sue memorie: la festa del Santo a Ferragosto, il profumo di burro e vaniglia dei biscotti preparati dal padre, il sapore dei babaluci in umido, l'emozione della Targa Florio, la celebre corsa automobilistica. E il calore dei baci di Maria Assunta che, forse, lo sta ancora aspettando e che lui desidera riabbracciare a ogni costo.", 
    link: "https://www.ibs.it/strada-giovane-libro-antonio-albanese/e/9788807036521"
  },
  {
    image: "9788831018913_0_0_536_0_75.jpg", 
    title: "Le parole fanno il solletico", 
    text: "Tutti sappiamo che ‘buttare un occhio' o ‘chiudere il becco' non significa necessariamente lanci di globi oculari che volano da una parte all'altra della stanza e che, anche volendo, non potremmo chiudere il becco, che sia con chiodi o supercolla, semplicemente perché non lo abbiamo. Sono ovviamente modi di dire. Ma cosa succederebbe se facessimo finta? Lollo, che ha sempre ‘la testa per aria' dovrebbe recuperarla dal soffitto con una scopa, e quando la zia Frignola (sì, si chiama proprio così, è proprio il suo nome di battesimo!) si scioglie in lacrime – e credetemi, succede spesso – tutta la famiglia dovrebbe accorrere con stracci, scopettoni e mocio e asciugare quella grande pozza dal pavimento... Tante storie surreali e divertentissime per ridere insieme alla famiglia più buffa del mondo, e per buttare – appunto – un occhio sui giochi che si possono fare con i modi di dire e la nostra lingua. Se poi chi ce le racconta sono un magistrale scrittore che si chiama Daniel Pennac e un genio delle parole come Stefano Bartezzaghi, non possiamo che farci coinvolgere dal loro gioco scanzonato, divertente e appassionante. Età di lettura: da 9 anni.", 
    link: "https://www.ibs.it/parole-fanno-solletico-libro-daniel-pennac-stefano-bartezzaghi/e/9788831018913"
  },
  {
    image: "2000000153681_0_0_536_0_75.jpg", 
    title: "Atalanta", 
    text: "Quando il sovrano di Arcadia vede nascere una figlia femmina, anziché l’erede maschio che tanto desiderava, la sua decisione è immediata: la bambina sarà abbandonata a morire sulle pendici di una montagna. Ma il destino ha in serbo ben altro per la piccola Atalanta, che prima viene salvata e allevata da un’orsa e poi, una volta cresciuta, viene scelta dalla dea Artemide per vivere nella foresta insieme alle sue ninfe. La giovane diventa così un’abilissima cacciatrice, forte e veloce quasi quanto Artemide stessa. Scoprendo che un equipaggio di eroi, gli Argonauti, sta per imbarcarsi in una missione impossibile alla ricerca del vello d’oro, la dea decide di inviare Atalanta in suo nome, con un unico avvertimento: se si sposerà, condannerà la sua esistenza alla rovina. Ma la ragazza non teme alcuna sfida, ed è pronta a lottare contro il disprezzo dei suoi compagni di viaggio e del loro capo, Giasone, per dimostrare che anche le donne meritano di diventare leggenda.", 
    link: "https://www.ibs.it/atalanta-libro-jennifer-saint/e/2000000153681"
  },
  {
    image: "2000000153360_0_0_536_0_75.jpg", 
    title: "L' uomo duplicato", 
    text: "Protagonista del romanzo è un professore di Storia di scuola media dal nome altisonante, Tertuliano Máximo Afonso. Separato dalla moglie senza ricordare né perché si fosse sposato né perché avesse divorziato, questi incontra grandi difficoltà nelle relazioni col prossimo. Lo si può definire un depresso. Conduce una vita solitaria e noiosa, fino al giorno in cui non scopre una cosa incredibile. Dietro consiglio di un collega, noleggia una commedia leggera in videocassetta, ed eccolo faccia a faccia con una comparsa che, ben più che somigliargli, è proprio lui. Un autentico doppio, la cui esistenza sconvolge quella di Tertuliano, che da quel momento fa di tutto per scoprire chi sia quell'attore, cosa faccia, che storia abbia, sprofondando così in una realtà parallela...", 
    link: "https://www.ibs.it/uomo-duplicato-libro-jose-saramago/e/2000000153360"
  },
]

const maxBooks = 250; // max 4222

class App extends Component {
  state = {
    data: [],
  }

  render(){
    return (
      <div>
        <Navbar/>
        <div className='container'>
          <div className='row'>
            {
              this.state.data.slice(0, maxBooks).map((book) => {
                return (
                  <div className='col mb-2'>
                    <Book image={book.img} author={book.author} title={book.title} link={book.link}/>
                  </div>
                )
              }) 
            }
          </div>
          <Footer/>
        </div>
      </div>
    );
  }

  componentDidMount() {
    this.getJson();
  }

  async getJson() {
    try{
      const response = await fetch("/books.json");
      const data = await response.json();
      this.setState( {data} );
    }
    catch{
      console.error("Errore durante il caricamento.");
    }
  }
}

export default App;
