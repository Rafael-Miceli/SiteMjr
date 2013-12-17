using System.Collections.Generic;
using WebSiteMjr.Domain.Model;

namespace WebSiteMjr.Domain.Interfaces.Services
{
    public interface IHolderService
    {
        IEnumerable<Holder> ListHolder();
        Holder FindHolder(object idHolder);
        Holder FindHolderByName(string holderName);
    }
}