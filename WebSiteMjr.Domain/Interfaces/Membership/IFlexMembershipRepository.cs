using WebSiteMjr.Domain.Interfaces.Repository.GenericRepository;
using WebSiteMjr.Domain.Model.Membership;

namespace WebSiteMjr.Domain.Interfaces.Membership
{
    public interface IFlexMembershipRepository: IGenericRepository<User>
    {   
        User Save(User user);
        User CreateOAuthAccount(string provider, string providerUserId, IFlexMembershipUser user);        
        User GetUserByUsername(string username);
        User GetUserByOAuthProvider(string provider, string providerUserId);        
        bool DeleteOAuthAccount(string provider, string providerUserId);
        User GetUserByPasswordResetToken(string passwordResetToken);
    }    
}