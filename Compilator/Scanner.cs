using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LFP_PROYECTO2.Compilator
{
	class Scanner
	{
		private String lexemaActual = "" ;
		private int fila = 0, columna = 0;
		private int estadoActual = 0;
		private Queue<Token> tokens = new Queue<Token>();
		/// <summary>
		/// Obtiene la palabra reservada según la cadena almacenada en lexemaActual
		/// </summary>
		/// <returns></returns>
		private String PalabraReservada()
		{
			switch (lexemaActual)
			{
				case "CrearEstructura":
				case "LeerArchivo":
				case "Ubicacion":
				case "Estructura":
				case "Carpeta":
				case "Archivo":
				case "Nombre":
				case "Extension":
				case "Texto":
					return lexemaActual;
			}
			return "Desconocido";
		}
		/// <summary>
		/// Realiza el análisis léxico de una cadena
		/// </summary>
		/// <param name="cadena"></param>
		/// <returns></returns>
		public Queue<Token> Analizar(String cadena)
		{
			this.estadoActual = 0;
			for (int i = 0; i < cadena.Length; i++)
			{
				char aux = cadena[i];
				switch (this.estadoActual)
				{
					case 0:
						//Alfabeto -> 1
						if ((aux >= 65 && aux <= 90) || (aux >= 97 && aux <= 122))//A -> Z a -> z   no 'ñ' ni 'Ñ'
						{
							this.lexemaActual += aux;
							this.columna++;
							this.estadoActual = 1;
						}
						//Comillas -> 3
						else if (aux == 34 /*"*/)
						{
							tokens.Enqueue(new Token(tokens.Count + 1, aux + "", this.fila, this.columna, "Comillas"));
							this.columna++;
							this.estadoActual = 3;
						}
						//Apostrofe -> 4
						else if (aux == 39 /*'*/ )
						{
							tokens.Enqueue(new Token(tokens.Count + 1, aux + "", this.fila, this.columna, "Apostrofe"));
							this.columna++;
							this.estadoActual = 4;
						}
						//Símbolos -> 0
						else if (aux == 44 || aux == 123 /* { */ || aux == 125 /* } */ || aux == 58 /* : */ || aux == 46 /* . */ || aux == 60 /* < */ || aux == 62 /* > */ || aux == 47 /* / */)
						{
							tokens.Enqueue(
							new Token(tokens.Count + 1,
							aux + "",
							this.fila,
							this.columna,
							aux == 123 ? "Llave Izq" :
							aux == 125 ? "Llave Der" :
							aux == 58 ? "Dos Puntos" :
							aux == 46 ? "Punto" :
							aux == 60 ? "Menor Que" :
							aux == 62 ? "Mayor Que" :
							aux == 47 ? "Diagonal" :
							aux == 44 ? "Coma" : "Desconocido"
							));
							this.columna++;
						}
						//Salto de línea -> 0
						else if (aux == 10 || aux == 11)
						{
							//Guarda lexemaActual
							if (!String.IsNullOrEmpty(this.lexemaActual) || !String.IsNullOrWhiteSpace(this.lexemaActual)) tokens.Enqueue(new Token(tokens.Count + 1, this.lexemaActual, this.fila, this.columna - this.lexemaActual.Length, PalabraReservada()));
							//Aumenta la fila
							this.fila++;
							//Reinicia la columna
							this.columna = 0;
							//Limpia el lexema actual
							lexemaActual = "";
						}
						//Espacio y tab -> 0
						else if (aux == 32 || aux == 9)
						{
							if (!String.IsNullOrEmpty(this.lexemaActual) || !String.IsNullOrWhiteSpace(this.lexemaActual)) tokens.Enqueue(new Token(tokens.Count + 1, this.lexemaActual, this.fila, this.columna - this.lexemaActual.Length, PalabraReservada()));
							//Limpia el lexema actual
							//Aumenta la columna
							this.columna++;
							//Guarda lexemaActual
							lexemaActual = "";
						}
						else
						{
							this.tokens.Enqueue(new Token(this.tokens.Count +1 ,""+ aux,this.fila,this.columna,"Desconocido"));
						}
						break;
					case 1:
						//Alfabeto -> 1
						if ((aux >= 65 && aux <= 90) || (aux >= 97 && aux <= 122))//A -> Z a -> z   no 'ñ' ni 'Ñ')
						{
							//Encola :v el caracter del alfabeto
							this.lexemaActual += aux;
							//Aumenta la columna
							this.columna++;
						}
						//Comillas -> 3
						else if (aux == 34 /*"*/)
						{
							//Guarda lexemaActual
							if (!String.IsNullOrEmpty(this.lexemaActual) || !String.IsNullOrWhiteSpace(this.lexemaActual)) tokens.Enqueue(new Token(tokens.Count + 1, this.lexemaActual, this.fila, this.columna - this.lexemaActual.Length, PalabraReservada()));
							//Limpia el lexema actual
							lexemaActual = "";
							//Guarda el aux
							tokens.Enqueue(new Token(tokens.Count + 1, aux + "", this.fila, this.columna, "Comillas"));
							//Se mueve de estado
							this.estadoActual = 3;
							//Aumenta columna
							this.columna++;
						}
						//Apostrofe -> 4
						else if (aux == 39 /*'*/)
						{
							//Guarda lexemaActual
							if (!String.IsNullOrEmpty(this.lexemaActual) || !String.IsNullOrWhiteSpace(this.lexemaActual)) tokens.Enqueue(new Token(tokens.Count + 1, this.lexemaActual, this.fila, this.columna - this.lexemaActual.Length, PalabraReservada()));
							//Limpia el lexema actual
							lexemaActual = "";
							//Guarda el aux
							tokens.Enqueue(new Token(tokens.Count + 1, aux + "", this.fila, this.columna, "Apostrofe"));
							//Se mueve de estado
							this.estadoActual = 4;
							//Aumenta columna
							this.columna++;
						}
						//Salto de línea -> 1
						else if (aux == 10 || aux == 11)
						{
							//Guarda lexemaActual
							if (!String.IsNullOrEmpty(this.lexemaActual) || !String.IsNullOrWhiteSpace(this.lexemaActual)) tokens.Enqueue(new Token(tokens.Count + 1, this.lexemaActual, this.fila, this.columna - this.lexemaActual.Length, PalabraReservada()));
							//Aumenta la fila
							this.fila++;
							//Reinicia la columna
							this.columna = 0;
							//Limpia el lexema actual
							lexemaActual = "";
						}
						//Espacio y tab -> 1
						else if (aux == 32 || aux == 9)
						{
							//Guarda lexemaActual
							if (!String.IsNullOrEmpty(this.lexemaActual) || !String.IsNullOrWhiteSpace(this.lexemaActual)) tokens.Enqueue(new Token(tokens.Count + 1, this.lexemaActual, this.fila, this.columna - this.lexemaActual.Length, PalabraReservada()));
							//Limpia el lexema actual
							lexemaActual = "";
							//Aumenta la columna
							this.columna++;
							//Guarda lexemaActual
							lexemaActual = "";
						}
						//Simbolos -> 0
						else if (aux == 44 || aux == 123 /* { */ || aux == 125 /* } */ || aux == 58 /* : */ || aux == 46 /* . */ || aux == 60 /* < */ || aux == 62 /* > */ || aux == 47 /* / */)
						{
							//Guarda lexemaActual
							if (!String.IsNullOrEmpty(this.lexemaActual) || !String.IsNullOrWhiteSpace(this.lexemaActual)) tokens.Enqueue(new Token(tokens.Count + 1, this.lexemaActual, this.fila, this.columna - this.lexemaActual.Length, PalabraReservada()));
							//Limpia el lexema actual
							lexemaActual = "";
							//Guarda el aux
							tokens.Enqueue(
							new Token(tokens.Count + 1,
							aux + "",
							this.fila,
							this.columna,
							aux == 123 ? "Llave Izq" :
							aux == 125 ? "Llave Der" :
							aux == 58 ? "Dos Puntos" :
							aux == 46 ? "Punto" :
							aux == 60 ? "Menor Que" :
							aux == 62 ? "Mayor Que" :
							aux == 47 ? "Diagonal" : aux == 44 ? "Coma" : "Desconocido"
							));
							this.columna++;
							this.estadoActual = 0;
						}
						else
						{
							this.tokens.Enqueue(new Token(this.tokens.Count + 1, "" + aux, this.fila, this.columna, "Desconocido"));
						}
						break;
					case 3://Comillas
						//Salto de línea -> 3
						if (aux == 10 || aux == 11)
						{
							this.fila++;
							this.columna = 0;
							this.lexemaActual += aux;
						}
						//Espacio y tab -> 3
						else if (aux == 32 || aux == 9)
						{
							this.columna++;
							this.lexemaActual += aux;
						}
						//Comillas -> 0
						else if (aux == 34 /*"*/)
						{
							//Guarda lexemaActual
							if (!String.IsNullOrEmpty(this.lexemaActual) || !String.IsNullOrWhiteSpace(this.lexemaActual)) tokens.Enqueue(new Token(tokens.Count + 1, this.lexemaActual, this.fila, this.columna - this.lexemaActual.Length, "Texto"));
							//Limpia el lexema actual
							lexemaActual = "";
							//Guarda el aux
							tokens.Enqueue(new Token(tokens.Count + 1, aux + "", this.fila, this.columna, "Comillas"));
							//Se mueve de estado
							this.estadoActual = 0;
							//Aumenta columna
							this.columna++;
						}
						//Cualquier cosa -> 3
						else
						{
							//Lo agrega a la cadena
							this.lexemaActual += aux;
							//Aumenta la columna
							this.columna++;
						}
						break;
					case 4://Apostrofe
						//Salto de línea -> 4
						if (aux == 10 || aux == 11)
						{
							this.fila++;
							this.columna = 0;
							this.lexemaActual += aux;
						}
						//Espacio y tab -> 4
						else if (aux == 32 || aux == 9)
						{
							this.columna++;
							this.lexemaActual += aux;
						}
						//Apostrofe -> 0
						else if (aux == 39 /*'*/)
						{
							//Guarda lexemaActual
							if (!String.IsNullOrEmpty(this.lexemaActual) || !String.IsNullOrWhiteSpace(this.lexemaActual)) tokens.Enqueue(new Token(tokens.Count + 1, this.lexemaActual, this.fila, this.columna - this.lexemaActual.Length, PalabraReservada()));
							//Limpia el lexema actual
							lexemaActual = "";
							//Guarda el aux
							tokens.Enqueue(new Token(tokens.Count + 1, aux + "", this.fila, this.columna, "Apostrofe"));
							//Se mueve de estado
							this.estadoActual = 0;
							//Aumenta columna
							this.columna++;
						}
						//Cualquier cosa -> 4
						else
						{
							//Lo agrega a la cadena
							this.lexemaActual += aux;
							//Aumenta la columna
							this.columna++;
						}
						break;
					case 2:
					case 5:
					case 6:
						break;
				}
			}
			return this.tokens;
		}
	}
}


