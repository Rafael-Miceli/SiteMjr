using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebSiteMjr.Domain.CustomerService.Model;

namespace WebSiteMjr.Domain.Model
{
    public class Company : Holder
    {
        
        public virtual string Address { get; set; }
        public virtual string City { get; set; }

        [RegularExpression(@"^(\([0-9][0-9]\) [0-9]{4}-[0-9]{4})|(\(1[2-9]\) [5-9][0-9]{3}-[0-9]{4})|(\([2-9][1-9]\) [5-9][0-9]{3}-[0-9]{4})$", ErrorMessage = "Telefone Inválido")]
        public virtual string Phone { get; set; }

        private ICollection<CompanyArea> _companyAreas;
        public virtual ICollection<CompanyArea> CompanyAreas
        {
            get
            {
                return _companyAreas ?? (_companyAreas = new Collection<CompanyArea>());
            }
            set
            {
                _companyAreas = value;
            } 
        }

        public virtual ICollection<Employee> Employees { get; set; }

        public ICollection<ServiceType> ServiceTypes { get; set; }

        [NotMapped]
        public override string ObjectName
        {
            get
            {
                return "Empresa/Condomínio";
            }
        }

        public bool IsMjrCompany()
        {
            return Name.Equals("Mjr Equipamentos eletrônicos LTDA");
        }

    }
}
