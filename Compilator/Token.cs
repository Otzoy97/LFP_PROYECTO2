using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LFP_PROYECTO2.Compilator
{
	class Token
	{
		private int id;
		private String lexema;
		private int fila;
		private int columna;
		private String token;

		public Token(int id, string lexema, int fila, int columna, string token)
		{
			this.id = id;
			this.lexema = lexema;
			this.fila = fila;
			this.columna = columna;
			this.token = token;
		}

		public String Lexema
		{
			get
			{
				return this.lexema;
			}
		}
		public String TToken
		{
			get
			{
				return this.token;
			}
		}
		public int Fila
		{
			get
			{
				return this.fila;
			}
		}
		public int Columna
		{
			get
			{
				return this.columna;
			}
		}
		public int Id
		{
			get
			{
				return this.id;
			}
		}
	}
}
