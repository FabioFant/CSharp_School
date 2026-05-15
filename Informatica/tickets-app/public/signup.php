<?php
session_start();
require_once "../config/db.php";

$messaggio = "";

function check_if_empty($param, $nuovo_messaggio) {
    global $messaggio;
    if(empty($param)) {
        $messaggio = $nuovo_messaggio;
    }
}
function check_if_not_alpha($param, $nuovo_messaggio) {
    global $messaggio;
    if(!ctype_alpha($param)) {
        $messaggio = $nuovo_messaggio;
    }
}

if($_SERVER["REQUEST_METHOD"] == "POST"){
    $matricola = trim($_POST["matricola"] ?? "");
    $nome      = trim($_POST["nome"]      ?? "");
    $cognome   = trim($_POST["cognome"]   ?? "");
    $password  = trim($_POST["password"]  ?? "");

    check_if_empty($matricola, "Matricola mancante.");
    check_if_empty($nome,      "Nome mancante.");
    check_if_empty($cognome,   "Cognome mancante.");
    check_if_empty($password,  "Password mancante.");

    check_if_not_alpha($nome,    "Il nome può contenere solo lettere.");
    check_if_not_alpha($cognome, "Il cognome può contenere solo lettere.");

    if(strlen($matricola) != 6 || !ctype_digit($matricola)) {
        $messaggio = "Matricola errata, deve contenere 6 numeri.";
    }

    if(strlen($password) < 8) {
        $messaggio = "La password deve esssere almeno lunga 8 caratteri.";
    }

    $query = 'SELECT * FROM studente WHERE matricola = :matricola;';
    $stmt = $pdo->prepare($query);
    $stmt->bindParam(":matricola", $matricola, PDO::PARAM_STR);
    $stmt->execute();
    if($stmt->rowCount() > 0) {
        $messaggio = "Studente già registrato.";
    }

    if(empty($messaggio)) {
        $hash = password_hash($password, PASSWORD_DEFAULT);
        $query = 'INSERT INTO studente (matricola, nome, cognome, password) VALUES (:matricola, :nome, :cognome, :password);';
        $stmt = $pdo->prepare($query);
        $stmt->bindParam(":matricola", $matricola,  PDO::PARAM_STR);
        $stmt->bindParam(":nome",      $nome,       PDO::PARAM_STR);
        $stmt->bindParam(":cognome",   $cognome,    PDO::PARAM_STR);
        $stmt->bindParam(":password",  $hash,       PDO::PARAM_STR);
        $stmt->execute();

        header("Location: /login.php?registrato=1");
        exit();
    }
}   
?>

<!DOCTYPE html>
<html lang="it">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Sign up</title>
</head>
<body>
    <h1>Sign up</h1>
    <?php
    if(!empty($messaggio)) {
        echo "<p>".$messaggio."</p>";
    }
    ?>
    <form action="" method="post">
        <label for="matricola">Matricola</label>
        <input type="text" name="matricola" id="matricola" required>
        <label for="nome">Nome</label>
        <input type="text" name="nome" id="nome" required>
        <label for="cognome">Cognome</label>
        <input type="text" name="cognome" id="cognome" required>
        <label for="password">Password</label>
        <input type="password" name="password" id="password" required>
        <input type="submit" value="Sign up">
    </form>
    <a href="/login.php">Login</a>
</body>
</html>