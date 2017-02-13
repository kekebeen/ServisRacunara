using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServisRacunara.Data.MODELS
{
    public class ZahtjevZaServis
    {
        public int ZahtjevZaServisId { get; set; }
        
        public DateTime Datum { get; set; }

        public string Napomena { get; set; }

        public bool ServisNaAdresi { get; set; }

        public bool Preuzimanje { get; set; }

        public bool Isporuka { get; set; }

        public string Adresa { get; set; }

        public int KlijentId { get; set; }
        public virtual Korisnik Klijent { get; set; }

    }
}
