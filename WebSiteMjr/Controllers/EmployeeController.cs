using System;
using System.Web.Mvc;
using WebSiteMjr.Assembler;
using WebSiteMjr.Domain.Exceptions;
using WebSiteMjr.Domain.Interfaces.Services;
using WebSiteMjr.Domain.Model;
using WebSiteMjr.Filters;
using WebSiteMjr.Models;
using WebSiteMjr.ViewModels;

namespace WebSiteMjr.Controllers
{
    [FlexAuthorize(Roles = "MjrAdmin, CompanyAdmin")]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;
        private readonly ICacheService _cacheService;
        private readonly IMembershipService _membershipService;
        private readonly EmployeeMapper _employeeMapper;

        public EmployeeController(IEmployeeService employeeService, ICacheService cacheService, IMembershipService membershipService)
        {
            _employeeService = employeeService;
            _cacheService = cacheService;
            _membershipService = membershipService;
            _employeeMapper = new EmployeeMapper(employeeService);
        }

        //
        // GET: /Employee/
        public ActionResult Index()
        {
            var employeeCompanyId = _cacheService.Get("User", () => _membershipService.GetLoggedUser(User.Identity.Name)).Employee.Company.Id;
            return View(_employeeService.ListEmployeesFromCompanyNotDeleted(employeeCompanyId));
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
        public ActionResult Create(CreateEmployeeViewModel employee)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var employeeCompany = _cacheService.Get("User", () => _membershipService.GetLoggedUser(User.Identity.Name)).Employee.Company;

                    if (employee.GenerateLogin)
                    {
                        if (String.IsNullOrEmpty(employee.Email))
                        {
                            ModelState.AddModelError("", "Para criar um login para o funcionário, preencha o E-mail do mesmo.");
                            return View(employee);
                        }

                        _employeeService.CreateEmployeeAndLogin(_employeeMapper.CreateEmployeeViewModelToEmployee(employee, employeeCompany));
                    }
                    else
                        _employeeService.CreateEmployee(_employeeMapper.CreateEmployeeViewModelToEmployee(employee, employeeCompany));

                    return RedirectToAction("Index");
                }

                return View(employee);
            }
            catch (EmployeeWithExistentEmailException ex)
            {
                ModelState.AddModelError("EmailExists", ex.Message);
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