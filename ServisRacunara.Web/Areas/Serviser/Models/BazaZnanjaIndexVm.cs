using ServisRacunara.Data.MODELS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ServisRacunara.Web.Areas.Serviser.Models
{
    public class BazaZnanjaIndexVm
    {
        public int ServisniNalogId { get; set; }

        public List<KvaroviStavke> Kvarovi { get; set; }
    }

    public class KvaroviStavke
    {
        public int KvarId { get; set; }
        public string Opis { get; set; }
        public bool Hardware { get; set; }
        public bool Software { get; set; }
    }
}