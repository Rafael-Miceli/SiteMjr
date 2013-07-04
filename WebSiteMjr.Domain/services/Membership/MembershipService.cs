using WebSiteMjr.Domain.Interfaces.Membership;

namespace WebSiteMjr.Domain.services.Membership
{
    public class MembershipService
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

        public void CreateAccount(IFlexMembershipUser user)
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
    }
}
