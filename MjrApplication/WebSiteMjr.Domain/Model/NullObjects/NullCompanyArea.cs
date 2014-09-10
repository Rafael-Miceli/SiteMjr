using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebSiteMjr.Domain.Model.NullObjects
{
    [NotMapped]
    public class NullCompanyArea : CompanyArea
    {
        public override bool IsNull
        {
            get { return true; }
        }

        public override ICollection<Company> Companies { get { return null; } set { } }

        public override string Name { get { return null; } set { } }

        public override int Id { get { return 0; } set { } }
    }
}
