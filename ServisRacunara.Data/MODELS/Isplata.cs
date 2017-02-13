using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServisRacunara.Data.MODELS
{
    public class Isplata
    {
        public int IsplataId { get; set; }

        public int PlataId { get; set; }
        public virtual Plata Plata { get; set; }

        public int UposlenikId { get; set; }
        public virtual Uposlenik Uposlenik { get; set; }

    }
}
