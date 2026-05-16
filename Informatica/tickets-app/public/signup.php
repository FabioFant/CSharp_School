<?php
session_start();
require_once "../config/db.php";
require_once "../config/constants.php";

$messaggio = "";

if($_SERVER["REQUEST_METHOD"] == "POST"){
    $matricola = trim($_POST["matricola"] ?? "");
    $nome      = trim($_POST["nome"]      ?? "");
    $cognome   = trim($_POST["cognome"]   ?? "");
    $password  = trim($_POST["password"]  ?? "");

    if (empty($matricola) || empty($nome) || empty($cognome) || empty($password)) {
        $messaggio = "Compila tutti i campi.";
    } 
    else if (!ctype_alpha($nome)) {
        $messaggio = "Il nome può contenere solo lettere.";
    }
    else if (!ctype_alpha($cognome)) {
        $messaggio = "Il cognome può contenere solo lettere.";
    }
    else if (strlen($matricola) != MATRICOLA_LENGTH || !ctype_digit($matricola)) {
        $messaggio = "Matricola errata, deve contenere ". MATRICOLA_LENGTH ." numeri.";
    } 
    else if (strlen($nome) > NOME_MAX_LENGTH) {
        $messaggio = "Nome troppo lungo (max ". NOME_MAX_LENGTH ." caratteri).";
    } 
    else if (strlen($cognome) > COGNOME_MAX_LENGTH) {
        $messaggio = "Cognome troppo lungo (max ". COGNOME_MAX_LENGTH ." caratteri).";
    } 
    else if (strlen($password) < PASSWORD_MIN_LENGTH || strlen($password) > PASSWORD_MAX_LENGTH) {
        $messaggio = "Password troppo corta (min ". PASSWORD_MIN_LENGTH ." caratteri).";
    } 
    else {
        $query = 'SELECT * FROM studente WHERE matricola = :matricola;';
        $stmt = $pdo->prepare($query);
        $stmt->bindParam(":matricola", $matricola, PDO::PARAM_STR);
        $stmt->execute();
        
        if($stmt->rowCount() > 0) {
            $messaggio = "Studente già registrato.";
        } else {
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
}   
?>

<!DOCTYPE html>
<html lang="it">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Sign up - Ticketing</title>
    <link rel="stylesheet" href="/css/style.css">
</head>
<body>
    <div class="auth-wrapper">
        <div class="card">
            <h1>Sign up</h1>
            
            <?php if(!empty($messaggio)): ?>
                <div class="messaggio"><?= e($messaggio) ?></div>
            <?php endif; ?>
            
            <form action="" method="post">
                <label for="matricola">Matricola</label>
                <input type="text" name="matricola" id="matricola" required>
                
                <label for="nome">Nome</label>
                <input type="text" name="nome" id="nome" required>
                
                <label for="cognome">Cognome</label>
                <input type="text" name="cognome" id="cognome" required>
                
                <label for="password">Password</label>
                <input type="password" name="password" id="password" required>
                
                <input type="submit" value="Registrati">
            </form>
            
            <a href="/login.php" class="link-alternativo">Hai già un account? Accedi</a>
        </div>
    </div>
</body>
</html>