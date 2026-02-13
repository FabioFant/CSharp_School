<?php
session_start();

if (isset($_POST['reset'])) {
    $_SESSION['registri'] = [];
}

if (!isset($_SESSION['registri'])) {
    $_SESSION['registri'] = [];
}

if ($_SERVER["REQUEST_METHOD"] == "POST" && isset($_POST['materia']) && isset($_POST['voto'])) {
    if (!empty($_POST['materia']) && !empty($_POST['voto'])) {
        $materia = $_POST['materia'];
        $voto = (int)$_POST['voto'];
        
        $_SESSION['registri'][] = [
            'materia' => $materia,
            'voto' => $voto
        ];
    }
}

$somma = 0;
$numero_voti = count($_SESSION['registri']);

foreach ($_SESSION['registri'] as $item) {
    $somma += $item['voto'];
}

$media = 0;
if ($numero_voti > 0) {
    $media = $somma / $numero_voti;
}
?>

<!DOCTYPE html>
<html lang="it">
<head>
    <meta charset="UTF-8">
    <title>Gestione Voti Studente</title>
    <style>
        body { font-family: sans-serif; padding: 20px; }
        table { border-collapse: collapse; width: 100%; max-width: 600px; margin-top: 20px; }
        th, td { border: 1px solid #ddd; padding: 8px; text-align: left; }
        th { background-color: #f2f2f2; }
        .box-risultati { margin-top: 20px; padding: 10px; border: 1px solid #333; max-width: 600px; }
        .promosso { color: green; font-weight: bold; }
        .rimandato { color: red; font-weight: bold; }
        input, button { padding: 5px; margin-right: 10px; }
        .btn-reset { background-color: #ffcccc; border: 1px solid red; cursor: pointer; }
    </style>
</head>
<body>

    <h2>Inserimento Voti</h2>
    <form method="POST" action="index.php">
        <label>Materia:</label>
        <input type="text" name="materia" required placeholder="Es. Matematica">
        
        <label>Voto (1-10):</label>
        <input type="number" name="voto" min="1" max="10" required>
        
        <button type="submit">Aggiungi Voto</button>
    </form>

    <h2>Riepilogo Voti</h2>
    
    <table>
        <thead>
            <tr>
                <th>Materia</th>
                <th>Voto</th>
            </tr>
        </thead>
        <tbody>
            <?php foreach ($_SESSION['registri'] as $riga): ?>
            <tr>
                <td><?php echo htmlspecialchars($riga['materia']); ?></td>
                <td><?php echo $riga['voto']; ?></td>
            </tr>
            <?php endforeach; ?>
        </tbody>
    </table>

    <div class="box-risultati">
        <p><strong>Numero totale voti:</strong> <?php echo $numero_voti; ?></p>
        <p><strong>Media voti:</strong> <?php echo number_format($media, 2); ?></p>
        <p><strong>Esito:</strong> 
            <?php 
            if ($numero_voti > 0) {
                if ($media >= 6) {
                    echo '<span class="promosso">PROMOSSO</span>';
                } else {
                    echo '<span class="rimandato">RIMANDATO</span>';
                }
            } else {
                echo "Nessun voto inserito.";
            }
            ?>
        </p>
    </div>

    <br>
    <form method="POST" action="index.php">
        <button type="submit" name="reset" value="1" class="btn-reset">Resetta Tutto</button>
    </form>

</body>
</html>