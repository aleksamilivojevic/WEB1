using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace WebProjekat.Models
{
    public class BazaAranzmana
    {
        public static Dictionary<int, Aranzman> Aranzmani = new Dictionary<int, Aranzman>();
        public static Dictionary<int, Amenities> Sadrzaji = new Dictionary<int, Amenities>();

        public BazaAranzmana(string putanjaAranzmana, string putanjaSadrzaja)
        {
            if (File.Exists(putanjaAranzmana))
            {
                List<Aranzman> AranzmaniList = new List<Aranzman>();
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Aranzman>));
                using (StreamReader reader = new StreamReader(putanjaAranzmana))
                {
                    AranzmaniList = (List<Aranzman>)xmlSerializer.Deserialize(reader);
                }
                foreach(var item in AranzmaniList)
                {
                    Aranzmani.Add(item.Id, item);
                }
            }
            if (File.Exists(putanjaSadrzaja))
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Amenities>));
                List<Amenities> amenities = new List<Amenities>();
                using (StreamReader reader = new StreamReader(putanjaSadrzaja))
                {
                    amenities = (List<Amenities>)xmlSerializer.Deserialize(reader);
                }
                foreach (var item in amenities)
                {
                    Sadrzaji.Add(item.Id, item);
                }
            }
        }
    }
}