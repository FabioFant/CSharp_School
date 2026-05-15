CREATE DATABASE IF NOT EXISTS tickets_app;
USE tickets_app;

CREATE TABLE IF NOT EXISTS studente (
    matricola VARCHAR(6) PRIMARY KEY,
    nome VARCHAR(50) NOT NULL,
    cognome VARCHAR(50) NOT NULL,
    password VARCHAR(255) NOT NULL
);

CREATE TABLE IF NOT EXISTS laboratorio (
    id INT AUTO_INCREMENT PRIMARY KEY,
    nome VARCHAR(50) NOT NULL UNIQUE
);

CREATE TABLE IF NOT EXISTS ticket (
    id INT AUTO_INCREMENT PRIMARY KEY,
    oggetto VARCHAR(250) DEFAULT 'Nessun oggetto.',
    descrizione VARCHAR(1000) DEFAULT 'Nessuna descrizione.',
    data_orario TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    matricola_studente VARCHAR(6) NOT NULL,
    id_lab INT NOT NULL,

    FOREIGN KEY (matricola_studente) REFERENCES studente(matricola),
    FOREIGN KEY (id_lab) REFERENCES laboratorio(id)
);

INSERT INTO laboratorio (nome) VALUES
('L1'),
('L2'),
('L3'),
('L4'),
('L5');