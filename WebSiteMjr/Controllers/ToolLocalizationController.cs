using System.Web.Mvc;
using WebSiteMjr.Domain.Model;
using WebSiteMjr.Domain.services.Stuffs;
using WebSiteMjr.Filters;

namespace WebSiteMjr.Controllers
{
    [FlexAuthorize(Roles = "MjrAdmin")]
    public class ToolLocalizationController : Controller
    {
        private readonly ToolLocalizationService _toolLocalizationService;

        public ToolLocalizationController(ToolLocalizationService toolLocalizationService)
        {
            _toolLocalizationService = toolLocalizationService;
        }

        //
        // GET: /StuffCategory/
        public ActionResult Index()
        {
            return View(_toolLocalizationService.ListToolsLocalizations());
        }

        //
        // GET: /StuffCategory/Details/5

        public ActionResult Details(int id)
        {
            return View(_toolLocalizationService.FindToolLocalization(id));
        }

        //
        // GET: /StuffCategory/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /StuffCategory/Create

        [HttpPost]
        public ActionResult Create(ToolLocalization toolLocalization)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View(toolLocalization);

                _toolLocalizationService.CreateToolLocalization(toolLocalization);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /StuffCategory/Edit/5

        public ActionResult Edit(int id)
        {
            return View(_toolLocalizationService.FindToolLocalization(id));
        }

        //
        // POST: /StuffCategory/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, ToolLocalization toolLocalization)
        {
            try
            {
                if (!ModelState.IsValid) return View(toolLocalization);

                _toolLocalizationService.UpdateToolLocalization(toolLocalization);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /StuffCategory/Delete/5

        public ActionResult Delete(int id)
        {

            return View(_toolLocalizationService.FindToolLocalization(id));
        }

        //
        // POST: /StuffCategory/Delete/5

        [HttpPost]
        public ActionResult Delete(ToolLocalization toolLocalization)
        {
            try
            {
                _toolLocalizationService.DeleteToolLocalization(toolLocalization.Id);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
