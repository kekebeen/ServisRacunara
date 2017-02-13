using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServisRacunara.Data.MODELS;

namespace ServisRacunara.Web.Areas.Klijent.Models
{
    public class KvarRacunarVM
    {
        public int RacunarId { get; set; }

        public List<Racunar> Racunari { get; set; }



    }
}