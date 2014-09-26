using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebSiteMjr.Assembler.CustomerService;
using WebSiteMjr.Domain.Interfaces.CustomerService;
using WebSiteMjr.Domain.Interfaces.Services;
using WebSiteMjr.ViewModels.CustomerService.Calls;

namespace WebSiteMjr.Areas.CustomerService.Controllers
{
    public class CallController : Controller
    {
        private readonly ICompanyService _companyService;
        private readonly IEmployeeService _employeeService;
        private readonly CallMapper _callMapper;

        public CallController(ICallService callService, ICompanyService companyService, IEmployeeService employeeService)
        {
            _companyService = companyService;
            _employeeService = employeeService;
            _callMapper = new CallMapper(callService);
        }

        //
        // GET: /CustomerService/Call/

        public ActionResult Index()
        {
            return View(_callMapper.GetIndexViewModel());
        }

        //
        // GET: /CustomerService/Call/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /CustomerService/Call/Create

        public ActionResult Create()
        {
            SetCompanyAndEmployeesViewBag();
            return View();
        }


        private void SetCompanyAndEmployeesViewBag(int? SelectedCompanyId = null, IEnumerable<int> SelectedEmployeesId = null)
        {
            var createCallViewModel = new CreateCallViewModel();

            var listCompanyNameAndIds = new List<CompanyNameAndId>();
            var listEmployeesToResolve = new List<EmployeeNameAndId>();

            foreach (var company in _companyService.ListCompaniesNotDeleted())
            {
                listCompanyNameAndIds.Add(new CompanyNameAndId
                {
                    CompanyName = company.Name,
                    Id = company.Id
                });
            }

            foreach (var employee in _employeeService.ListEmployeesNotDeleted())
            {
                listEmployeesToResolve.Add(new EmployeeNameAndId
                {
                    EmployeeName = employee.Name,
                    Id = employee.Id
                });
            }
            
            createCallViewModel.Companies = listCompanyNameAndIds.OrderBy(c => c.CompanyName);
            createCallViewModel.EmployeesToResolve = listEmployeesToResolve.OrderBy(e => e.EmployeeName);
            
            if (SelectedCompanyId == null)
                ViewBag.Companies = new SelectList(createCallViewModel.Companies, "Id", "CompanyName");
            else
                ViewBag.Companies = new SelectList(createCallViewModel.Companies, "Id", "CompanyName");

            if (SelectedEmployeesId == null)
                ViewBag.Employees = new MultiSelectList(createCallViewModel.EmployeesToResolve, "Id", "EmployeeName");


        }

        //
        // POST: /CustomerService/Call/Create

        [HttpPost]
        public ActionResult Create(CreateCallViewModel collection)
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
        // GET: /CustomerService/Call/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /CustomerService/Call/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /CustomerService/Call/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /CustomerService/Call/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
