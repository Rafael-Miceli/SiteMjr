﻿using WebSiteMjr.Domain.Interfaces.Repository;
using WebSiteMjr.Domain.Model.Membership;
using WebSiteMjr.EfData.Context;
using WebSiteMjr.EfData.DataRepository.GenericRepositorys;
using WebSiteMjr.EfData.UnitOfWork;

namespace WebSiteMjr.EfData.DataRepository
{
    public class UserRepository: GenericPersonRepository<User>, IUserRepository
    {
        public UserRepository(IUnitOfWork<PersonsContext> uow ) : base(uow)
        {}

        public User GetByUserName(string userName)
        {
            return Get(u => u.Username == userName);
        }
    }
}
