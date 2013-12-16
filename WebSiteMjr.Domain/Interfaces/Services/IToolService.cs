using System.Collections.Generic;
using WebSiteMjr.Domain.Model;

namespace WebSiteMjr.Domain.Interfaces.Services
{
    public interface IToolService
    {
        void CreateTool(Tool stuff);
        void UpdateTool(Tool stuff);
        void DeleteTool(object stuff);
        IEnumerable<Tool> ListTool();
        Tool FindTool(object idTool);
        Tool FindToolByName(string toolName);
    }
}