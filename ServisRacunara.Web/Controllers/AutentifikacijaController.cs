using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ServisRacunara.Data.DAL;
using ServisRacunara.Data.MODELS;
using ServisRacunara.Web.Helper;


namespace ServisRacunara.Web.Controllers
{
    public class AutentifikacijaController : Controller
    {
        private MojContext ctx = new MojContext();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Provjera(string username, string password, string zapamti)
        {
            Korisnik k = ctx.Korisnici.SingleOrDefault(x => x.KorisnickoIme == username && x.Lozinka == password);
            

            if (k == null)
            {
                return RedirectToAction("Index");
            }

            Autentifikacija.PokreniNovuSesiju(k, HttpContext, (zapamti == "on"));

            return RedirectToAction("Index", "Home");
        }

        public ActionResult Logout()
        {
            Autentifikacija.PokreniNovuSesiju(null, HttpContext, true);
            return RedirectToAction("Index");
        }
    }
}
