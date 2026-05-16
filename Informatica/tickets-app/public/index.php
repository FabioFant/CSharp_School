<?php
session_start();
require_once "../config/db.php";
require_once "../config/constants.php";

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
    $nome    = $res["nome"];
    $cognome = $res["cognome"];
} else {
    header("Location: /logout.php");
    exit();
}

# -----

$messaggio = "";
$tipo_messaggio = "errore";

if($_SERVER["REQUEST_METHOD"] == "POST") {
    $oggetto        = trim($_POST["oggetto"] ?? '');
    $descrizione    = trim($_POST["descrizione"] ?? '');
    $id_laboratorio = $_POST["id_laboratorio"] ?? '';

    if(strlen($oggetto) > OGGETTO_MAX_LENGTH || strlen($descrizione) > DESCRIZIONE_MAX_LENGTH) {
        header("Location: " . $_SERVER['PHP_SELF'] . "?errore=1");
        exit();
    }

    $valore_oggetto = empty($oggetto) ? "DEFAULT" : ":oggetto";
    $valore_descrizione = empty($descrizione) ? "DEFAULT" : ":descrizione";

    $query = 'SELECT * FROM laboratorio WHERE id = :id;';
    $stmt = $pdo->prepare($query);
    $stmt->bindParam(":id", $id_laboratorio, PDO::PARAM_STR);
    $stmt->execute();
    
    if($stmt->rowCount() == 0) {
        header("Location: " . $_SERVER['PHP_SELF'] . "?errore=1");
        exit();
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

# ------

$tutti_ticket = [];
if($_SERVER["REQUEST_METHOD"] == "GET") {
    if(($_GET["errore"] ?? '') == '1') {
        $messaggio = "Errore durante la creazione del ticket. Verifica la lunghezza dei campi e riprova.";
    } else if(($_GET["creato"] ?? '') == '1') {
        $messaggio = "Ticket creato con successo!";
        $tipo_messaggio = "successo";
    }

    $filtro_lab = ($_GET['filtro_lab'] ?? '');
    $ordine_data = ($_GET['ordine_data'] ?? '') === 'ASC' ? 'ASC' : 'DESC';

    # Tutti i Ticket
    $query = "SELECT ticket.*, 
                     laboratorio.nome AS nome_lab, 
                     studente.nome AS autore_nome, 
                     studente.cognome AS autore_cognome 
              FROM ticket
              JOIN laboratorio ON ticket.id_lab = laboratorio.id
              JOIN studente ON ticket.matricola_studente = studente.matricola";
    
    if (!empty($filtro_lab)) {
        $query .= " WHERE ticket.id_lab = :filtro_lab";
    }
    $query .= " ORDER BY ticket.data_orario $ordine_data;";
    $stmt = $pdo->prepare($query);
    
    if (!empty($filtro_lab)) {
        $stmt->bindParam(":filtro_lab", $filtro_lab, PDO::PARAM_INT);
    }
    
    $stmt->execute();
    $tutti_ticket = $stmt->fetchAll();
}

# Laboratori
$query = 'SELECT * FROM laboratorio ORDER BY nome;';
$stmt = $pdo->prepare($query);
$stmt->execute();
$laboratori = $stmt->fetchAll();

# I miei Ticket
$query = "SELECT ticket.*, laboratorio.nome AS nome_lab 
          FROM ticket
          JOIN laboratorio ON ticket.id_lab = laboratorio.id
          WHERE ticket.matricola_studente = :matricola
          ORDER BY ticket.data_orario DESC";
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
    <title>Dashboard - Ticketing d'Istituto</title>
    <link rel="stylesheet" href="/css/style.css">
</head>
<body>
    <div class="container-dashboard">
        
        <div class="header-top">
            <div>
                <h1>Sistema di Ticketing d'Istituto</h1>
                <h2>Ciao <?= e($nome) ?> <?= e($cognome) ?>!</h2>
            </div>
            <a href="/logout.php" class="btn-danger">Logout</a>
        </div>

        <?php if(!empty($messaggio)): ?>
            <div class="messaggio <?= $tipo_messaggio === 'successo' ? 'successo' : '' ?>">
                <strong><?= e($messaggio) ?></strong>
            </div>
        <?php endif; ?>

        <h3 class="section-title">Invia un Ticket</h3>
        <form action="" method="post">
            <label for="oggetto">Oggetto</label>
            <input type="text" name="oggetto" id="oggetto" maxlength="<?= OGGETTO_MAX_LENGTH ?>">
            
            <label for="descrizione">Descrizione</label>
            <input type="text" name="descrizione" id="descrizione" maxlength="<?= DESCRIZIONE_MAX_LENGTH ?>">
            
            <label for="laboratorio">Laboratorio</label>
            <select name="id_laboratorio" id="laboratorio" required>
                <option value="">Seleziona</option>
                <?php foreach ($laboratori as $lab): ?>
                <option value="<?= $lab['id']; ?>">
                    <?= e($lab["nome"]); ?>
                </option>
                <?php endforeach; ?>
            </select>
            <input type="submit" value="Invia Ticket">
        </form>
        
        <h3 class="section-title">I tuoi Ticket</h3>
        <?php if(!empty($miei_ticket)): ?>
            <?php foreach ($miei_ticket as $t): ?>
            <div class="ticket-card">
                <h2>N.<?= e($t["id"]) ?> | LAB: <?= e($t["nome_lab"]) ?> | <?= e($t["data_orario"]) ?></h2>
                <h3><?= e($t["oggetto"]) ?></h3>
                <p><?= e($t["descrizione"]) ?></p>
            </div>
            <?php endforeach; ?>
        <?php else: ?>
            <p>Non hai ancora inviato nessun ticket.</p>
        <?php endif; ?>

        <h3 class="section-title">Tutti i Ticket</h3>
        
        <form action="" method="get" class="filters-form">
            <label for="filtro_lab">Laboratorio:</label>
            <select name="filtro_lab" id="filtro_lab" onchange="this.form.submit()">
                <option value="">Tutti</option>
                <?php foreach ($laboratori as $lab): ?>
                <option value="<?= $lab['id']; ?>" <?= isset($_GET["filtro_lab"]) && (int)$_GET["filtro_lab"] === (int)$lab['id'] ? "selected" : ""; ?>>
                    <?= e($lab["nome"]); ?>
                </option>
                <?php endforeach; ?>
            </select>
            
            <label for="ordine_data">Ordina per:</label>
            <select name="ordine_data" id="ordine_data" onchange="this.form.submit()">
                <option value="DESC" <?= ($_GET["ordine_data"] ?? '') === "DESC" ? "selected" : ""; ?>>Più recenti</option>
                <option value="ASC" <?= ($_GET["ordine_data"] ?? '') === "ASC" ? "selected" : ""; ?>>Meno recenti</option>
            </select>   
        </form>
        
        <?php if(!empty($tutti_ticket)): ?>
            <?php foreach ($tutti_ticket as $t): ?>
            <div class="ticket-card">
                <h2>N.<?= e($t["id"]) ?> | LAB: <?= e($t["nome_lab"]) ?> | <?= e($t["data_orario"]) ?></h2>
                <h3><?= e($t["oggetto"]) ?></h3>
                <p><?= e($t["descrizione"]) ?></p>
                <span class="autore">Autore: (<?= e($t["matricola_studente"]); ?>) <?= e($t["autore_nome"]); ?> <?= e($t["autore_cognome"]); ?></span>
            </div>
            <?php endforeach; ?>
        <?php else: ?>
            <p>Non ci sono Ticket con questi filtri.</p>
        <?php endif; ?>

    </div>
</body>
</html>