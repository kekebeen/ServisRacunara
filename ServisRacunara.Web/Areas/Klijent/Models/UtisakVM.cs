using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServisRacunara.Web.Areas.Klijent.Models
{
    public class UtisakVM
    {
        public int ServisniNalogId { get; set; }

        public string Tekst { get; set; }

        public string OznakaRacunara { get; set; }
    }
}