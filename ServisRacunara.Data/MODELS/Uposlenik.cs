using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServisRacunara.Data.DAL;

namespace ServisRacunara.Data.MODELS
{
  
    public class Uposlenik {
        public int UposlenikId { get; set; }
        public int KorisnikId { get; set; }
        public virtual Korisnik Korisnik { get; set; }
        public DateTime UgovorPocetak { get; set; }
        public DateTime? UgovorKraj { get; set; }
        public bool? Administrator { get; set; }
        public bool? Serviser { get; set; }
        public bool? Prodavac { get; set; }
    }
}
