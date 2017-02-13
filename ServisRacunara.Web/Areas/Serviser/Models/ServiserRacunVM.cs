using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServisRacunara.Web.Areas.Serviser.Models
{

    public class ServiserRacunStavka
    {
        public int StavkaRacunaId { get; set; }
        public string Naziv { get; set; }

        public float Cijena { get; set; }

        public bool Usluga { get; set; }

        public string Opis { get; set; }

        public int ProizvodId { get; set; }


    }

    public class ServiserRacunVM
    {
        public List<ServiserRacunStavka> Stavke { get; set; }

        public int RacunId { get; set; }

        public int ServisniNalogId { get; set; }

        public DateTime DatumKreiranja { get; set; }
        public DateTime DatumIzdavanja { get; set; }


        public float Iznos { get; set; }

        public string ImePrezime { get; set; }

        public string Adresa { get; set; }

        public string Telefon { get; set; }

        public string OznakaRacunara { get; set; }

    }
}