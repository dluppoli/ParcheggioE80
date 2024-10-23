using ParcheggioE80.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParcheggioE80.Controllers
{
    internal class VeicoliController
    {
        private List<Veicolo> veicoli;
        private Parcheggio parcheggio;

        public VeicoliController()
        {
            veicoli = new List<Veicolo>();
            parcheggio = new Parcheggio("Reggio Emilia FS", "Via della stazione 1", 100, 2);
        }

        public Parcheggio getInfoParcheggio()
        { 
            return parcheggio; 
        }

        public List<Veicolo> GetPresenti() { 
            List<Veicolo> risultati = new List<Veicolo>();

            foreach (Veicolo veicolo in veicoli)
            {
                if (veicolo.Uscita == null) risultati.Add(veicolo);
            }
            return risultati;
        }

        private Veicolo GetPresente(string targa)
        {
            foreach( Veicolo veicolo in GetPresenti() )
            {
                if( veicolo.Targa == targa.ToUpper() ) return veicolo;
            }
            return null;
        }

        public int GetNumeroPresenti()
        {
            return GetPresenti().Count;
        }

        public EntrataVeicoloResult Entrata(string targa)
        {
            if (GetNumeroPresenti() == parcheggio.Posti) return EntrataVeicoloResult.ParcheggioPieno;
            if( GetPresente(targa) != null ) return EntrataVeicoloResult.VeicoloPresente;

            veicoli.Add(new Veicolo(targa));
            return EntrataVeicoloResult.Ok;
        }

        public Veicolo Uscita(string targa)
        {
            Veicolo v = GetPresente(targa);
            if(v==null) return null;

            v.Uscita = DateTime.Now;
            TimeSpan timeSpan = (DateTime)v.Uscita -  v.Ingresso;
            int quartiOra = (int)Math.Ceiling(timeSpan.TotalMinutes / 15);
            double tariffaQuartoOraBase = parcheggio.TariffaOrariaBase / 4;
            double importo = 0;
            int tariffaPiena = 0;
            int tariffaSconto30 = 0;
            int tariffaSconto50 = 0;
            int tariffaSconto75 = 0;

            tariffaPiena = Math.Min(quartiOra, 48);
            quartiOra -= 48;
            importo = tariffaPiena * tariffaQuartoOraBase;

            if (quartiOra > 0)
            { 
                tariffaSconto30 = Math.Min(quartiOra, 48);
                importo += tariffaSconto30 * tariffaQuartoOraBase * 0.7;
                quartiOra -= 48;
            }
            if (quartiOra > 0)
            {
                tariffaSconto50 = Math.Min(quartiOra, 96);
                importo += tariffaSconto50 * tariffaQuartoOraBase * 0.5;
                quartiOra -= 96;
            }
            if (quartiOra > 0)
            {
                tariffaSconto75 = quartiOra;
                importo += tariffaSconto75 * tariffaQuartoOraBase * 0.25;
            }
            v.Importo = Math.Round(importo,2);
            return v;
        }

        public List<Veicolo> GetSoste(string targa)
        {
            List<Veicolo> risultati = new List<Veicolo>();

            foreach (Veicolo veicolo in veicoli)
            {
                if (veicolo.Targa == targa.ToUpper()) risultati.Add(veicolo);
            }
            return risultati;
        }
    }
}
