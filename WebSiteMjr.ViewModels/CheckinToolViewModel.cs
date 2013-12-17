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
            CheckinDateTime = DateTime.Now;
        }
        [Required(ErrorMessage = "Nome do Condomínio/Funcionário não pode ser vazio")]
        public string EmployeeCompanyHolderName { get; set; }
        [Required(ErrorMessage = "Nome da Ferramenta não pode ser vazia")]
        public string ToolName { get; set; }
        [Required(ErrorMessage = "Data da movimentação não pode ser vazia")]
        [NoFutureDate()]
        public DateTime CheckinDateTime { get; set; }
    }

    //public class NoFutureDateAttribute : ValidationAttribute
    //{
    //    private const string DateFormat = "dd/MM/yyyy";
    //    private const string DefaultErrorMessage ="Data não pode ser futura.";

    //    public NoFutureDateAttribute(): base(DefaultErrorMessage)
    //    {}

    //    public override bool IsValid(object value)
    //    {
    //        if (value == null || !(value is DateTime))
    //        {
    //            return true;
    //        }
    //        var dateValue = (DateTime)value;
    //        return !(dateValue.Date > DateTime.Now.Date);
    //    }

    //    private static DateTime ParseDate(string dateValue)
    //    {
    //        return DateTime.ParseExact(dateValue, DateFormat,
    // CultureInfo.InvariantCulture);
    //    }
    //}
}
