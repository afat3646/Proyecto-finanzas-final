CREATE DATABASE Control_Finanzas;

USE Control_Finanzas;

CREATE TABLE Usuario (
No_Usuario INT NOT NULL AUTO_INCREMENT,
Correo VARCHAR (70),
Contraseña VARCHAR (25),
Balance DOUBLE,
Nombres VARCHAR (150),
Apellidos VARCHAR (150),
Direccion VARCHAR (150),
Telefono VARCHAR (15),
PRIMARY KEY (No_Usuario));

CREATE TABLE Movimientos (
ID_Mov INT NOT NULL AUTO_INCREMENT,
Monto NUMERIC,
Categoria VARCHAR (30),
Fecha TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
No_Usuario INT NOT NULL,
Balance_Usuario DOUBLE,
PRIMARY KEY (ID_Mov),
FOREIGN KEY (No_Usuario) REFERENCES Usuario (No_Usuario));

SELECT * FROM Usuario;
SELECT * FROM V_Mov;

CREATE VIEW V_Mov AS
SELECT Movimientos.ID_Mov, Movimientos.Monto, Movimientos.Categoria, Movimientos.Fecha, Movimientos.No_Usuario, Usuario.Balance FROM Movimientos 
INNER JOIN Usuario ON Usuario.No_Usuario = Movimientos.No_Usuario;

INSERT INTO Usuario (Correo, Contraseña, Balance, Nombres, Apellidos, Direccion, Telefono)
VALUES ("angelhdzflores772.AH@gmail.com", "Angel123456", 47567.89, "Angel Gabriel", "Hernandez Flores", "Paseo de los Brezos 711A", "3311089404");

INSERT INTO Movimientos (Monto, Categoria, No_Usuario)
VALUES (10500.99, "Ocio", 1);

DROP TABLE IF EXISTS Movimientos;
DROP TABLE IF EXISTS Usuario;

ALTER TABLE Movimientos ADD COLUMN No_Usuario INT NOT NULL;
ALTER TABLE Movimientos ADD COLUMN Balance_Usuario DOUBLE;

ALTER TABLE Movimientos ADD FOREIGN KEY (No_Usuario) REFERENCES Usuario (No_Usuario);
ALTER TABLE Movimientos ADD CONSTRAINT FK_Balance FOREIGN KEY (Balance_Usuario) REFERENCES Usuario (Balance);

COMMIT;

