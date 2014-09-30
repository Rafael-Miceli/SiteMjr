using System;
using System.Web.Mvc;
using WebSiteMjr.Domain.Exceptions;
using WebSiteMjr.Domain.Model;
using WebSiteMjr.Domain.Model.Membership;
using WebSiteMjr.Facade;
using WebSiteMjr.Filters;
using WebSiteMjr.Models;
using WebSiteMjr.ViewModels;

namespace WebSiteMjr.Controllers
{
    [FlexAuthorize(Roles = "MjrAdmin, CompanyAdmin")]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeLoginFacade _employeeLoginFacade;

        public EmployeeController(IEmployeeLoginFacade employeeLoginFacade)
        {
            _employeeLoginFacade = employeeLoginFacade;
        }

        //
        // GET: /Employee/
        public ActionResult Index()
        {
            var employeeCompanyId = GetLoggedUser().Employee.Company.Id;

            return View(_employeeLoginFacade.ListEmployeesFromCompanyNotDeleted(employeeCompanyId));
        }

        private User GetLoggedUser()
        {
            return _employeeLoginFacade.GetLoggedUser(User.Identity.Name);
        }

        //
        // GET: /Employee/Details/5

        public ActionResult Details(int id)
        {
            var employee = _employeeLoginFacade.FindEmployee(id);
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
                    var employeeCompany = _employeeLoginFacade.GetLoggedUser(User.Identity.Name).Employee.Company;

                    if (employee.GenerateLogin)
                    {
                        if (String.IsNullOrEmpty(employee.Email))
                        {
                            ModelState.AddModelError("", "Para criar um login para o funcionário, preencha o E-mail do mesmo.");
                            return View(employee);
                        }

                        _employeeLoginFacade.CreateEmployeeAndLogin(employee, employeeCompany);
                    }
                    else
                        _employeeLoginFacade.CreateEmployee(employee, employeeCompany);

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
            var employee = _employeeLoginFacade.FindEmployee(id);
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
        public ActionResult Edit(Employee employee, string submitButton)
        {
            switch (submitButton)
            {
                case "Atualizar":
                    return (UpdateEmployee(employee));
                case "CriarLogin":
                    return (CreateLoginForExistentEmployee(employee));
                default:
                    return View();
            }
            
        }

        public ActionResult UpdateEmployee(Employee employee)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _employeeLoginFacade.UpdateEmployee(employee);

                    return RedirectToAction("Index");
                }

                return View("Edit", employee);
            }
            catch
            {
                return View("Edit");
            }
        }

        public ActionResult CreateLoginForExistentEmployee(Employee employee)
        {
            try
            {
                if (String.IsNullOrEmpty(employee.Email))
                {
                    ModelState.AddModelError("", "Para criar um login para o funcionário, preencha o E-mail do mesmo.");
                    return View("Edit", employee);
                }

                //TODO it should be great to try not need to go to the database get the instance of the user, but use the same instace it was used in the GET of this page

                _employeeLoginFacade.CreateNewUserForExistentEmployeeAccount(employee);

                return RedirectToAction("Index");
            }
            catch (EmployeeWithExistentEmailException ex)
            {
                ModelState.AddModelError("EmailExists", ex.Message);
                return View("Edit", employee);
            }
            catch
            {
                return View("Edit");
            }
        }

        //
        // GET: /Employee/Delete/5

        public ActionResult Delete(int id)
        {
            var employee = _employeeLoginFacade.FindEmployee(id);
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
                _employeeLoginFacade.DeleteEmployee(employee.Id);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

       
    }
}