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
        public static Role ReturnMjrAdminRole()
        {
            var role = new Role
            {
                Id = 1,
                Name = "MjrAdmin"
            };

            return role;
        }

        public static Role ReturnCompanyAdminRole()
        {
            var role = new Role
            {
                Id = 2,
                Name = "CompanyAdmin"
            };

            return role;
        }

        public static Role ReturnUserRole()
        {
            var role = new Role
            {
                Id = 3,
                Name = "User"
            };

            return role;
        }

    }
}
