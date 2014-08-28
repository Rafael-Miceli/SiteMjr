using System;
using WebSiteMjr.Domain.Interfaces.Membership;
using WebSiteMjr.Domain.Model.Membership;
using WebSiteMjr.EfBaseData.UnitOfWork;
using WebSiteMjr.EfData.Context;
using WebSiteMjr.EfData.DataRepository.GenericRepositorys;

namespace WebSiteMjr.EfData.DataRepository
{
    public class MembershipRepository : GenericPersonRepository<User, int>, IFlexMembershipRepository
    {

        public MembershipRepository(UnitOfWork<PersonsContext> uow) : base(uow)
        {}

        public User Save(User user)
        {
            Update(user);
            return user;
        }

        public User CreateOAuthAccount(string provider, string providerUserId, IFlexMembershipUser user)
        {
            throw new System.NotImplementedException();
        }

        public User GetUserByUsername(string userName)
        {
            return  Get(u => u.Username == userName);
        }

        public User GetUserByOAuthProvider(string provider, string providerUserId)
        {
            throw new System.NotImplementedException();
        }

        public bool DeleteOAuthAccount(string provider, string providerUserId)
        {
            throw new System.NotImplementedException();
        }

        public User GetUserByPasswordResetToken(string passwordResetToken)
        {
            return null;
        }

        public User GetActiveUserByEmployeeId(int id)
        {
            return Get(u => u.Employee.Id == id && u.StatusUser == StatusUser.Active);
        }
    }
}
