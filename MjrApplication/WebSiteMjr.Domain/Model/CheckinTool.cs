using System;
using WebSiteMjr.Domain.Interfaces.Model;
using WebSiteMjr.Domain.Model.Person;

namespace WebSiteMjr.Domain.Model
{
    public class CheckinTool : Key<int>, IMjrException
    {
        public virtual int EmployeeCompanyHolderId { get; set; }
        public virtual Tool Tool { get; set; }
        public virtual DateTime CheckinDateTime { get; set; }

        public string ObjectName
        {
            get
            {
                return "Data de movimentação para a ferramenta";
            } 
        }

        public virtual int? CompanyAreaId { get; set; }
        public virtual string Informer { get; set; }
    }
}
