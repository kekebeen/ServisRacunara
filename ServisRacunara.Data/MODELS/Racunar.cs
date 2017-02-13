using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServisRacunara.Data.MODELS
{
    public class Racunar
    {
        public int RacunarId { get; set; }

        public string Oznaka { get; set; }

        public string Opis { get; set; }
        public string OS { get; set; }


        public int VlasnikId { get; set; }
        [ForeignKey(nameof(VlasnikId))]
        public virtual Korisnik Vlasnik { get; set; }

    }
}
