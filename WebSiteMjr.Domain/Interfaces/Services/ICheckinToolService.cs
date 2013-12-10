using System.Collections.Generic;
using WebSiteMjr.Domain.Model;

namespace WebSiteMjr.Domain.Interfaces.Services
{
    public interface ICheckinToolService
    {
        void CheckinTool(CheckinTool checkinTool);
        void UpdateToolCheckin(CheckinTool checkinTool);
        void DeleteToolCheckin(object checkinTool);
        IEnumerable<CheckinTool> ListToolCheckins();
        IEnumerable<CheckinTool> GetCheckinsByEmployeeName(string employeeName);
        Tool FindToolCheckin(object idCheckinTool);
    }
}