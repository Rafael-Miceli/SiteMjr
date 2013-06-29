using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebSiteMjr.Controllers
{
    public class ClientHomeController : Controller
    {
        //
        // GET: /ClientHome/

        public ActionResult Index()
        {
            return View("ClientHomeIndex");
        }

    }
}
