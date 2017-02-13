using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ServisRacunara.Web.Areas.Prodavac.Models
{
    public class PregledStavke
    {
        public int ServisniNalogId { get; set; }
        

        public bool Preuzimanje { get; set; }

        public bool Isporuka { get; set; }

        public bool ServisNaAdresi { get; set; }

        public string Napomena { get; set; }

        public int KlijentId { get; set; }
        public string ImeKlijenta { get; set; }
        public string PrezimeKlijenta { get; set; }
        public int RacunarId { get; set; }
        public string OznakaRacunara { get; set; }

        public int KvarId { get; set; }

        public string OpisKvara { get; set; }

        public string Status { get; set; }

        public DateTime DatumKreiranja { get; set; }

        public int RacunId { get; set; }

        public int ServiserId { get; set; }

        public string ServiserIme { get; set; }

        public DateTime ServiserPreuzeo { get; set; }

    }
    public class PregledServisaVM
    {
        
        public List<PregledStavke> Stavke { get; set; }

        public List<SelectListItem> Serviseri { get; set; }

        public int ServiserId { get; set; }

        public string ImePrezimeKlijenta { get; set; }
    }

}