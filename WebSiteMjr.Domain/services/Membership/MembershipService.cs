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
        private readonly IEmailService _emailService;
        private readonly IUnitOfWork _unitOfWork;

        public MembershipService(IFlexMembershipProvider membershipProvider, IRoleService roleService, IEmailService emailService, IUnitOfWork unitOfWork)
        {
            _membershipProvider = membershipProvider;
            _roleService = roleService;
            _emailService = emailService;
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

        public string CreateNewUserEmployeeAccount(Employee employee)
        {
            var password = _membershipProvider.GenerateNewPassword();

            var user = new User
            {
                IsLocal = true,
                Username = employee.Email,
                Roles = _roleService.GetRole_User_ForEmployee(),
                StatusUser = StatusUser.Active,
                Password = password,
                Employee = employee
            };
            
            if (employee.Company.IsMjrCompany())
                user.Roles = _roleService.GetRole_MjrUser_ForEmployee();

            CreateAccount(user);

            return password;
        }

        public void CreateNewUserForExistentEmployeeAccount(Employee employee)
        {
            var password = _membershipProvider.GenerateNewPassword();

            var user = new User
            {
                IsLocal = true,
                Username = employee.Email,
                Roles = _roleService.GetRole_User_ForEmployee(),
                StatusUser = StatusUser.Active,
                Password = password,
                Employee = employee
            };

            if (employee.Company.IsMjrCompany())
                user.Roles = _roleService.GetRole_MjrUser_ForEmployee();

            CreateAccount(user);

            _unitOfWork.Save();

            _emailService.SendFirstLoginToEmployee(password, employee.Email, employee.Name, employee.LastName);
        }

        public bool HasLocalAccount(string username)
        {
            return _membershipProvider.HasLocalAccount(username);
        }

        public bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            return _membershipProvider.ChangePassword(username, oldPassword, newPassword);
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
