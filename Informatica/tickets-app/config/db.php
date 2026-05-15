<?php
$dsn = "mysql:host=db;dbname=tickets_app;charset=utf8";

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
}
catch (PDOException $e) {
    echo "Connection with DB failed.";
}