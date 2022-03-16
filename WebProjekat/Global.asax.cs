using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using WebProjekat.Models;

namespace WebProjekat
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            string putanjaAdministratori = Server.MapPath("~/App_Data/Administratori.xml");
            string putanjaMenadzeri = Server.MapPath("~/App_Data/Menadzeri.xml");
            string putanjaTuristai = Server.MapPath("~/App_Data/Turistai.xml");

            BazaKorisnika bazaKorisnika = new BazaKorisnika(putanjaAdministratori, putanjaMenadzeri, putanjaTuristai);

            string putanjaAranzmana = Server.MapPath("~/App_Data/Aranzmani.xml");
            string putanjaSadrzaji = Server.MapPath("~/App_Data/Sadrzaji.xml");
            string putanjaRezervacije = Server.MapPath("~/App_Data/Rezervacije.xml");
            string putanjaKomentari = Server.MapPath("~/App_Data/Komentari.xml");
            string putanjaPraznici = Server.MapPath("~/App_Data/Praznici.xml");

            BazaAranzmana bazaAranzmana = new BazaAranzmana(putanjaAranzmana, putanjaSadrzaji);
            BazaRezervacija bazaRezervacija = new BazaRezervacija(putanjaRezervacije);
            BazaKomentara bazaKomentara = new BazaKomentara(putanjaKomentari);

            Praznici praznici = new Praznici(putanjaPraznici);
            LoggerXml logger = new LoggerXml(putanjaMenadzeri, putanjaTuristai, putanjaAranzmana, putanjaSadrzaji, putanjaRezervacije, putanjaKomentari, putanjaPraznici);
        }
    }
}
