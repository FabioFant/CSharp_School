<?php
session_start();
require_once "../config/db.php";

$messaggio = "";
$errore = "Matricola o Password non corretti.";

function check_if_empty($param, $nuovo_messaggio) {
    global $messaggio;
    if(empty($param)) {
        $messaggio = $nuovo_messaggio;
    }
}

if($_SERVER["REQUEST_METHOD"] == "GET") {
    if(!empty($_GET["registrato"]) && $_GET["registrato"] == 1) {
        $messaggio = "Studente registrato correttamente.";
    }
}

if($_SERVER["REQUEST_METHOD"] == "POST"){
    $matricola = trim($_POST["matricola"] ?? "");
    $password  = trim($_POST["password"] ?? "");

    check_if_empty($matricola, $errore);
    check_if_empty($password,  $errore);

    if(strlen($matricola) != 6 || !ctype_digit($matricola)) {
        $messaggio = $errore;
    }

    if(strlen($password) < 8) {
        $messaggio = $errore;
    }

    $query = 'SELECT * FROM studente WHERE matricola = :matricola;';
    $stmt = $pdo->prepare($query);
    $stmt->bindParam(":matricola", $matricola, PDO::PARAM_STR);
    $stmt->execute();
    if($stmt->rowCount() == 0) {
        $messaggio = $errore;
    } else {
        $res = $stmt->fetch();
        if($res && password_verify($password, $res['password'])) {
            $_SESSION["matricola"] = $matricola;
            header("Location: /");
            exit();
        } else {
            $messaggio = $errore;
        }
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
    <h1>Login</h1>
    <?php
    if(!empty($messaggio)) {
        echo "<p>".$messaggio."</p>";
    }
    ?>
    <form action="" method="post">
        <label for="matricola">Matricola</label>
        <input type="text" name="matricola" id="matricola" required>
        <label for="password">Password</label>
        <input type="password" name="password" id="password" required>
        <input type="submit" value="Login">
    </form>
    <a href="/signup.php">Sign up</a>
</body>
</html>