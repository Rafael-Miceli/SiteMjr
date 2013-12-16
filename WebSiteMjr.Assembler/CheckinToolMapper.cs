using WebSiteMjr.Domain.Interfaces.Services;
using WebSiteMjr.Domain.Model;
using WebSiteMjr.ViewModels;

namespace WebSiteMjr.Assembler
{
    public class CheckinToolMapper
    {
        private readonly ICheckinToolService _checkinToolService;
        private readonly IToolService _toolService;

        public CheckinToolMapper(ICheckinToolService checkinToolService, IToolService toolService)
        {
            _checkinToolService = checkinToolService;
            _toolService = toolService;
        }

        public ListCheckinToolViewModel GetChekinsFilter(ListCheckinToolViewModel checkinToolViewModel)
        {
            checkinToolViewModel.CheckinTools = _checkinToolService.FilterCheckins(checkinToolViewModel.EmployeeCompanyHolder,
                                                                                   checkinToolViewModel.Tool,
                                                                                   checkinToolViewModel.CheckinDateTime);

            return checkinToolViewModel;
        }

        public CheckinTool CreateCheckinToolViewModelToCheckinTool(CreateCheckinToolViewModel createCheckinToolViewModel)
        {
            var checkinTool = new CheckinTool
            {
                EmployeeCompanyHolder = _checkinToolService.FindEmployeeCompanyByName(createCheckinToolViewModel.EmployeeCompanyHolderName),
                Tool = _toolService.FindToolByName(createCheckinToolViewModel.ToolName),
                CheckinDateTime = createCheckinToolViewModel.CheckinDateTime
            };

            return checkinTool;
        }
    }
}
