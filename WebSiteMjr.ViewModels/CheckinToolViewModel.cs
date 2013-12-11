using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WebSiteMjr.Domain.Model;

namespace WebSiteMjr.ViewModels
{
    public class CheckinToolViewModel
    {
        [Required(ErrorMessage = "Nome do Funcionário ou Condomínio é necessário")]
        public string EmployeeCompanyHolder { get; set; }
        public Tool Tool { get; set; }
        [Required(ErrorMessage = "Data do checkin é necessário")]
        public DateTime CheckinDateTime { get; set; }
        public IEnumerable<CheckinTool> CheckinTools { get; set; }
    }
}
