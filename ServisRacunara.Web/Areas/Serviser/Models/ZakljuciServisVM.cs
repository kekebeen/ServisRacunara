using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServisRacunara.Web.Areas.Serviser.Models
{
    public class ZakljuciServisVM
    {
        public int ServisniNalogId { get; set; }
        public int NalogKreiraoId { get; set; }

        public DateTime DatumZavrsetka { get; set; }
        public int StatusId { get; set; }
        public string Naziv { get; set; }
    }
}