using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSiteMjr.Domain.Model.Person;

namespace WebSiteMjr.Domain.Model
{
    public class CheckinTool: IntId
    {
        public virtual Holder EmployeeCompanyHolder { get; set; }
        public virtual Tool Tool { get; set; }
        public virtual DateTime CheckinDateTime { get; set; }
    }
}
