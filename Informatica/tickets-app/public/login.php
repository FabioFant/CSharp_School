<?php
session_start();
require_once "../config/db.php";
require_once "../config/constants.php";

$messaggio = "";
$tipo_messaggio = "";
$errore_generico = "Matricola o Password non corretti.";

if ($_SERVER["REQUEST_METHOD"] == "GET") {
    if (($_GET["registrato"] ?? '') == '1') {
        $messaggio = "Studente registrato correttamente. Ora puoi accedere.";
        $tipo_messaggio = "successo";
    }
}

if ($_SERVER["REQUEST_METHOD"] == "POST") {
    $matricola = trim($_POST["matricola"] ?? "");
    $password  = $_POST["password"] ?? "";

    if (empty($matricola) || empty($password)) {
        $messaggio = "Compila tutti i campi."; 
    } 
    else if (strlen($matricola) != MATRICOLA_LENGTH || !ctype_digit($matricola)) {
        $messaggio = $errore_generico;
    } 
    else if (strlen($password) < PASSWORD_MIN_LENGTH || strlen($password) > PASSWORD_MAX_LENGTH) {
        $messaggio = $errore_generico;
    } 
    else {
        $query = 'SELECT * FROM studente WHERE matricola = :matricola;';
        $stmt = $pdo->prepare($query);
        $stmt->bindParam(":matricola", $matricola, PDO::PARAM_STR);
        $stmt->execute();
        
        $res = $stmt->fetch(PDO::FETCH_ASSOC);
        if ($res && password_verify($password, $res['password'])) {
            session_regenerate_id(true);
            $_SESSION["matricola"] = $matricola;
            header("Location: /index.php");
            exit();
        } else {
            $messaggio = $errore_generico;
        }
    }
}   
?>

<!DOCTYPE html>
<html lang="it">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Login - Ticketing</title>
    <link rel="stylesheet" href="/css/style.css">
</head>
<body>
    <div class="auth-wrapper">
        <div class="card">
            <h1>Login</h1>
            
            <?php if(!empty($messaggio)): ?>
                <div class="messaggio <?= $tipo_messaggio === 'successo' ? 'successo' : '' ?>">
                    <?= e($messaggio) ?>
                </div>
            <?php endif; ?>
            
            <form action="" method="post">
                <label for="matricola">Matricola</label>
                <input type="text" name="matricola" id="matricola" required>
                
                <label for="password">Password</label>
                <input type="password" name="password" id="password" required>
                
                <input type="submit" value="Accedi">
            </form>
            
            <a href="/signup.php" class="link-alternativo">Non hai un account? Registrati qui</a>
        </div>
    </div>
</body>
</html>