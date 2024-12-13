-- Crear Base De Datos
CREATE DATABASE STAGING_DATABASE

-- Creacion de Tablas

CREATE TABLE Usuario
(
    id VARCHAR(255) PRIMARY KEY,
    nombre VARCHAR(255) NOT NULL,
    apellido VARCHAR(255) NOT NULL,
    correo VARCHAR(255) NOT NULL,
    telefono VARCHAR(255) NOT NULL,
    password VARCHAR(255) NOT NULL,
    confirmPassword VARCHAR(255) NOT NULL,
    salt varbinary(1024) NOT NULL,
    date DATETIME NOT NULL,
    isActive BIT,
    role INT NOT NULL
);

INSERT INTO Usuario
    (id, nombre, apellido, correo, telefono, password, confirmPassword, salt, date, isActive, role)
VALUES
    ('US001', 'CarlosStage1', 'VargasStage2', 'carlosStage@gmail.com', '987766556', '123', '123', 0121, '2008-11-11 13:23:44', 1, 2)

SELECT *
FROM Usuario