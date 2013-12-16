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
        IEnumerable<CheckinTool> FilterCheckins(string employeeName, string toolName, DateTime ?date);
        CheckinTool FindToolCheckin(object idCheckinTool);
        IEnumerable<string> ListEmployeeCompanyHolderName();
        IEnumerable<string> ListToolName();
        Holder FindEmployeeCompanyByName(string name);
    }
}