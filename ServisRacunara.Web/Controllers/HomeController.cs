using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ServisRacunara.Data.MODELS;
using ServisRacunara.Data.DAL;
using ServisRacunara.Web.Helper;


namespace ServisRacunara.Web.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        MojContext ctx = new MojContext();
        public ActionResult Index()
        {
            Korisnik k = Autentifikacija.GetLogiraniKorisnik(HttpContext);

            if (k == null)
                return RedirectToAction("Logout", "Autentifikacija", new { area = "" });

            
            bool uposlenik = (ctx.Uposlenici.Where(x => x.KorisnikId == k.Id).SingleOrDefault() != null);

            if (uposlenik)
            {
                k.Uposlenja = ctx.Uposlenici.Where(x => x.KorisnikId == k.Id).ToList();
            }

            if (uposlenik == true)
            {
                if (k.Uposlenja.Any(x => (bool)x.Administrator))
                {

                    return RedirectToAction("Index", "Home", new { area = "Administrator" });

                }
                if (k.Uposlenja.Any(x => (bool)x.Prodavac))
                {
                    return RedirectToAction("Index", "Home", new { area = "Prodavac" });
                }
                
                if (k.Uposlenja.Any(x => (bool)x.Serviser))
                {
                    return RedirectToAction("Index", "Home", new { area = "Serviser" });

                }

            }
            if(k.Klijent==true)
                return RedirectToAction("Index", "Home", new { area = "Klijent" });

            return RedirectToAction("Logout", "Autentifikacija", new { area = "" });
        }

        public ActionResult Registracija()
        {

            return View();
        }

        public ActionResult Snimi(Korisnik reg)
        {
            Korisnik k = new Korisnik();

            k.Ime = reg.Ime;
            k.Prezime = reg.Prezime;
            k.KorisnickoIme = reg.KorisnickoIme;
            k.Lozinka = reg.Lozinka;
            k.Adresa = reg.Adresa;
            k.Email = reg.Email;
            k.Telefon = reg.Telefon;
            k.Klijent = true;

            ctx.Korisnici.Add(k);
            ctx.SaveChanges();
            return RedirectToAction("Provjera", "Autentifikacija", new { username = reg.KorisnickoIme, password = reg.Lozinka});

        }
    }
}