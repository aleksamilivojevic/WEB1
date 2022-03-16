using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebProjekat.Models
{
    public class Lokacija
    {
        public string GeografskaSirina { get; set; }
        public string GeografskaDuzina { get; set; }
        public Adresa Adresa { get; set; }

        public Lokacija()
        {
            Adresa = new Adresa();
        }
    }
}