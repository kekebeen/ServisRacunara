using ServisRacunara.Data.DAL;
using ServisRacunara.Data.MODELS;
using ServisRacunara.Web.Areas.Serviser.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ServisRacunara.Web.Areas.Serviser.Controllers
{
    public class BazaZnanjaController : Controller
    {
        // GET: Serviser/BazaZnanja
        MojContext ctx = new MojContext();
        public ActionResult Index()
        {
            BazaZnanjaIndexVm model = new BazaZnanjaIndexVm();
        
            //model.Kvarovi = ctx.KvarRacunarNalog.Select(x => new KvaroviStavke
            //{
            //    KvarId = x.KvarId,
            //    Opis = x.Kvar.Opis,
            //    Hardware = x.Kvar.Hardware,
            //    Software = x.Kvar.Software,
            //    RacunarId = x.RacunarId,
            //    OznakaRacunara = x.Racunar.Oznaka
            //}).ToList();

            model.Kvarovi = ctx.Kvarovi.Select(x => new KvaroviStavke
            {
                KvarId = x.KvarId,
                Opis = x.Opis,
                Hardware = x.Hardware,
                Software = x.Software,
            }).ToList();

            return View(model);
        }

        public ActionResult PretragaPoTipuKvara(int RacunarId)
        {
            BazaZnanjaIndexVm model = new BazaZnanjaIndexVm();

            model.Kvarovi = ctx.KvarRacunarNalog.Where(x => x.RacunarId == RacunarId).Select(a => new KvaroviStavke
            {
                KvarId = a.KvarId,
                Opis = a.Kvar.Opis,
                Hardware = a.Kvar.Hardware,
                Software = a.Kvar.Software
            }).ToList();

            return View("Index", model);
        }

        public ActionResult Details(int KvarId)
        {
            KvarDetailsVM model = new KvarDetailsVM();

            model.KvarId = KvarId;
            //model.Rjesenja = ctx.Rjesenja.Select(x => new RjesenjeStavke
            //{
            //    Datum = x.Datum,
            //    RjesenjeId = ctx.Popravke.Where(a => KvarId == KvarId).Select(y => y.KvarId).Single(),
            //    Hardware = x.Hardware,
            //    Software = x.Software,
            //    Trajanje = x.Trajanje,
            //    Opis = x.Opis
            //}).ToList();

            var rjesenja = (from rjesenje in ctx.Rjesenja
                            join popravka in ctx.Popravke on rjesenje.RjesenjeId equals popravka.RjesenjeId
                            join kvar in ctx.Kvarovi on popravka.KvarId equals kvar.KvarId
                            where kvar.KvarId == KvarId
                            select rjesenje).ToList();

            model.Rjesenja = rjesenja.Select(x => new RjesenjeStavke
            {
                Datum = x.Datum,
                RjesenjeId = x.RjesenjeId,
                Hardware = x.Hardware,
                Software = x.Software,
                Trajanje = x.Trajanje,
                Opis = x.Opis
            }).ToList();

            return View(model);
        }
    }
}