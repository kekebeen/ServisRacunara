using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ServisRacunara.Data.MODELS;
using ServisRacunara.Data.DAL;
using ServisRacunara.Web.Helper;
using ServisRacunara.Web.Areas.Klijent.Models;

namespace ServisRacunara.Web.Areas.Klijent.Controllers
{
    public class HomeController : Controller
    {
        // GET: Klijent/Home
        MojContext ctx = new MojContext();
        public ActionResult Index()
        {
            Korisnik korisnik = Autentifikacija.GetLogiraniKorisnik(HttpContext);

            KlijentIndexVM model = new KlijentIndexVM();

            model.Ime = korisnik.Ime;
            model.Prezime = korisnik.Prezime;
            model.KlijentId = korisnik.Id;

            model.ZahtjeviZaServis = ctx.ZahtjeviZaServis.Where(x => x.KlijentId == model.KlijentId 
            && ctx.ServisniNalozi.Any(y=>y.ZahtjevZaServisId==x.ZahtjevZaServisId)==false).ToList();

            model.ServisniNalozi = ctx.KvarRacunarNalog.Where(x => x.ServiniNalog.KlijentId == korisnik.Id)
                .Select(y => new ServisniNaloziKlijentVM
                {
                    Datum = y.ServiniNalog.DatumPrijema,
                    DatumZavrsetka = y.ServiniNalog.DatumZavrsetka,
                    OpisRacunara = y.Racunar.Opis,
                    OznakaRacunara = y.Racunar.Oznaka,
                    Status = y.ServiniNalog.Status.Naziv,
                    Utisak = y.ServiniNalog.UtisakKlijenta,
                    ServisniNalogId = y.ServisniNalogId
                }).ToList();
            
            return View(model);
        }

        public ActionResult NoviZahtjev()
        {
            ZahtjevZaServis model = new ZahtjevZaServis();

            model.KlijentId = Autentifikacija.GetLogiraniKorisnik(HttpContext).Id;

            

            return View(model);
        }

        public ActionResult SnimiZahtjev(ZahtjevZaServis z)
        {
            z.Datum = DateTime.Now;

            ctx.ZahtjeviZaServis.Add(z);
            ctx.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult ObrisiZahtjev(int ZahtjevZaServisId)
        {
            

            ctx.ZahtjeviZaServis.Remove(ctx.ZahtjeviZaServis.Find(ZahtjevZaServisId));
            ctx.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult DodajUtisak(int ServisniNalogId)
        {
            UtisakVM model = ctx.ServisniNalozi.Where(x => x.ServisniNalogId == ServisniNalogId).
                Select(y => new UtisakVM
                {
                    ServisniNalogId = y.ServisniNalogId
                }).FirstOrDefault();

            return View("Utisak",model);
        }

        public ActionResult SnimiUtisak(UtisakVM u)
        {
            ServisniNalog s = ctx.ServisniNalozi.Find(u.ServisniNalogId);

            s.UtisakKlijenta = u.Tekst;

            ctx.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}