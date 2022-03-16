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
    public class TuristaController : ApiController
    {
        [Route("Turista/OtkaziRezervaciju")]
        [HttpGet] 
        public HttpResponseMessage OtkaziRezervaciju(string Id)
        {
            int idRez = Int32.Parse(Id.Substring(10));
            BazaRezervacija.Rezervacije[idRez].StatusRezervacije = StatusRezervacije.ODUSTANAK;
            int idAp = BazaRezervacija.Rezervacije[idRez].IdAranzmana;
            DateTime datum = BazaRezervacija.Rezervacije[idRez].DatumRezervacije;
            for (int i = 0; i < BazaRezervacija.Rezervacije[idRez].BrojNocenja; i++)
            {
                BazaAranzmana.Aranzmani[idAp].Dostupnost.Add(datum);
                datum = datum.Date.AddDays(1);
            }
            LoggerXml.UpisiXml("Rezervacija");
            LoggerXml.UpisiXml("Aranzman");
            return new HttpResponseMessage();
        }

        [Route("Turista/RezervisaniAranzmani")]
        [HttpGet]
        public List<Aranzman> RezervisaniAranzmani ()
        {
            string uri = Request.RequestUri.ToString();
            string trenutniKorisnik = uri.Split('=')[1];
            List<int> idRezervacija = new List<int>();
            List<int> idRezervacijaSvih = BazaKorisnika.Turistai[trenutniKorisnik].IdRezervacija;
            foreach (var item in BazaRezervacija.Rezervacije.Values)
            {
                if (idRezervacijaSvih.Contains(item.Id))
                {
                    if (item.StatusRezervacije == StatusRezervacije.ODBIJENA || item.StatusRezervacije == StatusRezervacije.ZAVRSENA)
                    {
                        idRezervacija.Add(item.Id);
                    }
                }
            }
            
            List<int> idAranzmana = new List<int>();
            List<Aranzman> Aranzmani = new List<Aranzman>();
            foreach(var item in idRezervacija)
            {
                int idAp = BazaRezervacija.Rezervacije[item].IdAranzmana;
                if (!idAranzmana.Contains(idAp))
                {
                    idAranzmana.Add(idAp);
                    if(BazaAranzmana.Aranzmani[idAp].Obrisan == false)
                        Aranzmani.Add(BazaAranzmana.Aranzmani[idAp]);
                }
            }
            return Aranzmani;
        }
        
        [Route("Turista/Komentar")]
        [HttpPost]
        public bool Komentar([FromBody]Komentar komentar)
        {
            if(komentar.Tekst == null)
            {
                return false;
            }
            string uri = Request.RequestUri.ToString();
            string trenutniKorisnik = uri.Split('=')[1];
            komentar.Turista = BazaKorisnika.Turistai[trenutniKorisnik];
            int idAp = komentar.IdAranzmana;
            BazaKomentara.Komentari.Add(komentar.Id, komentar);
            LoggerXml.UpisiXml("Komentar");            
            return true;
        }
    }
}
