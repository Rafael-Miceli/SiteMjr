using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSiteMjr.Domain.Model;
using WebSiteMjr.ViewModels;

namespace WebSiteMjr.Domain.Test.Model
{
    public static class CheckinToolDummies
    {
        public static CheckinTool CreateOneCheckinTool()
        {
            return new CheckinTool
            {
                Id = 1,
                CheckinDateTime = new DateTime(2014, 1, 21, 13, 23, 00),
                EmployeeCompanyHolderId = 1,
                Tool = new Tool {Id = 1, Name = "Ferramenta 1"}
            };
        }

        public static CreateCheckinToolViewModel CreateOneCheckinToolViewModel()
        {
            return new CreateCheckinToolViewModel
            {
                Id = 1,
                CheckinDateTime = new DateTime(2014, 1, 21, 13, 23, 00),
                EmployeeCompanyHolderName = "Celso",
                ToolName = "Ferramenta 1",
                CompanyAreaName = "Portão"
            };
        }

        public static CreateCheckinToolViewModel CreateOneCheckinToolViewModelWithoutCompanyArea()
        {
            return new CreateCheckinToolViewModel
            {
                Id = 1,
                CheckinDateTime = new DateTime(2014, 1, 21, 13, 23, 00),
                EmployeeCompanyHolderName = "Celso",
                ToolName = "Ferramenta 1"
            };
        }
    }
}
