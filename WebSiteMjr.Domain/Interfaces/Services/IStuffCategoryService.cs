using System.Collections.Generic;
using WebSiteMjr.Domain.Model;

namespace WebSiteMjr.Domain.Interfaces.Services
{
    public interface IStuffCategoryService
    {
        void CreateStuffCategory(StuffCategory stuffCategory);
        void UpdateStuffCategory(StuffCategory stuffCategory);
        void DeleteStuffCategory(object stuffCategory);
        IEnumerable<StuffCategory> ListStuffCategory();
        StuffCategory FindStuffCategory(object idStuffCategory);
        StuffCategory FindStuffCategoryByName(string name);
    }
}
