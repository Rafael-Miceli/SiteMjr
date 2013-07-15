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
