using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebSiteMjr.Domain.Interfaces.Repository.Sena
{
    public interface ISenaClientRepository
    {
        void Add(string name);
        string GetByName(string name);
    }
}
