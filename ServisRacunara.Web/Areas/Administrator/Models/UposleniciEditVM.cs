using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ServisRacunara.Web.Areas.Administrator.Models
{
    public class UposleniciEditVM
    {
        public int? UposlenikId { get; set; }
        public int KorisnikId { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string Lozinka { get; set; }
        public string KorisnickoIme { get; set; }
        public string Adresa { get; set; }
        public string Email { get; set; }
        public string Telefon { get; set; }
        public bool Klijent { get; set; }
        public DateTime UgovorPocetak { get; set; }
        public DateTime? UgovorKraj { get; set; }
        public bool? Administrator { get; set; }
        public bool? Serviser { get; set; }
        public bool? Prodavac { get; set; }
        public int plataId { get; set; }
        public int isplataId { get; set; }
        public float iznos { get; set; }




    }
}