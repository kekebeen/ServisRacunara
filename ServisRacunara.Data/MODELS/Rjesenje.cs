using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServisRacunara.Data.MODELS
{
    public class Rjesenje
    {
        public int RjesenjeId { get; set; }
        public bool Hardware { get; set; }
        public bool Software { get; set; }

        public int Trajanje { get; set; }

        public DateTime Datum { get; set; }

        public string Opis { get; set; }

        public int UposlenikId { get; set; }
        public virtual Uposlenik Uposlenik { get; set; }

    }
}
