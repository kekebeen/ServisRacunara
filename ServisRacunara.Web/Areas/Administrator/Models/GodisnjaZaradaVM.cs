using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServisRacunara.Web.Areas.Administrator.Models
{
    public class GodisnjaZaradaVM
    {
        public string period { get; set; }
        public float zarada { get; set; }
        public float gubitak { get; set; }
    }
}