using WebSiteMjr.Domain.Interfaces.Services;
using WebSiteMjr.ViewModels;

namespace WebSiteMjr.Assembler
{
    public class CheckinToolMapper
    {
        private readonly ICheckinToolService _checkinToolService;

        public CheckinToolMapper(ICheckinToolService checkinToolService)
        {
            _checkinToolService = checkinToolService;
        }

        public CheckinToolViewModel GetChekinsFilter(CheckinToolViewModel checkinToolViewModel)
        {
            checkinToolViewModel.CheckinTools = _checkinToolService.FilterCheckins(checkinToolViewModel.EmployeeCompanyHolder,
                                                                                   checkinToolViewModel.Tool.Name,
                                                                                   checkinToolViewModel.CheckinDateTime);

            return checkinToolViewModel;
        }
    }
}
