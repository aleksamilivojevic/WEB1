using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebProjekat.Models.Enum;

namespace WebProjekat.Models
{
    public class Aranzman
    {
        public int Id { get; set; }
        public TipAranzmana Tip { get; set; }
        public TipPrevoza Tipp { get; set; }
        public int BrojSoba { get; set; }
        public int BrojTuristaiju { get; set; }
        public Lokacija Lokacija { get; set; }
        public List<DateTime> DatumiZaIzdavanje { get; set; } = new List<DateTime>();
        public List<DateTime> Dostupnost { get; set; } = new List<DateTime>();
        public KorisnikMenadzer Menadzer { get; set; }
        public List<Komentar> Komentari { get; set; }
        public string Slika { get; set; }
        public double CenaPoNocenju { get; set; }
        public DateTime VremeZaPrijavu { get; set; }
        public DateTime VremeZaOdjavu { get; set; }
        public StatusAranzmana StatusAranzmana { get; set; }
        public List<Amenities> SadrazajAranzmana { get; set; } = new List<Amenities>();
        public List<Rezervacija> Rezervacije { get; set; } = new List<Rezervacija>();
        public string Sadrzaji { get; set; }
        public string Datumi { get; set; }
        public string LokacijaStr { get; set; }
        public string DostupnostStr { get; set; }
        public string SadrzajiStr { get; set; }
        public bool Obrisan { get; set; }
        public Aranzman()
        {
            Obrisan = false;
            DostupnostStr = "";
            SadrzajiStr = "";
            Lokacija = new Lokacija();
            StatusAranzmana = StatusAranzmana.NEAKTIVAN;
            VremeZaPrijavu = DateTime.Parse("02:00:00 PM");
            VremeZaOdjavu = DateTime.Parse("10:00:00 AM");
            int ukSadrzaja = BazaAranzmana.Aranzmani.Count();
            if (ukSadrzaja > 0)
                Id = BazaAranzmana.Aranzmani.Values.ToList()[ukSadrzaja - 1].Id + 1;
            else
                Id = 0;
        }

        public void IspisDatuma()
        {
            foreach(var item in Dostupnost)
            {
                DostupnostStr += item.ToLongDateString() + "</br>";
            }
        }

        public void IspisSadrzaja()
        {
            foreach (var item in SadrazajAranzmana)
            {
                if (!item.Obrisan)
                {
                    SadrzajiStr += item.Naziv += "</br>";
                }
            }
        }
    }
}