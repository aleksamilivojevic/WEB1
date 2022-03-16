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
    public class PretragaController : ApiController
    {
        [Route("Pertraga/PretragaKorisnika")]
        [HttpGet]
        public List<Korisnik> PretragaKorisnika()
        {
            string uri = Request.RequestUri.ToString();
            string[] uriSplit = uri.Split('=');
            string trenutniKorisnik = uriSplit[1];
            string kriterijum1 = uriSplit[3];
            string kriterijum2 = uriSplit[5];
            string kriterijum3 = uriSplit[7];
            Pol pol = new Pol();
            Uloga uloga = new Uloga();
            string korisnik = null;
            bool K1 = false;
            bool K2 = false;
            bool K3 = false;
            if(kriterijum1 != "NISTA")
            {
                pol = kriterijum1 == "MUSKI" ? Pol.MUSKI : Pol.ZENSKI;
                K1 = true;
            }
            if (kriterijum2 != "NISTA")
            {
                K2 = true;
                if (kriterijum2 == "ADMINISTRATOR")
                    uloga = Uloga.ADMINISTRATOR;
                else if (kriterijum2 == "Menadzer")
                    uloga = Uloga.Menadzer;
                else
                    uloga = Uloga.Turista;             
            }
            if(kriterijum3 != "")
            {
                K3 = true;
                korisnik = kriterijum3;
            }

            List<Korisnik> korisnici = new List<Korisnik>();

            if(K1 && K2 && K3)
            {
                if(uloga == Uloga.ADMINISTRATOR)
                {
                    if(BazaKorisnika.Administratori.ContainsKey(korisnik))
                    {
                        if(BazaKorisnika.Administratori[korisnik].Pol == pol)
                            korisnici.Add(BazaKorisnika.Administratori[korisnik]);            
                    }
                }
                else if(uloga == Uloga.Menadzer)
                {
                    if (BazaKorisnika.Menadzeri.ContainsKey(korisnik))
                    {
                        if (BazaKorisnika.Menadzeri[korisnik].Pol == pol)
                            korisnici.Add(BazaKorisnika.Menadzeri[korisnik]);
                    }
                }
                else
                {
                    if (BazaKorisnika.Turistai.ContainsKey(korisnik))
                    {
                        if (BazaKorisnika.Turistai[korisnik].Pol == pol)
                            korisnici.Add(BazaKorisnika.Turistai[korisnik]);
                    }
                }
            }
            else if(K1 && K2)
            {
                if(uloga == Uloga.ADMINISTRATOR)
                {
                    foreach(var item in BazaKorisnika.Administratori.Values)
                    {
                        if(item.Pol == pol)
                        {
                            korisnici.Add(item);
                        }
                    }
                }
                else if (uloga == Uloga.Menadzer)
                {
                    foreach(var item in BazaKorisnika.Menadzeri.Values)
                    {
                        if(item.Pol == pol)
                        {
                            korisnici.Add(item);
                        }
                    }
                }
                else
                {
                    foreach (var item in BazaKorisnika.Turistai.Values)
                    {
                        if (item.Pol == pol)
                        {
                            korisnici.Add(item);
                        }
                    }
                }
            }
            else if(K1 && K3)
            {
                if(BazaKorisnika.Administratori.ContainsKey(korisnik))
                {
                    if(BazaKorisnika.Administratori[korisnik].Pol == pol)
                    {
                        korisnici.Add(BazaKorisnika.Administratori[korisnik]);
                    }
                }
                else if (BazaKorisnika.Menadzeri.ContainsKey(korisnik))
                {
                    if(BazaKorisnika.Menadzeri[korisnik].Pol == pol)
                    {
                        korisnici.Add(BazaKorisnika.Menadzeri[korisnik]);
                    }
                }
                else if(BazaKorisnika.Turistai.ContainsKey(korisnik))
                {
                    if (BazaKorisnika.Turistai[korisnik].Pol == pol)
                    {
                        korisnici.Add(BazaKorisnika.Turistai[korisnik]);
                    }
                }
            }
            else if(K2 && K3)
            {
                if(uloga == Uloga.ADMINISTRATOR)
                {
                    if(BazaKorisnika.Administratori.ContainsKey(korisnik))
                    {
                        korisnici.Add(BazaKorisnika.Administratori[korisnik]);
                    }
                }
                else if (uloga == Uloga.Menadzer)
                {
                    if(BazaKorisnika.Menadzeri.ContainsKey(korisnik))
                    {
                        korisnici.Add(BazaKorisnika.Menadzeri[korisnik]);
                    }
                }
                else
                {
                    if (BazaKorisnika.Turistai.ContainsKey(korisnik))
                    {
                        korisnici.Add(BazaKorisnika.Turistai[korisnik]);
                    }
                }
            }
            else if(K1)
            {
                foreach(var item in BazaKorisnika.Administratori.Values)
                {
                    if(item.Pol == pol)
                    {
                        korisnici.Add(item);
                    }
                }
                foreach (var item in BazaKorisnika.Menadzeri.Values)
                {
                    if(item.Pol == pol)
                    {
                        korisnici.Add(item);
                    }
                }
                foreach (var item in BazaKorisnika.Turistai.Values)
                {
                    if(item.Pol == pol)
                    {
                        korisnici.Add(item);
                    }
                }
            }
            else if(K2)
            {
                if (uloga == Uloga.ADMINISTRATOR)
                {
                    foreach (var item in BazaKorisnika.Administratori.Values)
                    {
                        korisnici.Add(item);
                    }
                }
                else if (uloga == Uloga.ADMINISTRATOR)
                {
                    foreach (var item in BazaKorisnika.Menadzeri.Values)
                    {
                        korisnici.Add(item);
                    }
                }
                else
                {
                    foreach (var item in BazaKorisnika.Turistai.Values)
                    {
                        korisnici.Add(item);
                    }
                }
            } 
            else if(K3)
            {
                if(BazaKorisnika.Administratori.ContainsKey(korisnik))
                {
                    korisnici.Add(BazaKorisnika.Administratori[korisnik]);
                }
                else if (BazaKorisnika.Menadzeri.ContainsKey(korisnik))
                {
                    korisnici.Add(BazaKorisnika.Menadzeri[korisnik]);
                }
                else if (BazaKorisnika.Turistai.ContainsKey(korisnik))
                {
                    korisnici.Add(BazaKorisnika.Turistai[korisnik]);
                }
            }

            return korisnici;
        }

        [Route("Pertraga/PretragaRezervacija")]
        [HttpGet]
        public List<Rezervacija> PretragaRezervacija()
        {
            string uri = Request.RequestUri.ToString();
            string[] uriSplit = uri.Split('=');
            string trenutniKorisnik = uriSplit[1];
            string kriterijum1 = uriSplit[3];

            List<Rezervacija> rezervacije = new List<Rezervacija>();
            if(BazaKorisnika.Turistai.ContainsKey(kriterijum1))
            {
                foreach(var item in BazaRezervacija.Rezervacije.Values)
                {
                    if(item.Turista.KorisnickoIme == kriterijum1)
                    {
                        rezervacije.Add(item);
                    }
                }
            }

            return rezervacije;
        }
    }
}
