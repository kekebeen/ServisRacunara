using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServisRacunara.Data.MODELS
{
    public class Racun
    {
        public int RacunId { get; set; }

        public int ServisniNalogId { get; set; }
        public virtual ServisniNalog ServisniNalog { get; set; }

        public DateTime DatumKreiranja { get; set; }

        public float Iznos { get; set; }

        public DateTime? DatumIzdavanja { get; set; }

    }
}
