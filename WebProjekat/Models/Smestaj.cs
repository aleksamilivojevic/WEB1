using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebProjekat.Models.Enum;

namespace WebProjekat.Models
{
    public class Smestaj
    {
        public TipSmestaja TipSmestaja { get; set; }
        public string Naziv { get; set; }
        public bool PostojanjeBazena { get; set; }
        public bool PostojanjeSpa { get; set; }
        public bool PrilagodjenoInvaliditet { get; set; }
        public bool WiFi { get; set; }
        public List<SmestajnaJedinica> SmestajneJedinice { get; set; } = new List<SmestajnaJedinica>();
        public Smestaj()
        {

        }
    }
}