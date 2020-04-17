#include <string.h>
#include <unistd.h>
#include <stdlib.h>
#include <sys/types.h>
#include <sys/socket.h>
#include <netinet/in.h>
#include <stdio.h>
#include <mysql.h>
#include <pthread.h>



void *AtenderCliente (void * socket)
{
	
	//int *s;
	//s=(int*)socket;
	//sock_conn = *s;
	
	int sock_conn = *(int *) socket;
	
	char buff[512];
	char respuesta[512];
	int ret;
	int resu;
	
	int terminar =0;
	while (terminar ==0)
	{
		
		// EL SERIVIDOR SE QUEDARA PARADO HASTA QUE ALGUIEN SE CONECTE
		// recoger la peticion, read = lectura, en ret me va a dar el numero de byts que he leido
		// en buff me va a guardar la peticion
		ret=read(sock_conn,buff, sizeof(buff));
		printf ("Recibido\n");
		
		// Añadimos una marca de fin de sting al mensaje que acabo de recivir
		// si el mensaje tiene 7 caract. estaran en las posiciones 0-6 del vector, y en al posicion 7
		//ponemos fin de linea "\0".
		buff[ret]='\0';
		
		//3. DETERMINAR QUE ES LO QUE ME PIDEN
		
		printf ("Se ha conectado: %s\n",buff);
		
		MYSQL *conn;
		int err;
		// Estructura especial para almacenar resultados de consultas 
		MYSQL_RES *resultado;
		MYSQL_ROW row;
		char consulta [1000];
		//Creamos una conexion al servidor MYSQL 
		conn = mysql_init(NULL);
		
		if (conn==NULL) {
			printf ("Error al crear la conexi??n: %u %s\n", 
					mysql_errno(conn), mysql_error(conn));
			exit (1);
		}
		//inicializar la conexion
		conn = mysql_real_connect (conn, "localhost","root", "mysql", "unoBD2",0, NULL, 0);
		if (conn==NULL) {
			printf ("Error al inicializar la conexi??n: %u %s\n", 
					mysql_errno(conn), mysql_error(conn));
			exit (1);
		}
		// consulta SQL para obtener una tabla con todos los datos
		// de la base de datos
		err=mysql_query (conn, "SELECT * FROM jugadores");
		if (err!=0) {
			printf ("Error al consultar datos de la base %u %s\n",
					mysql_errno(conn), mysql_error(conn));
			exit (1);
		}
	
		
		//recogemos el resultado de la consulta. El resultado de la
		//consulta se devuelve en una variable del tipo puntero a
		//MYSQL_RES tal y como hemos declarado anteriormente.
		//Se trata de una tabla virtual en memoria que es la copia
		//de la tabla real en disco.
		resultado = mysql_store_result (conn);
		// vamos a ver que quiere
	
		//cogemos la peticion y cortamos por donde hay una barra, coges lo que hay desde el inicio hasta
		//la barra
		char *p = strtok(buff,"/");
		//coges lo que has cortado y lo combiertes a numero enter y lo metes en la variable codigo
		int codigo = atoi (p);
		// corta por la siguiente barra o hasta que encuentre final de string y te devuelve un puntero al inicio
		//de ese segundo trozo, en p tengo ya el segundo trozo, que es un nombre
		char nombre [90];
		// copias lo que hay en el punter p (string) en la variable nombre
		char nombre2[30];
		if (codigo !=0)
		{
			p = strtok (NULL,"/");
			strcpy (nombre, p);
		}
		if(codigo ==0)
			terminar=1;
		
		if (codigo == 1) // piden la longitud del nobre
		{
			printf ("codigo:%d\n",codigo);
			strcpy (consulta,"SELECT MIN(tiempoPartida) FROM partidas,relacion,jugadores WHERE jugadores.numVictorias=(SELECT MAX(numVictorias) FROM jugadores) AND jugadores.id=relacion.idJugador AND relacion.idPartida=partidas.id;"
					); 
			
			// hacemos la consulta 
			err=mysql_query (conn, consulta); 
			if (err!=0) {
				printf ("Error al consultar datos de la base %u %s\n",
						mysql_errno(conn), mysql_error(conn));
				exit (1);
			}
			//recogemos el resultado de la consulta 
			resultado = mysql_store_result (conn); 
			row = mysql_fetch_row (resultado);
			if (row == NULL)
				printf ("No se han obtenido datos en la consulta\n");
			else
				// El resultado debe ser una matriz con una sola fila
				// y una columna que contiene el nombre
				resu= atoi(row[0]);
			sprintf(respuesta, "%d",resu);
			printf ("La partida más corta del jugador con más victorias es de %s segundos\n",row[0]);
			// cerrar la conexion con el servidor MYSQL 
			mysql_close (conn);
		}
		else if (codigo ==2)
		{	
			
			err=mysql_query (conn, "SELECT ganador FROM partidas WHERE tiempoPartida =(SELECT MIN(tiempoPartida) FROM partidas) ;"); 
			if (err!=0) {
				printf ("Error al consultar datos de la base %u %s\n",
						mysql_errno(conn), mysql_error(conn));
				exit (1);
			}
			//recogemos el resultado de la consulta 
			resultado = mysql_store_result (conn); 
			row = mysql_fetch_row (resultado);
			if (row == NULL)
				printf ("No se han obtenido datos en la consulta\n");
			else
			{
				// El resultado debe ser una matriz con una sola fila
				// y una columna que contiene el nombre
				sprintf(respuesta, "%s",row[0]);
				printf ("Nombre del ganador de la partida mas corta es: %s\n", row[0] );
			}
			
			mysql_close (conn);
		}
		else if (codigo == 3)//dime si soy alto?
		{
			
			resultado = mysql_store_result (conn);
			// volvemos a cortar y cogemos el nuemreo decimal y lo guardamos en la variable altur
			strcpy(consulta,"SELECT relacion.posicion FROM jugadores, partidas, relacion WHERE relacion.mas4Recibido=(SELECT MAX(mas4recibido) FROM partidas, relacion, jugadores WHERE relacion.idJugador= (SELECT jugadores.id FROM jugadores WHERE jugadores.nickname='");
			strcat(consulta, nombre);
			strcat(consulta, "'));");
			
			// hacemos la consulta 
			err=mysql_query (conn, consulta); 
			if (err!=0) {
				printf ("Error al consultar datos de la base %u %s\n",
						mysql_errno(conn), mysql_error(conn));
				exit (1);
			}
			//recogemos el resultado de la consulta 
			resultado = mysql_store_result (conn); 
			row = mysql_fetch_row (resultado);
			if (row == NULL)
				printf ("No se han obtenido datos en la consulta\n");
			else
				// El resultado debe ser una matriz con una sola fila
				// y una columna que contiene el nombre
				printf ("La partida más corta del jugador con más victorias es de %s segundos\n",row[0]);
			resu= atoi(row[0]);
			sprintf(respuesta, "%d",resu);
			
			// cerrar la conexion con el servidor MYSQL 
			mysql_close (conn);
			// Y lo enviamos
			
			
			// Se acabo el servicio para este cliente
			
		}else if (codigo == 4)//login
		{
			p = strtok (NULL,"/");
			strcpy (nombre2, p);
			
			resultado = mysql_store_result (conn);
			// volvemos a cortar y cogemos el nuemreo decimal y lo guardamos en la variable altur
			strcpy(consulta,"SELECT jugadores.passwords FROM jugadores WHERE jugadores.nickname='");
			strcat(consulta,nombre);
			strcat(consulta, "';");
			
			// hacemos la consulta 
			err=mysql_query (conn, consulta); 
			if (err!=0) {
				printf ("Error al consultar datos de la base %u %s\n",
						mysql_errno(conn), mysql_error(conn));
				exit (1);
			}
			//recogemos el resultado de la consulta 
		
			resultado = mysql_store_result (conn); 
			row = mysql_fetch_row (resultado);
			if (row == NULL)
			{
				terminar=1;
				printf ("No se han obtenido datos en la consulta\n");
			
			}
			else{
				// El resultado debe ser una matriz con una sola fila
				// y una columna que contiene el nombre
			
				resu=strcmp(nombre2,row[0]);
			
				sprintf(respuesta,"%d",resu);
		
				printf ("yo devuelvo\n",row[0]);

			}
		
			// cerrar la conexion con el servidor MYSQL 
			mysql_close (conn);
			
			// Y lo enviamos
			// Se acabo el servicio para este cliente
		}else if (codigo == 5)//crear usuario
		{
			int id;
			char newid[30];
			p = strtok (NULL,"/");
			strcpy (nombre2, p);
			
			resultado = mysql_store_result (conn);
			
			// buscamos la carta id mas alta
			strcpy(consulta,"SELECT MAX(jugadores.id) FROM jugadores;");
			
			// hacemos la consulta 
			err=mysql_query (conn, consulta); 
			if (err!=0) {
				printf ("Error al consultar datos de la base %u %s\n",
						mysql_errno(conn), mysql_error(conn));
				exit (1);
			}
			//recogemos el resultado de la consulta 
			
			resultado = mysql_store_result (conn); 
			row = mysql_fetch_row (resultado);
			if (row == NULL)
			{
				terminar=1;
				printf ("No se han obtenido datos en la consulta\n");
				
			}
			else{
				// sumamos +1 al numero de id que nos ha dado, que sera el siguiente
				id = 1 + atoi(row[0]);
				printf("su id sera:%d \n", id);
				sprintf(newid,"%d",id);
				strcpy(consulta,"INSERT INTO jugadores VALUES(");
				strcat(consulta, newid);
				strcat(consulta, ",'");
				strcat(consulta, nombre);
				strcat(consulta, "','");
				strcat(consulta, nombre2);
				strcat(consulta, "',0,0);");
				printf("insert hecho\n");
				if (row == NULL)
				{
					terminar=1;
					printf ("No se han obtenido datos en la consulta\n");
					
				}
				resultado = mysql_query (conn,consulta);
				
			}
			
			// cerrar la conexion con el servidor MYSQL 
			mysql_close (conn);
			
			// Y lo enviamos
			// Se acabo el servicio para este cliente
		}
		if (codigo != 0)
		{
			printf("La repuesta a tu pregunta es %s\n",respuesta);
			
			//y lo enviamos
			write (sock_conn,respuesta,strlen(respuesta));
		}
	}
	//y acabamos el servicio con el cliente
	close(sock_conn);
} 

int main (int argc, char *argv[])
{
	int sock_conn, sock_listen;
	struct sockaddr_in serv_adr;
	
	// INICIALITZACIONS
	// Obrim el socket
	if ((sock_listen = socket(AF_INET, SOCK_STREAM, 0)) < 0)
		printf("Error creant socket");
	// Fem el bind al port
	
	
	memset(&serv_adr, 0, sizeof(serv_adr));// inicialitza a zero serv_addr
	serv_adr.sin_family = AF_INET;
	
	// asocia el socket a cualquiera de las IP de la m?quina. 
	//htonl formatea el numero que recibe al formato necesario
	serv_adr.sin_addr.s_addr = htonl(INADDR_ANY);
	// establecemos el puerto de escucha
	serv_adr.sin_port = htons(9050);
	if (bind(sock_listen, (struct sockaddr *) &serv_adr, sizeof(serv_adr)) < 0)
		printf ("Error al bind");
	
	if (listen(sock_listen, 3) < 0)
		printf("Error en el Listen");
	
	int i;
	int sockets[100];
	pthread_t thread;
	i=0;
	
	//bucle infinito
	int contador;
	for (;;){
		printf ("Escuchando..\n");
		
		sock_conn = accept(sock_listen, NULL, NULL);
		printf ("He recibido conexion\n");
		
		sockets[i] =sock_conn;
		//sock_conn es el socket que usaremos para este cliente
		
		// Crear thead y decirle lo que tiene que hacer
		
		pthread_create (&thread, NULL, AtenderCliente,&sockets[i]);
		i=i+1;
	}
	
	
}
