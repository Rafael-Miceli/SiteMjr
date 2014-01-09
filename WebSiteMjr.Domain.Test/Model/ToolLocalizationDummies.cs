using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebSiteMjr.Domain.Model;

namespace WebSiteMjr.Domain.Test.Model
{
    public static class ToolLocalizationDumies
    {
        public static ToolLocalization CreateOneToolLocalization()
        {
            return new ToolLocalization
            {
                Id = 1,
                Name = "Portão de visitantes"
            };
        }

        public static List<ToolLocalization> CreateListOfToolsLocalizations()
        {
            return new List<ToolLocalization>
            {
                new ToolLocalization
                {
                    Id = 2,
                    Name = "Portão de visitantes"
                },
                new ToolLocalization
                {
                    Id = 3,
                    Name = "Portão de moradores"
                },
                new ToolLocalization
                {
                    Id = 4,
                    Name = "Portão de pedestres"
                }
                ,
                new ToolLocalization
                {
                    Id = 5,
                    Name = "P1"
                }
            };
        }
    }
}
