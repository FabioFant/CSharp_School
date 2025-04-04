// Fabio Fantini 4H 2025-04-04
// JS per il form di validazione dell'input

// Aggiungi un metodo di validazione personalizzato per il campo 'Nome e Cognome'
$.validator.addMethod("validaNomeCognome", function(value, element) {
    // Verifica che il valore contenga solo lettere (maiuscole e minuscole) e abbia una lunghezza compresa tra 3 e 25 caratteri
    return this.optional(element) || /^[a-zA-Z]{3,25}$/.test(value);
}, "Il nome o il cognome deve contenere solo lettere e avere una lunghezza compresa tra 3 e 25 caratteri.");
// Messaggio di errore personalizzato se il nome o cognome non rispettano i requisiti

// Aggiungi un metodo di validazione personalizzato per l'email
$.validator.addMethod("validaEmail", function(value, element) {
    // Verifica che l'email rispetti un formato valido (esempio: nome@mail.com)
    return this.optional(element) || /^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$/.test(value);
}, "Inserisci un indirizzo email con caratteri validi e che rispetti il formato.");
// Messaggio di errore personalizzato per una email non valida

// Aggiungi un metodo di validazione personalizzato per il nome utente
$.validator.addMethod("validaNomeUtente", function(value, element) {
    // Verifica che il nome utente contenga solo caratteri alfanumerici, punto, underscore e trattino, con una lunghezza tra 8 e 16 caratteri
    return this.optional(element) || /^[a-zA-Z0-9._-]{8,16}$/.test(value);
}, "Il nome utente deve essere composto da 8 a 16 caratteri alfanumerici.");
// Messaggio di errore personalizzato per un nome utente non valido

// Aggiungi un metodo di validazione personalizzato per la password
$.validator.addMethod("validaPassword", function(value, element) {
    // Verifica che la password contenga almeno una lettera minuscola, una maiuscola, un numero e un carattere speciale
    return this.optional(element) || /^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[!@#$%^&*])(?=.{8,})/.test(value);
}, "La password non rispetta i requisiti.");
// Messaggio di errore personalizzato se la password non soddisfa i criteri di sicurezza

// Aggiungi un metodo di validazione personalizzato per la data di nascita
$.validator.addMethod("validaDataNascita", function(value, element) {
    // Converte la data selezionata e le date minima e massima in oggetti Date
    const dataSelezionata = new Date(value);
    const dataMinima = new Date(element.min);
    const dataMassima = new Date(element.max);
    // Verifica che la data di nascita sia compresa tra la data minima e massima
    return this.optional(element) || (dataSelezionata >= dataMinima && dataSelezionata <= dataMassima);
}, "La data di nascita deve essere compresa tra il 1° gennaio 1905 e il 31 dicembre 2011.");
// Messaggio di errore personalizzato se la data di nascita non è valida

$(document).ready(function() {
    // Imposta la validazione del modulo quando il documento è pronto
    $('#form').validate({
        rules: {
            // Definisce le regole di validazione per ciascun campo
            name: {
                required: true,            // Il campo 'Nome' è obbligatorio
                validaNomeCognome: true    // Applica la validazione personalizzata 'validaNomeCognome'
            },
            surname: {
                required: true,            // Il campo 'Cognome' è obbligatorio
                validaNomeCognome: true    // Applica la validazione personalizzata 'validaNomeCognome'
            },
            birthdate: {
                required: true,            // Il campo 'Data di nascita' è obbligatorio
                validaDataNascita: true    // Applica la validazione personalizzata 'validaDataNascita'
            },
            email: {
                required: true,            // Il campo 'Email' è obbligatorio
                validaEmail: true          // Applica la validazione personalizzata 'validaEmail'
            },
            username: {
                required: true,            // Il campo 'Nome utente' è obbligatorio
                validaNomeUtente: true     // Applica la validazione personalizzata 'validaNomeUtente'
            },
            password: {
                required: true,            // Il campo 'Password' è obbligatorio
                validaPassword: true       // Applica la validazione personalizzata 'validaPassword'
            }
        },
        // Personalizza la posizione degli errori
        errorPlacement: function(errore, elemento) {
            // Aggiunge l'errore nel contenitore di errore associato al campo
            errore.appendTo(elemento.closest('.mb-3').find('.error-message'));
        },
        // Evidenzia i campi con errori
        highlight: function(elemento) {
            $(elemento).addClass('is-invalid').removeClass('is-valid');
        },
        // Rimuove l'evidenziazione dei campi quando diventano validi
        unhighlight: function(elemento) {
            $(elemento).removeClass('is-invalid').addClass('is-valid');
        },
        // Gestisce gli errori globali del modulo
        invalidHandler: function(evento, validatore) {
            // Mostra il messaggio di errore globale quando ci sono errori nel modulo
            $('#form-errors').removeClass('d-none').html(
                'Si prega di correggere i campi evidenziati in rosso.'
            );
        },
        // Gestisce la sottomissione del modulo
        submitHandler: function(form) {
            // Nasconde i messaggi di errore globali e invia il modulo
            $('#form-errors').addClass('d-none').empty();
            form.submit();
        }
    });
});
