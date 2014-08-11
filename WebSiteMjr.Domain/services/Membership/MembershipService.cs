using System;
using System.Transactions;
using WebSiteMjr.Domain.Exceptions;
using WebSiteMjr.Domain.Interfaces.Membership;
using WebSiteMjr.Domain.Interfaces.Services;
using WebSiteMjr.Domain.Interfaces.Uow;
using WebSiteMjr.Domain.Model;
using WebSiteMjr.Domain.Model.Membership;
using WebSiteMjr.Domain.Model.Roles;

namespace WebSiteMjr.Domain.services.Membership
{
    public class MembershipService : IMembershipService
    {
        private readonly IFlexMembershipProvider _membershipProvider;
        private readonly IRoleService _roleService;
        private readonly IUnitOfWork _unitOfWork;

        public MembershipService(IFlexMembershipProvider membershipProvider, IRoleService roleService, IUnitOfWork unitOfWork)
        {
            _membershipProvider = membershipProvider;
            _roleService = roleService;
            _unitOfWork = unitOfWork;
        }

        public bool Login(string username, string password, bool rememberMe = false)
        {
            return _membershipProvider.Login(username, password, rememberMe);
        }

        public void Logout()
        {
            _membershipProvider.Logout();
        }

        public void CreateAccount(User user)
        {
            _membershipProvider.CreateAccount(user);
        }

        public string CreateAccountAndReturnPassword(User user, bool isMjrCompany)
        {
            var password = CreateAccountPassword(user);
            DefineAccountRole(user, isMjrCompany);
            _membershipProvider.CreateAccount(user);
            return password;
        }

        public User FindActiveUserByEmployeeId(int id)
        {
            return _membershipProvider.FindActiveUserByEmployeeId(id);
        }

        public void InactiveUser(User user)
        {
            user.StatusUser = StatusUser.Unactive;
            _membershipProvider.UpdateAccount(user);
        }

        public void DeleteAccount(object userId)
        {
            _membershipProvider.DeleteAccount(userId);
            _unitOfWork.Save();
        }

        private void DefineAccountRole(User user, bool isMjrCompany)
        {
            user.Roles = isMjrCompany ? _roleService.GetRole_MjrUser_ForEmployee() : _roleService.GetRole_User_ForEmployee();
        }

        private string CreateAccountPassword(User user)
        {
            var password = _membershipProvider.GenerateNewPassword();
            user.Password = password;
            return password;
        }

        public bool HasLocalAccount(string username)
        {
            return _membershipProvider.HasLocalAccount(username);
        }

        public bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            var changed = _membershipProvider.ChangePassword(username, oldPassword, newPassword);

            if (changed)
                _unitOfWork.Save();

            return changed;
        }

        public void SetLocalPassword(string username, string newPassword)
        {
            _membershipProvider.SetLocalPassword(username, newPassword);
        }

        public MjrAppRole GetUserRole(string userName)
        {
            return _membershipProvider.GetUserRole(userName);
        }

        public User GetLoggedUser(string userName)
        {
            return _membershipProvider.GetUser(userName);
        }
    }
}
