using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ServisRacunara.Web.Areas.Prodavac.Models
{
    public class DodajRacunarZahtjevVM
    {
        public string Oznaka { get; set; }
        
        public string Opis { get; set; }
        
        public string OS { get; set; }


        public int VlasnikId { get; set; }

        public int ZahtjevZaServisId { get; set; }
    }
}