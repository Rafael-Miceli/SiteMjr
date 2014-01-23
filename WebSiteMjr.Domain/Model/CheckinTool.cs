using System;
using WebSiteMjr.Domain.Model.Person;

namespace WebSiteMjr.Domain.Model
{
    public class CheckinTool: IntId, IMjrException
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

        public virtual CompanyArea CompanyArea { get; set; }
    }
}
