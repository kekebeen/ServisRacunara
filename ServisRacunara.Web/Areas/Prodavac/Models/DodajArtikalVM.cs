using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;


namespace ServisRacunara.Web.Areas.Prodavac.Models
{
    public class DodajArtikalVM
    {
        public List<SelectListItem> ListaArtikala { get; set; }

        public int RacunId { get; set; }
        
        public int ProizvodId { get; set; }

    }
}