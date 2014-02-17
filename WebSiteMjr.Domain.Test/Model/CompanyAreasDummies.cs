using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebSiteMjr.Domain.Model;

namespace WebSiteMjr.Domain.Test.Model
{
    public static class CompanyAreasDummies
    {
        public static CompanyArea CreateOneCompanyArea()
        {
            return new CompanyArea
            {
                Id = 1,
                Name = "Portão de visitantes"
            };
        }

        public static List<CompanyArea> CreateListOfCompanyAreas()
        {
            return new List<CompanyArea>
            {
                new CompanyArea
                {
                    Id = 2,
                    Name = "Portão de visitantes"
                },
                new CompanyArea
                {
                    Id = 3,
                    Name = "Portão de moradores"
                },
                new CompanyArea
                {
                    Id = 4,
                    Name = "Portão de pedestres"
                }
                ,
                new CompanyArea
                {
                    Id = 5,
                    Name = "P1"
                }
            };
        }
    }
}
