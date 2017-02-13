using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServisRacunara.Web.Areas.Klijent.Models
{
    public class ServisniNaloziKlijentVM
    {
        public int ServisniNalogId { get; set; }
        public DateTime Datum { get; set; }

        public string OznakaRacunara { get; set; }

        public string OpisRacunara { get; set; }

        public string Status { get; set; }

        public DateTime? DatumZavrsetka { get; set; }

        public string Utisak { get; set; }

    }
}