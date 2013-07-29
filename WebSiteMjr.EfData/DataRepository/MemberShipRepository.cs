using System.Data;
using System.Data.Entity;
using System.Linq;
using WebSiteMjr.Domain.Interfaces.Membership;
using WebSiteMjr.EfData.Context;

namespace WebSiteMjr.EfData.DataRepository
{
    public class MembershipRepository<TUser> : IFlexUserStore where TUser : class, IFlexMembershipUser, new() 
    {
        private readonly DbContext _context;
        
        public MembershipRepository(DbContext context)
        {
            _context = context;
        }

        public IFlexMembershipUser Add(IFlexMembershipUser user)
        {
            _context.Set<TUser>().Add((TUser)user);
            _context.SaveChanges();
            return user;
        }

        public IFlexMembershipUser Save(IFlexMembershipUser user)
        {
            _context.Entry(user).State = EntityState.Modified;
            _context.SaveChanges();
            return user;
        }

        public IFlexMembershipUser CreateOAuthAccount(string provider, string providerUserId, IFlexMembershipUser user)
        {
            throw new System.NotImplementedException();
        }

        public IFlexMembershipUser GetUserByUsername(string username)
        {
            return _context.Set<TUser>().SingleOrDefault(u => u.Username == username);
        }

        public IFlexMembershipUser GetUserByOAuthProvider(string provider, string providerUserId)
        {
            throw new System.NotImplementedException();
        }

        public bool DeleteOAuthAccount(string provider, string providerUserId)
        {
            throw new System.NotImplementedException();
        }

        public IFlexMembershipUser GetUserByPasswordResetToken(string passwordResetToken)
        {
            var user = _context.Set<TUser>().SingleOrDefault(u => u.PasswordResetToken == passwordResetToken);
            return user;
        }
    }
}
