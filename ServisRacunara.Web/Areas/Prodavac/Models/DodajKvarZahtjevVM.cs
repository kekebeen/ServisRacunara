using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServisRacunara.Web.Areas.Prodavac.Models
{
    public class DodajKvarZahtjevVM
    {
        public int ZahtjevZaServisId { get; set; }
        public string Opis { get; set; }
        public bool Hardware { get; set; }
        public bool Software { get; set; }

    }
}