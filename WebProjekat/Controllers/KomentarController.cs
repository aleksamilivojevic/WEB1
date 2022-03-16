using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebProjekat.Models;

namespace WebProjekat.Controllers
{
    public class KomentarController : ApiController
    {
        [Route("Komentar/KomentariAp")]
        [HttpGet]
        public List<Komentar> KomentariAp()
        {
            string uri = Request.RequestUri.ToString();
            string trenutniKorisnik = uri.Split('=')[1];
            List<Komentar> komentari = new List<Komentar>();
            List<int> idAranzmana = BazaKorisnika.Menadzeri[trenutniKorisnik].IdAranzmana;

            foreach (var item in BazaKomentara.Komentari.Values)
            { 
                if (idAranzmana.Contains(item.IdAranzmana))
                {
                    komentari.Add(item);
                }
            }

            return komentari;
        }

        [Route("Komentar/Komentari")]
        [HttpGet]
        public List<Komentar> Komentari()
        {
            return BazaKomentara.Komentari.Values.ToList();
        }

        [Route("Komentar/KomentariTurista")]
        [HttpGet]
        public List<Komentar> KomentariTurista()
        {
            List<Komentar> komentari = new List<Komentar>();

            foreach(var item in BazaKomentara.Komentari.Values)
            {
                if (item.Odobren)
                    komentari.Add(item);
            }

            return komentari;
        }

    }
}
