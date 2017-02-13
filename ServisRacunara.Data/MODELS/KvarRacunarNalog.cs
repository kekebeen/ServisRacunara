using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServisRacunara.Data.MODELS
{
    public class KvarRacunarNalog
    {
        public int KvarRacunarNalogId { get; set; }

        public int KvarId { get; set; }
        public virtual Kvar Kvar { get; set; }

        public int RacunarId { get; set; }
        public virtual Racunar Racunar { get; set; }

        public int ServisniNalogId { get; set; }
        public virtual ServisniNalog ServiniNalog { get; set; }

    }
}
