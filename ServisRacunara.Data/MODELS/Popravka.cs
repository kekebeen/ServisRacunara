using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServisRacunara.Data.MODELS
{
    public class Popravka
    {
        public int PopravkaId { get; set; }

        public int KvarId { get; set; }
        public virtual Kvar Kvar { get; set; }

        public int RjesenjeId { get; set; }
        public virtual Rjesenje Rjesenje { get; set; }

    }
}
