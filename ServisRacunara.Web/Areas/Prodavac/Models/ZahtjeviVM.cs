using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServisRacunara.Web.Areas.Prodavac.Models
{
    public class ZahtjeviVM
    {
        public int ZahtjevZaServisId { get; set; }

        public DateTime Datum { get; set; }

        public string Napomena { get; set; }

        public bool ServisNaAdresi { get; set; }

        public bool Preuzimanje { get; set; }

        public bool Isporuka { get; set; }

        public string Adresa { get; set; }

        public int KlijentId { get; set; }
        
        public string ImePrezime { get; set; }
    }
}