using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebSiteMjr.Domain.Interfaces.Services
{
    public interface ISenaClientService
    {
        Task Create(string name);
        Task<string> FindByName(string name);
    }
}
