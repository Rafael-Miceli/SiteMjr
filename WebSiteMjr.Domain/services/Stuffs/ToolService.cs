using System.Collections.Generic;
using System.Linq;
using WebSiteMjr.Domain.Exceptions;
using WebSiteMjr.Domain.Interfaces.Repository;
using WebSiteMjr.Domain.Interfaces.Services;
using WebSiteMjr.Domain.Interfaces.Uow;
using WebSiteMjr.Domain.Model;

namespace WebSiteMjr.Domain.services.Stuffs
{
    public class ToolService: IToolService
    {
        private readonly IToolRepository _toolRepository;

        private readonly IUnitOfWork _unitOfWork;

        public ToolService(IToolRepository toolRepository, IUnitOfWork unitOfWork)
        {
            _toolRepository = toolRepository;
            _unitOfWork = unitOfWork;
        }

        public void CreateTool(Tool tool)
        {
            if (ToolExists(tool)) throw new ObjectExistsException<Tool>(); 

            _toolRepository.AddOrUpdateGraph(tool);
            _unitOfWork.Save();
        }

        private bool ToolExists(Tool tool)
        {
            return _toolRepository.Get(t => t.Name == tool.Name && t.Id != tool.Id) != null;
        }

        public void UpdateTool(Tool tool)
        {
            if (ToolExists(tool)) throw new ObjectExistsException<Tool>();

            tool.State = State.Modified;

            if (tool.StuffCategory != null)
                tool.StuffCategory.State = State.Modified;

            if (tool.StuffManufacture != null)
                tool.StuffManufacture.State = State.Modified;

            _toolRepository.Update(tool);
            _unitOfWork.Save();
        }

        public void DeleteTool(object tool)
        {
            _toolRepository.Remove(tool);
            _unitOfWork.Save();
        }

        public IEnumerable<Tool> ListTool()
        {
            return _toolRepository.GetAll();
        }

        public IEnumerable<string> ListToolName()
        {
            return ListTool().Select(t => t.Name);
        }

        public Tool FindTool(object idTool)
        {
            return _toolRepository.GetById(idTool);
        }

        public Tool FindToolByName(string toolName)
        {
            return _toolRepository.GetToolByName(toolName);
        }
    }
}
