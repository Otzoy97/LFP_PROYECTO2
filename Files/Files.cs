using System;
using System.Collections.Generic;
using System.IO;
using LFP_PROYECTO2.Compilator;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LFP_PROYECTO2.Files
{
	class Files
	{
		private String executable = @".\External\dot.exe";
		private String output = @".\External\" + Guid.NewGuid().ToString();
		public String ReportarTokens(Queue<Token> token)
		{
			//Ayudará a crear el string a retornar
			StringBuilder htmlBuilder = new StringBuilder();
			//Crea un html
			htmlBuilder.Append("<!DOCTYPE html>");
			htmlBuilder.Append("<html>");
			//Crea un css
			htmlBuilder.Append("<style type=\"text / css\">");
			htmlBuilder.Append(".token  {border-collapse:collapse;border-spacing:0;border-color:#aaa;margin:0px auto;}");
			htmlBuilder.Append(".token td{font-family:Arial, sans-serif;font-size:14px;padding:10px 5px;border-style:solid;border-width:0px;overflow:hidden;word-break:normal;border-color:#aaa;color:#333;background-color:#fff;border-top-width:1px;border-bottom-width:1px;}");
			htmlBuilder.Append(".token th{font-family:Arial, sans-serif;font-size:14px;font-weight:normal;padding:10px 5px;border-style:solid;border-width:0px;overflow:hidden;word-break:normal;border-color:#aaa;color:#fff;background-color:#f38630;border-top-width:1px;border-bottom-width:1px;}");
			htmlBuilder.Append(".err  {border-collapse:collapse;border-spacing:0;border-color:#999;margin:0px auto;}");
			htmlBuilder.Append(".err td{font-family:Arial, sans-serif;font-size:14px;padding:10px 5px;border-style:solid;border-width:0px;overflow:hidden;word-break:normal;border-color:#999;color:#444;background-color:#F7FDFA;border-top-width:1px;border-bottom-width:1px;}");
			htmlBuilder.Append(".err th{font-family:Arial, sans-serif;font-size:14px;font-weight:normal;padding:10px 5px;border-style:solid;border-width:0px;overflow:hidden;word-break:normal;border-color:#999;color:#fff;background-color:#26ADE4;border-top-width:1px;border-bottom-width:1px;}");
			htmlBuilder.Append(".tg-f4we{font-weight:bold;font-size:15px;font-family:Arial, Helvetica, sans-serif !important;;text-align:center;vertical-align:top}");
			htmlBuilder.Append(".tg-cjcd{font-style:italic;font-size:12px;font-family:Arial, Helvetica, sans-serif !important;;vertical-align:top}");
			htmlBuilder.Append(".tg-do2s{font-size:12px;font-family:Arial, Helvetica, sans-serif !important;;vertical-align:top}");
			htmlBuilder.Append("</style>");
			//Permite la utilización de acentos utf-8
			htmlBuilder.Append("<head>");
			htmlBuilder.Append("<meta charset =\"UTF-8\">");
			htmlBuilder.Append("<title>");
			htmlBuilder.Append("Reporte-");
			htmlBuilder.Append(Guid.NewGuid().ToString());
			htmlBuilder.Append("</title>");
			htmlBuilder.Append("</head>");
			htmlBuilder.Append("<body>");
			///
			///Inicia tabla de TOKEN
			///
			htmlBuilder.Append("<table class= \"token\">");
			htmlBuilder.Append("<tr>" +
								"<th class=\"tg-f4we\" colspan=\"5\">TOKEN</th> " +
							   "</tr>");
			htmlBuilder.Append("<tr>" +
								"<td class=\"tg-cjcd\">#<br></td>" +
								"<td class=\"tg-cjcd\">LEXEMA<br></td>" +
								"<td class=\"tg-cjcd\">FILA</td>" +
								"<td class=\"tg-cjcd\">COLUMNA</td>" +
								"<td class=\"tg-cjcd\">TOKEN</td>" +
							   "</tr>");
			int contador = 0;
			///Agrega los elementos de la lista a la tabla recién creada
			int index = 0;
			while (index < token.Count())
			{
				Token cast = token.ElementAt(index++);
				htmlBuilder.Append("<tr>");
				htmlBuilder.Append("<td class=\"tg-do2s\">" + ++contador + "</td>");
				htmlBuilder.Append("<td class=\"tg-do2s\">" + cast.Lexema + "</td>");
				htmlBuilder.Append("<td class=\"tg-do2s\">" + cast.Fila + "</td>");
				htmlBuilder.Append("<td class=\"tg-do2s\">" + cast.Columna + "</td>");
				htmlBuilder.Append("<td class=\"tg-do2s\">" + cast.TToken + "</td>");
				htmlBuilder.Append("</tr>");
			}
			htmlBuilder.Append("<tr>" +
								"<th class=\"tg-cjcd\" colspan=\"5\"> ---------- Fin de línea. " + contador + " tokens ---------- </th> " +
							   "</tr>");
			htmlBuilder.Append("</table><br><br>");
			htmlBuilder.Append("</body>");
			htmlBuilder.Append("</html>");
			//Crea un string de todo lo escrito anteriormente
			return htmlBuilder.ToString();
		}
		public String ReportarTokens(Queue<Token> errlexico, Queue<Token> errsintactico)
		{
			//Ayudará a crear el string a retornar
			StringBuilder htmlBuilder = new StringBuilder();
			//Crea un html
			htmlBuilder.Append("<!DOCTYPE html>");
			htmlBuilder.Append("<html>");
			//Crea un css
			htmlBuilder.Append("<style type=\"text / css\">");
			htmlBuilder.Append(".token  {border-collapse:collapse;border-spacing:0;border-color:#aaa;margin:0px auto;}");
			htmlBuilder.Append(".token td{font-family:Arial, sans-serif;font-size:14px;padding:10px 5px;border-style:solid;border-width:0px;overflow:hidden;word-break:normal;border-color:#aaa;color:#333;background-color:#fff;border-top-width:1px;border-bottom-width:1px;}");
			htmlBuilder.Append(".token th{font-family:Arial, sans-serif;font-size:14px;font-weight:normal;padding:10px 5px;border-style:solid;border-width:0px;overflow:hidden;word-break:normal;border-color:#aaa;color:#fff;background-color:#f38630;border-top-width:1px;border-bottom-width:1px;}");
			htmlBuilder.Append(".err  {border-collapse:collapse;border-spacing:0;border-color:#999;margin:0px auto;}");
			htmlBuilder.Append(".err td{font-family:Arial, sans-serif;font-size:14px;padding:10px 5px;border-style:solid;border-width:0px;overflow:hidden;word-break:normal;border-color:#999;color:#444;background-color:#F7FDFA;border-top-width:1px;border-bottom-width:1px;}");
			htmlBuilder.Append(".err th{font-family:Arial, sans-serif;font-size:14px;font-weight:normal;padding:10px 5px;border-style:solid;border-width:0px;overflow:hidden;word-break:normal;border-color:#999;color:#fff;background-color:#26ADE4;border-top-width:1px;border-bottom-width:1px;}");
			htmlBuilder.Append(".tg-f4we{font-weight:bold;font-size:15px;font-family:Arial, Helvetica, sans-serif !important;;text-align:center;vertical-align:top}");
			htmlBuilder.Append(".tg-cjcd{font-style:italic;font-size:12px;font-family:Arial, Helvetica, sans-serif !important;;vertical-align:top}");
			htmlBuilder.Append(".tg-do2s{font-size:12px;font-family:Arial, Helvetica, sans-serif !important;;vertical-align:top}");
			htmlBuilder.Append("</style>");
			//Permite la utilización de acentos utf-8
			htmlBuilder.Append("<head>");
			htmlBuilder.Append("<meta charset =\"UTF-8\">");
			htmlBuilder.Append("<title>");
			htmlBuilder.Append("Reporte-");
			htmlBuilder.Append(Guid.NewGuid().ToString());
			htmlBuilder.Append("</title>");
			htmlBuilder.Append("</head>");
			htmlBuilder.Append("<body>");
			int contador, index;
			///
			///INICIA TABLA DE ERRORES LÉXICO
			///
			htmlBuilder.Append("<table class= \"err\">");
			htmlBuilder.Append("<tr>" +
								"<th class=\"tg-f4we\" colspan=\"5\">ERROR LÉXICO</th> " +
							   "</tr>");
			htmlBuilder.Append("<tr>" +
								"<td class=\"tg-cjcd\">#<br></td>" +
								"<td class=\"tg-cjcd\">DESCRIPCIÓN<br></td>" +
								"<td class=\"tg-cjcd\">FILA</td>" +
								"<td class=\"tg-cjcd\">COLUMNA</td>" +
								"<td class=\"tg-cjcd\">ERROR</td>" +
							   "</tr>");
			contador = 0;
			index = 0;
			///Agrega los elementos de la lista a la tabla recién creada
			while (index < errlexico.Count())
			{
				Token cast = errlexico.ElementAt(index++);
				htmlBuilder.Append("<tr>");
				htmlBuilder.Append("<td class=\"tg-do2s\">" + ++contador + "</td>");
				htmlBuilder.Append("<td class=\"tg-do2s\">" + cast.Lexema + "</td>");
				htmlBuilder.Append("<td class=\"tg-do2s\">" + cast.Fila + "</td>");
				htmlBuilder.Append("<td class=\"tg-do2s\">" + cast.Columna + "</td>");
				htmlBuilder.Append("<td class=\"tg-do2s\">" + cast.TToken + "</td>");
				htmlBuilder.Append("</tr>");
			}
			htmlBuilder.Append("<tr>" +
					"<th class=\"tg-cjcd\" colspan=\"5\"> ---------- Fin de línea. " + contador + " errores ---------- </th> " +
				   "</tr>");
			htmlBuilder.Append("</table><br><br>");
			///
			///INICIA TABLA DE ERROR SINTÁCTICO
			///
			htmlBuilder.Append("<table class= \"err\">");
			htmlBuilder.Append("<tr>" +
								"<th class=\"tg-f4we\" colspan=\"5\">ERROR SINTÁCTICO</th> " +
							   "</tr>");
			htmlBuilder.Append("<tr>" +
								"<td class=\"tg-cjcd\">#<br></td>" +
								"<td class=\"tg-cjcd\">DESCRIPCIÓN<br></td>" +
								"<td class=\"tg-cjcd\">FILA</td>" +
								"<td class=\"tg-cjcd\">COLUMNA</td>" +
								"<td class=\"tg-cjcd\">ERROR</td>" +
							   "</tr>");
			contador = 0;
			index = 0;
			///Agrega los elementos de la lista a la tabla recién creada
			while (index < errsintactico.Count())
			{
				Token cast = errsintactico.ElementAt(index++);
				htmlBuilder.Append("<tr>");
				htmlBuilder.Append("<td class=\"tg-do2s\">" + ++contador + "</td>");
				htmlBuilder.Append("<td class=\"tg-do2s\">" + cast.Lexema + "</td>");
				htmlBuilder.Append("<td class=\"tg-do2s\">" + cast.Fila + "</td>");
				htmlBuilder.Append("<td class=\"tg-do2s\">" + cast.Columna + "</td>");
				htmlBuilder.Append("<td class=\"tg-do2s\">" + cast.TToken + "</td>");
				htmlBuilder.Append("</tr>");
			}
			htmlBuilder.Append("<tr>" +
					"<th class=\"tg-cjcd\" colspan=\"5\"> ---------- Fin de línea. " + contador + " errores ---------- </th> " +
				   "</tr>");
			htmlBuilder.Append("</table><br><br>");
			htmlBuilder.Append("</body>");
			htmlBuilder.Append("</html>");
			//Crea un string de todo lo escrito anteriormente
			return htmlBuilder.ToString();
		}
		/// <summary>
		/// Mueve los tokens a tokens y los errores a errores :v
		/// </summary>
		public void DepurarTokens(Queue<Token> colaEntrada, Queue<Token> errores,  Queue<Token> tokens)
		{
			while (colaEntrada.Count > 0)
			{
				//Si es error va a error
				if (colaEntrada.Peek().TToken.Equals("Desconocido"))
				{
					errores.Enqueue(colaEntrada.Dequeue());
				}
				//Si no es error es token
				else
				{
					tokens.Enqueue(colaEntrada.Dequeue());
				}
			}
		}
		/// <summary>
		/// Genera y Abre un archivo
		/// </summary>
		/// <param name="Texto"></param>
		/// <param name="extension"></param>
		/// <param name="abrir"></param>
		public void GenerarYAbrir(String nombre, String texto, String extension, bool abrir)
		{
			StreamWriter outputFile = null;
			try
			{
				using (outputFile = new StreamWriter("C:\\Users\\otzoy\\Desktop\\" + nombre + "." + extension))
				{
					outputFile.Write(texto.ToString());
				}
			}
			catch (Exception)
			{
			}
			finally
			{
				try
				{
					if (outputFile != null)
					{
						outputFile.Close();
						if (abrir)
						{
							System.Diagnostics.Process.Start("C:\\Users\\otzoy\\Desktop\\" + nombre + "." + extension);
						}
					}
				}
				catch (Exception)
				{
				}
			}

		}
		public void GenerarYAbrir(String ext, String nombre, String texto, String extension, bool abrir)
		{
			StreamWriter outputFile = null;
			try
			{
				using (outputFile = new StreamWriter(ext +"\\"+ nombre + "." + extension))
				{
					outputFile.Write(texto.ToString());
				}
			}
			catch (Exception)
			{
			}
			finally
			{
				try
				{
					if (outputFile != null)
					{
						outputFile.Close();
						if (abrir)
						{
							System.Diagnostics.Process.Start(ext +"\\" + nombre + "." + extension);
						}
					}
				}
				catch (Exception)
				{
				}
			}

		}
		/// <summary>
		/// Lee un archivo .txt en la ruta URL
		/// </summary>
		/// <param name="URL"></param>
		/// <returns></returns>
		public String Leer(String URL)
		{
			//Prepara el archivo para su lectura
			StreamReader reader = null;
			//Se almacenar temporalmente una linea
			String texto = "";
			//Aquí se almacenara TODAS las lineas leídas :v
			String retorno = "";
			//El sistema intentará escribir un string
			try
			{
				//Se instancia el archivo para su lectura
				reader = new StreamReader(URL);
				//Mientras el texto no sea nulo
				while ((texto = reader.ReadLine()) != null)
				{
					//Agrega texto a la cadena a retornar
					retorno += texto + Environment.NewLine;
				}
			}
			catch (Exception)
			{
				//Ocurre un error al leer el archivo
			}
			finally
			{
				//Intentará cerrar la lectura del archivo
				try
				{
					//Verifica que se haya instanciado el StreamReader
					if (reader != null)
					{
						//Cierra la lectura del archivo
						reader.Close();
					}
				}
				catch (Exception)
				{
					//Ocurre un error al cerrar el archivo
				}
			}
			//Retorna la cadena de texto, con o sin errores en ella
			return retorno;
		}
		public void Leer(String URL, System.Windows.Forms.RichTextBox txtbox)
		{
			//Prepara el archivo para su lectura
			StreamReader reader = null;
			//Se almacenar temporalmente una linea
			String texto = "";
			//Aquí se almacenara TODAS las lineas leídas :v
			String retorno = "";
			//El sistema intentará escribir un string
			if (File.Exists(URL))
			{
				try
				{
					//Se instancia el archivo para su lectura
					reader = new StreamReader(URL);
					//Mientras el texto no sea nulo
					while ((texto = reader.ReadLine()) != null)
					{
						//Agrega texto a la cadena a retornar
						retorno += texto + Environment.NewLine;
					}
				}
				catch (Exception)
				{
					//Ocurre un error al leer el archivo
				}
				finally
				{
					//Intentará cerrar la lectura del archivo
					try
					{
						//Verifica que se haya instanciado el StreamReader
						if (reader != null)
						{
							//Cierra la lectura del archivo
							reader.Close();
						}
					}
					catch (Exception)
					{
						//Ocurre un error al cerrar el archivo
					}
				}
				//Retorna la cadena de texto, con o sin errores en ella
				txtbox.Text = retorno; 
			}
		}
		/// <summary>
		/// Crea las carpetas en el url inicial url0 con las carpetas url1
		/// </summary>
		/// <param name="url0"></param>
		/// <param name="url1"></param>
		/// <returns></returns>
		public string CrearDirectorio(String url0, String url1)
		{
			DirectoryInfo dd = null;
			if (Directory.Exists(url0))
			{
				dd = Directory.CreateDirectory(url0+url1);
			}
			return dd.FullName;
		}
		/// <summary>
		/// Genera la instrucción de graphviz
		/// </summary>
		/// <param name="instruccion"></param>
		/// <returns></returns>
		private string ConstruirInstruccion(Queue<string> instruccion)
		{
			StringBuilder stream = new StringBuilder();

			stream.Append("digraph G {" + Environment.NewLine);
			stream.Append("graph[ dpi = 300 ]" + Environment.NewLine);

			while (instruccion.Count > 0)
			{
				stream.Append(instruccion.Dequeue());
			}

			stream.Append("}");

			return stream.ToString();
		}
		/// <summary>
		/// Crea el gráfico de la instrucción dada en la carpeta exteral
		/// </summary>
		/// <param name="instruccionGraphviz"></param>
		/// <returns></returns>
		private string Graficar(string instruccionGraphviz)
		{
			//String executable = @".\External\dot.exe";
			//String output = @".\External\" + Guid.NewGuid().ToString();

			File.WriteAllText(this.output,instruccionGraphviz);

			System.Diagnostics.Process process = new System.Diagnostics.Process();

			process.StartInfo.RedirectStandardOutput = true;
			process.StartInfo.UseShellExecute = false;
			process.StartInfo.CreateNoWindow = true;

			process.StartInfo.FileName = executable;
			process.StartInfo.Arguments = String.Format(@"{0} -Tjpg -O", output);

			process.Start();
			process.WaitForExit();

			File.Delete(output);

			return Path.GetFileName(output) + ".jpg";
		}
		/// <summary>
		/// Genera el string del HTML a generar
		/// </summary>
		/// <param name="imageURL"></param>
		/// <returns></returns>
		private String EscribirHTML(string imageURL)
		{
			//Ayudará a crear el string a retornar
			StringBuilder htmlBuilder = new StringBuilder();
			//Crea un html
			htmlBuilder.Append("<!DOCTYPE html>");
			htmlBuilder.Append("<html>");
			//Crea un css
			htmlBuilder.Append("<style type=\"text / css\">");
			////htmlBuilder.Append(".token  {border-collapse:collapse;border-spacing:0;border-color:#aaa;margin:0px auto;}");
			htmlBuilder.Append(@".tg  {border-collapse:collapse;border-spacing:0;margin:0px auto;}");
			htmlBuilder.Append(@".tg td{font-family:Arial, sans-serif;font-size:14px;padding:10px 5px;border-style:solid;border-width:1px;overflow:hidden;word-break:normal;border-color:black;}");
			htmlBuilder.Append(@".tg th{font-family:Arial, sans-serif;font-size:14px;font-weight:normal;padding:10px 5px;border-style:solid;border-width:1px;overflow:hidden;word-break:normal;border-color:black;}");
			htmlBuilder.Append(@".tg .tg-fk2z{font-weight:bold;background-color:#34cdf9;border-color:inherit;text-align:center;vertical-align:top}");
			htmlBuilder.Append(@".tg .tg-us36{border-color:inherit;text-align:center;vertical-align:top}");
			htmlBuilder.Append(@".tg .tg-2b1a{background-color:#bbdaff;border-color:inherit;text-align:center;vertical-align:top}");
			htmlBuilder.Append(@".tg .tg-5mgg{font-weight:bold;background-color:#c0c0c0;text-align:center;vertical-align:top}");
			htmlBuilder.Append(@".tg .tg-yw4l{vertical-align:top,text-align:center;}");
			htmlBuilder.Append(@"img {max-width:auto;max-height:auto;}");
			//			htmlBuilder.Append(@);
			htmlBuilder.Append("</style>");
			//Permite la utilización de acentos utf-8
			htmlBuilder.Append("<head>");
			htmlBuilder.Append("<meta charset =\"UTF-8\">");
			htmlBuilder.Append("<title>");
			htmlBuilder.Append("Salida-" + Guid.NewGuid().ToString());
			htmlBuilder.Append("</title>");
			htmlBuilder.Append("</head>");
			htmlBuilder.Append("<body >");
			htmlBuilder.Append("<tr>" +
							  "<th class= \"tg-5mgg\" colspan=\"2\">ESTRUCTURA<br></th>" +
							  "</tr>");
			htmlBuilder.Append("<tr>" +
							  "<td class=\"tg-yw41\">" + "<img src=\"" + imageURL + "\" alt =  \" " + Guid.NewGuid().ToString() + "\" >" + "</td>" +
							  "</tr>");
			htmlBuilder.Append("</body>");
			htmlBuilder.Append("</html>");
			//Crea un string de todo lo escrito anteriormente
			return htmlBuilder.ToString();
		}

		public void HacerArbol(Queue<string> instruccion)
		{
			var aux01 = this.ConstruirInstruccion(instruccion);
			var aux02 = this.Graficar(aux01);
			var aux03 = this.EscribirHTML(aux02);

			GenerarYAbrir(@".\External","Salida" + Guid.NewGuid().ToString(),aux03,"html",true);

		}
	}
}
//		public String EscribirHTML(Queue execute)
		//		{
		//			//Ayudará a crear el string a retornar
		//			StringBuilder htmlBuilder = new StringBuilder();
		//			//Crea un html
		//			htmlBuilder.Append("<!DOCTYPE html>");
		//			htmlBuilder.Append("<html>");
		//			//Crea un css
		//			htmlBuilder.Append("<style type=\"text / css\">");
		//			////htmlBuilder.Append(".token  {border-collapse:collapse;border-spacing:0;border-color:#aaa;margin:0px auto;}");
		//			htmlBuilder.Append(@".tg  {border-collapse:collapse;border-spacing:0;margin:0px auto;}");
		//			htmlBuilder.Append(@".tg td{font-family:Arial, sans-serif;font-size:14px;padding:10px 5px;border-style:solid;border-width:1px;overflow:hidden;word-break:normal;border-color:black;}");
		//			htmlBuilder.Append(@".tg th{font-family:Arial, sans-serif;font-size:14px;font-weight:normal;padding:10px 5px;border-style:solid;border-width:1px;overflow:hidden;word-break:normal;border-color:black;}");
		//			htmlBuilder.Append(@".tg .tg-fk2z{font-weight:bold;background-color:#34cdf9;border-color:inherit;text-align:center;vertical-align:top}");
		//			htmlBuilder.Append(@".tg .tg-us36{border-color:inherit;text-align:center;vertical-align:top}");
		//			htmlBuilder.Append(@".tg .tg-2b1a{background-color:#bbdaff;border-color:inherit;text-align:center;vertical-align:top}");
		//			htmlBuilder.Append(@".tg .tg-5mgg{font-weight:bold;background-color:#c0c0c0;text-align:center;vertical-align:top}");
		//			htmlBuilder.Append(@".tg .tg-yw4l{vertical-align:top,text-align:center;}");
		//			htmlBuilder.Append(@"img {max-width:auto;max-height:auto;}");
		////			htmlBuilder.Append(@);
		//			htmlBuilder.Append("</style>");
		//			//Permite la utilización de acentos utf-8
		//			htmlBuilder.Append("<head>");
		//			htmlBuilder.Append("<meta charset =\"UTF-8\">");
		//			htmlBuilder.Append("<title>");
		//			htmlBuilder.Append("Salida-"+Guid.NewGuid().ToString());
		//			htmlBuilder.Append("</title>");
		//			htmlBuilder.Append("</head>");
		//			htmlBuilder.Append("<body >");

		//			while (!execute.EsVacio())
		//			{
		//				Console.WriteLine(execute.VerPrimero());
		//				if (execute.VerPrimero()!=null)
		//				{
		//					Result temp = (Result)execute.Desencolar();
		//					htmlBuilder.Append(temp.GetTable());
		//				}
		//				else
		//				{
		//					execute.Desencolar();
		//				}
		//			}
		//			htmlBuilder.Append("</body>");
		//			htmlBuilder.Append("</html>");
		//			//Crea un string de todo lo escrito anteriormente
		//			return htmlBuilder.ToString();
		//		}
		//		/// <summary>
		//		/// Escribe el HTML de reporte de Tokens
		//		/// </summary>
		//		/// <param name="tokens"></param>
		//		/// <returns></returns>
		//public String ReportarTokens(Queue<Token> token)
		//{
		//	//Realiza una referencia a la pila
		//	//colaEntrada = token;
		//	//Prerpara las pilas de tokens y errores
		//	//DepurarTokens();
		//	//Aquí se alojará el string final a retornar
		//	//string htmlString = "";
		//	//Ayudará a crear el string a retornar
		//	StringBuilder htmlBuilder = new StringBuilder();
		//	//Crea un html
		//	htmlBuilder.Append("<!DOCTYPE html>");
		//	htmlBuilder.Append("<html>");
		//	//Crea un css
		//	htmlBuilder.Append("<style type=\"text / css\">");
		//	htmlBuilder.Append(".token  {border-collapse:collapse;border-spacing:0;border-color:#aaa;margin:0px auto;}");
		//	htmlBuilder.Append(".token td{font-family:Arial, sans-serif;font-size:14px;padding:10px 5px;border-style:solid;border-width:0px;overflow:hidden;word-break:normal;border-color:#aaa;color:#333;background-color:#fff;border-top-width:1px;border-bottom-width:1px;}");
		//	htmlBuilder.Append(".token th{font-family:Arial, sans-serif;font-size:14px;font-weight:normal;padding:10px 5px;border-style:solid;border-width:0px;overflow:hidden;word-break:normal;border-color:#aaa;color:#fff;background-color:#f38630;border-top-width:1px;border-bottom-width:1px;}");
		//	htmlBuilder.Append(".err  {border-collapse:collapse;border-spacing:0;border-color:#999;margin:0px auto;}");
		//	htmlBuilder.Append(".err td{font-family:Arial, sans-serif;font-size:14px;padding:10px 5px;border-style:solid;border-width:0px;overflow:hidden;word-break:normal;border-color:#999;color:#444;background-color:#F7FDFA;border-top-width:1px;border-bottom-width:1px;}");
		//	htmlBuilder.Append(".err th{font-family:Arial, sans-serif;font-size:14px;font-weight:normal;padding:10px 5px;border-style:solid;border-width:0px;overflow:hidden;word-break:normal;border-color:#999;color:#fff;background-color:#26ADE4;border-top-width:1px;border-bottom-width:1px;}");
		//	htmlBuilder.Append(".tg-f4we{font-weight:bold;font-size:15px;font-family:Arial, Helvetica, sans-serif !important;;text-align:center;vertical-align:top}");
		//	htmlBuilder.Append(".tg-cjcd{font-style:italic;font-size:12px;font-family:Arial, Helvetica, sans-serif !important;;vertical-align:top}");
		//	htmlBuilder.Append(".tg-do2s{font-size:12px;font-family:Arial, Helvetica, sans-serif !important;;vertical-align:top}");
		//	htmlBuilder.Append("</style>");
		//	//Permite la utilización de acentos utf-8
		//	htmlBuilder.Append("<head>");
		//	htmlBuilder.Append("<meta charset =\"UTF-8\">");
		//	htmlBuilder.Append("<title>");
		//	htmlBuilder.Append("Reporte-");
		//	htmlBuilder.Append(Guid.NewGuid().ToString());
		//	htmlBuilder.Append("</title>");
		//	htmlBuilder.Append("</head>");
		//	htmlBuilder.Append("<body>");
		//	///
		//	///Inicia tabla de TOKEN
		//	///
		//	htmlBuilder.Append("<table class= \"token\">");
		//	htmlBuilder.Append("<tr>" +
		//						"<th class=\"tg-f4we\" colspan=\"5\">TOKEN</th> " +
		//					   "</tr>");
		//	htmlBuilder.Append("<tr>" +
		//						"<td class=\"tg-cjcd\">#<br></td>" +
		//						"<td class=\"tg-cjcd\">LEXEMA<br></td>" +
		//						"<td class=\"tg-cjcd\">FILA</td>" +
		//						"<td class=\"tg-cjcd\">COLUMNA</td>" +
		//						"<td class=\"tg-cjcd\">TOKEN</td>" +
		//					   "</tr>");
		//	int contador = 0;
		//	///Agrega los elementos de la lista a la tabla recién creada
		//	while (token.Count > 0)
		//	{
		//		Token cast = token.Dequeue();
		//		htmlBuilder.Append("<tr>");
		//		htmlBuilder.Append("<td class=\"tg-do2s\">" + cast.Id + "</td>");
		//		htmlBuilder.Append("<td class=\"tg-do2s\">" + cast.Lexema + "</td>");
		//		htmlBuilder.Append("<td class=\"tg-do2s\">" + cast.Fila + "</td>");
		//		htmlBuilder.Append("<td class=\"tg-do2s\">" + cast.Columna + "</td>");
		//		htmlBuilder.Append("<td class=\"tg-do2s\">" + cast.TToken + "</td>");
		//		htmlBuilder.Append("</tr>");
		//	}
		//	htmlBuilder.Append("<tr>" +
		//						"<th class=\"tg-cjcd\" colspan=\"5\"> ---------- Fin de línea. " + contador + " tokens ---------- </th> " +
		//					   "</tr>");
		//	htmlBuilder.Append("</table><br><br>");
		//	///
		//	///Inicia tabla de ERRORES
		//	///
		//	htmlBuilder.Append("<table class= \"err\">");
		//	htmlBuilder.Append("<tr>" +
		//						"<th class=\"tg-f4we\" colspan=\"5\">ERROR</th> " +
		//					   "</tr>");
		//	htmlBuilder.Append("<tr>" +
		//						"<td class=\"tg-cjcd\">#<br></td>" +
		//						"<td class=\"tg-cjcd\">DESCRIPCIÓN<br></td>" +
		//						"<td class=\"tg-cjcd\">FILA</td>" +
		//						"<td class=\"tg-cjcd\">COLUMNA</td>" +
		//						"<td class=\"tg-cjcd\">ERROR</td>" +
		//					   "</tr>");
		//	contador = 0;
		//	///Agrega los elementos de la lista a la tabla recién creada
		//	//while (!errores.EsVacio())
		//	//{
		//	//	Token cast = (Token)errores.Desencolar();
		//	//	htmlBuilder.Append("<tr>");
		//	//	htmlBuilder.Append("<td class=\"tg-do2s\">" + ++contador + "</td>");
		//	//	htmlBuilder.Append("<td class=\"tg-do2s\">" + cast.lexema + "</td>");
		//	//	htmlBuilder.Append("<td class=\"tg-do2s\">" + cast.fila + "</td>");
		//	//	htmlBuilder.Append("<td class=\"tg-do2s\">" + cast.columna + "</td>");
		//	//	htmlBuilder.Append("<td class=\"tg-do2s\">" + cast.id + "</td>");
		//	//	htmlBuilder.Append("</tr>");
		//	//}
		//	//htmlBuilder.Append("<tr>" +
		//	//		"<th class=\"tg-cjcd\" colspan=\"5\"> ---------- Fin de línea. " + contador + " errores ---------- </th> " +
		//	//	   "</tr>");
		//	//htmlBuilder.Append("</table><br><br>");
		//	htmlBuilder.Append("</body>");
		//	htmlBuilder.Append("</html>");
		//	//Crea un string de todo lo escrito anteriormente
		//	return htmlBuilder.ToString();
		//}
