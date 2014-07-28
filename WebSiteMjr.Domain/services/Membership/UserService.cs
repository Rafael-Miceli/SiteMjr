using System.Collections.Generic;
using System.Linq;
using WebSiteMjr.Domain.Interfaces.Repository;
using WebSiteMjr.Domain.Interfaces.Services;
using WebSiteMjr.Domain.Interfaces.Uow;
using WebSiteMjr.Domain.Model;
using WebSiteMjr.Domain.Model.Membership;
using WebSiteMjr.Domain.Model.Roles;

namespace WebSiteMjr.Domain.services.Membership
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _user;
        private readonly ICompanyService _company;
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUserRepository user, ICompanyService company, IUnitOfWork unitOfWork)
        {
            _user = user;
            _company = company;
            _unitOfWork = unitOfWork;
        }

        public void CreateUser(User user)
        {
            _user.Add(user);
            _unitOfWork.Save();
        }

        public void UpdateUser(User user)
        {
            _user.Update(user);
            _unitOfWork.Save();
        }

        public void DeleteUser(object user)
        {
            _user.Remove(user);
            _unitOfWork.Save();
        }

        public IEnumerable<User> ListUser()
        {
            return _user.GetAll();
        }

        public User GetLoggedUser(string userName)
        {
            return _user.GetByUserName(userName);
        }

        public MjrAppRole GetUserRole(string userName)
        {
            var user = _user.GetByUserName(userName);
            return user != null ? user.Roles.FirstOrDefault() : null;
        }

        public Company GetUserCompany(string userName)
        {
            var user = _user.GetByUserName(userName);
            var idCompany = user != null ? user.Employee.Company.Id : 0;

            return _company.FindCompany(idCompany);
        }

        public User FindUser(object iduser)
        {
            return _user.GetById(iduser);
        }

    }
}
