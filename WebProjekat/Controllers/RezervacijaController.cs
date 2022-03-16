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
    public class RezervacijaController : ApiController
    {
        [Route("Rezervacija/Zahtev")]
        [HttpPost]
        public bool Zahtev([FromBody]Rezervacija rezervacija)
        {
            if(rezervacija.BrojNocenja < 0 || rezervacija.DatumRezervacije == null)
            {
                return false;
            }
            string uri = Request.RequestUri.ToString();
            string trenutniKorisnik = uri.Split('=')[1];
            int id = Int32.Parse(rezervacija.BtnId.Substring(6));
            Aranzman Aranzman = new Aranzman();
            if(BazaAranzmana.Aranzmani.TryGetValue(id, out Aranzman))
            {
                rezervacija.IdAranzmana = Aranzman.Id;
                rezervacija.Turista = BazaKorisnika.Turistai[trenutniKorisnik];
                rezervacija.UkupnaCena = 0;
                bool fleg = false;
                DateTime datum = rezervacija.DatumRezervacije;
                for (int i = 0; i < rezervacija.BrojNocenja; i++)
                {
                    if (Aranzman.Dostupnost.Contains(datum))
                    {
                        string danSplit = datum.DayOfWeek.ToString();
                        string dan = danSplit.Split(',')[0];
                        double cena = Aranzman.CenaPoNocenju;
                        if (Praznici.PraznicniDatumi.Contains(datum))
                        {
                            cena *= 1.05;
                        }
                        else if (dan == "Friday" || dan == "Saturday" || dan == "Sunday")
                        {
                            cena *= 0.9;
                        }

                        rezervacija.UkupnaCena += cena;
                        datum = datum.Date.AddDays(1);
                        fleg = true;
                    }
                    else
                    {
                        fleg = false;
                        break;
                    }
                }
                if (fleg)
                {
                    DateTime datumRez = rezervacija.DatumRezervacije;
                    for (int i = 0; i < rezervacija.BrojNocenja; i++)
                    {
                        BazaAranzmana.Aranzmani[Aranzman.Id].Dostupnost.Remove(datumRez);
                        datumRez = datumRez.Date.AddDays(1);
                    }
                    BazaAranzmana.Aranzmani[Aranzman.Id].Rezervacije.Add(rezervacija);
                    BazaRezervacija.Rezervacije.Add(rezervacija.Id, rezervacija);
                    BazaKorisnika.Turistai[trenutniKorisnik].IdRezervacija.Add(rezervacija.Id);
                    LoggerXml.UpisiXml("Aranzman");
                    LoggerXml.UpisiXml("Rezervacija");
                    LoggerXml.UpisiXml("Turista");
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }

        [Route("Rezervacija/Rezervacije")]
        [HttpGet]
        public List<Rezervacija> Rezervacije()
        {
            return BazaRezervacija.Rezervacije.Values.ToList();
        }

        [Route("Rezervacija/MenadzerRezervacije")]
        [HttpGet]
        public List<Rezervacija> MenadzerRezervacije()
        {
            string uri = Request.RequestUri.ToString();
            string trenutniKorisnik = uri.Split('=')[1];
            List<Rezervacija> rezervacije = new List<Rezervacija>();
            foreach(var itemAp in BazaKorisnika.Menadzeri[trenutniKorisnik].IdAranzmana)
            {
                foreach(var itemRez in BazaAranzmana.Aranzmani[itemAp].Rezervacije)
                {
                    if(itemRez.DatumRezervacije.Date.AddDays(itemRez.BrojNocenja) <= DateTime.Now)
                    {
                        itemRez.Zavrsena = true;
                        LoggerXml.UpisiXml("Rezervacija");
                    }
                    rezervacije.Add(itemRez);
                }
            }
            return rezervacije;
        }

        [Route("Rezervacija/TuristaRezervacije")]
        [HttpGet]
        public List<Rezervacija> TuristaRezervacije()
        {
            string uri = Request.RequestUri.ToString();
            string trenutniKorisnik = uri.Split('=')[1];
            List<Rezervacija> rezervacije = new List<Rezervacija>();
            foreach(var item in BazaRezervacija.Rezervacije.Values)
            {
                if(item.Turista.KorisnickoIme == trenutniKorisnik)
                    rezervacije.Add(item);
            }
            return rezervacije;
        }

        [Route("Rezervacija/SortiraneRezervacijeOp")]
        [HttpGet]
        public List<Rezervacija> SortiraneRezervacijeOp()
        {
            string uri = Request.RequestUri.ToString();
            string trenutniKorisnik = uri.Split('=')[1];
            List<Rezervacija> rezervacije = new List<Rezervacija>();
            foreach (var itemAp in BazaKorisnika.Menadzeri[trenutniKorisnik].IdAranzmana)
            {
                foreach (var itemRez in BazaAranzmana.Aranzmani[itemAp].Rezervacije)
                {
                    if (itemRez.DatumRezervacije.Date.AddDays(itemRez.BrojNocenja) <= DateTime.Now)
                    {
                        itemRez.Zavrsena = true;
                        LoggerXml.UpisiXml("Rezervacija");
                    }
                    rezervacije.Add(itemRez);
                }
            }
            int indexMin = 0;
            Rezervacija pomocni = new Rezervacija();
            for (int i = 0; i < rezervacije.Count - 1; i++)
            {
                indexMin = i;
                for (int j = i + 1; j < rezervacije.Count; j++)
                {
                    if (rezervacije[indexMin].UkupnaCena < rezervacije[j].UkupnaCena)
                    {
                        indexMin = j;
                    }
                }
                pomocni = rezervacije[indexMin];
                rezervacije[indexMin] = rezervacije[i];
                rezervacije[i] = pomocni;
            }
            return rezervacije;
        }

        [Route("Rezervacija/SortiraneRezervacijeRa")]
        [HttpGet]
        public List<Rezervacija> SortiraneRezervacijeRa()
        {
            string uri = Request.RequestUri.ToString();
            string trenutniKorisnik = uri.Split('=')[1];
            List<Rezervacija> rezervacije = new List<Rezervacija>();
            foreach (var itemAp in BazaKorisnika.Menadzeri[trenutniKorisnik].IdAranzmana)
            {
                foreach (var itemRez in BazaAranzmana.Aranzmani[itemAp].Rezervacije)
                {
                    if (itemRez.DatumRezervacije.Date.AddDays(itemRez.BrojNocenja) <= DateTime.Now)
                    {
                        itemRez.Zavrsena = true;
                        LoggerXml.UpisiXml("Rezervacija");
                    }
                    rezervacije.Add(itemRez);
                }
            }
            int indexMin = 0;
            Rezervacija pomocni = new Rezervacija();
            for (int i = 0; i < rezervacije.Count - 1; i++)
            {
                indexMin = i;
                for (int j = i + 1; j < rezervacije.Count; j++)
                {
                    if (rezervacije[indexMin].UkupnaCena > rezervacije[j].UkupnaCena)
                    {
                        indexMin = j;
                    }
                }
                pomocni = rezervacije[indexMin];
                rezervacije[indexMin] = rezervacije[i];
                rezervacije[i] = pomocni;
            }
            return rezervacije;
        }
    }
}
