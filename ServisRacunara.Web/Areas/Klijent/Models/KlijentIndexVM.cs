using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServisRacunara.Data.DAL;
using ServisRacunara.Data.MODELS;



namespace ServisRacunara.Web.Areas.Klijent.Models
{
    public class KlijentIndexVM
    {
        public int KlijentId { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }

        public Racunar Racunar { get; set; }
        public List<ZahtjevZaServis> ZahtjeviZaServis { get; set; }
        
        public List<ServisniNaloziKlijentVM> ServisniNalozi { get; set; }

    }
}