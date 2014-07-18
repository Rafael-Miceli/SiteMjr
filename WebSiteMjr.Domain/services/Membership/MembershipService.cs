using WebSiteMjr.Domain.Interfaces.Membership;
using WebSiteMjr.Domain.Interfaces.Services;
using WebSiteMjr.Domain.Model.Membership;
using WebSiteMjr.Domain.Model.Roles;

namespace WebSiteMjr.Domain.services.Membership
{
    public class MembershipService : IMembershipService
    {
        private readonly IFlexMembershipProvider _membershipProvider;

        public MembershipService(IFlexMembershipProvider membershipProvider)
        {
            _membershipProvider = membershipProvider;
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

        public Role GetUserRole(string userName)
        {
            return _membershipProvider.GetUserRole(userName);
        }

        public User GetLoggedUser(string userName)
        {
            return _membershipProvider.GetUser(userName);
        }
    }
}
