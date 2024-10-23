using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParcheggioE80.Models
{
    internal class Parcheggio
    {
		private string _nome;

		public string Nome
		{
			get { return _nome; }
		}

		private string _indirizzo;

		public string Indirizzo
		{
			get { return _indirizzo; }
		}

		private int _posti;

		public int Posti
		{
			get { return _posti; }
		}

		private double _tariffaOrariaBase;

		public double TariffaOrariaBase
		{
			get { return _tariffaOrariaBase; }
		}

        public Parcheggio(string nome, string indirizzo, int posti, double tariffaOrariaBase)
        {
            _nome = nome;
			_indirizzo = indirizzo;
			_posti = posti;
			_tariffaOrariaBase = tariffaOrariaBase;
        }
    }
}
