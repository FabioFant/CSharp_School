<?php
$dsn = "mysql:host=db;dbname=tickets_app;charset=utf8";

$max_retries = 10;
$retry_delay = 5;
for ($i = 1; $i <= $max_retries; $i++) {
    try {
        $pdo = new PDO(
            $dsn,
            "admin",
            "admin",
            [
                PDO::ATTR_ERRMODE => PDO::ERRMODE_EXCEPTION,
                PDO::ATTR_DEFAULT_FETCH_MODE => PDO::FETCH_ASSOC
            ]
        );
        
        break;
        
    } catch (PDOException $e) {
        if ($i === $max_retries) {
            die("Connection with DB failed dopo $max_retries tentativi.");
        }
        sleep($retry_delay);
    }
}
?>