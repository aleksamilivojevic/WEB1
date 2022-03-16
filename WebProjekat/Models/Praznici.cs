using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace WebProjekat.Models
{
    public class Praznici
    {
        public static List<DateTime> PraznicniDatumi { get; set; } = new List<DateTime>();
        public string Datumi { get; set; }

        public Praznici(string putanjaPraznici)
        {
            if (File.Exists(putanjaPraznici))
            {
                List<DateTime> praziciList = new List<DateTime>();
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<DateTime>));
                using (StreamReader reader = new StreamReader(putanjaPraznici))
                {
                    praziciList = (List<DateTime>)xmlSerializer.Deserialize(reader);
                }
                foreach (var item in praziciList)
                {
                    PraznicniDatumi.Add(item);
                }
            }
        }
    }
}