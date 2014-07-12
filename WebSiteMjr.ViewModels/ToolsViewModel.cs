using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Mjr.Extensions;
using WebSiteMjr.Domain.Model;

namespace WebSiteMjr.ViewModels
{
    public class EditToolViewModel
    {
        public int Id { get; set; }
        public int? StuffCategoryId { get; set; }
        public int? StuffManufactureId { get; set; }
        [Required(ErrorMessage = "Nome é um campo obrigatório")]
        public string Name { get; set; }
        public string Description { get; set; }
        public StuffCategory StuffCategory { get; set; }
        public StuffManufacture StuffManufacture { get; set; }
    }

    public class CheckinToolTabViewModel
    {
        public CheckinToolTabViewModel()
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
        public IEnumerable<EnumerableCheckinToolViewModel> CheckinTools { get; set; }
        public int ToolId { get; set; }
        [Required(ErrorMessage = "Nome do Condomínio/Funcionário não pode ser vazio")]
        public string HolderName { get; set; }
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
        public string StrCheckinDateTime { get; set; }

        public string Informer { get; set; }
    }
}
