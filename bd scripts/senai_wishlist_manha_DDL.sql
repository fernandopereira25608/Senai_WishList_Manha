CREATE DATABASE senai_wishlist_manha

USE senai_wishlist_manha
GO

CREATE TABLE DESEJO(
idDesejo INT PRIMARY KEY IDENTITY(1,1),
descricao VARCHAR(256) NOT NULL
)

CREATE TABLE USUARIO(
idUsuario INT PRIMARY KEY IDENTITY(1,1),
email VARCHAR(256) NOT NULL UNIQUE,
senha VARCHAR(30) NOT NULL
)

CREATE TABLE DESEJOUSUARIO(
idUsuario INT FOREIGN KEY REFERENCES USUARIO(idUsuario),
idDesejo INT FOREIGN KEY REFERENCES DESEJO(idDesejo)
)