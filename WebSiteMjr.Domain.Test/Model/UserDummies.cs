using System.Collections.Generic;
using FlexProviders.Membership;
using WebSiteMjr.Domain.Interfaces.Membership;
using WebSiteMjr.Domain.Model.Membership;
using WebSiteMjr.Domain.Model.Roles;
using System.Linq;

namespace WebSiteMjr.Domain.Test.Model
{
    public static class UserDummies
    {
        private static readonly ISecurityEncoder Encoder;
        private static readonly string Salt;

        static UserDummies()
        {
            Encoder = new DefaultSecurityEncoder();
            Salt = "/eYHK+MXIeHmZOgbffphWQ==";
        }

        public static User ReturnOneMjrActiveUser()
        {
            var user = new User
            {
                Id = 1,
                Password = Encoder.Encode("12345678", Salt),
                Salt = Salt,
                IsLocal = true,
                StatusUser = StatusUser.Active,
                Username = "Administrator",
                Roles = new List<Role>
                {
                    RoleDummies.ReturnMjrAdminRole()
                },
                Employee = EmployeeDummies.CreateListOfEmployees().FirstOrDefault(e => e.Id == 7)
            };

            return user;
        }

        public static User ReturnOneNonMjrActiveUser()
        {
            var user = new User
            {
                Id = 2,
                IsLocal = true,
                Password = "12345678",
                Salt = "/eYHK+MXIeHmZOgbffphWQ==",
                StatusUser = StatusUser.Unactive,
                Username = "Administrator",
                Roles = new List<Role>
                {
                    RoleDummies.ReturnMjrAdminRole()
                }
            };

            return user;
        }

    }
}
