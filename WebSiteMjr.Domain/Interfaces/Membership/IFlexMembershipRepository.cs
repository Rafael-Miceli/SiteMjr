namespace WebSiteMjr.Domain.Interfaces.Membership
{
    public interface IFlexMembershipRepository
    {        
        IFlexMembershipUser Add(IFlexMembershipUser user);        
        IFlexMembershipUser Save(IFlexMembershipUser user);
        IFlexMembershipUser CreateOAuthAccount(string provider, string providerUserId, IFlexMembershipUser user);        
        IFlexMembershipUser GetUserByUsername(string username);
        IFlexMembershipUser GetUserByOAuthProvider(string provider, string providerUserId);        
        bool DeleteOAuthAccount(string provider, string providerUserId);
        IFlexMembershipUser GetUserByPasswordResetToken(string passwordResetToken);
    }    
}