using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace WebSiteMjr.ViewModels.Company
{
    public class ListCompanyViewModel
    {
        public IEnumerable<Domain.Model.Company> Companies { get; set; }
    }

    public class EditCompanyViewModel
    {
        [RegularExpression(@"^[_a-z0-9-]+(\.[_a-z0-9-]+)*@[a-z0-9-]+(\.[a-z0-9-]+)*(\.[a-z]{2,4})$", ErrorMessage = "E-mail inválido")]
        public string Email { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }

        [RegularExpression(@"^(\([0-9][0-9]\) [0-9]{4}-[0-9]{4})|(\(1[2-9]\) [5-9][0-9]{3}-[0-9]{4})|(\([2-9][1-9]\) [5-9][0-9]{3}-[0-9]{4})$",
                           ErrorMessage = "Telefone Inválido")]
        public string Phone { get; set; }
        public IList<SelectListItem> ToolsLocalizations { get; set; }
        public int Id { get; set; }
    }
}
