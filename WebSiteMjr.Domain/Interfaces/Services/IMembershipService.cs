using WebSiteMjr.Domain.Interfaces.Membership;

namespace WebSiteMjr.Domain.Interfaces.Services
{
    public interface IMembershipService
    {
        bool Login(string username, string password, bool rememberMe = false);
        void Logout();
        void CreateAccount(IFlexMembershipUser user);
        bool HasLocalAccount(string username);
        bool ChangePassword(string username, string oldPassword, string newPassword);
        void SetLocalPassword(string username, string newPassword);
    }
}