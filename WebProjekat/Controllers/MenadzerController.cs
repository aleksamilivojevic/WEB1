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
    public class MenadzerController : ApiController
    {
        [Route("Menadzer/RegistracijaAranzmana")]
        [HttpPost]
        public bool RegistracijaAranzmana([FromBody]Aranzman Aranzman)
        {
            if(Aranzman.BrojSoba < 1 || Aranzman.BrojTuristaiju < 1 || Aranzman.CenaPoNocenju < 1 || Aranzman.Datumi == null || Aranzman.LokacijaStr == null || Aranzman.Slika == null)
            {
                return false;
            }
            string uri = Request.RequestUri.ToString();
            string trenutniKorisnik = uri.Split('=')[1];
            string[] idSadrzajaSplit = Aranzman.Sadrzaji.Split(' ');
            for (int i = 0; i < idSadrzajaSplit.Count() - 1; i++)
            {
                int j = Int32.Parse(idSadrzajaSplit[i]);
                Aranzman.SadrazajAranzmana.Add(BazaAranzmana.Sadrzaji[j]);
            }
            Aranzman.IspisSadrzaja();

            string[] datumiSplit = Aranzman.Datumi.Split(',');

            for (int i = 0; i < datumiSplit.Count(); i++)
            {
                DateTime datum = DateTime.Parse(datumiSplit[i]);
                Aranzman.DatumiZaIzdavanje.Add(datum);
                Aranzman.Dostupnost.Add(datum);
            }
            Aranzman.IspisDatuma();
            string[] lokacijaSplit = Aranzman.LokacijaStr.Split('|');

            Aranzman.Lokacija.Adresa.Mesto = lokacijaSplit[0];
            Aranzman.Lokacija.Adresa.PostanskiBroj = Int32.Parse(lokacijaSplit[1]);
            Aranzman.Lokacija.Adresa.Ulica = lokacijaSplit[2];
            if (lokacijaSplit[3] != "undefined")
                Aranzman.Lokacija.Adresa.Broj = lokacijaSplit[3];
            else
                Aranzman.Lokacija.Adresa.Broj = "bb";
            Aranzman.Lokacija.GeografskaSirina = lokacijaSplit[4];
            Aranzman.Lokacija.GeografskaDuzina = lokacijaSplit[5];

            string[] slikaSplit = Aranzman.Slika.Split('\\');
            Aranzman.Slika = slikaSplit[slikaSplit.Count() - 1];

            Aranzman.Menadzer = BazaKorisnika.Menadzeri[trenutniKorisnik];
            Aranzman.StatusAranzmana = StatusAranzmana.NEAKTIVAN;
            BazaAranzmana.Aranzmani.Add(Aranzman.Id, Aranzman);
            LoggerXml.UpisiXml("Aranzman");
            BazaKorisnika.Menadzeri[trenutniKorisnik].IdAranzmana.Add(Aranzman.Id);
            LoggerXml.UpisiXml("Menadzer");
            return true;
        }

        [Route("Menadzer/PrihvatiRezervaciju")]
        [HttpGet]
        public HttpResponseMessage PrihvatiRezervaciju(string id)
        {
            int idRez = Int32.Parse(id.Substring(12));
            BazaRezervacija.Rezervacije[idRez].StatusRezervacije = StatusRezervacije.PRIHVACENA;
            int idAp = BazaRezervacija.Rezervacije[idRez].IdAranzmana;
            foreach(var item in BazaAranzmana.Aranzmani[idAp].Rezervacije)
            {
                if(item.Id == idRez)
                {
                    item.StatusRezervacije = StatusRezervacije.PRIHVACENA;
                    break;
                }
            }
            //BazaAranzmana.Aranzmani[idAp].Rezervacije[BazaRezervacija.Rezervacije[idRez].Id].StatusRezervacije = StatusRezervacije.PRIHVACENA;
            LoggerXml.UpisiXml("Rezervacija");
            LoggerXml.UpisiXml("Aranzman");
            return new HttpResponseMessage();
        }

        [Route("Menadzer/OdbiRezervaciju")]
        [HttpGet]
        public HttpResponseMessage OdbiRezervaciju(string id)
        {
            int idRez = Int32.Parse(id.Substring(9));
            BazaRezervacija.Rezervacije[idRez].StatusRezervacije = StatusRezervacije.ODBIJENA;

            DateTime datum = BazaRezervacija.Rezervacije[idRez].DatumRezervacije;
            int idAp = BazaRezervacija.Rezervacije[idRez].IdAranzmana;
            for (int i = 0; i < BazaRezervacija.Rezervacije[idRez].BrojNocenja; i++)
            {
                BazaAranzmana.Aranzmani[idAp].Dostupnost.Add(datum);
                datum = datum.Date.AddDays(1);
            }

            foreach(var item in BazaAranzmana.Aranzmani[idAp].Rezervacije)
            {
                if (item.Id == idRez)
                    item.StatusRezervacije = StatusRezervacije.ODBIJENA;
            }
            LoggerXml.UpisiXml("Rezervacija");
            LoggerXml.UpisiXml("Aranzman");
            return new HttpResponseMessage();
        }

        [Route("Menadzer/ZavrsiRezervaciju")]
        [HttpGet]
        public HttpResponseMessage ZavrsiRezervaciju(string id)
        {
            int idRez = Int32.Parse(id.Substring(10));
            BazaRezervacija.Rezervacije[idRez].StatusRezervacije = StatusRezervacije.ZAVRSENA;

            LoggerXml.UpisiXml("Rezervacija");
            return new HttpResponseMessage();
        }

        [Route("Menadzer/OdobriKomentar")]
        [HttpGet]
        public HttpResponseMessage OdobriKomentar(string id)
        {
            int idKom = Int32.Parse(id.Substring(6));
            BazaKomentara.Komentari[idKom].Odobren = true;
            LoggerXml.UpisiXml("Komentar");
            return new HttpResponseMessage();
        }

        [Route("Menadzer/OdbaciKomentar")]
        [HttpGet]
        public HttpResponseMessage OdbaciKomentar(string id)
        {
            int idKom = Int32.Parse(id.Substring(6));
            BazaKomentara.Komentari[idKom].Odobren = false;
            LoggerXml.UpisiXml("Komentar");
            return new HttpResponseMessage();
        }

        [Route("Menadzer/TuristaiAranzmana")] 
        [HttpGet]
        public List<KorisnikTurista> TuristaiAranzmana()
        {
            string uri = Request.RequestUri.ToString();
            string trenutniKorisnik = uri.Split('=')[1];
            List<KorisnikTurista> Turistai = new List<KorisnikTurista>();
            foreach(var item in BazaAranzmana.Aranzmani.Values)
            {
                if(item.Menadzer.KorisnickoIme == trenutniKorisnik)
                {
                    foreach(var rezervacija in item.Rezervacije)
                    {
                        if(!Turistai.Contains(rezervacija.Turista))
                        {
                            Turistai.Add(rezervacija.Turista);
                        }
                    }
                }
            }
            return Turistai;         
        }
    }
}
