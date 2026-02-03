<?php
// Configurazione
$COSTO_PIENO = 20;
$COSTO_RIDOTTO = 10;

$ETA_RIDOTTO = 65;
$ETA_GRATUITO = 12;

// Variabili
$nome = "";
$cognome = "";
$eta = "";
$costo = 0;

if(isset($_POST['nomeInserito']) && isset($_POST['cognomeInserito']) && isset($_POST['etaInserita'])) {
    $nome = htmlspecialchars($_POST['nomeInserito']);
    $cognome = htmlspecialchars($_POST['cognomeInserito']);
    $eta = htmlspecialchars($_POST['etaInserita']);

    if(!empty($eta)) {
        if($eta < $ETA_GRATUITO) {
            $costo = 0;
        }
        elseif($eta > $ETA_RIDOTTO) {
            $costo = $COSTO_RIDOTTO;
        }
        else {
            $costo = $COSTO_PIENO;
        }
    }
}
?>

<!DOCTYPE html>
<html lang="it">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Acquisto biglietto - Fabio Fantini 5H</title>
    
    <style>
        * {
            box-sizing: border-box;
            margin: 0;
            padding: 0;
        }

        body {
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
            background-color: #f0f2f5;
            display: flex;
            justify-content: center;
            align-items: center;
            min-height: 100vh;
            color: #333;
        }

        .container {
            background-color: white;
            padding: 2rem;
            border-radius: 12px;
            box-shadow: 0 4px 15px rgba(0,0,0,0.1);
            width: 100%;
            max-width: 400px;
        }

        h1 {
            text-align: center;
            margin-bottom: 1.5rem;
            color: #2c3e50;
            font-size: 1.8rem;
        }

        form {
            display: flex;
            flex-direction: column;
        }

        label {
            margin-bottom: 5px;
            font-weight: 600;
            font-size: 0.9rem;
            color: #555;
        }

        input {
            padding: 10px;
            margin-bottom: 15px;
            border: 1px solid #ddd;
            border-radius: 6px;
            font-size: 1rem;
            transition: border-color 0.3s;
        }

        input:focus {
            outline: none;
            border-color: #007bff;
            box-shadow: 0 0 0 3px rgba(0,123,255,0.1);
        }

        button {
            background-color: #007bff;
            color: white;
            padding: 12px;
            border: none;
            border-radius: 6px;
            font-size: 1rem;
            font-weight: bold;
            cursor: pointer;
            transition: background-color 0.3s;
            margin-top: 10px;
        }

        button:hover {
            background-color: #0056b3;
        }

        .ticket-result {
            margin-top: 25px;
            padding-top: 20px;
            border-top: 2px dashed #eee;
            text-align: center;
            animation: fadeIn 0.5s ease-in;
        }

        .ticket-result h2 {
            font-size: 1.4rem;
            color: #27ae60;
            margin-bottom: 10px;
        }

        .ticket-info {
            background-color: #f8f9fa;
            padding: 15px;
            border-radius: 8px;
            border: 1px solid #e9ecef;
            margin-bottom: 10px;
        }

        .ticket-msg {
            color: #e67e22;
            font-size: 0.9rem;
            font-style: italic;
        }

        @keyframes fadeIn {
            from { opacity: 0; transform: translateY(10px); }
            to { opacity: 1; transform: translateY(0); }
        }
    </style>
</head>
<body>

    <div class="container">
        <h1>Acquisto Biglietti</h1>
        
        <form action="" method="POST">
            <label for="nome">Nome</label>
            <input type="text" name="nomeInserito" id="nome" required placeholder="Inserisci il nome">
            
            <label for="cognome">Cognome</label>
            <input type="text" name="cognomeInserito" id="cognome" required placeholder="Inserisci il cognome">
            
            <label for="eta">Età</label>
            <input type="number" name="etaInserita" id="eta" required min="0" placeholder="Anni">
            
            <button type="submit" name="acquista">Calcola Prezzo</button>
        </form>

        <?php
        if(!empty($nome) && !empty($cognome) && !empty($eta)) {
            echo "<div class='ticket-result'>";
            echo "<h2>Il tuo biglietto</h2>";
            
            echo "<div class='ticket-info'>";
            echo "<p><strong>Passeggero:</strong> " . $nome . " " . $cognome . "</p>";
            echo "<p style='font-size: 1.2rem; margin-top: 5px;'><strong>Costo: " . $costo . "€</strong></p>";
            echo "</div>";

            if($eta > $ETA_RIDOTTO) {
                echo "<h3 class='ticket-msg'>Hai diritto ad un costo ridotto (Over 65)</h3>";
            }
            elseif($eta < $ETA_GRATUITO) {
                echo "<h3 class='ticket-msg'>Hai diritto ad un biglietto gratuito (Under 12)</h3>";
            }
            echo "</div>";
        }
        ?>
    </div>

</body>
</html>