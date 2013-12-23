using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using Mjr.Extensions;
using WebSiteMjr.Domain.Model;

namespace WebSiteMjr.ViewModels
{
    public class ListCheckinToolViewModel
    {
        public string EmployeeCompanyHolder { get; set; }
        public string Tool { get; set; }
        public DateTime ?CheckinDateTime { get; set; }
        public IEnumerable<EnumerableCheckinToolViewModel> CheckinTools { get; set; }
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
        public CreateCheckinToolViewModel()
        {
            try
            {
                var tz = TimeZoneInfo.FindSystemTimeZoneById("Central Brazilian Standard Time");
                CheckinDateTime = TimeZoneInfo.ConvertTimeToUtc(DateTime.Now, tz);
            }
            catch (Exception)
            {
                CheckinDateTime = DateTime.Now;
            }
            
        }

        public int Id { get; set; }
        [Required(ErrorMessage = "Nome do Condomínio/Funcionário não pode ser vazio")]
        public string EmployeeCompanyHolderName { get; set; }
        [Required(ErrorMessage = "Nome da Ferramenta não pode ser vazia")]
        public string ToolName { get; set; }
        [Required(ErrorMessage = "Data da movimentação não pode ser vazia")]
        [NoFutureDate()]
        public DateTime ?CheckinDateTime { get; set; }
    }
}
