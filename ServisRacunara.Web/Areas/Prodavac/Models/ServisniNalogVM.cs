using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServisRacunara.Data.MODELS;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ServisRacunara.Web.Areas.Prodavac.Models
{
    public class ServisniNalogVM
    {
        public int ServisniNalogId { get; set; }

        public bool Dostava { get; set; }

        public bool Preuzimanje { get; set; }

        public bool Isporuka { get; set; }


        public bool ServisNaAdresi { get; set; }


        public string Napomena { get; set; }

        public int KlijentId { get; set; }
        public string  ImeKlijenta { get; set; }
        public string  PrezimeKlijenta { get; set; }

        public string KorisnickoIme { get; set; }

        public string email { get; set; }
        public string  AdresaKlijenta { get; set; }
        public string TelefonKlijenta { get; set; }
        public int? ZahtjevZaServisId { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Obavezno odabrati")]
        public int RacunarId { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Obavezno odabrati")]
        public int KvarId { get; set; }

        public List<SelectListItem> Racunari { get; set; }

        public List<SelectListItem> Korisnici { get; set; }


        public List<SelectListItem> Kvarovi { get; set; }
        

    }
}