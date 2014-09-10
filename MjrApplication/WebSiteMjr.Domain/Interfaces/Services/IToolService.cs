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
        IEnumerable<Tool> ListNotDeletedTools();
        IEnumerable<string> ListToolName();
        Tool FindTool(object idTool);
        Tool FindToolByName(string toolName);
    }
}