<?php

define('MATRICOLA_LENGTH', 6);
define('NOME_MAX_LENGTH', 50);
define('COGNOME_MAX_LENGTH', 50);
define('PASSWORD_MIN_LENGTH', 8);
define('PASSWORD_MAX_LENGTH', 255);
define('OGGETTO_MAX_LENGTH', 250);
define('DESCRIZIONE_MAX_LENGTH', 1000);

function e(string $stringa) {
    return htmlspecialchars($stringa ?? '', ENT_QUOTES, 'UTF-8');
}