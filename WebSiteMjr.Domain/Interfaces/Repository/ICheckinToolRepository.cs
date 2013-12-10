using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSiteMjr.Domain.Interfaces.Repository.GenericRepository;
using WebSiteMjr.Domain.Model;

namespace WebSiteMjr.Domain.Interfaces.Repository
{
    public interface ICheckinToolRepository : IGenericRepository<CheckinTool>
    {
        IEnumerable<CheckinTool> GetCheckinsByTool(string tool);
        IEnumerable<CheckinTool> GetCheckinsByEmployeeName(string employeeName);
        IEnumerable<CheckinTool> GetCheckinsByDate(DateTime date);
        IEnumerable<CheckinTool> GetCheckinsByEmployeeNameAndTool(string employeeName, string tool);
        IEnumerable<CheckinTool> GetCheckinsByEmployeeNameAndToolAndDate(string employeeName, string tool, DateTime date);
    }
}
