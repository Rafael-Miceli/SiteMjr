using System;
using System.Collections.Generic;
using WebSiteMjr.Domain.Model;

namespace WebSiteMjr.ViewModels
{
    public class CheckinToolViewModel
    {
        public string EmployeeCompanyHolder { get; set; }
        public Tool Tool { get; set; }
        public DateTime CheckinDateTime { get; set; }
        public IEnumerable<CheckinTool> CheckinTools { get; set; }
    }
}
