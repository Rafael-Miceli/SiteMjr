using System.Collections.Generic;
using WebSiteMjr.Domain.Model;

namespace WebSiteMjr.Domain.Interfaces.Services
{
    public interface IStuffService
    {
        void CreateStuff(Stuff stuff);
        void UpdateStuff(Stuff stuff);
        void DeleteStuff(object stuff);
        IEnumerable<Stuff> ListStuff();
        Stuff FindStuff(object idStuff);
    }
}