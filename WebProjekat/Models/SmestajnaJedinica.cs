using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebProjekat.Models
{
    public class SmestajnaJedinica
    {
        public int DozvoljenBrojGostiju { get; set; }
        public bool DozvoljenoKucnimLjubimcima { get; set; }
        public int CenaZaSmestaj { get; set; }
        public SmestajnaJedinica()
        {

        }
    }
}