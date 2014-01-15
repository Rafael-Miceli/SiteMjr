﻿using WebSiteMjr.Domain.Interfaces.Repository;
using WebSiteMjr.Domain.Model;
using WebSiteMjr.EfBaseData.UnitOfWork;
using WebSiteMjr.EfData.Context;
using WebSiteMjr.EfData.DataRepository.GenericRepositorys;

namespace WebSiteMjr.EfData.DataRepository
{
    public class ToolLocalizationRepository : GenericPersonRepository<ToolLocalization>, IToolLocalizationRepository
    {
        public ToolLocalizationRepository(UnitOfWork<PersonsContext> uow) : base(uow)
        {}

        public ToolLocalization GetByName(string name)
        {
            return Get(tl => tl.Name.ToLower() == name.ToLower());
        }
    }
}
