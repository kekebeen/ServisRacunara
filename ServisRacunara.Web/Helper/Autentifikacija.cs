﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServisRacunara.Data.MODELS;
using ServisRacunara.Data.DAL;


namespace ServisRacunara.Web.Helper
{
    public class Autentifikacija
    {
        private const string LogiraniKorisnik = "logirani_korisnik";

        public static void PokreniNovuSesiju(Korisnik korisnik, HttpContextBase context, bool zapamtiPassword)
        {
            context.Session.Add(LogiraniKorisnik, korisnik);

            if (zapamtiPassword)
            {
                HttpCookie cookie = new HttpCookie("_mvc_session", korisnik != null ? korisnik.Id.ToString() : "");
                cookie.Expires = DateTime.Now.AddDays(10);
                context.Response.Cookies.Add(cookie);
            }
        }

        public static Korisnik GetLogiraniKorisnik(HttpContextBase context)
        {
            Korisnik korisnik = (Korisnik)context.Session[LogiraniKorisnik];

            if (korisnik != null)
                return korisnik;

            HttpCookie cookie = context.Request.Cookies.Get("_mvc_session");

            if (cookie == null)
                return null;

            long userId;
            try
            {
                userId = Int64.Parse(cookie.Value);
            }
            catch
            {
                return null;
            }


            using (MojContext db = new MojContext())
            {
                Korisnik k = db.Korisnici.SingleOrDefault(x => x.Id == userId);

                PokreniNovuSesiju(k, context, true);
                return k;
            }
        }
    }
}