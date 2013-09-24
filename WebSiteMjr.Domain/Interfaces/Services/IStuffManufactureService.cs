using System.Collections.Generic;
using WebSiteMjr.Domain.Model;

namespace WebSiteMjr.Domain.Interfaces.Services
{
    public interface IStuffManufactureService
    {
        void CreateStuffManufacture(StuffManufacture stuffManufacture);
        void UpdateStuffManufacture(StuffManufacture stuffManufacture);
        void DeleteStuffManufacture(object stuffManufacture);
        IEnumerable<StuffManufacture> ListStuffManufacture();
        StuffManufacture FindStuffManufacture(object idStuffManufacture)
        StuffManufacture FindStuffManufactureByName(string name);
    }
}
