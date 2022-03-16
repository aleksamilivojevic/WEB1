using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebProjekat.Models;

namespace WebProjekat.Controllers
{
    public class AmenitiesController : ApiController
    {
        [Route("Amenities/Sadrzaji")]
        [HttpGet]
        public List<Amenities> Sadrzaji()
        {
            List<Amenities> sadrzaji = new List<Amenities>();
            foreach (var item in BazaAranzmana.Sadrzaji.Values)
            {
                if (item.Obrisan == false)
                {
                    sadrzaji.Add(item);
                }
            }
            return sadrzaji;
        }
    }
}
