using WebSiteMjr.Domain.Model.Membership;

namespace WebSiteMjr.Domain.Interfaces.Repository
{
    public interface IUserRepository: IGenericRepository<User>
    {
        User GetByUserName(string userName);
    }
}
