using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServisRacunara.Data.MODELS
{
    public class Korisnik
    {
        public int Id { get; set; }
        public string KorisnickoIme { get; set; }
        public string Lozinka { get; set; }
        
        public string Ime { get; set; }
        public string Prezime { get; set; }

        public string Adresa { get; set; }

        public string Email { get; set; }

        public string Telefon { get; set; }

        public bool Klijent { get; set; }

        public List<Uposlenik> Uposlenja { get; set; }
        public virtual Uposlenik Uposlenjaa { get; set; }
        
    }
}
