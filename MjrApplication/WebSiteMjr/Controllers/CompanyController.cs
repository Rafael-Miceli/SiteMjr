﻿using System.Web.Mvc;
using WebSiteMjr.Assembler;
using WebSiteMjr.Domain.Interfaces.Services;
using WebSiteMjr.Domain.Model;
using WebSiteMjr.Facade;
using WebSiteMjr.Filters;
using WebSiteMjr.ViewModels.CompaniesModel;

namespace WebSiteMjr.Controllers
{
    public class CompanyController : Controller
    {
        private readonly ICompanyService _companyService;
        private readonly ICompanyAdminUserFacade _companyAdminUserFacade;
        private readonly CompanyMapper _companyMapper;

        public CompanyController(ICompanyService companyService, ICompanyAreasService companyAreasService, ICompanyAdminUserFacade companyAdminUserFacade)
        {
            _companyService = companyService;
            _companyAdminUserFacade = companyAdminUserFacade;
            _companyMapper = new CompanyMapper(_companyService, companyAreasService);
        }

        //
        // GET: /Company/
        [FlexAuthorize(Roles = "MjrAdmin, MjrUser")]
        public ActionResult Index()
        {
            return View(_companyMapper.CompanyToListCompanyViewModel());
        }

        //
        // GET: /Company/Details/5

        [FlexAuthorize(Roles = "MjrAdmin, MjrUser, User")]
        public ActionResult Details(int id)
        {
            var company = _companyService.FindCompany(id);
            return View(company);
        }

        //
        // GET: /Company/Create
        [FlexAuthorize(Roles = "MjrAdmin, MjrUser")]
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Company/Create
        [FlexAuthorize(Roles = "MjrAdmin, MjrUser")]
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
        [FlexAuthorize(Roles = "MjrAdmin, MjrUser")]
        public ActionResult SendLogin(int idCompany)
        {
            try
            {
                _companyAdminUserFacade.CreateAdminUserForCompany(idCompany);
                return RedirectToAction("Index");
            }
            catch
            {
                return View("Edit");
            }
        }

        //
        // GET: /Company/Edit/5
        [FlexAuthorize(Roles = "MjrAdmin, MjrUser")]
        public ActionResult Edit(int id)
        {
            var edtiCompanyViewModel = _companyMapper.CompanyToEditCompanyViewModel(_companyService.FindCompany(id));
            return View(edtiCompanyViewModel);
        }

        //
        // POST: /Company/Edit/5
        [FlexAuthorize(Roles = "MjrAdmin, MjrUser")]
        [HttpPost]
        public ActionResult Edit(int id, EditCompanyViewModel editCompanyViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _companyService.UpdateCompany(_companyMapper.EditCompanyViewModelToCompany(editCompanyViewModel));

                    return RedirectToAction("Index");    
                }

                return View(editCompanyViewModel);
            }
            catch
            {
                return View(editCompanyViewModel);
            }
        }

        //
        // GET: /Company/Delete/5
        [FlexAuthorize(Roles = "MjrAdmin, MjrUser")]
        public ActionResult Delete(int id)
        {
            var company = _companyService.FindCompany(id);
            return View(company);
        }

        //
        // POST: /Company/Delete/5
        [FlexAuthorize(Roles = "MjrAdmin, MjrUser")]
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
