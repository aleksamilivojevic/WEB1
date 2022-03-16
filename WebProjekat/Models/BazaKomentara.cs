using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace WebProjekat.Models
{
    public class BazaKomentara
    {
        public static Dictionary<int, Komentar> Komentari = new Dictionary<int, Komentar>();

        public BazaKomentara(string putnajaKomentara)
        {
            if (File.Exists(putnajaKomentara))
            {
                List<Komentar> komentariList = new List<Komentar>();
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Komentar>));
                using (StreamReader reader = new StreamReader(putnajaKomentara))
                {
                    komentariList = (List<Komentar>)xmlSerializer.Deserialize(reader);
                }
                foreach (var item in komentariList)
                {
                    Komentari.Add(item.Id, item);
                }
            }
        }
    }
}