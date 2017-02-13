using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ServisRacunara.Web.Areas.Prodavac.Models;
using ServisRacunara.Data.MODELS;
using ServisRacunara.Data.DAL;


namespace ServisRacunara.Web.Areas.Prodavac.Controllers
{
    public class PregledController : Controller
    {
        MojContext ctx = new MojContext();
        // GET: Prodavac/Pregled
        public ActionResult Index()
        {
            PregledServisaVM model = new PregledServisaVM();
            model.Stavke = ctx.ServisniNalozi.Select(x => new PregledStavke
            {
                ImeKlijenta = x.Klijent.Ime, Isporuka = x.Isporuka, KlijentId = x.KlijentId,
                KvarId = ctx.KvarRacunarNalog.Where(a => a.ServisniNalogId == x.ServisniNalogId).Select(s => s.KvarId).FirstOrDefault(),
                Napomena = x.Napomena,
                OpisKvara = ctx.KvarRacunarNalog.Where(a => a.ServisniNalogId == x.ServisniNalogId).Select(s => s.Kvar.Opis).FirstOrDefault(),
                OznakaRacunara = ctx.KvarRacunarNalog.Where(b => b.ServisniNalogId == x.ServisniNalogId).Select(d => d.Racunar.Oznaka).FirstOrDefault(),
                Preuzimanje = x.Preuzimanje, PrezimeKlijenta = x.Klijent.Prezime,
                RacunarId = ctx.KvarRacunarNalog.Where(b => b.ServisniNalogId == x.ServisniNalogId).Select(d => d.RacunarId).FirstOrDefault(),
                ServisNaAdresi = x.ServisNaAdresi, ServisniNalogId = x.ServisniNalogId, Status = x.Status.Naziv, DatumKreiranja = x.DatumPrijema,
                RacunId = ctx.Racuni.Where(f => f.ServisniNalogId == x.ServisniNalogId).Select(j=> j.RacunId).FirstOrDefault(),
                ServiserId = ctx.ServisPreuzimanja.Where(c=> c.ServisniNalogId == x.ServisniNalogId).Select(y=> y.UposlenikId).FirstOrDefault(),
                ServiserIme = ctx.ServisPreuzimanja.Where(c => c.ServisniNalogId == x.ServisniNalogId).Select(y => y.Uposlenik.Korisnik.Ime + " " + y.Uposlenik.Korisnik.Prezime).FirstOrDefault(),
                ServiserPreuzeo = ctx.ServisPreuzimanja.Where(c => c.ServisniNalogId == x.ServisniNalogId).Select(y=> y.DatumPreuzimanja).FirstOrDefault()
            }).OrderByDescending(x => x.DatumKreiranja).ToList();

            foreach (PregledStavke x in model.Stavke)
            {
                if (x.ServiserIme == null)
                {
                    x.ServiserIme = "Servis nije preuzet";
                }
            }

            model.Serviseri = ctx.Uposlenici.Where(x => x.Serviser == true).Select(s => new SelectListItem
            {
                Text = s.Korisnik.Ime + " " + s.Korisnik.Prezime,
                Value = s.UposlenikId.ToString()
            }).ToList();

            return View(model);
        }
        

        public ActionResult NisuPreuzetiNalozi()
        {
            PregledServisaVM model = new PregledServisaVM();
            model.Stavke = ctx.ServisniNalozi.Where(y=> ctx.ServisPreuzimanja.Any(s=> s.ServisniNalogId == y.ServisniNalogId) == false).Select(x => new PregledStavke
            {
                ImeKlijenta = x.Klijent.Ime,
                Isporuka = x.Isporuka,
                KlijentId = x.KlijentId,
                KvarId = ctx.KvarRacunarNalog.Where(a => a.ServisniNalogId == x.ServisniNalogId).Select(s => s.KvarId).FirstOrDefault(),
                Napomena = x.Napomena,
                OpisKvara = ctx.KvarRacunarNalog.Where(a => a.ServisniNalogId == x.ServisniNalogId).Select(s => s.Kvar.Opis).FirstOrDefault(),
                OznakaRacunara = ctx.KvarRacunarNalog.Where(b => b.ServisniNalogId == x.ServisniNalogId).Select(d => d.Racunar.Oznaka).FirstOrDefault(),
                Preuzimanje = x.Preuzimanje,
                PrezimeKlijenta = x.Klijent.Prezime,
                RacunarId = ctx.KvarRacunarNalog.Where(b => b.ServisniNalogId == x.ServisniNalogId).Select(d => d.RacunarId).FirstOrDefault(),
                ServisNaAdresi = x.ServisNaAdresi,
                ServisniNalogId = x.ServisniNalogId,
                Status = x.Status.Naziv,
                DatumKreiranja = x.DatumPrijema,
                RacunId = ctx.Racuni.Where(f => f.ServisniNalogId == x.ServisniNalogId).Select(j => j.RacunId).FirstOrDefault(),
                ServiserId = ctx.ServisPreuzimanja.Where(c => c.ServisniNalogId == x.ServisniNalogId).Select(y => y.UposlenikId).FirstOrDefault(),
                ServiserIme = ctx.ServisPreuzimanja.Where(c => c.ServisniNalogId == x.ServisniNalogId).Select(y => y.Uposlenik.Korisnik.Ime + " " + y.Uposlenik.Korisnik.Prezime).FirstOrDefault(),
                ServiserPreuzeo = ctx.ServisPreuzimanja.Where(c => c.ServisniNalogId == x.ServisniNalogId).Select(y => y.DatumPreuzimanja).FirstOrDefault()
            }).OrderByDescending(x => x.DatumKreiranja).ToList();

            foreach (PregledStavke x in model.Stavke)
            {
                if (x.ServiserIme == null)
                {
                    x.ServiserIme = "Servis nije preuzet";
                }
            }

            model.Serviseri = ctx.Uposlenici.Where(x => x.Serviser == true).Select(s => new SelectListItem
            {
                Text = s.Korisnik.Ime + " " + s.Korisnik.Prezime,
                Value = s.UposlenikId.ToString()
            }).ToList();

            return View("Index",model);
        }

        public ActionResult PreuzetiNalozi()
        {
            PregledServisaVM model = new PregledServisaVM();
            model.Stavke = ctx.ServisniNalozi.Where(y => ctx.ServisPreuzimanja.Any(s => s.ServisniNalogId == y.ServisniNalogId) == true).Select(x => new PregledStavke
            {
                ImeKlijenta = x.Klijent.Ime,
                Isporuka = x.Isporuka,
                KlijentId = x.KlijentId,
                KvarId = ctx.KvarRacunarNalog.Where(a => a.ServisniNalogId == x.ServisniNalogId).Select(s => s.KvarId).FirstOrDefault(),
                Napomena = x.Napomena,
                OpisKvara = ctx.KvarRacunarNalog.Where(a => a.ServisniNalogId == x.ServisniNalogId).Select(s => s.Kvar.Opis).FirstOrDefault(),
                OznakaRacunara = ctx.KvarRacunarNalog.Where(b => b.ServisniNalogId == x.ServisniNalogId).Select(d => d.Racunar.Oznaka).FirstOrDefault(),
                Preuzimanje = x.Preuzimanje,
                PrezimeKlijenta = x.Klijent.Prezime,
                RacunarId = ctx.KvarRacunarNalog.Where(b => b.ServisniNalogId == x.ServisniNalogId).Select(d => d.RacunarId).FirstOrDefault(),
                ServisNaAdresi = x.ServisNaAdresi,
                ServisniNalogId = x.ServisniNalogId,
                Status = x.Status.Naziv,
                DatumKreiranja = x.DatumPrijema,
                RacunId = ctx.Racuni.Where(f => f.ServisniNalogId == x.ServisniNalogId).Select(j => j.RacunId).FirstOrDefault(),
                ServiserId = ctx.ServisPreuzimanja.Where(c => c.ServisniNalogId == x.ServisniNalogId).Select(y => y.UposlenikId).FirstOrDefault(),
                ServiserIme = ctx.ServisPreuzimanja.Where(c => c.ServisniNalogId == x.ServisniNalogId).Select(y => y.Uposlenik.Korisnik.Ime + " " + y.Uposlenik.Korisnik.Prezime).FirstOrDefault(),
                ServiserPreuzeo = ctx.ServisPreuzimanja.Where(c => c.ServisniNalogId == x.ServisniNalogId).Select(y => y.DatumPreuzimanja).FirstOrDefault()
            }).OrderByDescending(x=>x.DatumKreiranja).ToList();

            foreach (PregledStavke x in model.Stavke)
            {
                if (x.ServiserIme == null)
                {
                    x.ServiserIme = "Servis nije preuzet";
                }
            }

            model.Serviseri = ctx.Uposlenici.Where(x => x.Serviser == true).Select(s => new SelectListItem
            {
                Text = s.Korisnik.Ime + " " + s.Korisnik.Prezime,
                Value = s.UposlenikId.ToString()
            }).ToList();

            return View("Index", model);
        }

        public ActionResult NaloziPoServiseru(int ServiserId)
        {
            PregledServisaVM model = new PregledServisaVM();

            model.Stavke = ctx.ServisPreuzimanja.Where(x=>x.UposlenikId == ServiserId).Select(y=> new PregledStavke {

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
                Status = y.ServisniNalog.Status.Naziv,
                DatumKreiranja = y.ServisniNalog.DatumPrijema,
                RacunId = ctx.Racuni.Where(f => f.ServisniNalogId == y.ServisniNalog.ServisniNalogId).Select(j => j.RacunId).FirstOrDefault(),
                ServiserId = ctx.ServisPreuzimanja.Where(c => c.ServisniNalogId == y.ServisniNalog.ServisniNalogId).Select(k => k.UposlenikId).FirstOrDefault(),
                ServiserIme = ctx.ServisPreuzimanja.Where(c => c.ServisniNalogId == y.ServisniNalog.ServisniNalogId).Select(k => k.Uposlenik.Korisnik.Ime + " " + k.Uposlenik.Korisnik.Prezime).FirstOrDefault(),
                ServiserPreuzeo = ctx.ServisPreuzimanja.Where(c => c.ServisniNalogId == y.ServisniNalog.ServisniNalogId).Select(k => k.DatumPreuzimanja).FirstOrDefault()

                }).ToList();

            foreach (PregledStavke x in model.Stavke)
            {
                if (x.ServiserIme == null)
                {
                    x.ServiserIme = "Servis nije preuzet";
                }
            }

            model.Serviseri = ctx.Uposlenici.Where(x => x.Serviser == true).Select(s => new SelectListItem
            {
                Text = s.Korisnik.Ime + " " + s.Korisnik.Prezime,
                Value = s.UposlenikId.ToString()
            }).ToList();

            return View("Index", model);
        }

        public ActionResult NaloziPoImenuKlijenta(string ImePrezimeKlijenta)
        {
            PregledServisaVM model = new PregledServisaVM();

            model.Stavke = ctx.ServisniNalozi.Where(x => (x.Klijent.Ime + " " + x.Klijent.Prezime).Contains(ImePrezimeKlijenta)).Select(x => new PregledStavke
            {
                ImeKlijenta = x.Klijent.Ime,
                Isporuka = x.Isporuka,
                KlijentId = x.KlijentId,
                KvarId = ctx.KvarRacunarNalog.Where(a => a.ServisniNalogId == x.ServisniNalogId).Select(s => s.KvarId).FirstOrDefault(),
                Napomena = x.Napomena,
                OpisKvara = ctx.KvarRacunarNalog.Where(a => a.ServisniNalogId == x.ServisniNalogId).Select(s => s.Kvar.Opis).FirstOrDefault(),
                OznakaRacunara = ctx.KvarRacunarNalog.Where(b => b.ServisniNalogId == x.ServisniNalogId).Select(d => d.Racunar.Oznaka).FirstOrDefault(),
                Preuzimanje = x.Preuzimanje,
                PrezimeKlijenta = x.Klijent.Prezime,
                RacunarId = ctx.KvarRacunarNalog.Where(b => b.ServisniNalogId == x.ServisniNalogId).Select(d => d.RacunarId).FirstOrDefault(),
                ServisNaAdresi = x.ServisNaAdresi,
                ServisniNalogId = x.ServisniNalogId,
                Status = x.Status.Naziv,
                DatumKreiranja = x.DatumPrijema,
                RacunId = ctx.Racuni.Where(f => f.ServisniNalogId == x.ServisniNalogId).Select(j => j.RacunId).FirstOrDefault(),
                ServiserId = ctx.ServisPreuzimanja.Where(c => c.ServisniNalogId == x.ServisniNalogId).Select(y => y.UposlenikId).FirstOrDefault(),
                ServiserIme = ctx.ServisPreuzimanja.Where(c => c.ServisniNalogId == x.ServisniNalogId).Select(y => y.Uposlenik.Korisnik.Ime + " " + y.Uposlenik.Korisnik.Prezime).FirstOrDefault(),
                ServiserPreuzeo = ctx.ServisPreuzimanja.Where(c => c.ServisniNalogId == x.ServisniNalogId).Select(y => y.DatumPreuzimanja).FirstOrDefault()
            }).OrderByDescending(x => x.DatumKreiranja).ToList();

            foreach (PregledStavke x in model.Stavke)
            {
                if (x.ServiserIme == null)
                {
                    x.ServiserIme = "Servis nije preuzet";
                }
            }

            model.Serviseri = ctx.Uposlenici.Where(x => x.Serviser == true).Select(s => new SelectListItem
            {
                Text = s.Korisnik.Ime + " " + s.Korisnik.Prezime,
                Value = s.UposlenikId.ToString()
            }).ToList();

            return View("Index", model);
        }

        public ActionResult PrikaziRacun(int RacunId)
        {
            RacunVM model = new RacunVM();

            Racun r = ctx.Racuni.Find(RacunId);

            model.Stavke = ctx.StavkeRacuna.Where(x => x.RacunId == RacunId).Select(y => new RacunStavkaVM
            {
                Cijena = y.Proizvod.Cijena,
                Naziv = y.Proizvod.Naziv,
                Opis = y.Proizvod.Opis,
                ProizvodId = y.ProizvodId,
                Usluga = y.Proizvod.Usluga,
                 StavkaRacunaId = y.StavkaRacunaId
            }).ToList();

            model.Iznos = 0;
            foreach(RacunStavkaVM x in model.Stavke)
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

        public ActionResult DodajArtikal(int RacunId) {
            DodajArtikalVM model = new DodajArtikalVM();

            model.ListaArtikala = ctx.Proizvodi.Select(x => new SelectListItem
            {
                Text = x.Naziv + " - " + x.Opis + ", " + x.Cijena + " KM",
                Value = x.ProizvodId.ToString()
            }).ToList();

            model.RacunId = RacunId;

            return View(model);
        }

        public ActionResult SnimiNoviArtikal(DodajArtikalVM d)
        {
            if(d.ProizvodId == 0)
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


    }
}