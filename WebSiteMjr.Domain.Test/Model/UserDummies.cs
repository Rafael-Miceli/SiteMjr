using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSiteMjr.Domain.Model.Membership;

namespace WebSiteMjr.Domain.Test.Model
{
    public static class UserDummies
    {
        public static User ReturneOneMjrActiveUser()
        {
            var user = new User
            {
                Id = 1,
                Email = "rafael.miceli@hotmail.com"
            };

            return user;
        }

        public static User ReturneOneNonMjrActiveUser()
        {
            var user = new User
            {
                Id = 1,
                Email = "rafael.miceli@hotmail.com",
                IdCompany = CompanyDummies.CreateOneCompany().Id,
                IsLocal = true,
                Name = "Rafael",
                LastName = "Miceli",
                Password = "",
                StatusUser = StatusUser.Active,
                Username = "rafael.miceli@hotmail.com"
            };

            return user;
        }

    }
}
