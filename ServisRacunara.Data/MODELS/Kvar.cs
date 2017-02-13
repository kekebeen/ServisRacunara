using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServisRacunara.Data.MODELS
{
    public class Kvar
    {
        public int KvarId { get; set; }
        public string Opis { get; set; }
        public bool Hardware { get; set; }
        public bool Software { get; set; }
    }
}
