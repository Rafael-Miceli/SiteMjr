﻿using System.Linq;
using System.Web.Mvc;
using WebSiteMjr.Assembler;
using WebSiteMjr.Domain.Model;
using WebSiteMjr.Domain.services;
using WebSiteMjr.Filters;
using WebSiteMjr.ViewModels;

namespace WebSiteMjr.Controllers
{
    [FlexAuthorize(Roles = "MjrAdmin")] 
    public class StuffController : Controller
    {
        private readonly StuffService _stuffService;
        private readonly StuffMapper _stuffMapper;

        public StuffController(StuffService stuffService)
        {
            _stuffService = stuffService;
            _stuffMapper = new StuffMapper();
        }

        private void SetCategory_ManufactureViewBag(int? stuffCategoryId = null, int? stuffManufactureId = null)
        {
            ViewBag.StuffCategory = stuffCategoryId == null ?
                ViewBag.StuffCategory =  new SelectList(_stuffService.ListStuffCategories(), "Id", "Name") :
                ViewBag.StuffCategory =  new SelectList(_stuffService.ListStuffCategories().ToArray(), "Id", "Name", stuffCategoryId);

            ViewBag.StuffManufacture = stuffManufactureId == null ?
                ViewBag.StuffManufacture =  new SelectList(_stuffService.ListStuffManufacures(), "Id", "Name") :
                ViewBag.StuffManufacture =  new SelectList(_stuffService.ListStuffManufacures().ToArray(), "Id", "Name", stuffManufactureId);
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

        [HttpPost]
        public ActionResult CreateStuffCategory(StuffCategory stuffCategory)
        {
            _stuffService.CreateStuffCategory(stuffCategory);
            var stuffCateg = _stuffService.FindStuffCategoryByName(stuffCategory.Name);
            return Json(new {StuffCategory = stuffCateg});
        }

        [HttpPost]
        public ActionResult CreateStuffManufacture(StuffManufacture stuffManufacture)
        {
            _stuffService.CreateStuffManufacture(stuffManufacture);
            var stuffManu = _stuffService.FindStuffManufactureByName(stuffManufacture.Name);
            return Json(new { StuffManufacture = stuffManu });
        }

        //
        // GET: /Stuff/Edit/5

        public ActionResult Edit(int id)
        {
            var stuff = _stuffService.FindStuff(id);
            return View(_stuffMapper.StuffToStuffViewModel(stuff));
        }

        //
        // POST: /Stuff/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, EditStuffsViewModel stuff)
        {
            try
            {
                if (!ModelState.IsValid) return View(stuff);

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
