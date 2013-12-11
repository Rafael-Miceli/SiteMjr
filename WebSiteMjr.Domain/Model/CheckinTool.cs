using System;
using System.Collections.Generic;
using WebSiteMjr.Domain.Model.Person;

namespace WebSiteMjr.Domain.Model
{
    public class CheckinTool: IntId, IHolder
    {
        public virtual IHolder EmployeeCompanyHolder { get; set; }
        public virtual Tool Tool { get; set; }
        public virtual DateTime CheckinDateTime { get; set; }
        public string Name { get; set; }
        public IEnumerable<Stuff> Stuff { get; set; }
    }
}
