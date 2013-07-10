using System.Web.Mvc;
using WebSiteMjr.Domain.Model;
using WebSiteMjr.Domain.services;
using WebSiteMjr.Filters;

namespace WebSiteMjr.Controllers
{
    public class CompanyController : Controller
    {
        private readonly CompanyService _companyService;

        public CompanyController(CompanyService companyService)
        {
            _companyService = companyService;
        }

        //
        // GET: /Company/
        [FlexAuthorize(Roles = "MjrAdmin")]
        public ActionResult Index()
        {
            return View(_companyService.ListCompany());
        }

        //
        // GET: /Company/Details/5

        public ActionResult Details(int id)
        {
            var company = _companyService.FindCompany(id);
            return View(company);
        }

        //
        // GET: /Company/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Company/Create

        [HttpPost]
        public ActionResult Create(Company company)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _companyService.CreateCompany(company);

                    return RedirectToAction("Index");
                }

                return View(company);
            }
            catch
            {
                return View();
            }
        }

        public ActionResult SendLogin(int idCompany)
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
        // GET: /Company/Edit/5

        public ActionResult Edit(int id)
        {
            var company = _companyService.FindCompany(id);
            return View(company);
        }

        //
        // POST: /Company/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, Company company)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _companyService.UpdateCompany(company);

                    return RedirectToAction("Index");    
                }

                return View(company);
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Company/Delete/5

        public ActionResult Delete(int id)
        {
            var company = _companyService.FindCompany(id);
            return View(company);
        }

        //
        // POST: /Company/Delete/5

        [HttpPost]
        public ActionResult Delete(Company company)
        {
            try
            {
                _companyService.DeleteCompany(company.Id);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
