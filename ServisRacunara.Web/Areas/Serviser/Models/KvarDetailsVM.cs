using ServisRacunara.Data.MODELS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServisRacunara.Web.Areas.Serviser.Models
{
    public class KvarDetailsVM
    {
        public int KvarId { get; set; }

        public List<RjesenjeStavke> Rjesenja { get; set; }
    }

    public class RjesenjeStavke
    {
        public int RjesenjeId { get; set; }
        public DateTime Datum { get; set; }
        public bool Hardware { get; set; }
        public bool Software { get; set; }
        public string Opis { get; set; }
        public int Trajanje { get; set; }
    }
}