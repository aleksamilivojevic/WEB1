using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using WebProjekat.Models;
using WebProjekat.Models.Enum;

namespace WebProjekat.Controllers
{
    public class RegistracijaController : ApiController
    {
        [Route("Registracija/RegistracijaKorisnika")]
        [HttpPost]
        public bool RegistracijaKorisnika([FromBody]KorisnikTurista korisnik)
        {
            if(korisnik.KorisnickoIme == "" || korisnik.Lozinka == "" || korisnik.Ime == "" || korisnik.Prezime == "")
            {
                return false;
            }
            if (!BazaKorisnika.Administratori.ContainsKey(korisnik.KorisnickoIme) && !BazaKorisnika.Menadzeri.ContainsKey(korisnik.KorisnickoIme) && !BazaKorisnika.Turistai.ContainsKey(korisnik.KorisnickoIme))
            {
                korisnik.Uloga = Uloga.Turista;
                BazaKorisnika.Turistai.Add(korisnik.KorisnickoIme, korisnik);
                LoggerXml.UpisiXml("Turista");
                return true;
            }
            else
            {
                return false;
            }
        }

        [Route("Registracija/PrijavaKorisnika")]
        [HttpPost]
        public string PrijavaKorisnika([FromBody]Prijava prijava)
        {
            if(prijava.KorisnickoIme == "" || prijava.Lozinka == "")
            {
                return "Neuspesna validacija"; 
            }
            if (BazaKorisnika.Administratori.ContainsKey(prijava.KorisnickoIme))
            {
                if (BazaKorisnika.Administratori[prijava.KorisnickoIme].Lozinka == prijava.Lozinka)
                {
                    return "Administrator";
                }
                else
                {
                    return "Pogresna lozinka";
                }
            }
            else if (BazaKorisnika.Menadzeri.ContainsKey(prijava.KorisnickoIme))
            {
                if (BazaKorisnika.Menadzeri[prijava.KorisnickoIme].Lozinka == prijava.Lozinka && BazaKorisnika.Menadzeri[prijava.KorisnickoIme].Blokiran == false)
                {
                    return "Menadzer";
                }
                else 
                {
                    if (BazaKorisnika.Menadzeri[prijava.KorisnickoIme].Blokiran == true)
                        return "Korisnik blokiran";
                    else
                        return "Pogresna lozinka";
                }
            }
            else if (BazaKorisnika.Turistai.ContainsKey(prijava.KorisnickoIme))
            {
                if (BazaKorisnika.Turistai[prijava.KorisnickoIme].Lozinka == prijava.Lozinka && BazaKorisnika.Turistai[prijava.KorisnickoIme].Blokiran == false)
                {
                    return "Turista";
                }
                else
                {
                    if (BazaKorisnika.Turistai[prijava.KorisnickoIme].Blokiran == true)
                        return "Korisnik blokiran";
                    else
                        return "Pogresna lozinka";
                }
            }
            else
            {
                return "Pogresno korisnicko ime";
            }
        }

        [Route("Registracija/OdjavaKorisnika")]
        [HttpGet]
        public bool OdjavaKorisnika()
        {
            return true;
        }
    }
}
