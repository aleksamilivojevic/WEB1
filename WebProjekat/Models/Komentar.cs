using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebProjekat.Models
{
    public class Komentar
    {
        public KorisnikTurista Turista { get; set; }
        public int IdAranzmana { get; set; }
        public string Tekst { get; set; }
        public int Ocena { get; set; }
        public int Id { get; set; }
        public bool Odobren { get; set; }
        public Komentar()
        {
            Odobren = false;
            int ukSadrzaja = BazaKomentara.Komentari.Count();
            if (ukSadrzaja > 0)
                Id = BazaKomentara.Komentari.Values.ToList()[ukSadrzaja - 1].Id + 1;
            else
                Id = 0;
        }
    }
}