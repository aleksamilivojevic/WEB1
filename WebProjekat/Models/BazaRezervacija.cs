using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace WebProjekat.Models
{
    public class BazaRezervacija
    {
        public static Dictionary<int, Rezervacija> Rezervacije = new Dictionary<int, Rezervacija>();

        public BazaRezervacija(string putnajaRezervacija)
        {
            if (File.Exists(putnajaRezervacija))
            {
                List<Rezervacija> rezervacijeList = new List<Rezervacija>();
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Rezervacija>));
                using (StreamReader reader = new StreamReader(putnajaRezervacija))
                {
                    rezervacijeList = (List<Rezervacija>)xmlSerializer.Deserialize(reader);
                }
                foreach (var item in rezervacijeList)
                {
                    Rezervacije.Add(item.Id, item);
                }
            }
        }
    }
}