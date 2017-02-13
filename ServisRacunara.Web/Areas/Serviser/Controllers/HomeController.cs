using ServisRacunara.Data.DAL;
using ServisRacunara.Data.MODELS;
using ServisRacunara.Web.Areas.Serviser.Models;
using ServisRacunara.Web.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ServisRacunara.Web.Areas.Serviser.Controllers
{
    public class HomeController : Controller
    {
        // GET: Serviser/Home
        MojContext ctx = new MojContext();
        public ActionResult Index()
        {
            ServiserIndexVM model = new ServiserIndexVM();

            model.Stavke = ctx.ServisniNalozi.Where(x => ctx.ServisPreuzimanja.Any(s => s.ServisniNalogId == x.ServisniNalogId ) == false).Select(y => new ServiserIndexStavkeVM
            {
                ImeKlijenta = y.Klijent.Ime,
                Isporuka = y.Isporuka,
                KlijentId = y.Klijent.Id,
                KvarId = ctx.KvarRacunarNalog.Where(a => a.ServisniNalogId == y.ServisniNalogId).Select(s => s.KvarId).FirstOrDefault(),
                Napomena = y.Napomena,
                OpisKvara = ctx.KvarRacunarNalog.Where(a => a.ServisniNalogId == y.ServisniNalogId).Select(s => s.Kvar.Opis).FirstOrDefault(),
                OznakaRacunara = ctx.KvarRacunarNalog.Where(b => b.ServisniNalogId == y.ServisniNalogId).Select(d => d.Racunar.Oznaka).FirstOrDefault(),
                Preuzimanje = y.Preuzimanje,
                PrezimeKlijenta = y.Klijent.Prezime,
                RacunarId = ctx.KvarRacunarNalog.Where(b => b.ServisniNalogId == y.ServisniNalogId).Select(d => d.RacunarId).FirstOrDefault(),
                ServisNaAdresi = y.ServisNaAdresi,
                ServisniNalogId = y.ServisniNalogId,
                StatusId = y.Status.StatusId,
                Status = y.Status.Naziv,
                DatumKreiranja = y.DatumPrijema,
                RacunId = ctx.Racuni.Where(f => f.ServisniNalogId == y.ServisniNalogId).Select(j => j.RacunId).FirstOrDefault(),
                ServiserId = ctx.ServisPreuzimanja.Where(c => c.ServisniNalogId == y.ServisniNalogId).Select(a => a.UposlenikId).FirstOrDefault(),
                ServiserIme = ctx.ServisPreuzimanja.Where(c => c.ServisniNalogId == y.ServisniNalogId).Select(a => a.Uposlenik.Korisnik.Ime + " " + a.Uposlenik.Korisnik.Prezime).FirstOrDefault(),
                ServiserPreuzeo = ctx.ServisPreuzimanja.Where(c => c.ServisniNalogId == y.ServisniNalogId).Select(a => a.DatumPreuzimanja).FirstOrDefault()

            }).OrderBy(x => x.DatumKreiranja).ToList();

            foreach (ServiserIndexStavkeVM x in model.Stavke)
            {
                if (x.ServiserIme == null)
                {
                    x.ServiserIme = "Servis nije preuzet";
                }
            }
       
            return View("Index", model);
        }
        public ActionResult PrikaziRacun(int RacunId)
        {
            ServiserRacunVM model = new ServiserRacunVM();

            Racun r = ctx.Racuni.Find(RacunId);

            model.Stavke = ctx.StavkeRacuna.Where(x => x.RacunId == RacunId).Select(y => new ServiserRacunStavka
            {
                Cijena = y.Proizvod.Cijena,
                Naziv = y.Proizvod.Naziv,
                Opis = y.Proizvod.Opis,
                ProizvodId = y.ProizvodId,
                Usluga = y.Proizvod.Usluga,
                StavkaRacunaId = y.StavkaRacunaId
            }).ToList();

            model.Iznos = 0;
            foreach (ServiserRacunStavka x in model.Stavke)
            {
                model.Iznos += x.Cijena;
            }

            model.DatumKreiranja = r.DatumKreiranja;
            model.RacunId = RacunId;
            model.ServisniNalogId = r.ServisniNalogId;

            model.ImePrezime = ctx.ServisniNalozi.Where(x => x.ServisniNalogId == r.ServisniNalogId).Select(y => y.Klijent.Ime + " " + y.Klijent.Prezime).FirstOrDefault();
            model.OznakaRacunara = ctx.KvarRacunarNalog.Where(x => x.ServisniNalogId == r.ServisniNalogId).Select(y => y.Racunar.Oznaka).FirstOrDefault();
            model.Telefon = ctx.ServisniNalozi.Where(x => x.ServisniNalogId == r.ServisniNalogId).Select(y => y.Klijent.Telefon).FirstOrDefault();
            model.Adresa = ctx.ServisniNalozi.Where(x => x.ServisniNalogId == r.ServisniNalogId).Select(y => y.Klijent.Adresa).FirstOrDefault();


            return View(model);

        }
        public ActionResult DodajArtikal(int RacunId)
        {
            ServiserDodajArtikalVM model = new ServiserDodajArtikalVM();

            model.ListaArtikala = ctx.Proizvodi.Select(x => new SelectListItem
            {
                Text = x.Naziv + " - " + x.Opis + ", " + x.Cijena + " KM",
                Value = x.ProizvodId.ToString()
            }).ToList();

            model.RacunId = RacunId;

            return View(model);
        }
        public ActionResult SnimiNoviArtikal(ServiserDodajArtikalVM d)
        {
            if (d.ProizvodId == 0)
            {
                return RedirectToAction("PrikaziRacun", new { RacunId = d.RacunId });
            }

            StavkaRacuna s = new StavkaRacuna();

            s.ProizvodId = d.ProizvodId;
            s.RacunId = d.RacunId;

            ctx.StavkeRacuna.Add(s);
            ctx.SaveChanges();


            return RedirectToAction("PrikaziRacun", new { RacunId = d.RacunId });
        }
        public ActionResult ObrisiSTavkuRacuna(int StavkaRacunaId, int RacunId)
        {
            StavkaRacuna s = new StavkaRacuna();
            s = ctx.StavkeRacuna.Find(StavkaRacunaId);

            ctx.StavkeRacuna.Remove(s);
            ctx.SaveChanges();


            return RedirectToAction("PrikaziRacun", new { RacunId = RacunId });
        }

        public ActionResult PreuzetiNalozi()
        {
            ServiserIndexVM model = new ServiserIndexVM();
            Korisnik k = Autentifikacija.GetLogiraniKorisnik(HttpContext);
            int ServiserId = k.Uposlenja.Select(x => x.UposlenikId).Single();

            model.Stavke = ctx.ServisPreuzimanja.Where(x => x.UposlenikId == ServiserId).Select(y => new ServiserIndexStavkeVM
            {
                ImeKlijenta = y.ServisniNalog.Klijent.Ime,
                Isporuka = y.ServisniNalog.Isporuka,
                KlijentId = y.ServisniNalog.KlijentId,
                KvarId = ctx.KvarRacunarNalog.Where(a => a.ServisniNalogId == y.ServisniNalog.ServisniNalogId).Select(s => s.KvarId).FirstOrDefault(),
                Napomena = y.ServisniNalog.Napomena,
                OpisKvara = ctx.KvarRacunarNalog.Where(a => a.ServisniNalogId == y.ServisniNalog.ServisniNalogId).Select(s => s.Kvar.Opis).FirstOrDefault(),
                OznakaRacunara = ctx.KvarRacunarNalog.Where(b => b.ServisniNalogId == y.ServisniNalog.ServisniNalogId).Select(d => d.Racunar.Oznaka).FirstOrDefault(),
                Preuzimanje = y.ServisniNalog.Preuzimanje,
                PrezimeKlijenta = y.ServisniNalog.Klijent.Prezime,
                RacunarId = ctx.KvarRacunarNalog.Where(b => b.ServisniNalogId == y.ServisniNalog.ServisniNalogId).Select(d => d.RacunarId).FirstOrDefault(),
                ServisNaAdresi = y.ServisniNalog.ServisNaAdresi,
                ServisniNalogId = y.ServisniNalog.ServisniNalogId,
                StatusId = y.ServisniNalog.Status.StatusId,
                Status = y.ServisniNalog.Status.Naziv,
                DatumKreiranja = y.ServisniNalog.DatumPrijema,
                RacunId = ctx.Racuni.Where(f => f.ServisniNalogId == y.ServisniNalog.ServisniNalogId).Select(j => j.RacunId).FirstOrDefault(),
                ServiserId = ctx.ServisPreuzimanja.Where(c => c.ServisniNalogId == y.ServisniNalog.ServisniNalogId).Select(a => a.UposlenikId).FirstOrDefault(),
                ServiserIme = ctx.ServisPreuzimanja.Where(c => c.ServisniNalogId == y.ServisniNalog.ServisniNalogId).Select(a => a.Uposlenik.Korisnik.Ime + " " + a.Uposlenik.Korisnik.Prezime).FirstOrDefault(),
                ServiserPreuzeo = ctx.ServisPreuzimanja.Where(c => c.ServisniNalogId == y.ServisniNalog.ServisniNalogId).Select(a => a.DatumPreuzimanja).FirstOrDefault()

            }).OrderBy(x => x.DatumKreiranja).ToList();

            foreach (ServiserIndexStavkeVM x in model.Stavke)
            {
                if (x.ServiserIme == null)
                {
                    x.ServiserIme = "Servis nije preuzet";
                }
            }

            return View("Index", model);
        }

        public ActionResult PreuzmiServis(int ServisniNalogId)
        {
            ServisPreuzeo p = new ServisPreuzeo();
            Korisnik korisnik = Autentifikacija.GetLogiraniKorisnik(HttpContext);
            Data.MODELS.Uposlenik uposlenik = ctx.Uposlenici.Where(x => x.KorisnikId == korisnik.Id).SingleOrDefault();

            p.DatumPreuzimanja = DateTime.Now;
            p.ServisniNalogId = ServisniNalogId;
            p.ServisPreuzeoId = uposlenik.UposlenikId;
            p.UposlenikId = uposlenik.UposlenikId;

            ServisniNalog nalog = ctx.ServisniNalozi.Find(ServisniNalogId);
            nalog.Status.Naziv = "Na servisu";

            ctx.ServisPreuzimanja.Add(p);
            ctx.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult ZakljuciServis (int ServisniNalogId)
        {
            ZakljuciServisVM model = new ZakljuciServisVM();
            Korisnik korisnik = Autentifikacija.GetLogiraniKorisnik(HttpContext);

            model.ServisniNalogId = ServisniNalogId;
            model.DatumZavrsetka = DateTime.Now;
            int ServiserId = korisnik.Uposlenja.Select(x => x.UposlenikId).Single();
            model.NalogKreiraoId = ServiserId;

            model.StatusId = 2;

            return View(model);
        }

        public ActionResult SnimiZakljucakServisa(ZakljuciServisVM vm)
        {
            ServisniNalog nalog = ctx.ServisniNalozi.Find(vm.ServisniNalogId);
            Status status = ctx.Statusi.Find(vm.StatusId);

            nalog.DatumZavrsetka = vm.DatumZavrsetka;
            nalog.NalogKreiraoId = vm.NalogKreiraoId;

            nalog.StatusId = 2;

            ctx.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult DodajProizvod(int RacunId)
        {
            ServiserDodajProizvodVM model = new ServiserDodajProizvodVM();
            model.RacunId = RacunId;

            return View(model);
        }
        public ActionResult SnimiNoviProizvod(ServiserDodajProizvodVM vm)
        {
            Proizvod proizvod = new Proizvod();

            proizvod.Cijena = vm.Cijena;
            proizvod.Naziv = vm.Naziv;
            proizvod.Opis = vm.Opis;
            proizvod.Usluga = vm.Usluga;

            ctx.Proizvodi.Add(proizvod);
            ctx.SaveChanges();

            return RedirectToAction("PrikaziRacun", new { RacunId = vm.RacunId });
        }

    }
}