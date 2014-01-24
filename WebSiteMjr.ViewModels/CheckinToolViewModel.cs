using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Mjr.Extensions;

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
                StrCheckinDateTime = DateTime.UtcNow.ConvertToTimeZone().AddHours(1).ToString();
            }
            catch (Exception)
            {
                CheckinDateTime = DateTime.UtcNow;
            }
            
        }

        public int Id { get; set; }
        [Required(ErrorMessage = "Nome do Condomínio/Funcionário não pode ser vazio")]
        public string EmployeeCompanyHolderName { get; set; }
        [Required(ErrorMessage = "Nome da Ferramenta não pode ser vazia")]
        public string ToolName { get; set; }
        public string CompanyAreaName { get; set; }
        public DateTime? CheckinDateTime 
        {
            get
            {
                DateTime checkinDateTime;
                if (DateTime.TryParse(StrCheckinDateTime, out checkinDateTime))
                {
                    return checkinDateTime;
                }

                return null;
            }
            set
            {
                DateTime checkinDateTime;
                if (DateTime.TryParse(StrCheckinDateTime, out checkinDateTime))
                {
                    value = checkinDateTime;
                }   
            }
        }

        [NoFutureDate]
        [DateTimeIsValid]
        [Required(ErrorMessage = "Data da movimentação não pode ser vazia")]
        public string StrCheckinDateTime{ get; set; }
    }
}
