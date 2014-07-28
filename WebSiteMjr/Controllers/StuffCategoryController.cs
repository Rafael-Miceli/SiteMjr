using System.Web.Mvc;
using WebSiteMjr.Domain.Model;
using WebSiteMjr.Domain.services.Stuffs;
using WebSiteMjr.Filters;

namespace WebSiteMjr.Controllers
{
    [FlexAuthorize(Roles = "MjrAdmin, MjrUser")]
    public class StuffCategoryController : Controller
    {
        private readonly StuffCategoryService _stuffCategoryService;

        public StuffCategoryController(StuffCategoryService stuffCategoryService)
        {
            _stuffCategoryService = stuffCategoryService;
        }

        //
        // GET: /StuffCategory/
        public ActionResult Index()
        {
            return View(_stuffCategoryService.ListStuffCategory());
        }

        //
        // GET: /StuffCategory/Details/5

        public ActionResult Details(int id)
        {
            return View(_stuffCategoryService.FindStuffCategory(id));
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
        public ActionResult Create(StuffCategory stuffCategory)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View(stuffCategory);

                _stuffCategoryService.CreateStuffCategory(stuffCategory);

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
            return View(_stuffCategoryService.FindStuffCategory(id));
        }

        //
        // POST: /StuffCategory/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, StuffCategory stuffCategory)
        {
            try
            {
                if (!ModelState.IsValid) return View(stuffCategory);

                _stuffCategoryService.UpdateStuffCategory(stuffCategory);

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

            return View(_stuffCategoryService.FindStuffCategory(id));
        }

        //
        // POST: /StuffCategory/Delete/5

        [HttpPost]
        public ActionResult Delete(StuffCategory stuffCategory)
        {
            try
            {
                _stuffCategoryService.DeleteStuffCategory(stuffCategory.Id);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
