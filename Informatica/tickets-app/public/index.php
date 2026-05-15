<?php
session_start();
require_once "../config/db.php";
function e($stringa) {
    return htmlspecialchars($stringa ?? '', ENT_QUOTES, 'UTF-8');
}

if(!isset($_SESSION["matricola"])) {
    header("Location: /login.php");
    exit();
}

# Utente
$matricola = $_SESSION["matricola"];
$nome      = "";
$cognome   = "";

$query = 'SELECT nome, cognome FROM studente WHERE matricola = :matricola;';
$stmt = $pdo->prepare($query);
$stmt->bindParam(":matricola", $matricola, PDO::PARAM_STR);
$stmt->execute();
$res = $stmt->fetch();

if($res) {
    $nome    = e($res["nome"]);
    $cognome = e($res["cognome"]);
} else {
    header("Location: /logout.php");
    exit();
}

# -----

$messaggio = "";
if($_SERVER["REQUEST_METHOD"] == "POST") {
    $oggetto        = $_POST["oggetto"];
    $descrizione    = $_POST["descrizione"];
    $id_laboratorio = $_POST["id_laboratorio"];

    $valore_oggetto = empty($oggetto) ? "DEFAULT" : ":oggetto";
    $valore_descrizione = empty($descrizione) ? "DEFAULT" : ":descrizione";

    $query = 'SELECT * FROM laboratorio WHERE id = :id;';
    $stmt = $pdo->prepare($query);
    $stmt->bindParam(":id", $id_laboratorio, PDO::PARAM_STR);
    $stmt->execute();
    if($stmt->rowCount() == 0) {
        $messaggio = "Errore: Laboratorio non trovato.";
    } else {
        $query = "INSERT INTO ticket (oggetto, descrizione, matricola_studente, id_lab)
        VALUES ($valore_oggetto, $valore_descrizione, :matricola_studente, :id_lab);";
        $stmt = $pdo->prepare($query);

        if(!empty($oggetto)) {
            $stmt->bindParam(":oggetto", $oggetto, PDO::PARAM_STR);
        }
        if(!empty($descrizione)) {
            $stmt->bindParam(":descrizione", $descrizione, PDO::PARAM_STR);
        }
        $stmt->bindParam(":matricola_studente", $matricola,      PDO::PARAM_STR);
        $stmt->bindParam(":id_lab",             $id_laboratorio, PDO::PARAM_STR);
        if($stmt->execute()) {
            header("Location: " . $_SERVER['PHP_SELF'] . "?creato=1");
            exit();
        }
    }
}

if($_SERVER["REQUEST_METHOD"] == "GET") {
    if(($_GET["creato"] ?? '') == '1') {
        $messaggio = "Ticket creato!";
    }
}

# ----

# Laboratori
$query = 'SELECT * FROM laboratorio ORDER BY nome;';
$stmt = $pdo->prepare($query);
$stmt->execute();
$laboratori = $stmt->fetchAll();

# I miei Ticket
$query = "SELECT ticket.*, laboratorio.nome AS nome_lab 
          FROM ticket
          JOIN laboratorio ON ticket.id_lab = laboratorio.id
          WHERE ticket.matricola_studente = :matricola";
$stmt = $pdo->prepare($query);
$stmt->bindParam(":matricola", $matricola, PDO::PARAM_STR);
$stmt->execute();
$miei_ticket = $stmt->fetchAll();
?>

<!DOCTYPE html>
<html lang="it">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Sistema di Ticketing d'Istituto</title>
</head>
<body>
    <h1>Sistema di Ticketing d'Istituto</h1>
    <?php echo "<h2>Ciao ". $nome ." ". $cognome ."!</h2>"; ?>
    <a href="/logout.php">Logout</a>

    <hr>

    <h3>Invia un Ticket</h3>
    <?php
    if(!empty($messaggio)) {
        echo "<p>".$messaggio."</p>";
    }
    ?>
    <form action="" method="post">
        <label for="oggetto">Oggetto</label>
        <input type="text" name="oggetto" id="oggetto">
        <label for="descrizione">Descrizione</label>
        <input type="text" name="descrizione" id="descrizione">
        <label for="laboratorio">Laboratorio</label>
        <select name="id_laboratorio" id="laboratorio" required>
            <option value="">Seleziona</option>
            <?php foreach ($laboratori as $lab): ?>
            <option value="<?php echo $lab['id']; ?>">
                <?php echo e($lab["nome"]); ?>
            </option>
            <?php endforeach; ?>
        </select>
        <input type="submit" value="Invia">
    </form>
    <h3>I tuoi Ticket</h3>
        <?php if(!empty($miei_ticket)): ?>
            <?php foreach ($miei_ticket as $t): ?>
            <div>
                <h2>
                    N.<?= e($t["id"]) ?> LAB: <?= e($t["nome_lab"]) ?> - <?= e($t["data_orario"]) ?>
                </h2>
                <h3><?= e($t["oggetto"]) ?></h3>
                <p><?= e($t["descrizione"]) ?></p>
            </div>
            <?php endforeach; ?>
        <?php else: ?>
        <p>Non ci sono Ticket.</p>
        <?php endif; ?>
    <hr>

    <h3>Tutti i Ticket</h3>
    <strong>Filtra per:</strong>
    <form action="" method="get">
        <label for="filtro_lab">Laboratorio</label>
        <select name="filtro_lab" id="filtro_lab" required>
            <?php foreach ($laboratori as $lab): ?>
            <option value="<?php echo $lab['id']; ?>">
                <?php echo e($lab["nome"]); ?>
            </option>
            <?php endforeach; ?>
        </select>
        <label for="ordine_data">Ordina per:</label>
        <select name="ordine_data" id="ordine_data">
            <option value="DESC">Più recenti</option>
            <option value="ASC">Meno recenti</option>
        </select>
    </form>
</body>
</html>