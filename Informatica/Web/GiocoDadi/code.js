// Fabio Fantini 4H 2025-02-28
// Codice del sito per giocare e puntare una somma

const moltiplicatore = 3; // Moltiplicatore per il guadagno in caso di vittoria

// Variabili per accedere agli elementi della pagina
var inp_numero = document.getElementById("numero_puntato");
var inp_saldo  = document.getElementById("saldo_puntato");
var btn_inizia      = document.getElementById("inizia");
var btn_rincomincia = document.getElementById("rincomincia");
var h_numero_uscito = document.getElementById("numero_uscito");
var h_saldo = document.getElementById("saldo");
var h_esito = document.getElementById("esito");

var saldo = 500; // Saldo iniziale

// Ricarica la pagina quando il saldo è esaurito
function ricaricaPagina() { 
    location.reload();
}

// Funzione che aggiorna il saldo sullo schermo
function mostraSaldo() {
    if(saldo <= 0) {
        saldo = 0; // Se il saldo è zero, lo imposto a zero e disabilito il pulsante "Inizia"
        btn_rincomincia.hidden = false; // Mostra il pulsante "Rincomincia"
        btn_inizia.disabled = true; // Disabilita il pulsante "Inizia"
    }
    h_saldo.textContent = "Saldo: " + saldo; // Visualizza il saldo aggiornato
}

// Funzione che assicura che l'input sia compreso tra i valori minimo e massimo
function controllaInput(input, massimo, minimo) {
    if(input < minimo) return minimo; // Se il valore è inferiore al minimo, lo imposto al minimo
    if(input > massimo) return massimo; // Se il valore è superiore al massimo, lo imposto al massimo
    return input; // Altrimenti restituisco il valore stesso
}

// Funzione che simula il lancio di un dado (numero casuale tra 1 e 6)
function lanciaDado() { 
    return Math.floor(Math.random() * 6) + 1; // Restituisce un numero casuale da 1 a 6
}

// Funzione che viene chiamata quando l'utente inizia la scommessa
function scommessa() {
    // Prendo i valori inseriti dall'utente
    var numero_puntato = inp_numero.value;
    var saldo_puntato = inp_saldo.value;

    // Se uno dei valori non è inserito o contiene "e" (input non valido), esco dalla funzione
    if(numero_puntato == "" || saldo_puntato == "") return;
    if(numero_puntato.includes("e") || saldo_puntato.includes("e")) return;

    // Controllo che il numero puntato e il saldo puntato siano nei limiti
    numero_puntato = controllaInput(parseInt(numero_puntato), 12, 2);
    saldo_puntato = controllaInput(parseInt(saldo_puntato), saldo, 1);

    // Aggiorno i campi di input con i valori validati
    inp_numero.value = numero_puntato;
    inp_saldo.value = saldo_puntato;

    // Sottraggo il saldo puntato dal saldo totale
    saldo -= saldo_puntato;
    inp_saldo.setAttribute("max", saldo); // Aggiorno il massimo del saldo possibile da puntare

    // Lancia i dadi e sommo i due risultati
    numero_uscito = lanciaDado() + lanciaDado();
    h_numero_uscito.textContent = numero_uscito; // Mostro il numero uscito

    // Verifico se l'utente ha vinto o perso
    if(numero_puntato != numero_uscito) {
        h_esito.textContent = "Hai perso!"; // Se non ha vinto, mostra "Hai perso"
        h_esito.classList.remove("text-success");
        h_esito.classList.add("text-danger");
    } else {
        // Se ha vinto, calcolo il guadagno e lo aggiungo al saldo
        guadagno = saldo_puntato * moltiplicatore;
        saldo += guadagno;

        h_esito.classList.add("text-success"); // Mostro il messaggio di vittoria in verde
        h_esito.classList.remove("text-danger");

        h_esito.textContent = "Hai vinto! Hai guadagnato " + guadagno + "!"; // Mostro il guadagno
    }

    mostraSaldo(); // Mostro il saldo aggiornato dopo la scommessa
}

// Inizializzo la visualizzazione del saldo quando la pagina viene caricata
mostraSaldo();
inp_saldo.setAttribute("max", saldo); // Imposto il massimo del saldo da puntare
