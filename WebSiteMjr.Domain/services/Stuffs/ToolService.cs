using System.Collections.Generic;
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
            _toolRepository.AddOrUpdateGraph(tool);
            _unitOfWork.Save();
        }

        public void UpdateTool(Tool tool)
        {
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

        public Tool FindTool(object idTool)
        {
            return _toolRepository.GetById(idTool);
        }
    }
}
