using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ServisRacunara.Data.MODELS;
using ServisRacunara.Data.DAL;
using ServisRacunara.Web.Areas.Prodavac.Models;
using ServisRacunara.Web.Helper;

namespace ServisRacunara.Web.Areas.Prodavac.Controllers
{
    public class HomeController : Controller
    {
        MojContext ctx = new MojContext();
        // GET: Prodavac/Home
        public ActionResult Index()
        {
            ProdavacIndexVM model = new ProdavacIndexVM();

            model.ZahtjeviZaServis = ctx.ZahtjeviZaServis.Where(x => ctx.ServisniNalozi.Any(y => y.ZahtjevZaServisId == x.ZahtjevZaServisId) == false)
                .Select(z=> new ZahtjeviVM {  Adresa = z.Adresa, Datum = z.Datum, ImePrezime = z.Klijent.Ime + " " + z.Klijent.Prezime,
                 Isporuka = z.Isporuka, Napomena = z.Napomena, Preuzimanje = z.Preuzimanje, ServisNaAdresi = z.ServisNaAdresi, KlijentId = z.KlijentId, ZahtjevZaServisId = z.ZahtjevZaServisId }).ToList();

            return View(model);
        }

        public ActionResult KreirajNalog(int ZahtjevZaServisId)
        {
            ServisniNalogVM model = new ServisniNalogVM();
            ZahtjevZaServis z = ctx.ZahtjeviZaServis.Where(x => x.ZahtjevZaServisId == ZahtjevZaServisId).SingleOrDefault();


            Korisnik Klijent = ctx.Korisnici.Where(x => x.Id == z.KlijentId).SingleOrDefault();

            model.ZahtjevZaServisId = ZahtjevZaServisId;
            model.AdresaKlijenta = Klijent.Adresa;
            model.email = Klijent.Email;
            model.ImeKlijenta = Klijent.Ime;
            model.PrezimeKlijenta = Klijent.Prezime;
            model.TelefonKlijenta = Klijent.Telefon;
            model.KorisnickoIme = Klijent.KorisnickoIme;
            model.KlijentId = Klijent.Id;
            model.Racunari = ctx.Racunari.Where(x => x.VlasnikId == Klijent.Id)
                .Select(y => new SelectListItem {
                    Text = y.Oznaka + " " + y.Opis,
                    Value = y.RacunarId.ToString()
                }).ToList();

            model.Kvarovi = ctx.Kvarovi.Select(x => new SelectListItem
            {
                Text = x.Opis,
                Value = x.KvarId.ToString()
            }).ToList();

            model.Racunari = ctx.Racunari.Where(x => x.VlasnikId == Klijent.Id).
                Select(y => new SelectListItem {
                    Text = y.Oznaka + " / " + y.Opis + " / " + y.OS,
                    Value = y.RacunarId.ToString()
                }).ToList();

            model.ServisNaAdresi = z.ServisNaAdresi;
            model.Preuzimanje = z.Preuzimanje;
            model.Isporuka = z.Isporuka;

            return View(model);
        }

        public ActionResult DodajRacunar(int KlijentId, int ZahtjevZaServisId)
        {
            DodajRacunarZahtjevVM model = new DodajRacunarZahtjevVM();
            model.VlasnikId = KlijentId;
            model.ZahtjevZaServisId = ZahtjevZaServisId;

            return View(model);

        }


        public ActionResult DodajKvar(int ZahtjevZaServisId)
        {


            DodajKvarZahtjevVM model = new DodajKvarZahtjevVM();
            model.ZahtjevZaServisId = ZahtjevZaServisId;

            return View(model);

        }

        public ActionResult SnimiRacunar(DodajRacunarZahtjevVM r)
        {
            Racunar rdb = new Racunar();

            if (!ModelState.IsValid)
            {
                return View("DodajRacunar", r);

            }

            rdb.Opis = r.Opis;
            rdb.Oznaka = r.Oznaka;
            rdb.VlasnikId = r.VlasnikId;
            rdb.OS = r.OS;

            if(rdb.Oznaka == "" || rdb.Oznaka == null ) { return RedirectToAction("KreirajNalog", new { @ZahtjevZaServisId = r.ZahtjevZaServisId }); }

            ctx.Racunari.Add(rdb);
            ctx.SaveChanges();

            return RedirectToAction("KreirajNalog", new { @ZahtjevZaServisId = r.ZahtjevZaServisId });

        }

        public ActionResult SnimiKvar(DodajKvarZahtjevVM k)
        {
            Kvar kdb = new Kvar();

            kdb.Opis = k.Opis;
            kdb.Hardware = k.Hardware;
            kdb.Software = k.Software;
            if(kdb.Opis == "" || kdb.Opis == null)
            {
                return RedirectToAction("KreirajNalog", new { @ZahtjevZaServisId = k.ZahtjevZaServisId });
            }

            ctx.Kvarovi.Add(kdb);
            ctx.SaveChanges();

            return RedirectToAction("KreirajNalog", new { @ZahtjevZaServisId = k.ZahtjevZaServisId });

        }

        public ActionResult SnimiNalog(ServisniNalogVM sn)
        {
            if(!ModelState.IsValid)
            {
                sn.Kvarovi = ctx.Kvarovi.Select(x => new SelectListItem
                {
                    Text = x.Opis,
                    Value = x.KvarId.ToString()
                }).ToList();

                sn.Racunari = ctx.Racunari.Where(x => x.VlasnikId == sn.KlijentId).
                    Select(y => new SelectListItem
                    {
                        Text = y.Oznaka + " " + y.Opis,
                        Value = y.RacunarId.ToString()
                    }).ToList();

                Korisnik Klijent = ctx.Korisnici.Where(x => x.Id == sn.KlijentId).SingleOrDefault();
                
                sn.AdresaKlijenta = Klijent.Adresa;
                sn.email = Klijent.Email;
                sn.ImeKlijenta = Klijent.Ime;
                sn.PrezimeKlijenta = Klijent.Prezime;
                sn.TelefonKlijenta = Klijent.Telefon;
                sn.KorisnickoIme = Klijent.KorisnickoIme;

                return View("KreirajNalog", sn);
            }

            ServisniNalog nalog = new ServisniNalog();

            Racun r = new Racun();

            KvarRacunarNalog krn = new KvarRacunarNalog();

            nalog.DatumPrijema = DateTime.Now;
            nalog.KlijentId = sn.KlijentId;

            int id = (Autentifikacija.GetLogiraniKorisnik(HttpContext)).Id;

            ServisRacunara.Data.MODELS.Uposlenik u = ctx.Uposlenici.Where(x => x.KorisnikId == id).FirstOrDefault();

            nalog.NalogKreiraoId = u.UposlenikId;
                
            nalog.Napomena = sn.Napomena;
            nalog.Preuzimanje = sn.Preuzimanje;
            nalog.ServisNaAdresi = sn.ServisNaAdresi;
            nalog.StatusId = 1;
            nalog.ZahtjevZaServisId = sn.ZahtjevZaServisId;
            nalog.Isporuka = sn.Isporuka;

            ctx.ServisniNalozi.Add(nalog);
            ctx.SaveChanges();

            r.ServisniNalogId = nalog.ServisniNalogId;
            r.DatumKreiranja = DateTime.Now;

            ctx.Racuni.Add(r);
            ctx.SaveChanges();

            krn.KvarId = sn.KvarId;
            krn.RacunarId = sn.RacunarId;
            krn.ServisniNalogId = nalog.ServisniNalogId;

            ctx.KvarRacunarNalog.Add(krn);
            ctx.SaveChanges();


            return RedirectToAction("Index");

        }
    }
}