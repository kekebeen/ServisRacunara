using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServisRacunara.Data.MODELS
{
    public class ServisPreuzeo
    {
        public int ServisPreuzeoId { get; set; }

        public int UposlenikId { get; set; }
        public virtual Uposlenik Uposlenik { get; set; }

        public int ServisniNalogId { get; set; }
        public virtual ServisniNalog ServisniNalog { get; set; }

        public DateTime DatumPreuzimanja { get; set; }
        
    }
}
