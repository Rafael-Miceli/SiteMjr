using System.Web.Mvc;
using WebSiteMjr.Domain.Model;
using WebSiteMjr.Domain.services;
using WebSiteMjr.Filters;

namespace WebSiteMjr.Controllers
{
    public class StuffController : Controller
    {
        private readonly StuffService _stuffService;

        public StuffController(StuffService stuffService)
        {
            _stuffService = stuffService;
        }

        //
        // GET: /Stuff/
        [FlexAuthorize(Roles = "MjrAdmin")]
        public ActionResult Index()
        {
            return View(_stuffService.ListStuff());
        }

        //
        // GET: /Stuff/Details/5

        public ActionResult Details(int id)
        {
            var stuff = _stuffService.FindStuff(id);
            return View(stuff);
        }

        //
        // GET: /Stuff/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Stuff/Create

        [HttpPost]
        public ActionResult Create(Stuff stuff)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _stuffService.CreateStuff(stuff);

                    return RedirectToAction("Index");
                }

                return View(stuff);
            }
            catch
            {
                return View();
            }
        }

        public ActionResult SendLogin(int idStuff)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Stuff/Edit/5

        public ActionResult Edit(int id)
        {
            var stuff = _stuffService.FindStuff(id);
            return View(stuff);
        }

        //
        // POST: /Stuff/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, Stuff stuff)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _stuffService.UpdateStuff(stuff);

                    return RedirectToAction("Index");
                }

                return View(stuff);
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Stuff/Delete/5

        public ActionResult Delete(int id)
        {
            var stuff = _stuffService.FindStuff(id);
            return View(stuff);
        }

        //
        // POST: /Stuff/Delete/5

        [HttpPost]
        public ActionResult Delete(Stuff stuff)
        {
            try
            {
                _stuffService.DeleteStuff(stuff.Id);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
