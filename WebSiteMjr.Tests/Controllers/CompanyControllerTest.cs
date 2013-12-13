﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebSiteMjr.Controllers;
using WebSiteMjr.Domain.services;
using WebSiteMjr.Domain.Test;

namespace WebSiteMjr.Tests.Controllers
{
    [TestClass]
    public class CompanyControllerTest
    {
        [TestMethod]
        public void Should_Return_Company_List()
        {
            var companyController = new CompanyController(new CompanyService(new StubCompanyRepository(), null));

            var result = companyController.Index();

            //Console.WriteLine(result.);

            //foreach (var company in result)
            //{
            //    Console.WriteLine(company.Name);
            //    Assert.AreNotEqual(null, company.Name);
            //}
        }
    }
}
