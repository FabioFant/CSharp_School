const regexName = "\w[a-zA-Z]{2, 25}";
const regexUser = "[a-zA-Z0-9]{3, 25}";
const regexEmail = "^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
const regexPassword = "^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[!@#$%^&*])[A-Za-z0-9!@#$%^&*]{8,}";
const regexData = "^(0[1-9]|[12][0-9]|3[01])/(0[1-9]|1[0-2])/(?:19\d{2}|20[01][0-5])$";

const errorMsgNomeCognome = "Il nome e cognome devono essere min.2 e max.25 caratteri e devono contenere solo lettere.";
const errorMsgData = "La data deve rispettare il formato gg/mm/aa e deve esistere, inoltre non può essere ne troppo recente nè troppo passata.";
const errorMsgEmail = "L'email deve rispettare il formato e deve contenere solo caratteri consentiti.";
const errorMsgUtente = "L'utente deve essere min.3 e max.25 caratteri e devono contenere solo lettere o numeri.";
const errorMsgPassword = "La password non rispetta tutti i requisiti elencati.";

let inputNome = $("#nome");
let inputCognome = $("#cognome");
let inputDataDiNascita = $("#dataDiNascita");
let inputEmail = $("#email");
let inputUtente = $("#utente");
let inputPassword = $("#password");
let inputSubmit = $("#submit");

let errorNome = $("#errorNome");
let errorDataDiNascita = $("#errorData");
let errorEmail = $("#errorEmail");
let errorUtente = $("#errorUser");
let errorPassword = $("#errorPassword");

$(document).ready(function() {
    inputSubmit.click(function() {
        
        // TODO : FIX
        checkNaNAndRegex(inputNome, errorNome, errorMsgNomeCognome, regexName);
        checkNaNAndRegex(inputCognome, errorNome, errorMsgNomeCognome, regexName);
        checkNaNAndRegex(inputDataDiNascita, errorDataDiNascita, errorMsgData, regexData);
        checkNaNAndRegex(inputEmail, errorEmail, errorMsgEmail, regexEmail);
        checkNaNAndRegex(inputUtente, errorUtente, errorMsgUtente, regexUser);
        checkNaNAndRegex(inputPassword, errorPassword, errorMsgPassword, regexPassword);

    })
})

function checkNaNAndRegex(inputComponent, errorComponent, errorMessage, regexString)
{
    let val = inputComponent.val();
    console.log(val);
    let regex = new RegExp(regexString);

    errorComponent.prop('hidden', true);
    if(val == "")
    {
        errorComponent.prop('hidden', false);
        errorComponent.text('Campo non compilato.');
        return;
    }
    if(regex.test(val))
    {
        errorComponent.prop('hidden', false);
        errorComponent.text(errorMessage);
        return;
    }
}