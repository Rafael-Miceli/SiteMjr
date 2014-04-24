using System.Web.Mvc;
using WebSiteMjr.Assembler;
using WebSiteMjr.Domain.Exceptions;
using WebSiteMjr.Domain.Interfaces.Services;
using WebSiteMjr.Domain.Model;
using WebSiteMjr.Filters;
using WebSiteMjr.ViewModels;

namespace WebSiteMjr.Controllers
{
    [FlexAuthorize(Roles = "MjrAdmin")] 
    public class CheckinToolController : Controller
    {
        private readonly ICheckinToolService _checkinToolService;
        private readonly CheckinToolMapper _checkinToolMapper;

        public CheckinToolController(ICheckinToolService checkinToolService, IToolService toolService, IHolderService holderService, ICompanyAreasService companyAreasService)
        {
            _checkinToolService = checkinToolService;
            _checkinToolMapper = new CheckinToolMapper(_checkinToolService, toolService, holderService, companyAreasService);
        }
        
        
        // GET: /Stuff/
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(ListCheckinToolViewModel checkinToolViewModel)
        {
            return View(_checkinToolMapper.GetChekinsFilter(checkinToolViewModel));
        }

        //
        // GET: /Stuff/Details/5

        public ActionResult Details(int id)
        {
            var checkinTool = _checkinToolMapper.CheckinToolToCreateCheckinToolViewModel((_checkinToolService.FindToolCheckin(id)));
            return View(checkinTool);
        }

        //
        // GET: /Stuff/Create

        public ActionResult Create()
        {
            var createCheckinToolViewModel = new CheckinToolViewModel();
            return View(createCheckinToolViewModel);
        }

        //
        // POST: /Stuff/Create

        [HttpPost]
        public ActionResult Create(CheckinToolViewModel checkin)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View(checkin);

                _checkinToolService.CheckinTool(_checkinToolMapper.CreateCheckinToolViewModelToCheckinTool(checkin));

                return RedirectToAction("Index");
            }
            catch (ObjectNotExistsException<Holder> ex)
            {
                ModelState.AddModelError("HolderNotExists", ex.Message);
                return View();
            }
            catch (ObjectNotExistsException<Tool> ex)
            {
                ModelState.AddModelError("ToolNotExists", ex.Message);
                return View();
            }
            catch (ObjectExistsException<CheckinTool> ex)
            {
                ModelState.AddModelError("DateExists", ex.Message);
                return View();
            }
            catch (CheckinInconsistencyException ex)
            {
                ModelState.AddModelError("DateExists", ex.Message);
                return View();
            }
            catch (CheckinCompanyToCompanyException ex)
            {
                ModelState.AddModelError("HolderNotExists", ex.Message);
                return View();
            }
            catch (CheckinHolderTwiceThenException ex)
            {
                ModelState.AddModelError("HolderNotExists", ex.Message);
                return View();
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
            var checkinTool = _checkinToolMapper.CheckinToolToCreateCheckinToolViewModel(_checkinToolService.FindToolCheckin(id));
            return View(checkinTool);
        }   
        

        //
        // POST: /Stuff/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, CheckinToolViewModel checkinTool)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View(checkinTool);

                _checkinToolService.UpdateToolCheckin(_checkinToolMapper.CreateCheckinToolViewModelToCheckinTool(checkinTool));

                return RedirectToAction("Index");
            }
            catch (ObjectNotExistsException<Holder> ex)
            {
                ModelState.AddModelError("HolderNotExists", ex.Message);
                return View();
            }
            catch (ObjectNotExistsException<Tool> ex)
            {
                ModelState.AddModelError("ToolNotExists", ex.Message);
                return View();
            }
            catch (ObjectExistsException<CheckinTool> ex)
            {
                ModelState.AddModelError("DateExists", ex.Message);
                return View();
            }
            catch (CheckinInconsistencyException ex)
            {
                ModelState.AddModelError("DateExists", ex.Message);
                return View();
            }
            catch (CheckinCompanyToCompanyException ex)
            {
                ModelState.AddModelError("HolderNotExists", ex.Message);
                return View();
            }
            catch (CheckinHolderTwiceThenException ex)
            {
                ModelState.AddModelError("HolderNotExists", ex.Message);
                return View();
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
            var checkinTool = _checkinToolMapper.CheckinToolToCreateCheckinToolViewModel(_checkinToolService.FindToolCheckin(id));
            return View(checkinTool);
        }

        //
        // POST: /Stuff/Delete/5

        [HttpPost]
        public ActionResult Delete(CheckinToolViewModel checkinTool)
        {
            try
            {
                _checkinToolService.DeleteToolCheckin(checkinTool.Id);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
