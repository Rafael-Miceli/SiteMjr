﻿using WebSiteMjr.Domain.Interfaces.Repository.GenericRepository;
using WebSiteMjr.Domain.Model;

namespace WebSiteMjr.Domain.Interfaces.Repository
{
    public interface IToolLocalizationRepository : IGenericRepository<ToolLocalization>
    {
        ToolLocalization GetByName(string name);
    }
}