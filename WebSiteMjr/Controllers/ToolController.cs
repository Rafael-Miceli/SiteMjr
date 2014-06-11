using System.Linq;
using System.Web.Mvc;
using Mjr.Extensions;
using WebSiteMjr.Assembler;
using WebSiteMjr.Domain.Exceptions;
using WebSiteMjr.Domain.Interfaces.Services;
using WebSiteMjr.Domain.Model;
using WebSiteMjr.Filters;
using WebSiteMjr.ViewModels;

namespace WebSiteMjr.Controllers
{
    [FlexAuthorize(Roles = "MjrAdmin")] 
    public class ToolController : Controller
    {
        private readonly IToolService _toolService;
        private readonly ToolMapper _toolMapper;
        private readonly IStuffCategoryService _stuffCategoryService;
        private readonly IStuffManufactureService _stuffManufactureService;
        private readonly ICheckinToolService _checkinToolService;

        public ToolController(IToolService toolService, IStuffCategoryService stuffCategoryService, IStuffManufactureService stuffManufactureService,
             IHolderService holderService, ICompanyAreasService companyAreasService, ICheckinToolService checkinToolService)
        {
            _toolService = toolService;
            _stuffCategoryService = stuffCategoryService;
            _stuffManufactureService = stuffManufactureService;
            _checkinToolService = checkinToolService;
            _toolMapper = new ToolMapper(_stuffCategoryService, _stuffManufactureService, _toolService, holderService, companyAreasService, checkinToolService);
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
            return View(_toolService.ListNotDeletedTools());
        }

        //
        // GET: /Stuff/Details/5

        public ActionResult Details(int id)
        {
            var tool = _toolService.FindTool(id);
            return View(_toolMapper.ToolToToolViewModel(tool));
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
        public ActionResult Create(EditToolViewModel tool)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View(tool);

                _toolService.CreateTool(_toolMapper.ToolViewModelToTool(tool));

                return RedirectToAction("Index");
            }
            catch (ObjectExistsException<Tool> ex)
            {
                SetCategory_ManufactureViewBag();
                ModelState.AddModelError("ToolExists", ex.Message);
                return View();
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
                return RedirectToAction("Index", "Tool");

            return PartialView("_CreateStuffCategory");
        }

        [HttpGet]
        public ActionResult CreateStuffManufacture()
        {
            // Don't allow this method to be called directly.
            if (HttpContext.Request.IsAjaxRequest() != true)
                return RedirectToAction("Index", "Tool");

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
            var tool = _toolService.FindTool(id);

            SetCategory_ManufactureViewBag(tool.StuffCategory.IfEntitieIsNotNullReturnId(), tool.StuffManufacture.IfEntitieIsNotNullReturnId());    
            
            return View(_toolMapper.ToolToToolViewModel(tool));
        }   

        //
        // POST: /Stuff/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, EditToolViewModel tool)
        {
            try
            {
                if (!ModelState.IsValid) 
                    return View(tool);
                
                _toolService.UpdateTool(_toolMapper.ToolViewModelToTool(tool));

                return RedirectToAction("Index");
            }
            catch (ObjectExistsException<Tool> ex)
            {
                SetCategory_ManufactureViewBag();
                ModelState.AddModelError("ToolExists", ex.Message);
                return View(tool);
            }
            catch
            {
                return View();
            }
        }

        public ActionResult CheckinTab(int id)
        {
            return View(_toolMapper.CheckinsOfThisToolToCheckinToolTabViewModel(id));
        }

        [HttpPost]
        public ActionResult CheckinTab(CheckinToolTabViewModel checkinToolTabViewModel)
        {
            try
            {
                if (!ModelState.IsValid) 
                    return View(checkinToolTabViewModel);

                var checkinTool = _toolMapper.MapCheckinToolTabViewModelToCheckinTool(checkinToolTabViewModel);
                _checkinToolService.CheckinTool(checkinTool);

                return RedirectToAction("Edit", new { id = checkinToolTabViewModel.ToolId });
            }
            catch (ObjectNotExistsException<Holder> ex)
            {
                ModelState.AddModelError("HolderNotExists", ex.Message);
                return RedirectToAction("Edit", new { id = checkinToolTabViewModel.ToolId });
            }
            catch (ObjectExistsException<CheckinTool> ex)
            {
                ModelState.AddModelError("DateExists", ex.Message);
                return RedirectToAction("Edit", new { id = checkinToolTabViewModel.ToolId });
            }
            catch (CheckinInconsistencyException ex)
            {
                ModelState.AddModelError("DateExists", ex.Message);
                return RedirectToAction("Edit", new { id = checkinToolTabViewModel.ToolId });
            }
            catch (CheckinCompanyToCompanyException ex)
            {
                ModelState.AddModelError("HolderNotExists", ex.Message);
                return RedirectToAction("Edit", new { id = checkinToolTabViewModel.ToolId });
            }
            catch (CheckinHolderTwiceThenException ex)
            {
                ModelState.AddModelError("HolderNotExists", ex.Message);
                return RedirectToAction("Edit", new { id = checkinToolTabViewModel.ToolId });
            }
            catch
            {
                return RedirectToAction("Edit", new { id = checkinToolTabViewModel.ToolId });
            }
        }

        //
        // GET: /Stuff/Delete/5

        public ActionResult Delete(int id)
        {
            var tool = _toolService.FindTool(id);
            return View(_toolMapper.ToolToToolViewModel(tool));
        }

        //
        // POST: /Stuff/Delete/5

        [HttpPost]
        public ActionResult Delete(EditToolViewModel tool)
        {
            try
            {
                _toolService.DeleteTool(tool.Id);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
