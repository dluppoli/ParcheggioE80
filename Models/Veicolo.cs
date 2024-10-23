using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParcheggioE80.Models
{
    internal class Veicolo
    {
		private string _targa;

		public string Targa
		{
			get { return _targa; }
		}

		private DateTime _ingresso;

		public DateTime Ingresso
		{
			get { return _ingresso; }
		}

		private DateTime? _uscita;

		public DateTime? Uscita
		{
			get { return _uscita; }
			set { 
				if (_uscita == null) _uscita = value; 
			}
		}

		private double? _importo;

		public double? Importo
		{
			get { return _importo; }
            set { 
				if( _importo == null) _importo = value; 
			}
        }

        public Veicolo(string targa)
        {
            _targa = targa.ToUpper();
			_ingresso = DateTime.Now;
			_uscita = null;
			_importo = null;
        }

		public override string ToString()
		{
			string risultato = $"{_targa} - Entrata: {_ingresso.ToLongDateString()} ";
			if (_uscita != null)
				risultato += $"- Uscita: {((DateTime)_uscita).ToLongDateString()} ";
            if (_importo != null)
                risultato += $"- Importo: {_importo} euro";
			return risultato;
        }

    }
}
