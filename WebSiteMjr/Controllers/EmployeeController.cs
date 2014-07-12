using System.Web.Mvc;
using WebSiteMjr.Domain.Interfaces.Services;
using WebSiteMjr.Domain.Model;
using WebSiteMjr.Filters;

namespace WebSiteMjr.Controllers
{
    [FlexAuthorize(Roles = "MjrAdmin")]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        //
        // GET: /Employee/
        [FlexAuthorize(Roles = "MjrAdmin")]
        public ActionResult Index()
        {
            return View(_employeeService.ListEmployeesNotDeleted());
        }

        //
        // GET: /Employee/Details/5

        public ActionResult Details(int id)
        {
            var employee = _employeeService.FindEmployee(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        //
        // GET: /Employee/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Employee/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Employee employee)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _employeeService.CreateEmployee(employee);

                    return RedirectToAction("Index");
                }

                return View(employee);
            }
            catch
            {
                return View();
            }
            
        }

        //
        // GET: /Employee/Edit/5

        public ActionResult Edit(int id)
        {
            var employee = _employeeService.FindEmployee(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        //
        // POST: /Employee/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Employee employee)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _employeeService.UpdateEmployee(employee);

                    return RedirectToAction("Index");
                }

                return View(employee);
            }
            catch
            {
                return View();
            }
            
        }

        //
        // GET: /Employee/Delete/5

        public ActionResult Delete(int id)
        {
            var employee = _employeeService.FindEmployee(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        //
        // POST: /Employee/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Employee employee)
        {
            try
            {
                _employeeService.DeleteEmployee(employee.Id);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}