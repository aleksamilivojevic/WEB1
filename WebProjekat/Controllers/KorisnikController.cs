using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebProjekat.Models;
using WebProjekat.Models.Enum;

namespace WebProjekat.Controllers
{
    public class KorisnikController : ApiController
    {
        [Route("Korisnik/PrikaziProfil")]
        [HttpGet]
        public Korisnik PrikaziProfil()
        {
            string uri = Request.RequestUri.ToString();
            string trenutniKorisnik = uri.Split('=')[1];
            if (BazaKorisnika.Administratori.ContainsKey(trenutniKorisnik))
            {
                return BazaKorisnika.Administratori[trenutniKorisnik];
            }
            else if (BazaKorisnika.Menadzeri.ContainsKey(trenutniKorisnik))
            {
                return BazaKorisnika.Menadzeri[trenutniKorisnik];
            }
            else
            {
                return BazaKorisnika.Turistai[trenutniKorisnik];
            }
        }

        [Route("Korisnik/IzmeniKorisnika")]
        [HttpPost]
        public bool IzmeniKorisnika(Korisnik korisnik)
        {
            if (korisnik.KorisnickoIme == "" || korisnik.Lozinka == "" || korisnik.Ime == "" || korisnik.Prezime == "")
            {
                return false;
            }
            string uri = Request.RequestUri.ToString();
            string trenutniKorisnik = uri.Split('=')[1];
            if (BazaKorisnika.Administratori.ContainsKey(trenutniKorisnik))
                korisnik.Uloga = Uloga.ADMINISTRATOR;
            else if (BazaKorisnika.Menadzeri.ContainsKey(trenutniKorisnik))
                korisnik.Uloga = Uloga.Menadzer;
            else
                korisnik.Uloga = Uloga.Turista;
            korisnik.KorisnickoIme = trenutniKorisnik;
            switch (korisnik.Uloga)
            {
                case Uloga.ADMINISTRATOR:
                    {
                        BazaKorisnika.Administratori[korisnik.KorisnickoIme].KorisnickoIme = korisnik.KorisnickoIme;
                        BazaKorisnika.Administratori[korisnik.KorisnickoIme].Lozinka = korisnik.Lozinka;
                        BazaKorisnika.Administratori[korisnik.KorisnickoIme].Ime = korisnik.Ime;
                        BazaKorisnika.Administratori[korisnik.KorisnickoIme].Prezime = korisnik.Prezime;
                        BazaKorisnika.Administratori[korisnik.KorisnickoIme].Pol = korisnik.Pol;
                        
                        LoggerXml.UpisiXml("Administrator");
                        break;
                    }
                case Uloga.Menadzer:
                    {
                        BazaKorisnika.Menadzeri[korisnik.KorisnickoIme].KorisnickoIme = korisnik.KorisnickoIme;
                        BazaKorisnika.Menadzeri[korisnik.KorisnickoIme].Lozinka = korisnik.Lozinka;
                        BazaKorisnika.Menadzeri[korisnik.KorisnickoIme].Ime = korisnik.Ime;
                        BazaKorisnika.Menadzeri[korisnik.KorisnickoIme].Prezime = korisnik.Prezime;
                        BazaKorisnika.Menadzeri[korisnik.KorisnickoIme].Pol = korisnik.Pol;

                        LoggerXml.UpisiXml("Menadzer");
                        break;
                    }
                case Uloga.Turista:
                    {
                        BazaKorisnika.Turistai[korisnik.KorisnickoIme].KorisnickoIme = korisnik.KorisnickoIme;
                        BazaKorisnika.Turistai[korisnik.KorisnickoIme].Lozinka = korisnik.Lozinka;
                        BazaKorisnika.Turistai[korisnik.KorisnickoIme].Ime = korisnik.Ime;
                        BazaKorisnika.Turistai[korisnik.KorisnickoIme].Prezime = korisnik.Prezime;
                        BazaKorisnika.Turistai[korisnik.KorisnickoIme].Pol = korisnik.Pol;

                        LoggerXml.UpisiXml("Turista");                   
                        break;
                    }
            }
            return true;
        }

        [Route("Korisnik/Korisnici")]
        [HttpGet]
        public List<Korisnik> Korisnici()
        {
            List<Korisnik> sviKorisnici = new List<Korisnik>();
            foreach (var item in BazaKorisnika.Administratori.Values)
            {
                sviKorisnici.Add(item);
            }
            foreach (var item in BazaKorisnika.Menadzeri.Values)
            {
                sviKorisnici.Add(item);
            }
            foreach (var item in BazaKorisnika.Turistai.Values)
            {
                sviKorisnici.Add(item);
            }
            return sviKorisnici;
        }
    }
}
