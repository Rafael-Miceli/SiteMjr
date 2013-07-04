using System.Web.Mvc;

namespace WebSiteMjr.Controllers
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
