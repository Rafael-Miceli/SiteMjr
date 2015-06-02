using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.Sync;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebAppTalkToAzure.Controllers
{
    public class Clients
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }

    public class HomeController : Controller
    {
        MobileServiceClient mobileClient = new MobileServiceClient("https://arduinoapp.azure-mobile.net/", "QkTMsFHSEaNGuiKVsywYYHpHnIHMUB64");

        public ActionResult Index()
        {

            var clientTable = mobileClient.GetTable("Clients");

            var client = clientTable.ReadAsync("").Result;
            //var clientTable = mobileClient.GetTable<Clients>();

            //var client = clientTable.ToListAsync().Result;


            if (!client.Any())
                ViewBag.TemCliente = "Não";
            else
                ViewBag.Cliente = client.First();

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}