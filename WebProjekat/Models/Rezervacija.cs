using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebProjekat.Models.Enum;

namespace WebProjekat.Models
{
    public class Rezervacija
    {
        public int Id { get; set; }
        public int IdAranzmana { get; set; }
        public TipPrevoza TipP { get; set; }
        public DateTime DatumRezervacije { get; set; }
        public int BrojNocenja { get; set; }
        public double UkupnaCena { get; set; }
        public KorisnikTurista Turista { get; set; }
        public StatusRezervacije StatusRezervacije { get; set; }
        public string BtnId { get; set; }
        public bool Zavrsena { get; set; }
        public Rezervacija()
        {
            Zavrsena = false;
            BrojNocenja = 1;
            TipP = TipPrevoza.AUTOBUS;
            int ukSadrzaja = BazaRezervacija.Rezervacije.Count();
            if (ukSadrzaja > 0)
                Id = BazaRezervacija.Rezervacije.Values.ToList()[ukSadrzaja - 1].Id + 1;
            else
                Id = 0;
        }
    }
}