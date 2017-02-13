using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServisRacunara.Data.DAL;
using ServisRacunara.Data.MODELS;

namespace ServisRacunara.Web.Areas.Administrator.Models
{
    public class AdministratorIndexVM
    {
        public int KlijentId { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public int BrojZaposlenih { get; set; }
        public int BrojKorisnika { get; set; }
        public int BrojZahtjeva { get; set; }
        public int BrojServisnihNaloga { get; set; }
        public float MjesecnaZaradaUkupno { get; set; }
        public List<ZahtjevZaServis> Zahtjevi { get; set; }
        public List<ServisniNalog> Nalozi { get; set; }
        public List<Data.MODELS.Uposlenik> Uposlenici { get; set; }
    }
}