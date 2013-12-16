﻿using WebSiteMjr.Domain.Interfaces.Repository;
using WebSiteMjr.Domain.Model;
using WebSiteMjr.EfBaseData.UnitOfWork;
using WebSiteMjr.EfData.Context;
using WebSiteMjr.EfData.DataRepository.GenericRepositorys;

namespace WebSiteMjr.EfData.DataRepository
{
    public class CompanyRepository: GenericPersonRepository<Company>, ICompanyRepository
    {
        public CompanyRepository(UnitOfWork<PersonsContext> uow) : base(uow)
        {}
        public Company GetCompanyByName(string name)
        {
            return Get(n => n.Name.ToLower() == name.ToLower());
        }
    }
}
