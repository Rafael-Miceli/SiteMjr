using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WebSiteMjr.Domain.Model;

namespace WebSiteMjr.ViewModels
{
    public class ListCheckinToolViewModel
    {
        public Holder EmployeeCompanyHolder { get; set; }
        public Tool Tool { get; set; }
        public DateTime CheckinDateTime { get; set; }
        public IEnumerable<CheckinTool> CheckinTools { get; set; }
    }

    public class CreateCheckinToolViewModel
    {
        public string EmployeeCompanyHolderName { get; set; }
        public string ToolName { get; set; }
        public DateTime CheckinDateTime { get; set; }
    }
}
