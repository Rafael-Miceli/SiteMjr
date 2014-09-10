using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSiteMjr.Domain.Model.Roles;

namespace WebSiteMjr.Domain.Test.Model
{
    public static class RoleDummies
    {
        public static MjrAppRole ReturnMjrAdminRole()
        {
            var role = new MjrAppRole
            {
                Id = 1,
                Name = "MjrAdmin"
            };

            return role;
        }

        public static MjrAppRole ReturnCompanyAdminRole()
        {
            var role = new MjrAppRole
            {
                Id = 2,
                Name = "CompanyAdmin"
            };

            return role;
        }

        public static MjrAppRole ReturnUserRole()
        {
            var role = new MjrAppRole
            {
                Id = 3,
                Name = "User"
            };

            return role;
        }

        public static IEnumerable<MjrAppRole> ReturnAllRoles()
        {
            return new List<MjrAppRole>
            {
                ReturnMjrAdminRole(),
                ReturnCompanyAdminRole(),
                ReturnUserRole()
            };
        }

    }
}
