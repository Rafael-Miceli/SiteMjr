using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebSiteMjr.Domain.Model;

namespace WebSiteMjr.Domain.Test.Model
{
    public static class ToolLocalizationDummies
    {
        public static ToolLocalization CreateOneToolLocalization()
        {
            return new ToolLocalization
            {
                Id = 1,
                Name = "Portão de visitantes",
                Company = CompanyDumies.CreateOneCompany()
            };
        }
    }
}
