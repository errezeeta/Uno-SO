DROP DATABASE IF EXISTS unoBD2;
CREATE DATABASE unoBD2;

USE unoBD2;

CREATE TABLE jugadores (
	id INT,
	nickname VARCHAR(100),
	passwords VARCHAR(100),
	numVictorias INT,
	numPartidas INT,
	PRIMARY KEY (id)
);

CREATE TABLE partidas (
	id INT,
	ganador VARCHAR(100),
	turnos INT,
	jugador1 VARCHAR(100),
	jugador2 VARCHAR(100),
	jugador3 VARCHAR(100),
	jugador4 VARCHAR(100),
	PRIMARY KEY (id)
);

CREATE TABLE relacion (
	idJugador INT,
	idPartida INT,
	FOREIGN KEY (idJugador) REFERENCES jugadores(id),
	FOREIGN KEY (idPartida) REFERENCES partidas(id)
);


INSERT INTO jugadores VALUES (1,'errezeeta','errezeeta',3,10);
INSERT INTO jugadores VALUES (2,'angelfm','angelfm',3,6);
INSERT INTO jugadores VALUES (3,'ricart_robles','ricart_robles',2,4);
INSERT INTO jugadores VALUES (4,'root','root',6,9);
INSERT INTO partidas VALUES (1,'angelfm',22,'angelfm','ricart_robles','errezeeta','-');
INSERT INTO partidas VALUES (2,'ricart_robles',18,'ricart_robles','errezeeta','angelfm','-');
INSERT INTO partidas VALUES (3,'errezeeta',8,'errezeeta','angelfm','ricart_robles','-');
INSERT INTO relacion VALUES (1,3);
INSERT INTO relacion VALUES (2,3);
INSERT INTO relacion VALUES (3,3);

