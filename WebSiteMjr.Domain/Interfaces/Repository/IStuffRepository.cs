﻿using WebSiteMjr.Domain.Interfaces.Repository.GenericRepository;
using WebSiteMjr.Domain.Model;

namespace WebSiteMjr.Domain.Interfaces.Repository
{
    public interface IStuffRepository: IGenericRepository<Stuff>
    {
        void AddGraph(Stuff stuff);
    }
}
