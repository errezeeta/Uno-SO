#include <string.h>
#include <unistd.h>
#include <stdlib.h>
#include <sys/types.h>
#include <sys/socket.h>
#include <netinet/in.h>
#include <stdio.h>
#include <mysql.h>
#include <pthread.h>


int contador;
char texto[150];
//estructura necesaria para acceso excluyente
// variable mutex,
pthread_mutex_t mutex= PTHREAD_MUTEX_INITIALIZER;//se inicializa de manera global para que todos los threads puedan
//acceder a el
int i;
int sockets[100];

typedef struct 
{
	char Nombre[20];//Nombre del conectado
	int socket;
	int idBBDDCon;
}Conectado;

typedef struct 
{
	Conectado conectados [100];//Lista de conectados
	int num;// numero de conectados
}ListaConectados;

ListaConectados miLista;

typedef struct 
{
	char player_invitado[30];
	int socket_invitado;
	int idBBDDInv;
	int nForm;
	int SiONO;
}Invitado;

typedef struct 
{
	int numero;
	int color;
	
}Carta;

typedef struct 
{
	Invitado invitados [4];
	int numero_invitados;
	int respuesta_invitados;
	Carta cartas[40];
	int num_cartas;
	int contadorRobar;
	int Tiradas;
	int idBBDD;
	
	int num_movimientos;
	
}Partida;	

typedef struct 
{
	Partida partidas[20];
	int num;
}ListaPartida;

ListaPartida miListaPartdia;




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
	//int socketCorrecto;
	int terminar =0;
	int jugadores_invitados;
	int j;
	while (terminar ==0)
	{
		printf("a principio de atender cliente %d\n",sock_conn);
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
		conn = mysql_real_connect (conn, "shiva2.upc.es","root", "mysql", "unoBD2",0, NULL, 0);
		if (conn==NULL) {
			printf ("Error al inicializar la conexi??n: %u %s\n", 
					mysql_errno(conn), mysql_error(conn));
			exit (1);
		}
		// consulta SQL para obtener una tabla con todos los datos
		// de la base de datos
		printf("hola\n");
		
		
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
		int SIoNO;
		int nForm;
		int idPartida;
		
		printf("hola\n");
		if (codigo==11)
		{
			p= strtok (NULL,"/");
			nForm = atoi (p);
			printf("nForm:%d\n",nForm);
			
			p= strtok (NULL,"/");
			idPartida = atoi (p);
			printf("idPartida:%d\n",idPartida);
			
		}
		if ((codigo !=0))
		{
			p = strtok (NULL,"/");
			strcpy (nombre, p);
		}
		
		if(codigo ==0){
			
			p = strtok (NULL,"/");
			strcpy (nombre, p);
			//miLista.num=0;
			terminar=1;	
			int res=Eliminar(&miLista,nombre);
			
			if (res==-1)
				printf("NO esta.\n");
			else
				printf("Eliminado.\n");
		}
		
		if (codigo == 1) {//Jugadores cruzados
			//Vamos a necesitar hacer una consulta: SELECT relacion.idPartida FROM relacion WHERE relacion.idJugador IN (SELECT id FROM jugadores WHERE nickname ='errezeeta');
			//Y la otra SELECT jugadores.nickname FROM jugadores,relacion WHERE relacion.idPartida = (LA PRIMERA) AND jugadores.id =relacion.idJugador;
			printf ("codigo:%d\n",codigo);
			printf("%s\n",nombre);
			printf("1\n");
			strcpy (consulta,"SELECT relacion.idPartida FROM relacion WHERE relacion.idJugador IN (SELECT id FROM jugadores WHERE nickname ='");
			printf("1\n");
			strcat (consulta, nombre);
			printf("1\n");
			strcat(consulta,"');");
			printf("1\n");
			printf("Consulta:%s\n",consulta);
			printf("1\n");
			// hacemos la consulta 
			err=mysql_query (conn, consulta);
			printf("2\n");
			if (err!=0) {
				printf ("Error al consultar datos de la base %u %s\n",
						mysql_errno(conn), mysql_error(conn));
				exit (1);
			}
			//recogemos el resultado de la consulta 
			printf("2\n");
			resultado = mysql_store_result (conn); 
			printf("2\n");
			row = mysql_fetch_row (resultado);
			printf("2\n");
			if (row == NULL)
				printf ("No se han obtenido datos en la consulta\n");
			else
			{
				resu= atoi(row[0]);
			}
			printf ("El id de partidas a buscar es %s\n",row[0]);
			
			//ahora la segunda parte
			strcpy(consulta, "SELECT jugadores.nickname FROM jugadores,relacion WHERE relacion.idPartida = ");
			printf("Llevo esto %s\n",consulta);
			strcat(consulta,row[0]);
			printf("Llevo esto %s\n",consulta);
			strcat(consulta, " AND jugadores.id =relacion.idJugador;");
			printf("Llevo esto %s\n",consulta);
			printf("Voy a hacer la consulta %s\n",consulta);
			// hacemos la consulta 
			err=mysql_query (conn, consulta); 
			if (err!=0) {
				printf ("Error al consultar datos de la base %u %s\n",
						mysql_errno(conn), mysql_error(conn));
				exit (1);
			}
			//recogemos el resultado de la consulta 
			printf("3\n");
			resultado = mysql_store_result (conn); 
			printf("3\n");
			row = mysql_fetch_row (resultado);
			printf("3\n");
			if (row == NULL)
			{
				printf ("No se han obtenido datos en la consulta\n");
			}
			//Vamos a construir la sentencia
			char nombres[200];
			strcpy(nombres,"1/");
			strcat(nombres, row[0]);
			strcat(nombres,",");
			printf("Llevo %s\n",nombres);
			int completado= 0;
			row = mysql_fetch_row (resultado);
			int filas= mysql_num_rows(resultado);
			printf("filas %d",filas);
			int j=1;
			while (j<filas && completado==0)
			{
				strcat(nombres,row[0]);
				strcat(nombres,",");
				printf("Llevo %s\n",nombres);
				printf("5\n");
				row = mysql_fetch_row (resultado);
				j++;
			}
			printf("4\n");
			nombres[strlen(nombres)-1]='\0';
			printf("4\n");
			printf("He creado esto: %s", nombres);
			write (sock_conn,nombres,strlen(nombres));
			
		}
		
		if (codigo ==2){//
			//Cortamos el nombre del rival a buscar:
			char rival[20];
			p = strtok (NULL,"/");
			strcpy (rival, p);
			printf("Rival: %s\n", rival);
			//Creamos la consulta
			strcpy(consulta, "SELECT partidas.ganador FROM partidas WHERE partidas.id IN (SELECT relacion.idPartida FROM relacion WHERE relacion.idJugador IN (SELECT id FROM jugadores WHERE nickname ='");
			strcat(consulta, nombre);
			strcat(consulta, "')) AND partidas.id IN (SELECT relacion.idPartida FROM relacion WHERE relacion.idJugador IN (SELECT id FROM jugadores WHERE nickname ='");
			strcat(consulta, rival);
			strcat(consulta, "'));");
			printf("Consulta: %s\n", consulta);
			err=mysql_query (conn, consulta); 
			if (err!=0) {
				printf ("Error al consultar datos de la base %u %s\n",
						mysql_errno(conn), mysql_error(conn));
				exit (1);
			}
			//recogemos el resultado de la consulta 
			printf("8\n");
			resultado = mysql_store_result (conn); 
			printf("8\n");
			row = mysql_fetch_row (resultado);
			printf("8\n");
			if (row == NULL)
			{
				printf ("No se han obtenido datos en la consulta\n");
				strcpy(respuesta,"?");
			}
			else
			{
				int filas= mysql_num_rows(resultado);
				printf("filas vale: %d\n",filas);
				strcpy(respuesta, "2/");
				int j=0;
				while (j<filas)
				{
					strcat(respuesta, row[0]);
					strcat(respuesta, ",");
					printf("Llevo %s\n",respuesta);
					printf("La proxima sera %s\n", row[0]);
					row = mysql_fetch_row (resultado);
					printf("8\n");
					j++;
				}
				respuesta[strlen(respuesta)-1]='\0';
				printf("He creado esto %s\n",respuesta);
				
			}
			
			mysql_close (conn);
			printf("Respondo esto: %s\n",respuesta);
			write (sock_conn,respuesta,strlen(respuesta));
		}
		
		if (codigo == 3){//dime si soy alto?
			
			// volvemos a cortar y cogemos el nuemreo decimal y lo guardamos en la variable altur
			strcpy(consulta,"SELECT * FROM partidas WHERE partidas.id IN (SELECT relacion.idPartida FROM relacion WHERE relacion.idJugador IN (SELECT id FROM jugadores WHERE nickname ='");
			strcat(consulta, nombre);
			strcat(consulta, "'));");
			printf("Hare esta consulta: %s", consulta);
			// hacemos la consulta 
			err=mysql_query (conn, consulta); 
			printf("9\n");
			if (err!=0) {
				printf ("Error al consultar datos de la base %u %s\n",
						mysql_errno(conn), mysql_error(conn));
				exit (1);
			}
			printf("9\n");
			//recogemos el resultado de la consulta 
			resultado = mysql_store_result (conn); 
			printf("9\n");
			int filas= mysql_num_rows(resultado);
			printf("9\n");
			int j=0;
			char tabla[100];
			printf("9\n");
			strcpy(tabla,"3/");
			if (row == NULL)
				printf ("No se han obtenido datos en la consulta\n");
			else
			{
				//Guardamos 
				while(j<filas)
				{
					printf("9\n");
					row = mysql_fetch_row (resultado);
					printf("9\n");
					strcat(tabla,row[0]);
					strcat(tabla,",");
					strcat(tabla,row[1]);
					strcat(tabla,",");
					strcat(tabla,row[2]);
					strcat(tabla,",");
					strcat(tabla,row[3]);
					strcat(tabla,",");
					strcat(tabla,row[4]);
					strcat(tabla,",");
					strcat(tabla,row[5]);
					strcat(tabla,",");
					strcat(tabla,row[6]);
					strcat(tabla,".");
					j++;
				}
			}
			printf("Respondo esto: %s\n",tabla);
			write (sock_conn,tabla,strlen(tabla));
			// Y lo enviamos
			
			
			// Se acabo el servicio para este cliente
			
		}
		int res;
		if (codigo == 4){//login
			
			printf(" antes de ponerlos son: %s,%d\n",nombre,sock_conn);
			p = strtok (NULL,"/");
			strcpy (nombre2, p);
			//miLista.num=0;
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
				
				pthread_mutex_lock(&mutex);
				res= Pon(&miLista,nombre,sock_conn);
				pthread_mutex_unlock(&mutex);
				
				printf("socket valero %s,%d\n",nombre,sock_conn);
				
				int mipropiosocket;
				mipropiosocket= Socketfromnombre(&miLista,nombre);
				printf ("me voy a enviar mis socket que es el %d\n",mipropiosocket);		
				
				if (res==-1)
					printf("La lista esta llana\n");
				else {
					
					printf("¡Añadido bien!\n");
					codigo=8;
				}
				
				
				resu=strcmp(nombre2,row[0]);
				
				sprintf(respuesta,"4/%d,%d",mipropiosocket,resu);
				
				printf ("yo devuelvo\n",row[0]);
			}
			
			// cerrar la conexion con el servidor MYSQL 
			mysql_close (conn);
			printf("Respondo esto: %d\n",resu);
			printf ("respuesta:%s \n",respuesta);
			write (sock_conn,respuesta,strlen(respuesta));
			
			
			// Y lo enviamos
			// Se acabo el servicio para este cliente
		}
		
		if (codigo == 5){//crear usuario
			
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
			printf("Respondo esto: %s\n",respuesta);
			write (sock_conn,respuesta,strlen(respuesta));
			// Y lo enviamos
			// Se acabo el servicio para este cliente
		}
		
		if (codigo==6){
			char chat[100];
			int j;
			//sprintf(texto,"6/%s",nombre);
			printf("persona que escribe: %s\n",nombre);
			
			p = strtok (NULL,"/");
			char texto [30];
			strcpy (texto, p);
			
			sprintf(chat,"6/%s%s",nombre,texto);
			
			for(j=0;j<i; j++){
				write (sockets[j],chat,strlen(chat));
			}
			
			
			
		}
		
		if (codigo==8){
			
			int j;
			char enviados[400];
			char misConectados[300];
			
			pthread_mutex_lock(&mutex);
			DameConectados(&miLista,misConectados);
			pthread_mutex_unlock(&mutex);
			
			printf("Resultado: %s\n",misConectados);
			sprintf(enviados,"8/%s",misConectados);
			
			printf("%d\n",sock_conn);
			for (j=0;j<i; j++){
				printf("broadcast %d\n",sockets[j]);
				write (sockets[j],enviados,strlen(enviados));
			}
			
		}
		
		if (codigo==9){
			
			
			
			p = strtok (NULL,"/");
			char nombreinvitado[30];
			strcpy (nombreinvitado, p);
			//printf("Este es el nombre del invitado\n",nombreinvitado);
			
			p =strtok(NULL,"/");
			int socket_creador = atoi (p);
			
			p =strtok(NULL,"/");
			int nForm = atoi (p);
			
			//inserto el nombre y el socket del creador de la partida
			//pthread_mutex_lock(&mutex);
			//creador(&miListaPartdia,nombre,socket_creador);
			//pthread_mutex_unlock(&mutex);
			int h=0;
			pthread_mutex_lock(&mutex);
			pon_invitados(&miListaPartdia,nombre,socket_creador,h,nForm);
			pthread_mutex_unlock(&mutex);
			miListaPartdia.partidas[miListaPartdia.num].respuesta_invitados=0;
			
			
			p =strtok(nombreinvitado,",");
			int numerodeinvitados = atoi (p);
			printf("numerodeinvitados: %d\n",numerodeinvitados);
			
			
			int Socketdelinvitado;
			int i=1;
			while (i<numerodeinvitados+1)
			{
				p =strtok(NULL,",");
				char invit[20];
				strcpy (invit, p);
				
				// Cogo el socket del invitado
				pthread_mutex_lock(&mutex);
				Socketdelinvitado = Socketfromnombre(&miLista,invit);
				pthread_mutex_unlock(&mutex);
				
				//lo meto en la esttructura de invitados
				pthread_mutex_lock(&mutex);
				pon_invitados_normales(&miListaPartdia,invit,Socketdelinvitado,i);
				pthread_mutex_unlock(&mutex);
				
				//busco su posicion
				//pthread_mutex_lock(&mutex);
				//int posicioninv=DamePos_invitado(&miListaPartdia,invit);
				//pthread_mutex_unlock(&mutex);
				
				
				//printf("Este es el socket del invitado: %s\n",Socketdelinvitado);
				
				if (Socketdelinvitado==-1)
					printf("lo siento pero no se ha encontraedo\n");
				else 
				{
					printf("todo correcto\n");
					sprintf(respuesta,"9/%s",nombre);
					printf("voy a enviar %s\n",respuesta);
					write (Socketdelinvitado,respuesta,strlen(respuesta));
					printf("todo enviado\n");
					printf("socket del invitado %d \n", Socketdelinvitado);
					
				}
				i++;
			}
		}
		if (codigo==10)
		{
			p = strtok (NULL,"/");
			char invitador[30];
			strcpy (invitador, p);	
			printf("invitador :%s\n",invitador);
			p= strtok (NULL,"/");
			int numero = atoi (p);
			printf("numero final :%d\n",numero);
			//strcpy (SIoNO, p);
			
			p= strtok (NULL,"/");
			int socket_invitado = atoi (p);
			
			
			
			
			//int Respuesta2;
			//	pthread_mutex_lock(&mutex);
			//Respuesta2 = infopartidas2(&miListaPartdia,nombre);
			//pthread_mutex_unlock(&mutex);
			
			if (numero==1)
			{
				//buscamos la posicion del invitado en concreto
				p =strtok(NULL,"/");
				int nForm = atoi (p);
				
				pthread_mutex_lock(&mutex);
				int Damepos_invitado=DamePos_invitado(&miListaPartdia,nombre);
				pthread_mutex_unlock(&mutex);
				miListaPartdia.partidas[miListaPartdia.num].respuesta_invitados++;
				
				
				printf("nFrom: %d\n",nForm);
				
				//guardamos la respuesta 
				pthread_mutex_lock(&mutex);
				int respuesta_invitados=repuesta_invitados(&miListaPartdia,numero,Damepos_invitado,nForm);
				pthread_mutex_unlock(&mutex);
				printf("respueta invitados: %d\n",respuesta_invitados);
				printf("numero de invitados: %d\n",miListaPartdia.partidas[miListaPartdia.num].numero_invitados);
				//int nuevo_numero=miListaPartdia.partidas[0].numero_invitados -1;
				//printf("nuevo numeor de invitados: %d\n",nuevo_numero);
				int socketCorrecto;
				int id;
				char newid[30];
				if (miListaPartdia.partidas[miListaPartdia.num].respuesta_invitados== miListaPartdia.partidas[miListaPartdia.num].numero_invitados-1)
				{
					printf("Entro si\n");
					int S=0;
					int num_de_partida=0;
					char nombres[100];
					if(miListaPartdia.partidas[miListaPartdia.num].numero_invitados==2)
					{
						strcpy(nombres, miListaPartdia.partidas[miListaPartdia.num].invitados[0].player_invitado);
						strcat(nombres,"<");
						strcat(nombres, miListaPartdia.partidas[miListaPartdia.num].invitados[1].player_invitado);
					}
					if(miListaPartdia.partidas[miListaPartdia.num].numero_invitados==3)
					{
						strcpy(nombres, miListaPartdia.partidas[miListaPartdia.num].invitados[0].player_invitado);
						strcat(nombres,"<");
						strcat(nombres, miListaPartdia.partidas[miListaPartdia.num].invitados[1].player_invitado);
						strcat(nombres,"<");
						strcat(nombres, miListaPartdia.partidas[miListaPartdia.num].invitados[2].player_invitado);;
					}
					if(miListaPartdia.partidas[miListaPartdia.num].numero_invitados==4)
					{
						strcpy(nombres, miListaPartdia.partidas[miListaPartdia.num].invitados[0].player_invitado);
						strcat(nombres,"<");
						strcat(nombres, miListaPartdia.partidas[miListaPartdia.num].invitados[1].player_invitado);
						strcat(nombres,"<");
						strcat(nombres, miListaPartdia.partidas[miListaPartdia.num].invitados[2].player_invitado);
						strcat(nombres,"<");
						strcat(nombres, miListaPartdia.partidas[miListaPartdia.num].invitados[3].player_invitado);
					}
					printf("LA LISTA DE NOMBRES ES %s\n",nombres);
					while (S<miListaPartdia.partidas[miListaPartdia.num].numero_invitados){
						// tenemos que enviar a todos los de la partida que se puede jugar
						printf("socket:%d\n",miListaPartdia.partidas[miListaPartdia.num].invitados[S].socket_invitado);
						
						sprintf(respuesta,"10/SI,%d,%d,%d,%s",miListaPartdia.num,S,miListaPartdia.partidas[miListaPartdia.num].numero_invitados,nombres);
						printf(" respuesta: %s\n",respuesta);
						write (miListaPartdia.partidas[miListaPartdia.num].invitados[S].socket_invitado,respuesta,strlen(respuesta));
						printf("Enviado\n");
						S++;
					}
					
					pthread_mutex_lock(&mutex);
					Crea_baraja(&miListaPartdia,miListaPartdia.num);
					pthread_mutex_unlock(&mutex);
					//Añadiendo la partida a la Base de Datos
					resultado = mysql_store_result (conn);
					
					// buscamos la carta id mas alta
					strcpy(consulta,"SELECT MAX(partidas.id) FROM partidas;");
					
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
						miListaPartdia.partidas[miListaPartdia.num].idBBDD=id;
						printf("su id sera:%d \n", id);
						sprintf(newid,"%d",id);
						strcpy(consulta,"INSERT INTO partidas VALUES(");
						strcat(consulta, newid);
						strcat(consulta, ",'-',0,'");
						strcat(consulta, miListaPartdia.partidas[miListaPartdia.num].invitados[0].player_invitado);
						strcat(consulta, "','");
						strcat(consulta, miListaPartdia.partidas[miListaPartdia.num].invitados[1].player_invitado);
						strcat(consulta, "','");
						printf("El num de invitados es %d y la consulta lleva %s",miListaPartdia.partidas[miListaPartdia.num].numero_invitados,consulta);
						if (miListaPartdia.partidas[miListaPartdia.num].numero_invitados==2)
						{
							strcat(consulta,"-','-');");
							printf("%s",consulta);
						}
						else if(miListaPartdia.partidas[miListaPartdia.num].numero_invitados==3)
						{
							strcat(consulta, miListaPartdia.partidas[miListaPartdia.num].invitados[2].player_invitado);
							strcat(consulta, "','-');");
							printf("%s",consulta);
						}
						else if(miListaPartdia.partidas[miListaPartdia.num].numero_invitados==4)
						{
							strcat(consulta, miListaPartdia.partidas[miListaPartdia.num].invitados[2].player_invitado);
							strcat(consulta, "','");
							strcat(consulta, miListaPartdia.partidas[miListaPartdia.num].invitados[3].player_invitado);
							strcat(consulta, "');");
							printf("%s",consulta);
						}
						printf("insert hecho\n");
						if (row == NULL)
						{
							terminar=1;
							printf ("No se han obtenido datos en la consulta\n");
							
						}
						resultado = mysql_query (conn,consulta);
						
					}
					
					//Añadiendo las relaciones a la Base de Datos
					int j=0;
					int idJugador=0;
					while (j<miListaPartdia.partidas[miListaPartdia.num].numero_invitados)
					{
						//Saco id del jugador.
						strcpy(consulta,"SELECT id FROM jugadores WHERE nickname ='");
						strcat(consulta, miListaPartdia.partidas[miListaPartdia.num].invitados[j].player_invitado);
						strcat(consulta, "';");
						printf("Voy a pedir el id asi: %s\n", consulta);
						err=mysql_query (conn, consulta); 
						printf("Query hecho\n");
						if (err!=0) {
							printf ("Error al consultar datos de la base %u %s\n",
									mysql_errno(conn), mysql_error(conn));
							exit (1);
						} 
						resultado = mysql_store_result (conn); 
						row = mysql_fetch_row (resultado);
						printf("Resultado guardado (id)\n");
						if (row == NULL)
							printf ("No se han obtenido datos en la consulta\n");
						else
						{
							// El resultado debe ser una matriz con una sola fila
							// y una columna que contiene el nombre
							idJugador= atoi(row[0]);
						}
						printf("mi id es %d\n", idJugador);
						//Creo y meto la relacion
						printf("1\n");
						strcpy(consulta,"INSERT INTO relacion VALUES (");
						printf("1\n");
						char idAbuscar[10];
						sprintf(idAbuscar,"%d",idJugador);
						strcat(consulta, idAbuscar);
						printf("1\n");
						strcat(consulta,",");
						printf("1\n");
						strcat(consulta, newid);
						printf("1\n");
						strcat(consulta, ");");
						printf("1\n");
						printf("%s\n",consulta);
						printf("1\n");
						err=mysql_query (conn, consulta); 
						printf("1\n");
						printf("query hecho\n");
						printf("1\n");
						j++;
					}
					
					miListaPartdia.num++;
					
				}
			}
			
			if (numero==0) {
				printf("Entro no\n");
				int N=0;
				int num_de_partida=0;
				while (N<miListaPartdia.partidas[0].numero_invitados){
					printf("socket:%d\n",miListaPartdia.partidas[0].invitados[N].socket_invitado);
					
					sprintf(respuesta,"10/NO",nombre);
					printf(" respuesta: %s\n",respuesta);
					write (miListaPartdia.partidas[miListaPartdia.num].invitados[N].socket_invitado,respuesta,strlen(respuesta));
					printf("Enviado\n");	
					
					N++;
				}
				miListaPartdia.partidas[miListaPartdia.num].Tiradas=0;
				miListaPartdia.num++;
			}
			
		}
		if (codigo==11)
		{
			p = strtok (NULL,"/");
			char Texto[30];
			strcpy (Texto, p);
			pthread_mutex_lock(&mutex);
			int Damepos_invitadoForm=Damepos_invitado_partida(&miListaPartdia,nombre,idPartida);
			pthread_mutex_unlock(&mutex);
			//int nFormBueno=miListaPartdia.partidas[idPartida].invitados[Damepos_invitadoForm].nForm;
			char chat[50];
			
			int S=0;
			while (S<miListaPartdia.partidas[idPartida].numero_invitados){
				
				int nFormbueno=miListaPartdia.partidas[idPartida].invitados[S].nForm;
				sprintf(chat,"11/%d/%s%s",nFormbueno,nombre,Texto);
				write (miListaPartdia.partidas[idPartida].invitados[S].socket_invitado,chat,strlen(chat));
				S++;
			}
		}
		if (codigo==12){
			int cont;
			//cogemos la idPartida
			p= strtok (NULL,"/");
			idPartida = atoi (p);
			//Saco numero de invitados
			p= strtok (NULL,"/");
			int numerodeinvitados = atoi (p);
			//Saco posicion
			p= strtok (NULL,"/");
			int posicion = atoi (p);
			
			
			
			//if (miListaPartdia.partidas[idPartida].num_movimientos==0)
			if (numerodeinvitados==1)
			{ 
				char respuesta[30];
				char respfinal[40];
				int i=0;
				while (i<5)
				{
					sprintf(respuesta,"%s%d,",respuesta,miListaPartdia.partidas[idPartida].cartas[i].numero);
					i++;
					printf("%s\n",respuesta);
					
				}
				sprintf(respfinal,"12/%d/%d/%s",miListaPartdia.partidas[idPartida].invitados[0].nForm,miListaPartdia.partidas[idPartida].cartas[36].numero,respuesta);
				
				respfinal[strlen(respfinal)-1]='\0';//quitamos la ultima coma
				
				write(miListaPartdia.partidas[idPartida].invitados[0].socket_invitado,respfinal,strlen(respfinal));
				printf("%s\n",respfinal);
				
				i =0;
				char respuesta1[30];
				char respfinal1[40];
				while (i<5)
				{
					sprintf(respuesta1,"%s%d,",respuesta1,miListaPartdia.partidas[idPartida].cartas[5+i].numero);
					i++;
					printf("%s\n",respuesta1);
					
				}
				sprintf(respfinal1,"12/%d/%d/%s",miListaPartdia.partidas[idPartida].invitados[1].nForm,miListaPartdia.partidas[idPartida].cartas[36].numero,respuesta1);
				
				respfinal1[strlen(respfinal1)-1]='\0';//quitamos la ultima coma
				
				write(miListaPartdia.partidas[idPartida].invitados[1].socket_invitado,respfinal1,strlen(respfinal1));
				printf("%s\n",respfinal1);
				miListaPartdia.partidas[idPartida].contadorRobar=10;
			}
			if (numerodeinvitados==2)
			{ 
				char respuesta[30];
				char respfinal[40];
				int i=0;
				while (i<5)
				{
					sprintf(respuesta,"%s%d,",respuesta,miListaPartdia.partidas[idPartida].cartas[i].numero);
					i++;
					
				}
				sprintf(respfinal,"12/%d/%d/%s",miListaPartdia.partidas[idPartida].invitados[0].nForm,miListaPartdia.partidas[idPartida].cartas[36].numero,respuesta);
				respfinal[strlen(respfinal)-1]='\0';
				write(miListaPartdia.partidas[idPartida].invitados[0].socket_invitado,respfinal,strlen(respfinal));
				i=0;
				printf("%s\n",respfinal);
				
				
				char respuesta1[30];
				char respfinal1[40];		
				while (i<5)
				{
					sprintf(respuesta1,"%s%d,",respuesta1,miListaPartdia.partidas[idPartida].cartas[5+i].numero);
					i++;
					
				}
				sprintf(respfinal1,"12/%d/%d/%s",miListaPartdia.partidas[idPartida].invitados[1].nForm,miListaPartdia.partidas[idPartida].cartas[36].numero,respuesta1);
				respfinal1[strlen(respfinal1)-1]='\0';
				write(miListaPartdia.partidas[idPartida].invitados[1].socket_invitado,respfinal1,strlen(respfinal1));
				i=0;
				printf("%s",respfinal1);
				
				char respuesta2[30];
				char respfinal2[40];
				while (i<5)
				{
					sprintf(respuesta2,"%s%d,",respuesta2,miListaPartdia.partidas[idPartida].cartas[10+i].numero);
					i++;
					
				}
				sprintf(respfinal2,"12/%d/%d/%s",miListaPartdia.partidas[idPartida].invitados[2].nForm,miListaPartdia.partidas[idPartida].cartas[36].numero,respuesta2);
				respfinal2[strlen(respfinal2)-1]='\0';
				write(miListaPartdia.partidas[idPartida].invitados[2].socket_invitado,respfinal2,strlen(respfinal2));
				printf("%s\n",respfinal2);
				miListaPartdia.partidas[idPartida].contadorRobar=15;
			}
			if (numerodeinvitados==3)
			{ 
				char respuesta[30];
				char respfinal[40];
				int i=0;
				while (i<5)
				{
					sprintf(respuesta,"%s%d,",respuesta,miListaPartdia.partidas[idPartida].cartas[i].numero);
					i++;
					
				}
				sprintf(respfinal,"12/%d/%d/%s",miListaPartdia.partidas[idPartida].invitados[0].nForm,miListaPartdia.partidas[idPartida].cartas[36].numero,respuesta);
				respfinal[strlen(respfinal)-1]='\0';
				write(miListaPartdia.partidas[idPartida].invitados[0].socket_invitado,respfinal,strlen(respfinal));
				i=0;
				printf("%s\n",respfinal);
				
				
				char respuesta1[30];
				char respfinal1[40];
				while (i<5)
				{
					sprintf(respuesta1,"%s%d,",respuesta1,miListaPartdia.partidas[idPartida].cartas[5+i].numero);
					i++;
					
				}
				sprintf(respfinal1,"12/%d/&d/%s",miListaPartdia.partidas[idPartida].invitados[1].nForm,miListaPartdia.partidas[idPartida].cartas[36].numero,respuesta1);
				respfinal1[strlen(respfinal1)-1]='\0';
				write(miListaPartdia.partidas[idPartida].invitados[1].socket_invitado,respfinal1,strlen(respfinal1));
				i=0;
				printf("%s\n",respfinal1);
				
				
				char respuesta2[30];
				char respfinal2[40];
				while (i<5)
				{
					sprintf(respuesta2,"%s%d,",respuesta2,miListaPartdia.partidas[idPartida].cartas[10+i].numero);
					i++;
					
				}
				sprintf(respfinal2,"12/%d/%d/%s",miListaPartdia.partidas[idPartida].invitados[2].nForm,miListaPartdia.partidas[idPartida].cartas[36].numero,respuesta2);
				respfinal2[strlen(respfinal2)-1]='\0';
				write(miListaPartdia.partidas[idPartida].invitados[2].socket_invitado,respfinal2,strlen(respfinal2));
				i=0;
				printf("%s",respfinal2);
				
				
				char respuesta3[30];
				char respfinal3[40];
				while (i<5)
				{
					sprintf(respuesta3,"%s%d,",respuesta3,miListaPartdia.partidas[idPartida].cartas[15+i].numero);
					i++;
					
				}
				sprintf(respfinal3,"12/%d/%d/%s",miListaPartdia.partidas[idPartida].invitados[3].nForm,miListaPartdia.partidas[idPartida].cartas[36].numero,respuesta3);
				respfinal3[strlen(respfinal3)-1]='\0';
				write(miListaPartdia.partidas[idPartida].invitados[3].socket_invitado,respfinal3,strlen(respfinal3));
				printf("%s\n",respfinal3);
				miListaPartdia.partidas[idPartida].contadorRobar=20;
			}
			//miListaPartdia.partidas[idPartida].num_movimientos++;
			
		}	
		
		if (codigo==13){
			
			int cont;
			//cogemos la idPartida
			p= strtok (NULL,"/");
			idPartida = atoi (p);
			//Saco numero de invitados
			p= strtok (NULL,"/");
			int numerodeinvitados = atoi (p);
			//Saco posicion
			p= strtok (NULL,"/");
			int posicion = atoi (p);
			//Saco el movimiento
			p= strtok (NULL,"/");
			int movimiento = atoi (p);
			//Saco el ganador
			p=strtok (NULL,"/");
			int ganador= atoi(p);
			
			if (ganador==0)
			{
				if (numerodeinvitados== 1)
				{
					if(posicion==0)
					{
						sprintf(respuesta,"13/%d", miListaPartdia.partidas[idPartida].invitados[1].nForm);
						write(miListaPartdia.partidas[idPartida].invitados[1].socket_invitado,respuesta,strlen(respuesta));
					}
					if(posicion==1)
					{
						sprintf(respuesta,"13/%d", miListaPartdia.partidas[idPartida].invitados[0].nForm);
						write(miListaPartdia.partidas[idPartida].invitados[0].socket_invitado,respuesta,strlen(respuesta));
					}
					miListaPartdia.partidas[idPartida].num_movimientos++;
				}
				if (numerodeinvitados== 2)
				{
					if(posicion==0)
					{
						sprintf(respuesta,"13/%d", miListaPartdia.partidas[idPartida].invitados[1].nForm);
						write(miListaPartdia.partidas[idPartida].invitados[1].socket_invitado,respuesta,strlen(respuesta));
					}
					if(posicion==1)
					{
						sprintf(respuesta,"13/%d", miListaPartdia.partidas[idPartida].invitados[2].nForm);
						write(miListaPartdia.partidas[idPartida].invitados[2].socket_invitado,respuesta,strlen(respuesta));
					}
					if(posicion==2)
					{
						sprintf(respuesta,"13/%d", miListaPartdia.partidas[idPartida].invitados[0].nForm);
						write(miListaPartdia.partidas[idPartida].invitados[0].socket_invitado,respuesta,strlen(respuesta));
					}
					miListaPartdia.partidas[idPartida].num_movimientos++;
				}
				if (numerodeinvitados== 3)
				{
					if(posicion==0)
					{
						sprintf(respuesta,"13/%d", miListaPartdia.partidas[idPartida].invitados[1].nForm);
						write(miListaPartdia.partidas[idPartida].invitados[1].socket_invitado,respuesta,strlen(respuesta));
					}
					if(posicion==1)
					{
						sprintf(respuesta,"13/%d", miListaPartdia.partidas[idPartida].invitados[2].nForm);
						write(miListaPartdia.partidas[idPartida].invitados[2].socket_invitado,respuesta,strlen(respuesta));
					}
					if(posicion==2)
					{
						sprintf(respuesta,"13/%d", miListaPartdia.partidas[idPartida].invitados[3].nForm);
						write(miListaPartdia.partidas[idPartida].invitados[3].socket_invitado,respuesta,strlen(respuesta));
					}
					if(posicion==3)
					{
						sprintf(respuesta,"13/%d", miListaPartdia.partidas[idPartida].invitados[0].nForm);
						write(miListaPartdia.partidas[idPartida].invitados[0].socket_invitado,respuesta,strlen(respuesta));
					}
					miListaPartdia.partidas[idPartida].num_movimientos++;
					
				}
				char respuesta1[100];
				printf("%s\n",respuesta);
				for(int i=0; i<numerodeinvitados+1; i++)
				{
					sprintf(respuesta1,"14/%d/%d",miListaPartdia.partidas[idPartida].invitados[i].nForm, movimiento);
					write(miListaPartdia.partidas[idPartida].invitados[i].socket_invitado,respuesta1,strlen(respuesta1));
					printf("%s al socket %d\n", respuesta1,miListaPartdia.partidas[idPartida].invitados[i].socket_invitado);
				}
				miListaPartdia.partidas[idPartida].Tiradas++;
			}
			if (ganador ==1)
			{
				char respuesta1[100];
				printf("%s\n",respuesta);
				for(int i=0; i<numerodeinvitados+1; i++)
				{
					sprintf(respuesta1,"16/%d/%s",miListaPartdia.partidas[idPartida].invitados[i].nForm,nombre);
					write(miListaPartdia.partidas[idPartida].invitados[i].socket_invitado,respuesta1,strlen(respuesta1));
					printf("%s al socket %d\n", respuesta1,miListaPartdia.partidas[idPartida].invitados[i].socket_invitado);
				}
				miListaPartdia.partidas[idPartida].Tiradas++;
/*				Guardando datos en BBDD*/
/*				ganador de la partida*/
				resultado = mysql_store_result (conn);
				/*row = mysql_fetch_row (resultado);*/
				strcpy(consulta,"UPDATE partidas SET ganador= '");
				strcat(consulta, nombre);
				strcat(consulta, "' WHERE id =");
				char idBD[10];
				sprintf(idBD,"%d",miListaPartdia.partidas[idPartida].idBBDD);
				strcat(consulta, idBD);
				strcat(consulta,";");
				printf("update hecho: %s\n", consulta);
				printf("%s\n",consulta);
				resultado = mysql_query (conn,consulta);
				
				//Guardando numero de turnos
				resultado = mysql_store_result (conn);
				strcpy(consulta,"UPDATE partidas SET turnos= '");
				char turnosDB[10];
				sprintf(turnosDB,"%d",miListaPartdia.partidas[idPartida].Tiradas);
				strcat(consulta, turnosDB);
				strcat(consulta, "' WHERE id =");
				sprintf(idBD,"%d",miListaPartdia.partidas[idPartida].idBBDD);
				strcat(consulta, idBD);
				strcat(consulta,";");
				printf("update turnos hecho: %s\n", consulta);
				printf("%s\n",consulta);
				resultado = mysql_query (conn,consulta);
				
			}
			
			
			
		}
		if (codigo ==14){ //Robar Carta
			int cont;
			//cogemos la idPartida
			p= strtok (NULL,"/");
			idPartida = atoi (p);
			//Saco posicion
			p= strtok (NULL,"/");
			int posicion = atoi (p);
			char peticion[50];
			
			if (miListaPartdia.partidas[idPartida].contadorRobar==39){
				miListaPartdia.partidas[idPartida].contadorRobar=0;
			}else{
				int robada=miListaPartdia.partidas[idPartida].cartas[miListaPartdia.partidas[idPartida].contadorRobar].numero;
				sprintf(peticion,"15/%d/%d",miListaPartdia.partidas[idPartida].invitados[posicion].nForm, robada);
				write(miListaPartdia.partidas[idPartida].invitados[posicion].socket_invitado,peticion,strlen(peticion));
				printf("%s al socket %d\n", peticion,miListaPartdia.partidas[idPartida].invitados[posicion]);
				printf("%d\n",miListaPartdia.partidas[idPartida].contadorRobar);
				miListaPartdia.partidas[idPartida].contadorRobar++;
				
			}
			
		}
		if (codigo == 15){
			
			resultado = mysql_store_result (conn);
			
			//con el nombre cogemos el id
			strcpy(consulta,"SELECT id FROM jugadores WHERE nickname='");
			strcat(consulta, nombre);
			strcat(consulta,"';");
			printf("select id: %s\n", consulta);
			// hacemos la consulta 
			err=mysql_query (conn, consulta); 
			if (err!=0) {
				printf ("Error al consultar datos de la base %u %s\n",
						mysql_errno(conn), mysql_error(conn));
				exit (1);
			}
			
			resultado = mysql_store_result (conn);
			row = mysql_fetch_row (resultado);
			
			int id = atoi(row[0]);
			printf ("%d\n",id);
			char identificacion[10];
			sprintf(identificacion,"%d",id);
			
			//borramos todos los parametros del jugador
			
			resultado = mysql_store_result (conn);
			strcpy(consulta,"UPDATE jugadores SET nickname='' WHERE id=" );
			strcat(consulta,identificacion);
			strcat(consulta, ";");
			printf("update eliminar 1: %s\n", consulta);
			resultado = mysql_query (conn,consulta);
			printf("update eliminar 1: %s\n", consulta);
			
			strcpy(consulta,"UPDATE jugadores SET passwords='' WHERE id=" );
			strcat(consulta,identificacion);
			strcat(consulta, ";");
			resultado = mysql_query (conn,consulta);
			printf("update eliminar2: %s\n", consulta);
			
			strcpy(consulta,"UPDATE jugadores SET numVictorias=0 WHERE id=" );
			strcat(consulta,identificacion);
			strcat(consulta, ";");
			resultado = mysql_query (conn,consulta);
			printf("update eliminar3: %s\n", consulta);
			
			strcpy(consulta,"UPDATE jugadores SET numPartidas=0 WHERE id=" );
			strcat(consulta,identificacion);
			strcat(consulta, ";");
			resultado = mysql_query (conn,consulta);
			printf("update eliminar4: %s\n", consulta);
		}
/*		if ((codigo ==1) || (codigo==2)|| (codigo==3))*/
/*		{*/
			//antes de aungmentar el contador llamas a una funcion que se llama lock y les pasas por ref. (&mutex)
/*			pthread_mutex_lock(&mutex);//no interrumpas ahora estructura de datos por referencia(&)*/
/*			contador=contador+1;*/
/*			pthread_mutex_unlock(&mutex);//ya puedes interrumpirme*/
			//notificar a todos los clientes conectados
/*			char notificacion[20];*/
/*			sprintf(notificacion,"4/%d",contador);*/
			//a todos los clientes conectados
/*			int l;*/
/*			for(l=0;l<i; l++)*/
/*				write (sockets[l],notificacion,strlen(notificacion));*/
			
/*		}*/
		
		//y acabamos el servicio con el cliente
	}
	close(sock_conn);
	
	
}
void Crea_baraja(ListaPartida * lista,int idPartida)
{
	printf("entro en crear_baraja\n");
	int i=0;

	while(i<39){
		int numero = rand() % (39); 
		printf("%d\n",numero);
		int j=0;
		int coincide =0;
		while(j<i&&!coincide){
			if (numero==lista->partidas[idPartida].cartas[j].numero)
				coincide=1;
			if(!coincide)
				j++;
		}
		if (!coincide){
			
			lista->partidas[idPartida].cartas[i].numero=numero;
			printf("carta guardada: %d\n",lista->partidas[idPartida].cartas[i].numero);
			/*pthread_mutex_lock(&mutex);*/
/*			Decisor_caracteristica_carta(&miListaCartas,numero,idPartida,i);*/
/*			pthread_mutex_unlock(&mutex);*/
			printf("posicion baraja: %d\n",i);
			i++;
		}
		
	}
	printf("salgo de crear_baraja\n");
	
}
void Decisor_caracteristica_carta(ListaPartida* lista,int numero, int idPartida,int i){
	
	//Asignamos el color a la carta
	if (numero<10)
		lista->partidas[idPartida].cartas[i].color=1;
	else if (numero>9&&numero<20)
		lista->partidas[idPartida].cartas[i].color=2;
	else if (numero>19&&numero<30)
		lista->partidas[idPartida].cartas[i].color=3;
	else if (numero>29)
		lista->partidas[idPartida].cartas[i].color=4;
	
	printf("color de la carta: %d \n",lista->partidas[idPartida].cartas[i].color);
	
	//asignamos el numero a la carta
	if (numero==0||numero==10||numero==20||numero==30)
		lista->partidas[idPartida].cartas[i].numero=0;
	else if (numero==1||numero==11||numero==21||numero==31)
		lista->partidas[idPartida].cartas[i].numero=1;
	else if (numero==2||numero==12||numero==22||numero==32)
		lista->partidas[idPartida].cartas[i].numero=2;
	else if (numero==3||numero==13||numero==23||numero==33)
		lista->partidas[idPartida].cartas[i].numero=3;
	else if (numero==4||numero==14||numero==24||numero==34)
		lista->partidas[idPartida].cartas[i].numero=4;
	else if (numero==5||numero==15||numero==25||numero==35)
		lista->partidas[idPartida].cartas[i].numero=5;
	else if (numero==6||numero==16||numero==26||numero==36)
		lista->partidas[idPartida].cartas[i].numero=6;
	else if (numero==7||numero==17||numero==27||numero==37)
		lista->partidas[idPartida].cartas[i].numero=7;
	else if (numero==8||numero==18||numero==28||numero==38)
		lista->partidas[idPartida].cartas[i].numero=8;
	else if (numero==9||numero==19||numero==29||numero==39)
		lista->partidas[idPartida].cartas[i].numero=9;
	
	printf("numero de la carta: %d \n",lista->partidas[idPartida].cartas[i].numero);
	
}
int sockets_jugadorespartida(ListaPartida * lista,int partida,int posicion)
{//cogemos el socket del jugador en la posicion l y la partida (partida)
	printf("Entro en sockets_jugadorespartida \n");
	printf("partida: %d\n",partida);
	printf("posicion: %d\n",posicion);
	
	int socket;
	printf("socket del invitado: %d\n",lista->partidas[lista->num].invitados[posicion].socket_invitado);
	socket=lista->partidas[lista->num].invitados[posicion].socket_invitado;
	
	printf("socket: %d \n",socket);
	return socket;
	
	
	printf("Salgo en sockets_jugadorespartida \n");
	printf("\n");
}
int repuesta_invitados(ListaPartida * lista,int numero,int posicion,int nForm)
{
	printf("Entro en repuesta_invitados \n");
	printf("1 o 0: %d \n",numero);
	printf("posicion: %d\n",posicion);
	lista->partidas[lista->num].invitados[posicion].SiONO=numero;
	lista->partidas[lista->num].invitados[posicion].nForm=nForm;
	printf("nForm: %d\n",lista->partidas[lista->num].invitados[posicion].nForm);
	int contador=0;
	int i=0;
	while (i<lista->partidas[lista->num].numero_invitados)
	{
		contador=contador+lista->partidas[lista->num].invitados[i].SiONO;
		i++;
	}
	return contador;
	printf("contador real: %d \n",lista->partidas[lista->num].numero_invitados);
	
	printf("contador: %d \n",contador);
	printf("Salgo en sockets_jugadorespartida \n");
	printf("\n");
	
}



int DamePos_invitado (ListaPartida * lista, char name[20]){
	// desvuelve el socket o -1 si no esta en la lista
	printf("Entro en DamePos_invitado \n");
	int i=0;
	int encontrado =0;
	printf("1\n");
	while((i<3)&& !encontrado)
	{
		if (strcmp(lista->partidas[lista->num].invitados[i].player_invitado,name)==0)
			encontrado = 1;
		if (!encontrado)
			i++;
		printf("2\n");
	}
	if (encontrado)
	{
		return i;
		printf("posicio: %d \n",i);
	}
	else{
		return -1;
		printf("-1 \n");
	}
	
	printf("Salgo en DamePos_invitado \n");
	printf("\n");
	
}int Damepos_invitado_partida (ListaPartida * lista, char name[20],int idPartida){
	// desvuelve el socket o -1 si no esta en la lista
	printf("Entro en DamePos_invitado \n");
	int i=0;
	int encontrado =0;
	printf("1\n");
	while((i<3)&& !encontrado)
	{
		if (strcmp(lista->partidas[idPartida].invitados[i].player_invitado,name)==0)
			encontrado = 1;
		if (!encontrado)
			i++;
		printf("2\n");
	}
	if (encontrado)
	{
		return i;
		printf("posicio: %d \n",i);
	}
	else{
		return -1;
		printf("-1 \n");
	}
	
	printf("Salgo en DamePos_invitado \n");
	printf("\n");
	
}

	void pon_nForm(ListaPartida * lista,int posicion,int nForm)
	{
		printf("Entro en pon_nForm\n");
		lista->partidas[lista->num].invitados[posicion].nForm=nForm;
		printf("nForm: \n",lista->partidas[lista->num].invitados[posicion].nForm);
		printf("Salgo de pon_nForm\n");
		printf("\n");
	}
	void pon_invitados(ListaPartida * lista, char invitado[20], int socket_invitado,int posicion,int nForm){
		
		printf("Entro en pon_invitados \n");
		strcpy(lista->partidas[lista->num].invitados[posicion].player_invitado,invitado);
		lista->partidas[lista->num].invitados[posicion].socket_invitado=socket_invitado;
		lista->partidas[lista->num].invitados[posicion].nForm=nForm;
		lista->partidas[lista->num].numero_invitados++;
		printf("jugador: %s ,socket: %d ,posicion: %d\n",lista->partidas[lista->num].invitados[posicion].player_invitado,lista->partidas[0].invitados[posicion].socket_invitado,posicion);
		printf("nForm: \n",lista->partidas[lista->num].invitados[posicion].nForm);
		printf("Salgo en pon_invitados \n");
		printf("\n");
	}
		void pon_invitados_normales(ListaPartida * lista, char invitado[20], int socket_invitado,int posicion){
			
			printf("Entro en pon_invitados_normales \n");
			strcpy(lista->partidas[lista->num].invitados[posicion].player_invitado,invitado);
			lista->partidas[lista->num].invitados[posicion].socket_invitado=socket_invitado;
			lista->partidas[lista->num].numero_invitados++;
			printf("jugador: %s ,socket: %d ,posicion: %d\n",lista->partidas[lista->num].invitados[posicion].player_invitado,lista->partidas[0].invitados[posicion].socket_invitado,posicion);
			
			printf("Salgo en pon_invitados_normales \n");
			printf("\n");
		}
/*			void creador (ListaPartida * lista, char creador[20],int socket_creador){*/
				
/*				printf("Entro en creador \n");*/
				//strcpy(lista->partidas[0].invitados[0].socket_invitado,invitado);	
/*				strcpy (lista->partidas[lista->num].creador,creador);*/
/*				lista->partidas[lista->num].socket_creador=socket_creador;*/
/*				printf("Ya tenemos guardado al creador: %s y su socket: %d\n",creador,socket_creador);*/
/*				printf("Salgo en creador \n");*/
/*				printf("\n");*/
		/*	}*/
				
				
				int Pon (ListaConectados * lista, char nombre[20],int Socket){
					
					printf("Al entrar a la funcion Pon: %s, %d\n", nombre, Socket);
					if (lista->num ==100)
						return -1;
					else 
					{
						strcpy(lista->conectados[lista->num].Nombre,nombre);
						lista->conectados[lista->num].socket=Socket;
						lista->num++;
						return 0;
					}
					printf("Despues de ponerlos son: %s, %d\n",lista->conectados[lista->num].Nombre,lista->conectados[lista->num].socket);
				}
					
					int DamePos (ListaConectados * lista, char name[20])
					{// desvuelve el socket o -1 si no esta en la lista
						int i=0;
						int encontrado =0;
						while((i<lista->num)&& !encontrado)
						{
							if (strcmp(lista->conectados[i].Nombre,name)==0)
								encontrado = 1;
							if (!encontrado)
								i++;
						}
						if (encontrado)
							return i;
						else
							return -1;
					}
					int Socketfromnombre (ListaConectados * lista, char name[30])
					{// desvuelve el socket o -1 si no esta en la lista
						int i=0;
						int encontrado =0;
						int socket;
						printf("Entro a la funcion Socketfromnombre\n");
						printf("Este es el nombre %s\n",name);
						
						while(i<lista->num && encontrado==0)
						{
							if (strcmp(lista->conectados[i].Nombre,name)==0){
								socket=lista->conectados[i].socket;
								encontrado = 1;
								
							}
							else
								i++;
						}
						printf("este el el socket:%d\n",socket);
						if (encontrado)
						{
							return socket;
							printf("Ya he retornado el socket\n");
						}
						
						else
							return -1;
						printf("Salgo de Socketfromnombre\n");
						printf("\n");
					}
					int Eliminar(ListaConectados* lista, char nombre[20])
					{
						
						int pos = DamePos(lista,nombre);
						if(pos ==-1)
							return -1;
						else 
						{
							int i;
							for (i=pos; i<lista->num;i++)//movemos todos los nobres de la lista una poscion para que no haya huecos
								lista->conectados[i]=lista->conectados[i+1];
						}
						lista->num--;
						return 0;
					}
					
					void DameConectados(ListaConectados*lista,char conectados[300])
					{
						//pone en conectados los nombres de todos los conectados separados por /.
						//primero pone el numero de conectados. Ejempolo: "3/ricart/juan/pepe"
						
						sprintf(conectados,"%d", lista->num);
						int i;
						for(i=0;i<lista->num;i++)
						{
							if (lista->conectados[i].Nombre != NULL)
								sprintf(conectados,"%s,%s",conectados, lista->conectados[i].Nombre);
						}
					}
					
					int main (int argc, char *argv[])
					{
						int sock_conn, sock_listen;
						struct sockaddr_in serv_adr;
						pthread_t thread [20];
						
						// INICIALITZACIONS
						// Obrim el socket
						if ((sock_listen = socket(AF_INET, SOCK_STREAM, 0)) < 0)
							printf("Error creant socket\n");
						// Fem el bind al port
						
						
						memset(&serv_adr, 0, sizeof(serv_adr));// inicialitza a zero serv_addr
						serv_adr.sin_family = AF_INET;
						
						// asocia el socket a cualquiera de las IP de la m?quina. 
						//htonl formatea el numero que recibe al formato necesario
						serv_adr.sin_addr.s_addr = htonl(INADDR_ANY);
						// establecemos el puerto de escucha
						serv_adr.sin_port = htons(50064);
						if (bind(sock_listen, (struct sockaddr *) &serv_adr, sizeof(serv_adr)) < 0)
							printf ("Error al bind\n");
						
						if (listen(sock_listen, 3) < 0)
							printf("Error en el Listen");
						
						contador=0;
						
						//pthread_t thread;
						i=0;
						
						//miLista.num=0;
						
						//bucle infinito
						int contador;
						char texto;
						
						for (;;){
							printf ("Escuchando..\n");
							
							sock_conn = accept(sock_listen, NULL, NULL);
							printf ("He recibido conexion\n");
							
							sockets[i] =sock_conn;
							//sock_conn es el socket que usaremos para este cliente
							//miLista.num=0;
							
							// Crear thead y decirle lo que tiene que hacer
							
							pthread_create (&thread[i], NULL, AtenderCliente,&sockets[i]);
							i=i+1;
							
						}
						
						
						
						
					}
					
					
					
					
					
					
