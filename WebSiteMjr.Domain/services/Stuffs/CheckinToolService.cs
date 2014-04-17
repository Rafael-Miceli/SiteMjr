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
        private Company _checkinOfthisToolCompany;

        public CheckinToolService(ICheckinToolRepository checkinToolRepository, IUnitOfWork unitOfWork, ICompanyService companyService) 
        {
            _checkinToolRepository = checkinToolRepository;
            _unitOfWork = unitOfWork;
            _companyService = companyService;
        }

        public void CheckinTool(CheckinTool checkinTool)
        {
            if (AnyCheckinDateTimeOfThisToolAlreadyExists(checkinTool)) throw new ObjectExistsException<CheckinTool>();

            ValidateNewPositionOfCheckin(checkinTool);

            ValidateCheckinCompanyArea(checkinTool);

            _checkinToolRepository.Add(checkinTool);
            _unitOfWork.Save();
        }

        public void UpdateToolCheckin(CheckinTool checkinToolUpdated)
        {
            var checkinToolToUpdate = FindToolCheckin(checkinToolUpdated.Id);

            PerformValidationBeforeUpdateCheckin(checkinToolUpdated, checkinToolToUpdate);

            _checkinToolRepository.Update(checkinToolToUpdate);
            _unitOfWork.Save();
        }

        private void PerformValidationBeforeUpdateCheckin(CheckinTool checkinToolUpdated, CheckinTool checkinToolToUpdate)
        {
            if (checkinToolToUpdate.CheckinDateTime != checkinToolUpdated.CheckinDateTime)
            {
                if (AnyCheckinDateTimeOfThisToolAlreadyExists(checkinToolUpdated))
                    throw new ObjectExistsException<CheckinTool>();

                if (checkinToolToUpdate.Tool.Id != checkinToolUpdated.Tool.Id)
                {
                    ValidateOldPositionOfCheckin(checkinToolToUpdate);

                    ValidateNewPositionOfCheckin(checkinToolUpdated);
                }

                else
                    IsChangeCreatingInconsitencyBetweenOtherCheckinsWhenToolNotChanged(checkinToolToUpdate,
                        checkinToolUpdated);
                
            }
            else
            {
                if (checkinToolToUpdate.Tool.Id != checkinToolUpdated.Tool.Id)
                {
                    if (AnyCheckinDateTimeOfThisToolAlreadyExists(checkinToolUpdated))
                        throw new ObjectExistsException<CheckinTool>();

                    ValidateOldPositionOfCheckin(checkinToolToUpdate);

                    ValidateNewPositionOfCheckin(checkinToolUpdated);
                }
                else
                    ValidateNewPositionOfCheckin(checkinToolUpdated);
            }
            
            checkinToolToUpdate.EmployeeCompanyHolderId = checkinToolUpdated.EmployeeCompanyHolderId;
            checkinToolToUpdate.Tool = checkinToolUpdated.Tool;
            checkinToolToUpdate.CheckinDateTime = checkinToolUpdated.CheckinDateTime;

            if (checkinToolUpdated.CompanyAreaId != null)
            {
                var company = _companyService.ExistsCheckinOfToolInCompany(checkinToolUpdated.EmployeeCompanyHolderId);

                if (company != null)
                {
                    checkinToolToUpdate.CompanyAreaId = CompanyAreaExistsInCompany(company, checkinToolUpdated.CompanyAreaId.Value)
                        ? checkinToolUpdated.CompanyAreaId
                        : null;
                }
                else
                    checkinToolToUpdate.CompanyAreaId = null;
            }
            else
                checkinToolToUpdate.CompanyAreaId = null;
        }

        public void DeleteToolCheckin(object idCheckinTool)
        {
            var checkinToolToDelete = FindToolCheckin(idCheckinTool);
            IsChangeCreatingInconsitencyBetweenOtherCheckins(checkinToolToDelete);
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

        public bool IsCheckinOfToolInCompany(int holderId)
        {
            return _companyService.ExistsCheckinOfToolInCompany(holderId) != null;
        }

        private bool IsCheckinOfThisToolInCompany()
        {
            return _checkinOfthisToolCompany != null;
        }

        public void CheckinOfThisToolPopulateIfInCompany(int holderId)
        {
            _checkinOfthisToolCompany = _companyService.ExistsCheckinOfToolInCompany(holderId);
        }

        public CheckinTool LastCheckinOfThisTool(CheckinTool checkinTool)
        {
            var checkinsWithActualTool = ListCheckinToolsWithActualTool(checkinTool.Tool.Id).Where(c => c.Id != checkinTool.Id);

            return checkinsWithActualTool.OrderByDescending(c => c.CheckinDateTime).FirstOrDefault(c => c.CheckinDateTime < checkinTool.CheckinDateTime);
        }

        public CheckinTool NextCheckinOfThisTool(CheckinTool checkinTool)
        {
            var checkinsWithActualTool = ListCheckinToolsWithActualTool(checkinTool.Tool.Id).Where(c => c.Id != checkinTool.Id);

            return checkinsWithActualTool.OrderByDescending(c => c.CheckinDateTime).LastOrDefault(c => c.CheckinDateTime > checkinTool.CheckinDateTime);
        }





        private bool IsNewCheckinCreatingHolderTwice(CheckinTool checkinTool)
        {
            var lastCheckinOfThisTool = LastCheckinOfThisTool(checkinTool);
            var nextCheckinOfThisTool = NextCheckinOfThisTool(checkinTool);

            return (lastCheckinOfThisTool != null && lastCheckinOfThisTool.EmployeeCompanyHolderId == checkinTool.EmployeeCompanyHolderId) || (nextCheckinOfThisTool != null && nextCheckinOfThisTool.EmployeeCompanyHolderId == checkinTool.EmployeeCompanyHolderId);
        }

        private bool IsOldCheckinCreatingHolderTwice(CheckinTool checkinTool)
        {
            var lastCheckinOfThisTool = LastCheckinOfThisTool(checkinTool);
            var nextCheckinOfThisTool = NextCheckinOfThisTool(checkinTool);

            return (lastCheckinOfThisTool != null && nextCheckinOfThisTool != null && lastCheckinOfThisTool.EmployeeCompanyHolderId == nextCheckinOfThisTool.EmployeeCompanyHolderId);
        }

        private bool AnyCheckinDateTimeOfThisToolAlreadyExists(CheckinTool checkinTool)
        {
            return ListCheckinToolsWithActualTool(checkinTool.Tool.Id)
                .Any(c => c.CheckinDateTime == checkinTool.CheckinDateTime && c.Id != checkinTool.Id);
        }

        private bool IsActualAndBetweenCheckinsInACompany(CheckinTool checkinTool)
        {
            return IsCheckinOfThisToolInCompany() && IsLastOrNextCheckinOfThisToolInCompany(checkinTool);
        }

        private bool IsLastAndNextCheckinOfThisToolInCompany(CheckinTool checkinTool)
        {
            var checkinToolBeforeTheActual = LastCheckinOfThisTool(checkinTool);
            var checkinToolAfterTheActual = NextCheckinOfThisTool(checkinTool);

            return (checkinToolBeforeTheActual != null && IsCheckinOfToolInCompany(checkinToolBeforeTheActual.EmployeeCompanyHolderId)) &&
                   (checkinToolAfterTheActual != null && IsCheckinOfToolInCompany(checkinToolAfterTheActual.EmployeeCompanyHolderId));
        }

        private void ValidateCheckinCompanyArea(CheckinTool checkinTool)
        {
            var isCompanyAreaNull = checkinTool.CompanyAreaId == null;

            if (!IsCheckinOfThisToolInCompany() || isCompanyAreaNull || !CompanyAreaExistsInCompany(_checkinOfthisToolCompany, checkinTool.CompanyAreaId.Value))
                checkinTool.CompanyAreaId = null;
        }

        private bool CompanyAreaExistsInCompany(Company actualCheckinOfthisToolCompany, int companyAreaId)
        {
            if (actualCheckinOfthisToolCompany == null) return false;

            var companyCompanyAreas = _companyService.FindCompanyCompanyAreas(actualCheckinOfthisToolCompany.Name);

            return companyCompanyAreas != null && companyCompanyAreas.Any(ca => ca.Id == companyAreaId);
        }

        private bool IsLastOrNextCheckinOfThisToolInCompany(CheckinTool checkinTool)
        {
            var checkinToolBeforeTheActual = LastCheckinOfThisTool(checkinTool);
            var checkinToolAfterTheActual = NextCheckinOfThisTool(checkinTool);

            return (checkinToolBeforeTheActual != null && IsCheckinOfToolInCompany(checkinToolBeforeTheActual.EmployeeCompanyHolderId)) || (checkinToolAfterTheActual != null && IsCheckinOfToolInCompany(checkinToolAfterTheActual.EmployeeCompanyHolderId));
        }



        private void ValidateNewPositionOfCheckin(CheckinTool checkin)
        {
            if (IsNewCheckinCreatingHolderTwice(checkin))
                throw new CheckinHolderTwiceThenException();

            CheckinOfThisToolPopulateIfInCompany(checkin.EmployeeCompanyHolderId);

            if (!MjrSettings.Default.CanCheckinToolBetweenCompanies)
                if (IsActualAndBetweenCheckinsInACompany(checkin))
                    throw new CheckinCompanyToCompanyException();
        }

        private void ValidateOldPositionOfCheckin(CheckinTool checkin)
        {
            if (IsOldCheckinCreatingHolderTwice(checkin))
                throw new CheckinHolderTwiceThenException();

            if (MjrSettings.Default.CanCheckinToolBetweenCompanies) return;
            if (IsLastAndNextCheckinOfThisToolInCompany(checkin))
                throw new CheckinCompanyToCompanyException();
        }

        private bool IsChangeCreatingInconsitencyBetweenOtherCheckinsWhenToolNotChanged(CheckinTool checkinToUpdate, CheckinTool checkinUpdated)
        {
            var checkinChangedPosition = true;

            var checkinToUpdateBeforeActual  = LastCheckinOfThisTool(checkinToUpdate);
            var checkinToUpdateAfterActual = NextCheckinOfThisTool(checkinToUpdate);

            var checkinUpdatedBeforeActual = LastCheckinOfThisTool(checkinUpdated);
            var checkinUpdatedAfterActual = NextCheckinOfThisTool(checkinUpdated);

            if (checkinUpdatedBeforeActual != null && checkinToUpdateBeforeActual != null)
            {
                if (checkinToUpdateBeforeActual.EmployeeCompanyHolderId == checkinUpdatedBeforeActual.EmployeeCompanyHolderId)
                {
                    if (checkinToUpdateAfterActual != null && checkinUpdatedAfterActual != null)
                    {
                        if (checkinToUpdateAfterActual.EmployeeCompanyHolderId == checkinUpdatedAfterActual.EmployeeCompanyHolderId)
                        {
                            checkinChangedPosition = false;
                        }
                    }
                }
            }
            else
            {
                if (checkinToUpdateAfterActual != null && checkinUpdatedAfterActual != null)
                {
                    if (checkinToUpdateAfterActual.EmployeeCompanyHolderId == checkinUpdatedAfterActual.EmployeeCompanyHolderId)
                    {
                        checkinChangedPosition = false;
                    }
                }
            }

            if (checkinChangedPosition)
            {

                ValidateOldPositionOfCheckin(checkinToUpdate);

                ValidateNewPositionOfCheckin(checkinUpdated);

            }
            else
                ValidateNewPositionOfCheckin(checkinUpdated);

            return false;
        }


        private bool IsChangeCreatingInconsitencyBetweenOtherCheckins(CheckinTool originalCheckin)
        {
            var checkinsWithOriginalTool = ListCheckinToolsWithActualTool(originalCheckin.Tool.Id);

            if (checkinsWithOriginalTool == null)
                return false;

            checkinsWithOriginalTool = checkinsWithOriginalTool.OrderByDescending(c => c.CheckinDateTime).ToList();
            var checkinWithThisToolBeforeActual = GetFirstCheckinBeforeActual(checkinsWithOriginalTool,
                originalCheckin);
            var checkinWithThisToolAfterActual = GetFirstCheckinAfterActual(checkinsWithOriginalTool,
                originalCheckin);

            if (checkinWithThisToolBeforeActual == null || checkinWithThisToolAfterActual == null)
                return false;

            if (IsActualCheckinCreatingSequenceOfHoldersInconsistency(checkinWithThisToolBeforeActual,
                checkinWithThisToolAfterActual, originalCheckin))
                throw new CheckinHolderTwiceThenException();

            if (!MjrSettings.Default.CanCheckinToolBetweenCompanies)
                if (IsActualCheckinCreatingSequenceOfCompanyInconsistency(checkinWithThisToolBeforeActual,
                    checkinWithThisToolAfterActual))
                    throw new CheckinCompanyToCompanyException();

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
            return (checkinWithThisToolBeforeActual != null && IsCheckinOfToolInCompany(checkinWithThisToolBeforeActual.EmployeeCompanyHolderId)) &&
                   (checkinWithThisToolAfterActual != null && IsCheckinOfToolInCompany(checkinWithThisToolAfterActual.EmployeeCompanyHolderId));
        }

        private bool IsActualCheckinCreatingSequenceOfHoldersInconsistency(CheckinTool checkinWithThisToolBeforeActual, CheckinTool checkinWithThisToolAfterActual, CheckinTool originalCheckin)
        {
            return checkinWithThisToolBeforeActual.EmployeeCompanyHolderId == originalCheckin.EmployeeCompanyHolderId || checkinWithThisToolAfterActual.EmployeeCompanyHolderId == originalCheckin.EmployeeCompanyHolderId || checkinWithThisToolBeforeActual.EmployeeCompanyHolderId == checkinWithThisToolAfterActual.EmployeeCompanyHolderId;
        }
        
    }
}
