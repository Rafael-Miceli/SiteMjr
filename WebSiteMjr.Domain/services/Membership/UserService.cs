using System.Collections.Generic;
using System.Linq;
using WebSiteMjr.Domain.Interfaces.Repository;
using WebSiteMjr.Domain.Model.Membership;
using WebSiteMjr.Domain.Model.Roles;

namespace WebSiteMjr.Domain.services.Membership
{
    public class UserService
    {
        private readonly IUserRepository _user;
        private readonly ICompanyRepository _company;

        public UserService(IUserRepository user, ICompanyRepository company)
        {
            _user = user;
            _company = company;
        }

        public void CreateUser(User user)
        {
            _user.Add(user);
            _user.Save();
        }

        public void UpdateUser(User user)
        {
            _user.Update(user);
            _user.Save();
        }

        public void DeleteUser(object user)
        {
            _user.Remove(user);
            _user.Save();
        }

        public IEnumerable<User> ListUser()
        {
            return _user.GetAll();
        }

        public User GetLoggedUser(string userName)
        {
            return _user.GetByUserName(userName);
        }

        public Role GetUserRole(string userName)
        {
            var user = _user.GetByUserName(userName);
            return user != null ? user.Roles.FirstOrDefault() : null;
        }

        public int GetUserCompany(string userName)
        {
            var user = _user.GetByUserName(userName);
            var idCompany = user != null ? user.IdCompany : 0;

            var company = new CompanyService(_company);
        }

        public User FindUser(object iduser)
        {
            return _user.GetById(iduser);
        }
    }
}
