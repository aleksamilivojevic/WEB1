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
    public class AdministratorController : ApiController
    {
        [Route("Administrator/RegistracijaMenadzera")]
        [HttpPost]
        public bool RegistracijaMenadzera([FromBody]KorisnikMenadzer korisnik)
        {
            if (!BazaKorisnika.Administratori.ContainsKey(korisnik.KorisnickoIme) && !BazaKorisnika.Menadzeri.ContainsKey(korisnik.KorisnickoIme) && !BazaKorisnika.Turistai.ContainsKey(korisnik.KorisnickoIme))
            {
                korisnik.Uloga = Uloga.Menadzer;
                BazaKorisnika.Menadzeri.Add(korisnik.KorisnickoIme, korisnik);
                LoggerXml.UpisiXml("Menadzer");
                return true;
            }
            else
            {
                return false;
            }
        }

        [Route("Administrator/DodavanjeSadrzaja")]
        [HttpPost]
        public bool DodavanjeSadrzaja([FromBody]Amenities amenities)
        {
            BazaAranzmana.Sadrzaji.Add(amenities.Id, amenities);
            LoggerXml.UpisiXml("Amenities");
            return true;
        }

        [Route("Administrator/BrisanjeSadrzaja")]
        [HttpGet]
        public HttpResponseMessage BrisanjeSadrzaja(string Id)
        {
            int idSadrzaja = Int32.Parse(Id.Substring(3));
            BazaAranzmana.Sadrzaji[idSadrzaja].Obrisan = true;
            foreach (var item in BazaAranzmana.Aranzmani.Values)
            {
                for (int i = 0; i < item.SadrazajAranzmana.Count(); i++)
                {
                    if (item.SadrazajAranzmana[i].Id == idSadrzaja)
                    {
                        item.SadrazajAranzmana.RemoveAt(i);
                    }
                }
            }
            LoggerXml.UpisiXml("Amenities");
            LoggerXml.UpisiXml("Aranzman");
            return new HttpResponseMessage();
        }

        [Route("Administrator/AktivirajAranzman")]
        [HttpGet]
        public HttpResponseMessage AktivirajAranzman(string Id)
        {
            int idAp = Int32.Parse(Id.Substring(6));

            BazaAranzmana.Aranzmani[idAp].StatusAranzmana = StatusAranzmana.AKTIVAN;
            LoggerXml.UpisiXml("Aranzman");
            return new HttpResponseMessage();
        }

        [Route("Administrator/DeaktivirajAranzman")]
        [HttpGet]
        public HttpResponseMessage DeaktivirajAranzman(string Id)
        {
            int idAp = Int32.Parse(Id.Substring(6));

            BazaAranzmana.Aranzmani[idAp].StatusAranzmana = StatusAranzmana.NEAKTIVAN;
            LoggerXml.UpisiXml("Aranzman");
            return new HttpResponseMessage();
        }

        [Route("Administrator/Praznici")]
        [HttpPost]
        public bool PrazniciDodavanje([FromBody]Praznici praznici)
        {
            string[] datumiSplit = praznici.Datumi.Split(',');

            for (int i = 0; i < datumiSplit.Count(); i++)
            {
                DateTime datum = DateTime.Parse(datumiSplit[i]);
                Praznici.PraznicniDatumi.Add(datum);
            }
            LoggerXml.UpisiXml("Praznici");
            return true;
        }

        [Route("Administrator/BlokirajKorisnika")]
        [HttpGet]
        public HttpResponseMessage BlokirajKorisnika(string Id)
        {
            string korisnickoIme = Id.Substring(3);
            if (BazaKorisnika.Menadzeri.ContainsKey(korisnickoIme))
            {
                BazaKorisnika.Menadzeri[korisnickoIme].Blokiran = true;
                LoggerXml.UpisiXml("Menadzer");
            }
            else
            {
                BazaKorisnika.Turistai[korisnickoIme].Blokiran = true;
                LoggerXml.UpisiXml("Turista");
            }
            
            return new HttpResponseMessage();
        }

        [Route("Administrator/OdblokirajKorisnika")]
        [HttpGet]
        public HttpResponseMessage OdblokirajKorisnika(string Id)
        {
            string korisnickoIme = Id.Substring(3);
            if (BazaKorisnika.Menadzeri.ContainsKey(korisnickoIme))
            {
                BazaKorisnika.Menadzeri[korisnickoIme].Blokiran = false;
                LoggerXml.UpisiXml("Menadzer");
            }
            else
            {
                BazaKorisnika.Turistai[korisnickoIme].Blokiran = false;
                LoggerXml.UpisiXml("Turista");
            }
            return new HttpResponseMessage();
        }
    }
}
