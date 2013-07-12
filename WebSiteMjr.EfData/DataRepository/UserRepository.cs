using WebSiteMjr.Domain.Interfaces.Repository;
using WebSiteMjr.Domain.Model.Membership;
using WebSiteMjr.EfData.DataRepository.GenericRepositorys;

namespace WebSiteMjr.EfData.DataRepository
{
    public class UserRepository: GenericPersonRepository<User>, IUserRepository
    {
        public User GetByUserName(string userName)
        {
            return Get(u => u.Username == userName);
        }
    }
}
