using System;
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
        IEnumerable<CheckinTool> FilterCheckins(Holder employeeName, string toolName, DateTime ?date);
        CheckinTool FindToolCheckin(object idCheckinTool);
        IEnumerable<CheckinTool> ListCheckinToolsWithActualTool(int toolId);
        bool IsCheckinOfToolInCompany(int employeeCompanyHolderId);
        CheckinTool LastCheckinOfThisTool(CheckinTool checkinTool);
    }
}