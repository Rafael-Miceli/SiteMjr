using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WebSiteMjr.Domain.Model;

namespace WebSiteMjr.ViewModels
{
    public class ListCheckinToolViewModel
    {
        public string EmployeeCompanyHolder { get; set; }
        public Tool Tool { get; set; }
        public DateTime CheckinDateTime { get; set; }
        public IEnumerable<CheckinTool> CheckinTools { get; set; }
    }
    public class CreateCheckinToolViewModel
    {
        [Required(ErrorMessage = "Ferramenta é necessária.")]
        public Tool Tool { get; set; }
        [Required(ErrorMessage = "Nome do Funcionário ou Condomínio é necessário.")]
        public string EmployeeCompanyHolder { get; set; }
        [Required(ErrorMessage = "Data do checkin é necessária.")]
        public DateTime CheckinDateTime { get; set; }
    }
}
