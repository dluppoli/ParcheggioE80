using ParcheggioE80.Controllers;
using ParcheggioE80.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParcheggioE80
{
    internal class Program
    {
        static void Main(string[] args)
        {
            VeicoliController controller = new VeicoliController();
            Parcheggio parcheggio = controller.getInfoParcheggio();
            string scelta = "";

            do
            {
                Console.Clear();
                Console.WriteLine($"Gestione pargheggio {parcheggio.Nome}");
                Console.WriteLine(parcheggio.Indirizzo);
                Console.WriteLine($"{parcheggio.Posti} posti - {parcheggio.TariffaOrariaBase} euro/ora");
                Console.WriteLine();
                Console.WriteLine("1 - Stampa veicoli presenti");
                Console.WriteLine("2 - Stampa numero di veicoli presenti");
                Console.WriteLine("3 - Entrata nuovo veicolo");
                Console.WriteLine("4 - Uscita veicolo");
                Console.WriteLine("5 - Ricerca veicolo");
                Console.WriteLine("9 - Uscita");
                Console.WriteLine();

                Console.Write("Inserire la scelta: ");
                scelta = Console.ReadLine();

                List<Veicolo> veicoli;
                switch (scelta)
                {
                    case "1":
                        veicoli = controller.GetPresenti();
                        if (veicoli.Count == 0)
                            Console.WriteLine("Nessun veicolo presente");
                        else
                            foreach (Veicolo v in veicoli) Console.WriteLine(v);
                        break;
                    case "2":
                        Console.WriteLine($"Sono presenti n.{controller.GetNumeroPresenti()} veicoli");
                        break;
                    case "3":
                        switch(controller.Entrata(ConsoleRead("Inserire la targa")))
                        {
                            case EntrataVeicoloResult.Ok:
                                Console.WriteLine("Veicolo entrato con successo");
                                break;
                            case EntrataVeicoloResult.VeicoloPresente:
                                Console.WriteLine("Veicolo già in sosta");
                                break;
                            case EntrataVeicoloResult.ParcheggioPieno:
                                Console.WriteLine("Parcheggio pieno");
                                break;
                            default:
                                Console.WriteLine("Errore nell'entrata del veicolo");
                                break;
                        }
                        break;
                    case "4":
                        Veicolo uscente = controller.Uscita(ConsoleRead("Inserire la targa"));
                        if (uscente == null) 
                            Console.WriteLine("Veicolo non trovato");
                        else
                            Console.WriteLine($"Veicolo uscito con successo. Deve pagare {uscente.Importo}");
                        break;
                    case "5":
                        veicoli = controller.GetSoste(ConsoleRead("Inserire la targa"));
                        if (veicoli.Count == 0)
                            Console.WriteLine("Veicolo non trovato");
                        else
                            foreach (Veicolo v in veicoli) Console.WriteLine(v);
                        break;
                    case "9":
                        Console.WriteLine( "Arrivederci");
                        break;
                    default:
                        break;
                }
                Console.WriteLine("Premi invio per continuale");
                Console.ReadLine();
            } while (scelta != "9");
        }

        public static string ConsoleRead(string prompt)
        {
            Console.Write($"{prompt} ");
            return Console.ReadLine();
        }
    }
}
