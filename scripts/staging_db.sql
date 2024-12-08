-- Crear Base De Datos
CREATE DATABASE STAGING_DATABASE

-- Creacion de Tablas

CREATE TABLE Usuario
(
    id INT PRIMARY KEY IDENTITY(1,1),
    nombre VARCHAR(255) NOT NULL,
    apellido VARCHAR(255) NOT NULL,
    telefono INT
);

INSERT INTO Usuario (nombre, apellido, telefono) VALUES ('CarlosStage', 'VargasStage', 998887777)