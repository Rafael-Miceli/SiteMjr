using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebSiteMjr.Views
{
    public class TaskController : Controller
    {
        //
        // GET: /ClientTask/

        public ActionResult Index()
        {
            return PartialView("Index");
        }

    }
}
