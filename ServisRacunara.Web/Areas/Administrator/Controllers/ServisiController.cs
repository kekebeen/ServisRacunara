using ServisRacunara.Data.DAL;
using ServisRacunara.Data.MODELS;
using ServisRacunara.Web.Areas.Administrator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static ServisRacunara.Web.Areas.Administrator.Models.ServisiRacunVM;

namespace ServisRacunara.Web.Areas.Administrator.Controllers
{
    public class ServisiController : Controller
    {
        MojContext ctx = new MojContext();
        // GET: Administrator/Servisi
        public ActionResult Index()
        {
            ServisiIndexVM model = new ServisiIndexVM();

            model.Stavke = ctx.ServisniNalozi.Select(x => new ServisiPregledStavke
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

            foreach (ServisiPregledStavke x in model.Stavke)
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
            ServisiIndexVM model = new ServisiIndexVM();
            model.Stavke = ctx.ServisniNalozi.Where(y => ctx.ServisPreuzimanja.Any(s => s.ServisniNalogId == y.ServisniNalogId) == false).Select(x => new ServisiPregledStavke
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

            foreach (ServisiPregledStavke x in model.Stavke)
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

        public ActionResult PreuzetiNalozi()
        {
            ServisiIndexVM model = new ServisiIndexVM();
            model.Stavke = ctx.ServisniNalozi.Where(y => ctx.ServisPreuzimanja.Any(s => s.ServisniNalogId == y.ServisniNalogId) == true).Select(x => new ServisiPregledStavke
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
            }).OrderBy(x => x.ServiserPreuzeo).ToList();

            foreach (ServisiPregledStavke x in model.Stavke)
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
            ServisiRacunVM model = new ServisiRacunVM();

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
            foreach (RacunStavkaVM x in model.Stavke)
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

        public ActionResult RacunPrint(int RacunId)
        {
            ServisiRacunVM model = new ServisiRacunVM();

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
            foreach (RacunStavkaVM x in model.Stavke)
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

    }
}