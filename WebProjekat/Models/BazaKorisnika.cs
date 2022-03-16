using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml.Serialization;
using WebProjekat.Models.Enum;

namespace WebProjekat.Models
{
    public class BazaKorisnika
    {
        public static Dictionary<string, KorisnikAdministrator> Administratori = new Dictionary<string, KorisnikAdministrator>();
        public static Dictionary<string, KorisnikMenadzer> Menadzeri = new Dictionary<string, KorisnikMenadzer>();
        public static Dictionary<string, KorisnikTurista> Turistai = new Dictionary<string, KorisnikTurista>();
        public BazaKorisnika(string p1, string p2, string p3)
        {
            if (File.Exists(p1))
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<KorisnikAdministrator>));
                List<KorisnikAdministrator> administratori = new List<KorisnikAdministrator>();
                using (StreamReader sr = new StreamReader(p1))
                {
                    administratori = (List<KorisnikAdministrator>)xmlSerializer.Deserialize(sr);
                }
                foreach (var item in administratori)
                {
                    Administratori.Add(item.KorisnickoIme, item);
                }
            }
            if (File.Exists(p2))
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<KorisnikMenadzer>));
                List<KorisnikMenadzer> menadzeri = new List<KorisnikMenadzer>();
                using (StreamReader sr = new StreamReader(p2))
                {
                    menadzeri = (List<KorisnikMenadzer>)xmlSerializer.Deserialize(sr);
                }
                foreach (var item in menadzeri)
                {
                    Menadzeri.Add(item.KorisnickoIme, item);
                }
            }
            if (File.Exists(p3))
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<KorisnikTurista>));
                List<KorisnikTurista> turistai = new List<KorisnikTurista>();
                using (StreamReader sr = new StreamReader(p3))
                {
                    turistai = (List<KorisnikTurista>)xmlSerializer.Deserialize(sr);
                }
                foreach (var item in turistai)
                {
                    Turistai.Add(item.KorisnickoIme, item);
                }
            }
        }
    }
}