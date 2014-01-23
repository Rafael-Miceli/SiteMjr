using System.Web.Mvc;
using WebSiteMjr.Domain.Exceptions;
using WebSiteMjr.Domain.Model;
using WebSiteMjr.Domain.services.Stuffs;
using WebSiteMjr.Filters;

namespace WebSiteMjr.Controllers
{
    [FlexAuthorize(Roles = "MjrAdmin")]
    public class ToolLocalizationController : Controller
    {
        private readonly CompanyAreasService _companyAreasService;

        public ToolLocalizationController(CompanyAreasService companyAreasService)
        {
            _companyAreasService = companyAreasService;
        }

        //
        // GET: /StuffCategory/
        public ActionResult Index()
        {
            return View(_companyAreasService.ListToolsLocalizations());
        }

        //
        // GET: /StuffCategory/Details/5

        public ActionResult Details(int id)
        {
            return View(_companyAreasService.FindToolLocalization(id));
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
        public ActionResult Create(CompanyArea companyArea)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View(companyArea);

                _companyAreasService.CreateToolLocalization(companyArea);

                return RedirectToAction("Index");
            }
            catch (ObjectExistsException<CompanyArea> ex)
            {
                ModelState.AddModelError("ToolLocalizationExists", ex.Message);
                return View(companyArea);
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
            return View(_companyAreasService.FindToolLocalization(id));
        }

        //
        // POST: /StuffCategory/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, CompanyArea companyArea)
        {
            try
            {
                if (!ModelState.IsValid) return View(companyArea);

                _companyAreasService.UpdateToolLocalization(companyArea);

                return RedirectToAction("Index");
            }
            catch (ObjectExistsException<CompanyArea> ex)
            {
                ModelState.AddModelError("ToolLocalizationExists", ex.Message);
                return View(companyArea);
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

            return View(_companyAreasService.FindToolLocalization(id));
        }

        //
        // POST: /StuffCategory/Delete/5

        [HttpPost]
        public ActionResult Delete(CompanyArea companyArea)
        {
            try
            {
                _companyAreasService.DeleteToolLocalization(companyArea.Id);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
