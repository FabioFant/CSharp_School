// Fabio Fantini 4H 2025-02-28
// Codice del sito per giocare e puntare una somma

const moltiplicatore = 3; // Moltiplicatore per il guadagno in caso di vittoria

// Variabili per accedere agli elementi della pagina
var inp_numero = $("#numero_puntato")
var inp_saldo  = $("#saldo_puntato");
var btn_inizia      = $("#inizia");
var btn_rincomincia = $("#rincomincia");
var h_numero_uscito = $("#numero_uscito");
var h_saldo = $("#saldo");
var h_esito = $("#esito");

var saldo = 500; // Saldo iniziale


// Funzione che aggiorna il saldo sullo schermo
function mostraSaldo() {
    if(saldo <= 0) {
        saldo = 0; // Se il saldo è zero, lo imposto a zero e disabilito il pulsante "Inizia"
        btn_rincomincia.prop("hidden", false); // Mostra il pulsante "Rincomincia"
        btn_inizia.prop("disabled", true); // Disabilita il pulsante "Inizia"
    }

    h_saldo.text("Saldo: " + saldo); // Visualizza il saldo aggiornato
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


// Connette gli eventi ai metodi quando il documento è carico
$(document).ready(function() {

    // Funzione che viene chiamata quando l'utente inizia la scommessa
    btn_inizia.click(function() {

        // Prendo i valori inseriti dall'utente
        var numero_puntato = inp_numero.val();
        var saldo_puntato = inp_saldo.val();
    
        // Se uno dei valori non è inserito o contiene "e" (input non valido), esco dalla funzione
        if(numero_puntato == "" || saldo_puntato == "") return;
        if(numero_puntato.includes("e") || saldo_puntato.includes("e")) return;
    
        // Controllo che il numero puntato e il saldo puntato siano nei limiti
        numero_puntato = controllaInput(parseInt(numero_puntato), 12, 2);
        saldo_puntato = controllaInput(parseInt(saldo_puntato), saldo, 1);
    
        // Aggiorno i campi di input con i valori validati
        inp_numero.prop("value", numero_puntato);
        inp_saldo.prop("value", saldo_puntato);
    
        // Sottraggo il saldo puntato dal saldo totale
        saldo -= saldo_puntato;
        inp_saldo.attr("max", saldo); // Aggiorno il massimo del saldo possibile da puntare
    
        // Lancia i dadi e sommo i due risultati
        numero_uscito = lanciaDado() + lanciaDado();
        h_numero_uscito.text(numero_uscito); // Mostro il numero uscito
    
        // Verifico se l'utente ha vinto o perso
        if(numero_puntato != numero_uscito) {
            h_esito.text("Hai Perso!"); // Se non ha vinto, mostra "Hai perso"
            h_esito.removeClass("text-success");
            h_esito.addClass("text-danger");
        } else {
            // Se ha vinto, calcolo il guadagno e lo aggiungo al saldo
            guadagno = saldo_puntato * moltiplicatore;
            saldo += guadagno;
    
            h_esito.addClass("text-success"); // Mostro il messaggio di vittoria in verde
            h_esito.removeClass("text-danger");
    
            h_esito.text("Hai vinto! Hai guadagnato " + guadagno + "!"); // Mostro il guadagno
        }
    
        mostraSaldo(); // Mostro il saldo aggiornato dopo la scommessa

    })

    // Ricarica la pagina quando il saldo è esaurito
    btn_rincomincia.click(function() {
        location.reload();
    })
})

// Inizializzo la visualizzazione del saldo quando la pagina viene caricata
mostraSaldo();
inp_saldo.attr("max", saldo); // Imposto il massimo del saldo da puntare
