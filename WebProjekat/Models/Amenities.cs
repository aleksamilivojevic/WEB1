using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebProjekat.Models
{
    public class Amenities
    {
        public int Id { get; set; }
        public string Naziv { get; set; }
        public bool Obrisan { get; set; }
        public Amenities()
        {
            int ukSadrzaja = BazaAranzmana.Sadrzaji.Values.ToList().Count();
            if (ukSadrzaja > 0)
                Id = BazaAranzmana.Sadrzaji.Values.ToList()[ukSadrzaja - 1].Id + 1;
            else
                Id = 0;
            Obrisan = false;
        }
    }
}