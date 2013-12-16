using System.Web.Mvc;
using WebSiteMjr.Assembler;
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

        public CheckinToolController(ICheckinToolService checkinToolService)
        {
            _checkinToolService = checkinToolService;
            _checkinToolMapper = new CheckinToolMapper(_checkinToolService);
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
            var checkinTool = _checkinToolService.FindToolCheckin(id);
            return View(checkinTool);
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
        public ActionResult Create(CheckinTool checkin)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View(checkin);

                _checkinToolService.CheckinTool(checkin);

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
            var checkinTool = _checkinToolService.FindToolCheckin(id);
            
            return View(checkinTool);
        }   
        

        //
        // POST: /Stuff/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, CheckinTool checkinTool)
        {
            try
            {
                if (!ModelState.IsValid) 
                    return View(checkinTool);
                
                _checkinToolService.UpdateToolCheckin(checkinTool);

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
            var checkinTool = _checkinToolService.FindToolCheckin(id);
            return View(checkinTool);
        }

        //
        // POST: /Stuff/Delete/5

        [HttpPost]
        public ActionResult Delete(CheckinTool checkinTool)
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
