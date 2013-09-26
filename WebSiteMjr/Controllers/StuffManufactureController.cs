using System.Linq;
using System.Web.Mvc;
using WebSiteMjr.Assembler;
using WebSiteMjr.Domain.Model;
using WebSiteMjr.Domain.services.Stuffs;
using WebSiteMjr.Filters;
using WebSiteMjr.ViewModels;

namespace WebSiteMjr.Controllers
{
    [FlexAuthorize(Roles = "MjrAdmin")]
    public class StuffManufactureController : Controller
    {
        private readonly StuffManufactureService _stuffManufactureService;

        public StuffManufactureController(StuffManufactureService stuffManufactureService)
        {
            _stuffManufactureService = stuffManufactureService;
        }

        //
        // GET: /StuffManufacture/
        public ActionResult Index()
        {
            return View(_stuffManufactureService.ListStuffManufacture());
        }

        //
        // GET: /StuffManufacture/Details/5

        public ActionResult Details(int id)
        {
            return View(_stuffManufactureService.FindStuffManufacture(id));
        }

        //
        // GET: /StuffManufacture/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /StuffManufacture/Create

        [HttpPost]
        public ActionResult Create(StuffManufacture stuffManufacture)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View(stuffManufacture);

                _stuffManufactureService.CreateStuffManufacture(stuffManufacture);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /StuffManufacture/Edit/5

        public ActionResult Edit(int id)
        {
            return View(_stuffManufactureService.FindStuffManufacture(id));
        }

        //
        // POST: /StuffManufacture/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, StuffManufacture stuffManufacture)
        {
            try
            {
                if (!ModelState.IsValid) return View(stuffManufacture);

                _stuffManufactureService.UpdateStuffManufacture(_stuffMapper.StuffViewModelToStuff(stuff));

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /StuffManufacture/Delete/5

        public ActionResult Delete(int id)
        {
            
            return View(_stuffManufactureService.FindStuffManufacture(id));
        }

        //
        // POST: /StuffManufacture/Delete/5

        [HttpPost]
        public ActionResult Delete(StuffManufacture stuffManufacture)
        {
            try
            {
                _stuffManufactureService.DeleteStuffManufacture(stuffManufacture.Id);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}

