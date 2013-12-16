﻿using WebSiteMjr.Domain.Interfaces.Repository.GenericRepository;
using WebSiteMjr.Domain.Model;

namespace WebSiteMjr.Domain.Interfaces.Repository
{
    public interface IEmployeeRepository : IGenericRepository<Employee>
    {
        Employee GetEmployeeByName(string name);
    }
}
