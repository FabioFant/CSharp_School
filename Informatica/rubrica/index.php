<?php
session_start();

$errore = "";
$regex_nome = "/^[a-zA-ZàèéìòùÀÈÉÌÒÙ\s]+$/"; // Solo lettere e spazi
$regex_numero = "/^\+?[0-9]{6,15}$/"; // Prefisso facoltatico e solo numeri

if(!isset($_SESSION['contatti'])) {
    $_SESSION['contatti'] = [];
}

if(isset($_POST['aggiungi'])) {
    $nome = htmlspecialchars($_POST['nome_contatto']);
    $numero = htmlspecialchars($_POST['numero_contatto']);

    if(!empty($nome) && !empty($numero)) {
        if(!preg_match($regex_nome, $nome)) {
            $errore = "ERRORE: Il nome del contatto può contenere solo lettere e spazi.";
        }
        elseif(!preg_match($regex_numero, $numero)) {
            $errore = "ERRORE: Il numero di telefono può contenere solo numeri.";
        }
        else {
            $contatto = [
                "nome" => $nome,
                "numero" => $numero,
            ];
            array_push($_SESSION['contatti'], $contatto);
        }
    }
    else {
        $errore = "Alcuni campi del modulo non sono stati compilati";
    }
}

if(isset($_POST['reset'])) {
    session_destroy();
    header("Location: " . $_SERVER['PHP_SELF']); // Ricarica la pagina
    exit;
}
?>

<!DOCTYPE html>
<html lang="it">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Rubrica PHP - Fabio Fantini 5H</title>
    <style>
        body { font-family: sans-serif; padding: 20px; }
        .contatto { border-bottom: 1px solid #ccc; padding: 5px; }
        form { background: #f9f9f9; padding: 15px; border: 1px solid #ddd; width: 300px;}
        input { margin-bottom: 10px; display: block; width: 90%; padding: 5px;}
    </style>
</head>
<body>
    <?php if (!empty($errore)): ?>
        <script>
            alert("<?php echo $errore; ?>");
        </script>
    <?php endif; ?>

    <h2>Rubrica Contatti</h2>
    <form action="" method="POST">
        <label for="nome">Nome:</label>
        <input type="text" name="nome_contatto" id="nome">
        <label for="numero">Numero di telefono:</label>
        <input type="tel" name="numero_contatto" id="numero">
        <button type="submit" name="aggiungi">Aggiungi Contatto</button>
        <button type="submit" name="reset" style="background-color: red; color: white;">Cancella Rubrica</button>
    </form>

    <h3>Lista dei contatti salvati:</h3>
    <div id="lista_contatti">
        <?php
        if(!empty($_SESSION['contatti'])) {
            foreach ($_SESSION['contatti'] as $contatto) {
                echo "<div class='contatto'>";
                echo "<strong>Nome:</strong> " . $contatto['nome'] . " - ";
                echo "<strong>Tel:</strong> " . $contatto['numero'];
                echo "</div>";
            }
        }
        else {
            echo('<p>Nessun contatto nella rubrica.</p>');
        }
        ?>
    </div>
</body>
</html>
