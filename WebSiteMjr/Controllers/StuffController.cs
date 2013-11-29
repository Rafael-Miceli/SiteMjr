using System.Linq;
using System.Web.Mvc;
using Mjr.Extensions;
using WebSiteMjr.Assembler;
using WebSiteMjr.Domain.Exceptions;
using WebSiteMjr.Domain.Interfaces.Services;
using WebSiteMjr.Domain.Model;
using WebSiteMjr.Domain.Model.Person;
using WebSiteMjr.Filters;
using WebSiteMjr.ViewModels;

namespace WebSiteMjr.Controllers
{
    [FlexAuthorize(Roles = "MjrAdmin")] 
    public class StuffController : Controller
    {
        private readonly IStuffService _stuffService;
        private readonly StuffMapper _stuffMapper;
        private readonly IStuffCategoryService _stuffCategoryService;
        private readonly IStuffManufactureService _stuffManufactureService; 

        public StuffController(IStuffService stuffService, IStuffCategoryService stuffCategoryService, IStuffManufactureService stuffManufactureService)
        {
            _stuffService = stuffService;
            _stuffCategoryService = stuffCategoryService;
            _stuffManufactureService = stuffManufactureService;
            _stuffMapper = new StuffMapper(_stuffCategoryService, _stuffManufactureService, _stuffService);
        }

        private void SetCategory_ManufactureViewBag(int? stuffCategoryId = null, int? stuffManufactureId = null)
        {
            ViewBag.StuffCategory = stuffCategoryId == null ?
                ViewBag.StuffCategory =  new SelectList(_stuffCategoryService.ListStuffCategory(), "Id", "Name") :
                ViewBag.StuffCategory =  new SelectList(_stuffCategoryService.ListStuffCategory().ToArray(), "Id", "Name", stuffCategoryId);

            ViewBag.StuffManufacture = stuffManufactureId == null ?
                ViewBag.StuffManufacture =  new SelectList(_stuffManufactureService.ListStuffManufacture(), "Id", "Name") :
                ViewBag.StuffManufacture =  new SelectList(_stuffManufactureService.ListStuffManufacture().ToArray(), "Id", "Name", stuffManufactureId);
        }

        //
        // GET: /Stuff/
        public ActionResult Index()
        {
            return View(_stuffService.ListStuff());
        }

        //
        // GET: /Stuff/Details/5

        public ActionResult Details(int id)
        {
            var stuff = _stuffService.FindStuff(id);
            return View(_stuffMapper.StuffToStuffViewModel(stuff));
        }

        //
        // GET: /Stuff/Create

        public ActionResult Create()
        {
            SetCategory_ManufactureViewBag();
            return View();
        }

        //
        // POST: /Stuff/Create

        [HttpPost]
        public ActionResult Create(EditStuffsViewModel stuff)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View(stuff);

                _stuffService.CreateStuff(_stuffMapper.StuffViewModelToStuff(stuff));

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        [HttpGet]
        public ActionResult CreateStuffCategory()
        {
            // Don't allow this method to be called directly.
            if (HttpContext.Request.IsAjaxRequest() != true)
                return RedirectToAction("Index", "Stuff");

            return PartialView("_CreateStuffCategory");
        }

        [HttpGet]
        public ActionResult CreateStuffManufacture()
        {
            // Don't allow this method to be called directly.
            if (HttpContext.Request.IsAjaxRequest() != true)
                return RedirectToAction("Index", "Stuff");

            return PartialView("_CreateSuffManufacture");
        }

        [HttpPost]
        public JsonResult CreateStuffCategory(StuffCategory stuffCategory)
        {
            try
            {
                _stuffCategoryService.CreateStuffCategory(stuffCategory);
                var stuffCateg = _stuffCategoryService.FindStuffCategoryByName(stuffCategory.Name);
                return Json(new { StuffCategory = stuffCateg });
            }
            catch (ObjectExistsException<StuffCategory> ex)
            {
                return Json(new {Error = ex.Message});
            }
        }

        [HttpPost]
        public ActionResult CreateStuffManufacture(StuffManufacture stuffManufacture)
        {
            try
            {
                _stuffManufactureService.CreateStuffManufacture(stuffManufacture);
                var stuffManu = _stuffManufactureService.FindStuffManufactureByName(stuffManufacture.Name);
                return Json(new { StuffManufacture = stuffManu });
            }
            catch (ObjectExistsException<StuffManufacture> ex)
            {
                return Json(new {Error = ex.Message});
            }
            
        }

        //
        // GET: /Stuff/Edit/5

        public ActionResult Edit(int id)
        {
            var stuff = _stuffService.FindStuff(id);

            SetCategory_ManufactureViewBag(stuff.StuffCategory.IfEntitieIsNotNullReturnId(), stuff.StuffManufacture.IfEntitieIsNotNullReturnId());    
            
            return View(_stuffMapper.StuffToStuffViewModel(stuff));
        }   
        

        //
        // POST: /Stuff/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, EditStuffsViewModel stuff)
        {
            try
            {
                if (!ModelState.IsValid) 
                    return View(stuff);
                
                _stuffService.UpdateStuff(_stuffMapper.StuffViewModelToStuff(stuff));

                return RedirectToAction("Index");
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
            return View(_stuffMapper.StuffToStuffViewModel(stuff));
        }

        //
        // POST: /Stuff/Delete/5

        [HttpPost]
        public ActionResult Delete(EditStuffsViewModel stuff)
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
