-- Crear Base De Datos

CREATE DATABASE DEVELOP_DATABASE

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
    ('UD001', 'CarlosDev1', 'VargasDev1', 'carlosdv1@gmail.com', '987766556', '123', '123', 0121, '2008-11-11 13:23:44', 1, 2)

INSERT INTO Usuario
    (id, nombre, apellido, correo, telefono, password, confirmPassword, salt, date, isActive, role)
VALUES
    ('UD002', 'CarlosDev2', 'VargasDev2', 'carlosdv2@gmail.com', '987766556', '123', '123', 0121, '2008-11-11 13:23:44', 1, 2)


SELECT *
FROM Usuario