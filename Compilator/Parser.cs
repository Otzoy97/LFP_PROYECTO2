using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LFP_PROYECTO2.Compilator
{
	class Parser
	{
		/// <summary>
		/// Nodo raíz del árbol de carpetas
		/// </summary>
		private Queue<Token> tokens;
		public Queue<Token> err = new Queue<Token>();
		private Queue<string> graphviz = new Queue<string>();
		//private List<Node> arbol;
		private string ERROR = "Error Sintáctico";
		private Token tokenEn = null;
		private int index = 0;
		private bool bandera = true;
		private string DirectorioActual;
		private System.Windows.Forms.RichTextBox txtbox01;
		/// <summary>
		/// [0] Instruccion de directorios
		/// [1] Instrucción graphviz
		/// </summary>
		private string[] DirectorioAcumulado = new string[2];
		private string[] archivo = new string[3];

		/// <summary>
		/// Retorna la cola de instrucciones graphviz
		/// </summary>
		public Queue<string> Graph
		{
			get
			{
				return this.graphviz;
			}
		}
		/// <summary>
		/// Constructor por defecto
		/// </summary>
		/// <param name="tokens"></param>
		public Parser(Queue<Token> tokens, System.Windows.Forms.RichTextBox txtbox)
		{
			this.tokens = tokens;
			this.txtbox01 = txtbox;
		}
		/// <summary>
		/// Compara las cadenas entrantes de tokens
		/// </summary>
		/// <param name="cadenaAComparar"></param>
		/// <param name="EncolarError"></param>
		/// <returns></returns>
		private bool Match(string cadenaAComparar, bool EncolarError = true)
		{
			if (index < tokens.Count)
			{
				tokenEn = this.tokens.ElementAt(index++);
				if (cadenaAComparar.Equals(tokenEn.TToken))
				{
					Console.WriteLine(tokenEn.Lexema);
					return true;
				}
				else
				{
					while (index < tokens.Count && !cadenaAComparar.Equals(tokenEn.TToken))
					{
						String espera = cadenaAComparar.Equals("Desconocido") ? "extension" : cadenaAComparar;
						if (EncolarError) this.err.Enqueue(new Token(this.err.Count + 1, tokenEn.Lexema + Environment.NewLine + "Se esperaba : " + cadenaAComparar, tokenEn.Fila, tokenEn.Columna, ERROR));
						Console.WriteLine(tokenEn.Lexema + Environment.NewLine + "Se esperaba : " + cadenaAComparar);
						tokenEn = this.tokens.ElementAt(index++);
					}
					index--;
					this.bandera = false;
					return false;
				}
			}
			return false;
		}
		/// <summary>
		/// Ejecuta el análisis sintáctico
		/// </summary>
		public bool Run()
		{
			while (this.index < this.tokens.Count)
			{
				A();
			}
			return this.bandera;
		}
		/// <summary>
		/// A -> R{B}, || R{C},
		/// </summary>
		private void A()
		{
			int temp;
			temp = this.index;
			//Guarda el aux del siguiente token
			Token aux = this.tokens.ElementAt(temp);
			//Verifica si es estructura o lectura de archivo la palabra reservada
			switch (aux.TToken)
			{
				case "CrearEstructura":
					Match("CrearEstructura");
					Match("Llave Izq");
					B();
					Match("Llave Der");
					Match("Coma");
					break;
				case "LeerArchivo":
					Match("LeerArchivo");
					Match("Llave Izq");
					C();
					Match("Llave Der");
					Match("Coma");
					break;
				default:
					break;
			};
		}
		/// <summary>
		/// B->C D
		/// </summary>
		private void B()
		{
			C();
			D();
		}
		/// <summary>
		/// C -> 'R':"e",
		/// </summary>
		private void C()
		{
			Match("Apostrofe");
			Match("Ubicacion");
			Match("Apostrofe");
			Match("Dos Puntos");
			this.DirectorioActual = F();
			//new Files.Files().Leer(this.DirectorioActual,this.txtbox01);
			//this.DirectorioActual = "";
			Match("Coma");
		}
		/// <summary>
		/// D - > 'R':{E},
		/// </summary>
		private void D()
		{
			Match("Apostrofe");
			Match("Estructura");
			Match("Apostrofe");
			Match("Dos Puntos");
			Match("Llave Izq");
			while (index < tokens.Count && this.tokens.ElementAt(this.index).TToken.Equals("Menor Que"))
			{
				E();
				Ejecutar();
				this.DirectorioAcumulado[0] = "";
				this.graphviz.Enqueue((char)34 + this.DirectorioActual + (char)34 + this.DirectorioAcumulado[1] + Environment.NewLine);
				this.DirectorioAcumulado[1] = "";
			}
			Match("Llave Der");
			Match("Coma");
		}
		/// <summary>
		/// E -> <R F>E</R> || G
		/// </summary>
		private void E()
		{
			int temp;
			Match("Menor Que");
			//Carpeta o Archivo
			temp = this.index;
			//Guarda el aux del siguiente token
			Token aux = this.tokens.ElementAt(temp);
			//Verifica si es carpeta o archivo la palabra reservada
			switch (aux.TToken)
			{
				case "Carpeta":
					Match("Carpeta");
					var cadena = F();
					this.DirectorioAcumulado[0] += "\\" + cadena;
					this.DirectorioAcumulado[1] += " -> " + (char)34 + cadena + (char) 34 ;
					Match("Mayor Que");
					E();
					Match("Menor Que");
					Match("Diagonal");
					Match("Carpeta");
					Match("Mayor Que");
					//EPrima();
					break;
				case "Archivo":
					Match("Archivo");
					Match("Mayor Que");
					H();
					Match("Menor Que");
					Match("Diagonal");
					Match("Archivo");
					Match("Mayor Que");
					break;
				default:
					break;
			};
		}
		/// <summary>
		/// F -> "Texto"
		/// </summary>
		private String F()
		{
			Match("Comillas");
			Match("Texto");
			String aux = this.tokens.ElementAt(this.index - 1).Lexema;
			Match("Comillas");
			return aux;
		}
		/// <summary>
		/// G -> H I J
		/// </summary>
		private void H()
		{
			I();
			J();
			K();
		}
		/// <summary>
		/// H -> <R>F</R>
		/// </summary>
		private void I()
		{
			Match("Menor Que");
			Match("Nombre");
			Match("Mayor Que");
			this.archivo[0] = F();
			this.DirectorioAcumulado[1] += " -> " + (char)34 + this.archivo[0] + (char)34;
			Match("Menor Que");
			Match("Diagonal");
			Match("Nombre");
			Match("Mayor Que");
		}
		/// <summary>
		/// I -> <R>.unknow</R> 
		/// </summary>
		private void J()
		{
			Match("Menor Que");
			Match("Extension");
			Match("Mayor Que");
			Match("Punto");
			Match("Desconocido");
			this.archivo[1] = this.tokens.ElementAt(this.index - 1).Lexema;
			Match("Menor Que");
			Match("Diagonal");
			Match("Extension");
			Match("Mayor Que");
		}
		/// <summary>
		/// J -> <R>F</R>
		/// </summary>
		private void K()
		{
			Match("Menor Que");
			Match("Texto");
			Match("Mayor Que");
			this.archivo[2] = F();
			Match("Menor Que");
			Match("Diagonal");
			Match("Texto");
			Match("Mayor Que");
		}
		/// <summary>
		/// Crea los directorios y archivos que lleven consigo
		/// </summary>
		private void Ejecutar()
		{
			if (this.bandera)
			{
				var aux = new Files.Files().CrearDirectorio(this.DirectorioActual,this.DirectorioAcumulado[0]);

				if (!String.IsNullOrEmpty(aux))
				{
					//char temp01 = (char) 97;
					//for (int i = 0; i < 2; i++)
					//{
						new Files.Files().GenerarYAbrir(aux, this.archivo[0] /*+ temp01++*/, this.archivo[2], this.archivo[1], false); 
					//}
				}
			}
		}
	}
}
