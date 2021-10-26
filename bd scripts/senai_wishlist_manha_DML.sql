USE senai_wishlist_manha
GO

INSERT INTO DESEJO (descricao)
VALUES ('Ter uma porsche'),
	   ('Comprar uma casa'),
	   ('Sextar eternamente')

INSERT INTO USUARIO (email, senha)
VALUES ('saulo@gmail.com', 'saulindo123'),
	   ('yuri@gmail.com', 'yuri123'),
	   ('fernando@gmail.com', 'fernando123')

INSERT INTO DESEJOUSUARIO(idUsuario, idDesejo)
VALUES (2,1),
	   (1,3),
	   (3,2)