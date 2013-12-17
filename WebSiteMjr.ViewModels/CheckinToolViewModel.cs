using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WebSiteMjr.Domain.Model;

namespace WebSiteMjr.ViewModels
{
    public class ListCheckinToolViewModel
    {
        public string EmployeeCompanyHolder { get; set; }
        public string Tool { get; set; }
        public DateTime ?CheckinDateTime { get; set; }
        public List<EnumerableCheckinToolViewModel> CheckinTools { get; set; }
    }

    public class EnumerableCheckinToolViewModel
    {
        public int Id { get; set; }
        public string EmployeeCompanyHolderName { get; set; }
        public string ToolName { get; set; }
        public DateTime CheckinDateTime { get; set; }
    }

    public class CreateCheckinToolViewModel
    {
        public string EmployeeCompanyHolderName { get; set; }
        public string ToolName { get; set; }
        public DateTime CheckinDateTime { get; set; }
    }
}
