using System;
using System.Collections.Generic;
using System.Linq;
using WebSiteMjr.Domain.Exceptions;
using WebSiteMjr.Domain.Interfaces.Repository;
using WebSiteMjr.Domain.Interfaces.Services;
using WebSiteMjr.Domain.Interfaces.Uow;
using WebSiteMjr.Domain.Model;

namespace WebSiteMjr.Domain.services.Stuffs
{
    public class CheckinToolService: ICheckinToolService
    {
        private readonly ICheckinToolRepository _checkinToolRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICompanyService _companyService;
        private IEnumerable<CheckinTool> _listCheckinToolsWithActualTool;

        public CheckinToolService(ICheckinToolRepository checkinToolRepository, IUnitOfWork unitOfWork, ICompanyService companyService) 
        {
            _checkinToolRepository = checkinToolRepository;
            _unitOfWork = unitOfWork;
            _companyService = companyService;
        }

        public void CheckinTool(CheckinTool checkinTool)
        {
            if (IsAnyCheckiDateTimeOfThisToolAlreadyExists(checkinTool)) throw new ObjectExistsException<CheckinTool>();
            if (IsCheckinHolderTwiceThen(checkinTool)) throw new CheckinHolderTwiceThenException();
            if (IsActualCheckinAndLastCheckinOfThisToolInACompany(checkinTool)) throw new CheckinCompanyToCompanyException();

            _checkinToolRepository.Add(checkinTool);
            _unitOfWork.Save();
        }

        public void UpdateToolCheckin(CheckinTool checkinTool)
        {
            var checkinToolToUpdate = FindToolCheckin(checkinTool.Id);

            if (IsAnyCheckiDateTimeOfThisToolAlreadyExists(checkinTool)) throw new ObjectExistsException<CheckinTool>();
            if (IsActualCheckinCreatingInConsitencyBetweenOtherCheckins(checkinToolToUpdate)) throw new CheckinInconsistencyException();
            if (IsCheckinHolderTwiceThen(checkinTool)) throw new CheckinHolderTwiceThenException();
            if (IsActualCheckinAndLastCheckinOfThisToolInACompany(checkinTool)) throw new CheckinCompanyToCompanyException();

            checkinToolToUpdate.EmployeeCompanyHolderId = checkinTool.EmployeeCompanyHolderId;
            checkinToolToUpdate.Tool = checkinTool.Tool;
            checkinToolToUpdate.CheckinDateTime = checkinTool.CheckinDateTime;
            checkinToolToUpdate.CompanyAreaId = checkinTool.CompanyAreaId;

            _checkinToolRepository.Update(checkinToolToUpdate);
            _unitOfWork.Save();
        }

        public void DeleteToolCheckin(object idCheckinTool)
        {
            var checkinToolToDelete = FindToolCheckin(idCheckinTool);
            if (IsActualCheckinCreatingInConsitencyBetweenOtherCheckins(checkinToolToDelete)) throw new CheckinInconsistencyException();
            _checkinToolRepository.Remove(idCheckinTool);
            _unitOfWork.Save();
        }

        public IEnumerable<CheckinTool> ListToolCheckins()
        {
            return _checkinToolRepository.GetAll();
        }

        public IEnumerable<CheckinTool> FilterCheckins(Holder employeeCompany, string toolName, DateTime? date)
        {
            var checkins = _checkinToolRepository.GetAll();

            if (employeeCompany != null)
            {
                checkins = checkins.Where(c => c.EmployeeCompanyHolderId == employeeCompany.Id);
            }

            if (!String.IsNullOrEmpty(toolName))
            {
                checkins = checkins.Where(c => String.Equals(c.Tool.Name, toolName, StringComparison.CurrentCultureIgnoreCase));
            }

            if (date.HasValue)
            {
                checkins = checkins.Where(c => c.CheckinDateTime.Date == date.Value.Date);
            }

            return checkins;
        }

        public CheckinTool FindToolCheckin(object idCheckinTool)
        {
            return _checkinToolRepository.GetById(idCheckinTool);
        }

        public IEnumerable<CheckinTool> ListCheckinToolsWithActualTool(int toolId)
        {
            if (_listCheckinToolsWithActualTool != null && _listCheckinToolsWithActualTool.Any(c => c.Tool.Id == toolId))
            {
                return _listCheckinToolsWithActualTool;
            }

            _listCheckinToolsWithActualTool = _checkinToolRepository.Query(c => c.Tool.Id == toolId);

            return _listCheckinToolsWithActualTool;
        }

        public bool IsCheckinOfThisToolInCompany(int employeeCompanyHolderId)
        {
            try
            {
                return _companyService.FindCompany(employeeCompanyHolderId) != null;
            }
            catch (Exception)
            {
                return false;
            }

        }

        public Company ExistsCheckinOfThisToolInCompany(int employeeCompanyHolderId)
        {
            try
            {
                return _companyService.FindCompany(employeeCompanyHolderId);
            }
            catch (Exception)
            {
                return null;
            }

        }

        public CheckinTool LastCheckinOfThisTool(int toolId, DateTime checkinDateTime)
        {
            var checkinsWithActualTool = ListCheckinToolsWithActualTool(toolId);

            return checkinsWithActualTool.OrderByDescending(c => c.CheckinDateTime).FirstOrDefault(c => c.CheckinDateTime < checkinDateTime);
        }




        private bool IsCheckinHolderTwiceThen(CheckinTool checkinTool)
        {
            var lastCheckinsOfThisTool = LastCheckinOfThisTool(checkinTool.Tool.Id, checkinTool.CheckinDateTime);
            return lastCheckinsOfThisTool != null && lastCheckinsOfThisTool.EmployeeCompanyHolderId == checkinTool.EmployeeCompanyHolderId;
        }

        private bool IsAnyCheckiDateTimeOfThisToolAlreadyExists(CheckinTool checkinTool)
        {
            return ListCheckinToolsWithActualTool(checkinTool.Tool.Id)
                .Any(c => c.CheckinDateTime == checkinTool.CheckinDateTime && c.Id != checkinTool.Id);
        }

        private bool IsActualCheckinAndLastCheckinOfThisToolInACompany(CheckinTool checkinTool)
        {
            var actualCheckinOfthisToolCompany = ExistsCheckinOfThisToolInCompany(checkinTool.EmployeeCompanyHolderId);

            var isActualCheckinOfthisToolInCompany = actualCheckinOfthisToolCompany != null;
            var isCompanyAreaNull = checkinTool.CompanyAreaId == null;

            if (!isActualCheckinOfthisToolInCompany || isCompanyAreaNull || !CompanyAreaExistsInCompany(actualCheckinOfthisToolCompany, checkinTool.CompanyAreaId.Value))
                checkinTool.CompanyAreaId = null;

            return isActualCheckinOfthisToolInCompany && WasLastCheckinOfThisToolInCompany(checkinTool);
        }

        private bool CompanyAreaExistsInCompany(Company actualCheckinOfthisToolCompany, int companyAreaId)
        {
            if (actualCheckinOfthisToolCompany == null) return false;

            var companyCompanyAreas = _companyService.FindCompanyCompanyAreas(actualCheckinOfthisToolCompany.Name);

            return companyCompanyAreas != null && companyCompanyAreas.Any(ca => ca.Id == companyAreaId);
        }

        private bool WasLastCheckinOfThisToolInCompany(CheckinTool checkinTool)
        {
            var lastCheckinToolBeforeTheActual = LastCheckinOfThisTool(checkinTool.Tool.Id, checkinTool.CheckinDateTime);

            return lastCheckinToolBeforeTheActual != null && IsCheckinOfThisToolInCompany(lastCheckinToolBeforeTheActual.EmployeeCompanyHolderId);
        }

        private bool IsActualCheckinCreatingInConsitencyBetweenOtherCheckins(CheckinTool originalCheckin)
        {
            var checkinsWithOriginalTool = ListCheckinToolsWithActualTool(originalCheckin.Tool.Id);

            if (checkinsWithOriginalTool == null)
                return false;

            checkinsWithOriginalTool = checkinsWithOriginalTool.OrderByDescending(c => c.CheckinDateTime).ToList();
            var checkinWithThisToolBeforeActual = GetFirstCheckinBeforeActual(checkinsWithOriginalTool, originalCheckin);
            var checkinWithThisToolAfterActual = GetFirstCheckinAfterActual(checkinsWithOriginalTool, originalCheckin);

            if (checkinWithThisToolBeforeActual == null || checkinWithThisToolAfterActual == null)
                return false;

            if (IsActualCheckinCreatingSequenceOfCompanyInconsistency(checkinWithThisToolBeforeActual, checkinWithThisToolAfterActual))
            {
                return true;
            }

            if (IsActualCheckinCreatingSequenceOfEmployeeInconsistency(checkinWithThisToolBeforeActual, checkinWithThisToolAfterActual, originalCheckin))
            {
                return true;
            }

            return false;
        }

        private CheckinTool GetFirstCheckinBeforeActual(IEnumerable<CheckinTool> checkins, CheckinTool originalCheckin)
        {
            return checkins.FirstOrDefault(c => c.CheckinDateTime < originalCheckin.CheckinDateTime);
        }

        private CheckinTool GetFirstCheckinAfterActual(IEnumerable<CheckinTool> checkins, CheckinTool originalCheckin)
        {
            return checkins.LastOrDefault(c => c.CheckinDateTime > originalCheckin.CheckinDateTime);
        }

        private bool IsActualCheckinCreatingSequenceOfCompanyInconsistency(CheckinTool checkinWithThisToolBeforeActual, CheckinTool checkinWithThisToolAfterActual)
        {
            return IsCheckinOfThisToolInCompany(checkinWithThisToolBeforeActual.EmployeeCompanyHolderId) &&
                   IsCheckinOfThisToolInCompany(checkinWithThisToolAfterActual.EmployeeCompanyHolderId);
        }

        private bool IsActualCheckinCreatingSequenceOfEmployeeInconsistency(CheckinTool checkinWithThisToolBeforeActual, CheckinTool checkinWithThisToolAfterActual, CheckinTool originalCheckin)
        {
            return checkinWithThisToolBeforeActual.EmployeeCompanyHolderId == originalCheckin.EmployeeCompanyHolderId || checkinWithThisToolAfterActual.EmployeeCompanyHolderId == originalCheckin.EmployeeCompanyHolderId;
        }
        
    }
}
