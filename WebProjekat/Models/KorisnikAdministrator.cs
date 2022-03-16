using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebProjekat.Models.Enum;

namespace WebProjekat.Models
{
    public class KorisnikAdministrator : Korisnik
    {
        public KorisnikAdministrator()
        {

        }

        public KorisnikAdministrator(string korisnickoIme, string lozinka, string ime, string prezime, string pol)
        {
            KorisnickoIme = korisnickoIme;
            Lozinka = lozinka;
            Ime = ime;
            Prezime = prezime;
            if (pol.Equals("Muski")) { Pol = Pol.MUSKI; } else { Pol = Pol.ZENSKI; }
            Uloga = Uloga.ADMINISTRATOR;
        }
    }
}