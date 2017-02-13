using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServisRacunara.Data.MODELS
{
    public class ServisniNalog
    {
        public int ServisniNalogId { get; set; }

        public DateTime DatumPrijema { get; set; }

        public bool Isporuka { get; set; }

        public bool ServisNaAdresi { get; set; }

        public bool Preuzimanje { get; set; }
        
        public string Napomena { get; set; }

        public DateTime? DatumZavrsetka { get; set; }

        public int StatusId { get; set; }
        public virtual Status Status { get; set; }

        public int KlijentId { get; set; }
        public virtual Korisnik Klijent { get; set; }

        public int NalogKreiraoId { get; set; }
        [ForeignKey(nameof(NalogKreiraoId))]
        public virtual Uposlenik NalogKreirao { get; set; }

        public int? ZahtjevZaServisId { get; set; }

        public virtual ZahtjevZaServis ZahtjevZaServis { get; set; }

        public string UtisakKlijenta { get; set; }

    }
}
