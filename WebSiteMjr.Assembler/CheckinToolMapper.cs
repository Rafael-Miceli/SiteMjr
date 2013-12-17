﻿using System.Collections.Generic;
using WebSiteMjr.Domain.Interfaces.Services;
using WebSiteMjr.Domain.Model;
using WebSiteMjr.ViewModels;

namespace WebSiteMjr.Assembler
{
    public class CheckinToolMapper
    {
        private readonly ICheckinToolService _checkinToolService;
        private readonly IToolService _toolService;
        private readonly IHolderService _holderService;

        public CheckinToolMapper(ICheckinToolService checkinToolService, IToolService toolService, IHolderService holderService)
        {
            _checkinToolService = checkinToolService;
            _toolService = toolService;
            _holderService = holderService;
        }

        public ListCheckinToolViewModel GetChekinsFilter(ListCheckinToolViewModel checkinToolViewModel)
        {
            //checkinToolViewModel.CheckinTools = _checkinToolService.FilterCheckins(_checkinToolService.FindEmployeeCompanyByName(checkinToolViewModel.EmployeeCompanyHolder),
            //                                                                       checkinToolViewModel.Tool,
            //                                                                       checkinToolViewModel.CheckinDateTime);

            var checkins = _checkinToolService.FilterCheckins(_checkinToolService.FindEmployeeCompanyByName(checkinToolViewModel.EmployeeCompanyHolder),
                                                                                   checkinToolViewModel.Tool,
                                                                                   checkinToolViewModel.CheckinDateTime);
            checkinToolViewModel.CheckinTools = new List<EnumerableCheckinToolViewModel>();

            foreach (var checkinTool in checkins)
            {
                checkinToolViewModel.CheckinTools.Add(CheckinToolToCreateCheckinToolViewModel(checkinTool));
            }

            return checkinToolViewModel;
        }

        public EnumerableCheckinToolViewModel CheckinToolToCreateCheckinToolViewModel(CheckinTool checkinTool)
        {
            var createCheckinToolViewModel = new EnumerableCheckinToolViewModel
            {
                Id = checkinTool.EmployeeCompanyHolderId,
                CheckinDateTime = checkinTool.CheckinDateTime,
                ToolName = checkinTool.Tool.Name,
                EmployeeCompanyHolderName = _holderService.FindHolder(checkinTool.EmployeeCompanyHolderId).Name
            };

            return createCheckinToolViewModel;
        }

        public CheckinTool CreateCheckinToolViewModelToCheckinTool(CreateCheckinToolViewModel createCheckinToolViewModel)
        {
            var checkinTool = new CheckinTool
            {
                EmployeeCompanyHolderId = _checkinToolService.FindEmployeeCompanyByName(createCheckinToolViewModel.EmployeeCompanyHolderName).Id,
                Tool = _toolService.FindToolByName(createCheckinToolViewModel.ToolName),
                CheckinDateTime = createCheckinToolViewModel.CheckinDateTime
            };

            return checkinTool;
        }
    }
}
