using WebSiteMjr.Domain.Interfaces.Membership;
using WebSiteMjr.Domain.Model;
using WebSiteMjr.Domain.Model.Membership;

namespace WebSiteMjr.Domain.Interfaces.Services
{
    public interface IMembershipService
    {
        bool Login(string username, string password, bool rememberMe = false);
        void Logout();
        void CreateAccount(User user);
        bool HasLocalAccount(string username);
        bool ChangePassword(string username, string oldPassword, string newPassword);
        void SetLocalPassword(string username, string newPassword);
        User GetLoggedUser(string name);
        Domain.Model.Roles.MjrAppRole GetUserRole(string userName);
        string CreateNewUserEmployeeAccount(Employee employee);
        void CreateNewUserForExistentEmployeeAccount(Employee employee);
    }
}