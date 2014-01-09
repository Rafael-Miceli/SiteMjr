﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSiteMjr.Domain.Model;

namespace WebSiteMjr.ViewModels
{
    public class ListCompanyViewModel
    {
        public IEnumerable<Company> Companies { get; set; }
    }

    public class EditCompanyViewModel
    {
        [RegularExpression(@"^[_a-z0-9-]+(\.[_a-z0-9-]+)*@[a-z0-9-]+(\.[a-z0-9-]+)*(\.[a-z]{2,4})$", ErrorMessage = "E-mail inválido")]
        public virtual string Email { get; set; }
        public virtual string Address { get; set; }
        public virtual string City { get; set; }

        [RegularExpression(@"^(\([0-9][0-9]\) [0-9]{4}-[0-9]{4})|(\(1[2-9]\) [5-9][0-9]{3}-[0-9]{4})|(\([2-9][1-9]\) [5-9][0-9]{3}-[0-9]{4})$",
                           ErrorMessage = "Telefone Inválido")]
        public virtual string Phone { get; set; }

        private ICollection<ToolLocalization> _toolsLocalizations;
        public virtual ICollection<ToolLocalization> ToolsLocalizations
        {
            get
            {
                return _toolsLocalizations ?? (_toolsLocalizations = new Collection<ToolLocalization>());
            }
            set
            {
                _toolsLocalizations = value;
            }
        }
    }
}
