using WebSiteMjr.Domain.Interfaces.Membership;
using WebSiteMjr.Domain.Model;
using WebSiteMjr.Domain.Model.Membership;
using WebSiteMjr.Domain.Model.Roles;

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
        MjrAppRole GetUserRole(string userName);
        string CreateAccountAndReturnPassword(User user, bool isMjrCompany);
        User FindActiveUserByEmployeeId(int id);
        void InactiveUser(User user);
        void DeleteAccount(object userId);
        void CreateCompanyAdminAccount(User user);
    }
}