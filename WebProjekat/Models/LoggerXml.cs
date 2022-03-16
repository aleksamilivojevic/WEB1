using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace WebProjekat.Models
{
    public class LoggerXml
    {
        public static string MenadzerPutanja { get; set; }
        public static string TuristaPutanja { get; set; }
        public static string AranzmaniPutanja { get; set; }
        public static string SadrzajiPutanja { get; set; }
        public static string RezervacijaPutanja { get; set; }
        public static string KomentarPutanja { get; set; }
        public static string PrazniciPutanja { get; set; }
        public LoggerXml(string p1, string p2, string p3, string p4, string p5, string p6, string p7)
        {
            MenadzerPutanja = p1;
            TuristaPutanja = p2;
            AranzmaniPutanja = p3;
            SadrzajiPutanja = p4;
            RezervacijaPutanja = p5;
            KomentarPutanja = p6;
            PrazniciPutanja = p7;
        }
        public static void UpisiXml(string tip)
        {
            switch (tip)
            {
                case "Menadzer":
                    {
                        XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<KorisnikMenadzer>));
                        using (StreamWriter sw = new StreamWriter(MenadzerPutanja))
                        {
                            xmlSerializer.Serialize(sw, BazaKorisnika.Menadzeri.Values.ToList());
                        }
                        break;
                    }
                case "Turista":
                    {
                        XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<KorisnikTurista>));
                        using (StreamWriter sw = new StreamWriter(TuristaPutanja))
                        {
                            xmlSerializer.Serialize(sw, BazaKorisnika.Turistai.Values.ToList());
                        }
                        break;
                    }
                case "Aranzman":
                    {
                        XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Aranzman>));
                        using (StreamWriter sw = new StreamWriter(AranzmaniPutanja))
                        {
                            xmlSerializer.Serialize(sw, BazaAranzmana.Aranzmani.Values.ToList());
                        }
                        break;
                    }

                case "Amenities":
                    {
                        XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Amenities>));
                        using (StreamWriter sw = new StreamWriter(SadrzajiPutanja))
                        {
                            xmlSerializer.Serialize(sw, BazaAranzmana.Sadrzaji.Values.ToList());
                        }
                        break;
                    }

                case "Rezervacija":
                    {
                        XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Rezervacija>));
                        using (StreamWriter sw = new StreamWriter(RezervacijaPutanja))
                        {
                            xmlSerializer.Serialize(sw, BazaRezervacija.Rezervacije.Values.ToList());
                        }
                        break;
                    }

                case "Komentar":
                    {
                        XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Komentar>));
                        using (StreamWriter sw = new StreamWriter(KomentarPutanja))
                        {
                            xmlSerializer.Serialize(sw, BazaKomentara.Komentari.Values.ToList());
                        }
                        break;
                    }

                case "Praznici":
                    {
                        XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<DateTime>));
                        using (StreamWriter sw = new StreamWriter(PrazniciPutanja))
                        {
                            xmlSerializer.Serialize(sw, Praznici.PraznicniDatumi);
                        }
                        break;
                    }
            }
        }
    }
}