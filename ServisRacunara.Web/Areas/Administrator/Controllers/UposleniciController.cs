using ServisRacunara.Data.DAL;
using ServisRacunara.Data.MODELS;
using ServisRacunara.Web.Areas.Administrator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ServisRacunara.Web.Areas.Administrator.Controllers
{
    public class UposleniciController : Controller
    {
        MojContext ctx = new MojContext();
        // GET: Administrator/Uposlenici
        public ActionResult Index()
        {
            UposleniciIndexVM model = new UposleniciIndexVM();

            
            model.Uposleni = ctx.Uposlenici.Select(x => new KorisnikRowVM
            {
                UposlenikId = x.UposlenikId,
                Ime = x.Korisnik.Ime,
                Prezime = x.Korisnik.Prezime,
                Adresa = x.Korisnik.Adresa,
                Telefon = x.Korisnik.Telefon,
                Administrator = (bool)x.Administrator?"DA":"NE",
                Prodavac = (bool)x.Prodavac?"DA":"NE",
                Serviser = (bool)x.Serviser?"DA":"NE"
            }).ToList();

            return View(model);
        }

        public ActionResult PretragaUposlenikaPoImenu(string searchQuery)
        {
            UposleniciIndexVM model = new UposleniciIndexVM();

           
                model.Uposleni = ctx.Uposlenici.Where(a => (a.Korisnik.Ime + " " + a.Korisnik.Prezime).Contains(searchQuery) ).Select(x => new KorisnikRowVM
                {
                    UposlenikId = x.UposlenikId,
                    Ime = x.Korisnik.Ime,
                    Prezime = x.Korisnik.Prezime,
                    Adresa = x.Korisnik.Adresa,
                    Telefon = x.Korisnik.Telefon,
                    Administrator = (bool)x.Administrator ? "DA" : "NE",
                    Prodavac = (bool)x.Prodavac ? "DA" : "NE",
                    Serviser = (bool)x.Serviser ? "DA" : "NE"
                }).ToList();

            return View("Index", model);
        }

        public ActionResult Edit(int uposlenikId)
        {
            UposleniciEditVM korisnik;

            korisnik = ctx.Uposlenici.Where(x => x.UposlenikId == uposlenikId).Select(y => new UposleniciEditVM
            {
                Ime = y.Korisnik.Ime,
                Prezime = y.Korisnik.Prezime,
                KorisnikId = y.KorisnikId,
                UposlenikId = uposlenikId,
                Lozinka = y.Korisnik.Lozinka,
                KorisnickoIme = y.Korisnik.KorisnickoIme,
                Adresa = y.Korisnik.Adresa,
                Telefon = y.Korisnik.Telefon,
                Email = y.Korisnik.Email,
                Klijent = y.Korisnik.Klijent,
                UgovorPocetak = y.UgovorPocetak,
                UgovorKraj = y.UgovorKraj,
                Administrator = y.Administrator,
                Serviser = y.Serviser,
                Prodavac = y.Prodavac
            }).SingleOrDefault();

            //Plata iznos single
            Isplata isplata = ctx.Isplate.Where(x => x.UposlenikId == uposlenikId).SingleOrDefault();

            korisnik.isplataId = isplata.IsplataId;
            korisnik.plataId = isplata.PlataId;
            korisnik.iznos = isplata.Plata.Iznos;

            return View(korisnik);
        }

        public ActionResult New()
        {
            return View("New");
        }

        public ActionResult SnimiProfil(UposleniciEditVM vm)
        {
            Data.MODELS.Uposlenik k;
            Plata p;
            Isplata i;
            if (vm.UposlenikId == 0)
            {
                k = new Data.MODELS.Uposlenik();
                p = new Plata();
                i = new Isplata();
                k.UgovorKraj = null;
                k.UgovorPocetak = DateTime.Now;
                
            }else
            {
                k = ctx.Uposlenici.Find(vm.UposlenikId);
                p = ctx.Plate.Find(vm.plataId);
                i = ctx.Isplate.Find(vm.isplataId);
            }
            //uposlenik
            k.KorisnikId = vm.KorisnikId;
            k.Prodavac = vm.Prodavac;
            k.Prodavac = vm.Prodavac;
            k.Serviser = vm.Serviser;
            k.Administrator = vm.Administrator;
            k.UgovorKraj = vm.UgovorKraj;
            k.UgovorPocetak = vm.UgovorPocetak;
            k.UposlenikId = vm.UposlenikId.Value;
            //korisnik
            k.Korisnik.Id = vm.KorisnikId;
            k.Korisnik.Ime = vm.Ime;
            k.Korisnik.Prezime = vm.Prezime;
            k.Korisnik.Lozinka = vm.Lozinka;
            k.Korisnik.Adresa = vm.Lozinka;
            k.Korisnik.Telefon = vm.Telefon;
            k.Korisnik.Email = vm.Email;
            k.Korisnik.KorisnickoIme = vm.KorisnickoIme;
            k.Korisnik.Klijent = vm.Klijent;
            //plata
            p.Iznos = vm.iznos;
            p.PlataId = vm.plataId;
            //isplata
            i.IsplataId = vm.isplataId;
            i.PlataId = vm.plataId;

            
            ctx.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult KreirajProfil(UposleniciNewVM vm)
        {
            Data.MODELS.Uposlenik u = new Data.MODELS.Uposlenik();
            Korisnik k = new Korisnik();
            Plata p = new Plata();
            Isplata i = new Isplata();

            ctx.Korisnici.Add(k);
            ctx.Uposlenici.Add(u);
            ctx.Plate.Add(p);
            ctx.Isplate.Add(i);

            k.Adresa = vm.Adresa;
            k.Email = vm.Email;
            k.Ime = vm.Ime;
            k.KorisnickoIme = vm.KorisnickoIme;
            k.Lozinka = vm.Lozinka;
            k.Prezime = vm.Prezime;
            k.Telefon = vm.Telefon;
            u.KorisnikId = k.Id;
            u.UgovorPocetak = DateTime.Today;
            u.UgovorKraj = vm.UgovorKraj;

            u.Administrator = vm.Administrator != null ? vm.Administrator : false;
            u.Prodavac = vm.Prodavac != null ? vm.Prodavac: false;
            u.Serviser = vm.Serviser != null ? vm.Serviser: false;

            //plate
            p.Iznos = vm.iznos;
            i.PlataId = p.PlataId;
            i.UposlenikId = u.UposlenikId;

            ctx.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult DeleteProfil(int id)
        {
            Data.MODELS.Uposlenik u = ctx.Uposlenici.FirstOrDefault(x => x.UposlenikId == id);
            Korisnik k = ctx.Korisnici.FirstOrDefault(x => x.Id == u.KorisnikId);

            ctx.Uposlenici.Remove(u);
            ctx.Korisnici.Remove(k);

            Isplata isplata = ctx.Isplate.Where(x=> x.UposlenikId == id).SingleOrDefault();
            Plata plata = ctx.Plate.Where(x => x.PlataId == isplata.PlataId).SingleOrDefault();

            ctx.Isplate.Remove(isplata);
            ctx.Plate.Remove(plata);

            ctx.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult ZaradaIndexJson()
        {
            GodisnjaZaradaVM model = new GodisnjaZaradaVM();
            string currentYear = DateTime.Now.ToString("yyyy");

            var gubitak = ctx.Plate.Sum(y => y.Iznos);

            var data = ctx.Racuni.Where(a=> a.DatumIzdavanja.Value.Year.ToString() == currentYear).GroupBy(d => d.DatumIzdavanja.Value.Month).Select(x => new GodisnjaZaradaVM
            {
                period = x.Key.ToString(),
                zarada = x.Sum(a => a.Iznos),
                gubitak = gubitak
            });

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ZaradaIndex()
        {
            return View();
        }
    }
}