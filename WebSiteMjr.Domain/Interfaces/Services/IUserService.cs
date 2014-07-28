using System.Collections.Generic;
using WebSiteMjr.Domain.Model;
using WebSiteMjr.Domain.Model.Membership;

namespace WebSiteMjr.Domain.Interfaces.Services
{
    public interface IUserService
    {
        void CreateUser(User user);
        void UpdateUser(User user);
        void DeleteUser(object user);
        IEnumerable<User> ListUser();
        User GetLoggedUser(string userName);
        Domain.Model.Roles.MjrAppRole GetUserRole(string userName);
        Company GetUserCompany(string userName);
        User FindUser(object iduser);
    }
}