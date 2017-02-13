using ServisRacunara.Data.DAL;
using ServisRacunara.Data.MODELS;
using ServisRacunara.Web.Areas.Administrator.Models;
using ServisRacunara.Web.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ServisRacunara.Web.Areas.Administrator.Controllers
{
    public class HomeController : Controller
    {
        // GET: Administrator/Home
        MojContext ctx = new MojContext();
        public ActionResult Index()
        {
            var startOfTthisMonth = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            var firstDay = startOfTthisMonth.AddMonths(-1);
            var lastDay = startOfTthisMonth.AddDays(-1);

            Korisnik korisnik = Autentifikacija.GetLogiraniKorisnik(HttpContext);
            AdministratorIndexVM model = new AdministratorIndexVM();

            model.KlijentId = korisnik.Id;
            model.Ime = korisnik.Ime;
            model.Prezime = korisnik.Prezime;
            model.BrojKorisnika = ctx.Korisnici.Count();
            model.BrojServisnihNaloga = ctx.ServisniNalozi.Count();
            model.BrojZaposlenih = ctx.Uposlenici.Count();
            model.BrojZahtjeva = ctx.ZahtjeviZaServis.Count();

            model.Nalozi = ctx.ServisniNalozi.Take(5).ToList();
            model.Zahtjevi = ctx.ZahtjeviZaServis.Take(5).ToList();
            model.Uposlenici = ctx.Uposlenici.Take(5).ToList();

            model.MjesecnaZaradaUkupno = ctx.Racuni.Where(x => x.DatumIzdavanja >= firstDay && x.DatumIzdavanja <= lastDay).Sum(y => y.Iznos);

            return View(model);
        }

        public ActionResult HomeZarada()
        {
            HomeMjesecnaZaradaVM model = new HomeMjesecnaZaradaVM();
            var startOfTthisMonth = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            var firstDay = startOfTthisMonth.AddMonths(-1);
            var lastDay = startOfTthisMonth.AddDays(-1);

            var data = ctx.Racuni.Where(x => x.DatumIzdavanja >= firstDay && x.DatumIzdavanja <= lastDay).Select(y => new HomeMjesecnaZaradaVM
            {
                period = y.DatumIzdavanja.ToString(),
                value = y.Iznos.ToString()
            }).ToList();
            
            return Json(data, JsonRequestBehavior.AllowGet);
        }
    }
}