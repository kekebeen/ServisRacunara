using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServisRacunara.Data.MODELS;
using ServisRacunara.Web.Areas.Prodavac.Models;

namespace ServisRacunara.Web.Areas.Prodavac.Models
{
    public class ProdavacIndexVM
    {
        public List<ZahtjeviVM> ZahtjeviZaServis { get; set; }
    }
}