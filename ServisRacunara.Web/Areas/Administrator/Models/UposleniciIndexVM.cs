using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServisRacunara.Data.DAL;
using ServisRacunara.Data.MODELS;

namespace ServisRacunara.Web.Areas.Administrator.Models
{
    public class UposleniciIndexVM
    {
        //public List<KorisnikRowVM> Uposlenici { get; set; }
        public List<KorisnikRowVM> Uposleni { get; set; }

        public string searchQuery { get; set; }
       
    }

    public class KorisnikRowVM
    {
        public int UposlenikId { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string Telefon { get; set; }
        public string Adresa { get; set; }
        public string Administrator { get; set; }
        public string Serviser { get; set; }
        public string Prodavac { get; set; }
    }
}