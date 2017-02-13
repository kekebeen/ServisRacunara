using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ServisRacunara.Web.Areas.Serviser.Models
{
    public class ServiserDodajArtikalVM
    {
        public List<SelectListItem> ListaArtikala { get; set; }

        public int RacunId { get; set; }

        public int ProizvodId { get; set; }
    }
}