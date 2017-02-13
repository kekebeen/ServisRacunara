using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServisRacunara.Web.Areas.Serviser.Models
{
    public class ServiserDodajProizvodVM
    {
        public int ProizvodId { get; set; }
        public int RacunId { get; set; }
        public string Naziv { get; set; }
        public float Cijena { get; set; }
        public string Opis { get; set; }
        public bool Usluga { get; set; }
    }
}