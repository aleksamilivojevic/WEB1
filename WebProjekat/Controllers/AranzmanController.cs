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
    public class AranzmanController : ApiController
    {
        [Route("Aranzman/Aranzmani")]
        [HttpGet]
        public List<Aranzman> Aranzmani()
        {
            List<Aranzman> Aranzmani = new List<Aranzman>();
            foreach(var item in BazaAranzmana.Aranzmani.Values)
            {
                if (item.Obrisan == false)
                    Aranzmani.Add(item);
            }
            return Aranzmani;
        }

        [Route("Aranzman/AktivniAranzmani")]
        [HttpGet]
        public List<Aranzman> AktivniAranzmani()
        {
            string uri = Request.RequestUri.ToString();
            string trenutniKorisnik = uri.Split('=')[1];
            string trenutnaUloga = uri.Split('=')[2];
            List<Aranzman> Aranzmani = new List<Aranzman>();
            if (trenutnaUloga == "Menadzer")
            {
                foreach (var i in BazaKorisnika.Menadzeri[trenutniKorisnik].IdAranzmana)
                {
                    if (BazaAranzmana.Aranzmani[i].StatusAranzmana == StatusAranzmana.AKTIVAN && BazaAranzmana.Aranzmani[i].Obrisan == false)
                        Aranzmani.Add(BazaAranzmana.Aranzmani[i]);
                }
            }
            else
            {
                foreach(var item in BazaAranzmana.Aranzmani.Values)
                {
                    if (item.StatusAranzmana == StatusAranzmana.AKTIVAN && item.Obrisan == false)
                        Aranzmani.Add(item);
                }
            }
            return Aranzmani;
        }
        
        [Route("Aranzman/NeaktivniAranzmani")]
        [HttpGet]
        public List<Aranzman> NeaktivniAranzmani()
        {
            string uri = Request.RequestUri.ToString();
            string trenutniKorisnik = uri.Split('=')[1];
            string trenutnaUloga = uri.Split('=')[2];
            List<Aranzman> Aranzmani = new List<Aranzman>();
            if (trenutnaUloga == "Menadzer")
            {
                foreach (var i in BazaKorisnika.Menadzeri[trenutniKorisnik].IdAranzmana)
                {
                    if (BazaAranzmana.Aranzmani[i].StatusAranzmana == StatusAranzmana.NEAKTIVAN && BazaAranzmana.Aranzmani[i].Obrisan == false)
                        Aranzmani.Add(BazaAranzmana.Aranzmani[i]);
                }
            }
            else
            {
                foreach(var item in BazaAranzmana.Aranzmani.Values)
                {
                    if (item.StatusAranzmana == StatusAranzmana.NEAKTIVAN && item.Obrisan == false)
                        Aranzmani.Add(item);
                }
            }
            return Aranzmani;
        }
        
        [Route("Aranzman/SortiraniAranzmaniRa")]
        [HttpGet]
        public List<Aranzman> SortiraniAranzmaniRa()
        {
            string uri = Request.RequestUri.ToString();
            string trenutniKorisnik = uri.Split('=')[1];
            string trenutnaUloga = uri.Split('=')[2];
            List<Aranzman> Aranzmani = new List<Aranzman>();
            if (trenutnaUloga == "Menadzer")
            {
                foreach (var i in BazaKorisnika.Menadzeri[trenutniKorisnik].IdAranzmana)
                {
                    if (BazaAranzmana.Aranzmani[i].StatusAranzmana == StatusAranzmana.AKTIVAN && BazaAranzmana.Aranzmani[i].Obrisan == false)
                        Aranzmani.Add(BazaAranzmana.Aranzmani[i]);
                }
            }
            else
            {
                foreach (var item in BazaAranzmana.Aranzmani.Values)
                {
                    if (item.StatusAranzmana == StatusAranzmana.AKTIVAN && item.Obrisan == false)
                        Aranzmani.Add(item);
                }
            }

            int indexMax = 0;
            Aranzman pomocni = new Aranzman();
            for (int i = 0; i < Aranzmani.Count - 1; i++)
            {
                indexMax = i;
                for (int j = i + 1; j < Aranzmani.Count; j++)
                {
                    if (Aranzmani[indexMax].BrojTuristaiju > Aranzmani[j].BrojTuristaiju)
                    {
                        indexMax = j;
                    }
                }
                pomocni = Aranzmani[indexMax];
                Aranzmani[indexMax] = Aranzmani[i];
                Aranzmani[i] = pomocni;
            }
            return Aranzmani;
        }

        [Route("Aranzman/SortiraniAranzmaniOp")]
        [HttpGet]
        public List<Aranzman> SortiraniAranzmaniOp()
        {
            string uri = Request.RequestUri.ToString();
            string trenutniKorisnik = uri.Split('=')[1];
            string trenutnaUloga = uri.Split('=')[2];
            List<Aranzman> Aranzmani = new List<Aranzman>();
            if (trenutnaUloga == "Menadzer")
            {
                foreach (var i in BazaKorisnika.Menadzeri[trenutniKorisnik].IdAranzmana)
                {
                    if (BazaAranzmana.Aranzmani[i].StatusAranzmana == StatusAranzmana.AKTIVAN && BazaAranzmana.Aranzmani[i].Obrisan == false)
                        Aranzmani.Add(BazaAranzmana.Aranzmani[i]);
                }
            }
            else
            {
                foreach (var item in BazaAranzmana.Aranzmani.Values)
                {
                    if (item.StatusAranzmana == StatusAranzmana.AKTIVAN && item.Obrisan == false)
                        Aranzmani.Add(item);
                }
            }

            int indexMin = 0;
            Aranzman pomocni = new Aranzman();
            for (int i = 0; i < Aranzmani.Count - 1; i++)
            {
                indexMin = i;
                for (int j = i + 1; j < Aranzmani.Count; j++)
                {
                    if (Aranzmani[indexMin].BrojTuristaiju < Aranzmani[j].BrojTuristaiju)
                    {
                        indexMin = j;
                    }
                }
                pomocni = Aranzmani[indexMin];
                Aranzmani[indexMin] = Aranzmani[i];
                Aranzmani[i] = pomocni;
            }
            return Aranzmani;
        }

        [Route("Aranzman/NeregistrovaniKorisniciAranzmani")]
        [HttpGet]
        public List<Aranzman> NeregistrovaniKorisniciAranzmani()
        {
            List<Aranzman> Aranzmani = new List<Aranzman>();
            foreach(var item in BazaAranzmana.Aranzmani.Values)
            {
                if (item.StatusAranzmana == StatusAranzmana.AKTIVAN && item.Obrisan == false)
                {
                    Aranzmani.Add(item);
                }
            }

            return Aranzmani;
        }

        [Route("Aranzman/NeregistrovaniKorisniciAranzmaniSortiranoOp")]
        [HttpGet]
        public List<Aranzman> NeregistrovaniKorisniciAranzmaniSortiranoOp()
        {
            List<Aranzman> Aranzmani = new List<Aranzman>();
            foreach(var item in BazaAranzmana.Aranzmani.Values)
            {
                if(item.Obrisan == false && item.StatusAranzmana == StatusAranzmana.AKTIVAN)
                {
                    Aranzmani.Add(item);
                }
            }
            int indexMin = 0;
            Aranzman pomocni = new Aranzman();
            for (int i = 0; i < Aranzmani.Count - 1; i++)
            {
                indexMin = i;
                for (int j = i + 1; j < Aranzmani.Count; j++)
                {
                    if (Aranzmani[indexMin].BrojTuristaiju < Aranzmani[j].BrojTuristaiju)
                    {
                        indexMin = j;
                    }
                }
                pomocni = Aranzmani[indexMin];
                Aranzmani[indexMin] = Aranzmani[i];
                Aranzmani[i] = pomocni;
            }
            return Aranzmani;
        }

        [Route("Aranzman/NeregistrovaniKorisniciAranzmaniSortiranoRa")]
        [HttpGet]
        public List<Aranzman> NeregistrovaniKorisniciAranzmaniSortiranoRa()
        {
            List<Aranzman> Aranzmani = new List<Aranzman>();
            foreach(var item in BazaAranzmana.Aranzmani.Values)
            {
                if(item.Obrisan == false && item.StatusAranzmana == StatusAranzmana.AKTIVAN)
                {
                    Aranzmani.Add(item);
                }
            }
            int indexMin = 0;
            Aranzman pomocni = new Aranzman();
            for (int i = 0; i < Aranzmani.Count - 1; i++)
            {
                indexMin = i;
                for (int j = i + 1; j < Aranzmani.Count; j++)
                {
                    if (Aranzmani[indexMin].BrojTuristaiju > Aranzmani[j].BrojTuristaiju)
                    {
                        indexMin = j;
                    }
                }
                pomocni = Aranzmani[indexMin];
                Aranzmani[indexMin] = Aranzmani[i];
                Aranzmani[i] = pomocni;
            }
            return Aranzmani;
        }

        [Route("Aranzman/IzmeniAranzmanGet")]
        [HttpGet]
        public Aranzman IzmeniAranzmanGet()
        {
            string uri = Request.RequestUri.ToString();
            string id = uri.Split('=')[1];
            int idA = Int32.Parse(id.Substring(6));
            return BazaAranzmana.Aranzmani[idA];
        }

        [Route("Aranzman/IzmeniAranzman")]
        [HttpPost]
        public bool IzmeniAranzman([FromBody]Aranzman Aranzman)
        {
            string uri = Request.RequestUri.ToString();
            string id = uri.Split('=')[1];
            int idA = Int32.Parse(id.Substring(6));

            BazaAranzmana.Aranzmani[idA].Tip = Aranzman.Tip;
            BazaAranzmana.Aranzmani[idA].BrojSoba = Aranzman.BrojSoba;
            BazaAranzmana.Aranzmani[idA].BrojTuristaiju = Aranzman.BrojTuristaiju;

            string[] lokacijaSplit = Aranzman.LokacijaStr.Split('|');

            BazaAranzmana.Aranzmani[idA].Lokacija.Adresa.Mesto = lokacijaSplit[0];
            BazaAranzmana.Aranzmani[idA].Lokacija.Adresa.PostanskiBroj = Int32.Parse(lokacijaSplit[1]);
            BazaAranzmana.Aranzmani[idA].Lokacija.Adresa.Ulica = lokacijaSplit[2];
            if (lokacijaSplit[3] != "undefined")
                BazaAranzmana.Aranzmani[idA].Lokacija.Adresa.Broj = lokacijaSplit[3];
            else
                BazaAranzmana.Aranzmani[idA].Lokacija.Adresa.Broj = "bb";

            BazaAranzmana.Aranzmani[idA].CenaPoNocenju = Aranzman.CenaPoNocenju;

            if (Aranzman.Slika != "")
            {
                string[] slikaSplit = Aranzman.Slika.Split('\\');
                BazaAranzmana.Aranzmani[idA].Slika = slikaSplit[slikaSplit.Count() - 1];
            }

            LoggerXml.UpisiXml("Aranzman");
            return true;
        }

        [Route("Aranzman/ObrisiAranzman")]
        [HttpGet]
        public HttpResponseMessage ObrisiAranzman(string Id)
        {
            int idAp = Int32.Parse(Id.Substring(6));

            BazaAranzmana.Aranzmani[idAp].Obrisan = true;
            LoggerXml.UpisiXml("Aranzman");
            return new HttpResponseMessage();
        }
    }
}
