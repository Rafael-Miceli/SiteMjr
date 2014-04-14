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
            PerforValidationsBeforeCreateCheckinOfTool(checkinTool);

            _checkinToolRepository.Add(checkinTool);
            _unitOfWork.Save();
        }

        private void PerforValidationsBeforeCreateCheckinOfTool(CheckinTool checkinTool)
        {
            if (IsAnyCheckinDateTimeOfThisToolAlreadyExists(checkinTool)) throw new ObjectExistsException<CheckinTool>();
            if (IsCheckinHolderTwiceThen(checkinTool)) throw new CheckinHolderTwiceThenException();

            if (!MjrSettings.Default.CanCheckinToolBetweenCompanies)
            {
                if (IsActualCheckinAndLastCheckinOfThisToolInACompany(checkinTool))
                    throw new CheckinCompanyToCompanyException();
            }
            else
            {
                if (!IsCheckinOfThisToolInCompany(checkinTool.EmployeeCompanyHolderId))
                {
                    checkinTool.CompanyAreaId = null;
                }
            }
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
                if (IsAnyCheckinDateTimeOfThisToolAlreadyExists(checkinToolUpdated))
                    throw new ObjectExistsException<CheckinTool>();

                if (checkinToolToUpdate.Tool.Id != checkinToolUpdated.Tool.Id)
                    IsChangeCreatingInconsitencyBetweenOtherCheckinsWhenToolChanged(checkinToolToUpdate,
                        checkinToolUpdated);
                else
                    IsChangeCreatingInconsitencyBetweenOtherCheckinsWhenToolNotChanged(checkinToolToUpdate,
                        checkinToolUpdated);
                
            }
            else
            {
                if (checkinToolToUpdate.Tool.Id != checkinToolUpdated.Tool.Id)
                {
                    if (IsAnyCheckinDateTimeOfThisToolAlreadyExists(checkinToolUpdated))
                        throw new ObjectExistsException<CheckinTool>();

                    IsChangeCreatingInconsitencyBetweenOtherCheckinsWhenToolChanged(checkinToolToUpdate,
                        checkinToolUpdated);
                }
                else
                {
                    var checkinsWithOriginalTool = ListCheckinToolsWithActualTool(checkinToolUpdated.Tool.Id);

                    if (checkinsWithOriginalTool != null)
                    {

                        checkinsWithOriginalTool = checkinsWithOriginalTool.OrderByDescending(c => c.CheckinDateTime).ToList();

                        var checkinUpdatedBeforeActual = GetFirstCheckinBeforeActual(checkinsWithOriginalTool, checkinToolUpdated);
                        var checkinUpdatedAfterActual = GetFirstCheckinAfterActual(checkinsWithOriginalTool, checkinToolUpdated);

                        if (checkinUpdatedAfterActual != null &&
                            (checkinUpdatedBeforeActual != null &&
                             (checkinUpdatedBeforeActual.EmployeeCompanyHolderId ==
                              checkinToolUpdated.EmployeeCompanyHolderId ||
                              checkinUpdatedAfterActual.EmployeeCompanyHolderId ==
                              checkinToolUpdated.EmployeeCompanyHolderId)))
                            throw new CheckinHolderTwiceThenException();


                        if (!MjrSettings.Default.CanCheckinToolBetweenCompanies)
                            if (IsCheckinOfThisToolInCompany(checkinToolUpdated.EmployeeCompanyHolderId))
                                if (IsCheckinOfThisToolInCompany(checkinUpdatedBeforeActual.EmployeeCompanyHolderId) ||
                                    IsCheckinOfThisToolInCompany(checkinUpdatedAfterActual.EmployeeCompanyHolderId))
                                    throw new CheckinCompanyToCompanyException();
                    }
                }
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

        public bool IsCheckinOfThisToolInCompany(int holderId)
        {
            return _companyService.ExistsCheckinOfToolInCompany(holderId) != null;
        }

        public CheckinTool LastCheckinOfThisTool(int toolId, DateTime checkinDateTime)
        {
            var checkinsWithActualTool = ListCheckinToolsWithActualTool(toolId);

            return checkinsWithActualTool.OrderByDescending(c => c.CheckinDateTime).FirstOrDefault(c => c.CheckinDateTime < checkinDateTime);
        }

        public CheckinTool NextCheckinOfThisTool(int toolId, DateTime checkinDateTime)
        {
            var checkinsWithActualTool = ListCheckinToolsWithActualTool(toolId);

            return checkinsWithActualTool.OrderByDescending(c => c.CheckinDateTime).FirstOrDefault(c => c.CheckinDateTime > checkinDateTime);
        }





        private bool IsCheckinHolderTwiceThen(CheckinTool checkinTool)
        {
            var lastCheckinOfThisTool = LastCheckinOfThisTool(checkinTool.Tool.Id, checkinTool.CheckinDateTime);
            var nextCheckinOfThisTool = NextCheckinOfThisTool(checkinTool.Tool.Id, checkinTool.CheckinDateTime);
            return (lastCheckinOfThisTool != null && lastCheckinOfThisTool.EmployeeCompanyHolderId == checkinTool.EmployeeCompanyHolderId) || (nextCheckinOfThisTool != null && nextCheckinOfThisTool.EmployeeCompanyHolderId == checkinTool.EmployeeCompanyHolderId);
        }

        private bool IsAnyCheckinDateTimeOfThisToolAlreadyExists(CheckinTool checkinTool)
        {
            return ListCheckinToolsWithActualTool(checkinTool.Tool.Id)
                .Any(c => c.CheckinDateTime == checkinTool.CheckinDateTime && c.Id != checkinTool.Id);
        }

        private bool IsActualCheckinAndLastCheckinOfThisToolInACompany(CheckinTool checkinTool)
        {
            var actualCheckinOfthisToolCompany = _companyService.ExistsCheckinOfToolInCompany(checkinTool.EmployeeCompanyHolderId);

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


        private bool IsChangeCreatingInconsitencyBetweenOtherCheckinsWhenToolChanged(CheckinTool checkinToUpdate, CheckinTool checkinUpdated)
        {
            var checkinsWithOriginalTool = ListCheckinToolsWithActualTool(checkinToUpdate.Tool.Id);

            if (checkinsWithOriginalTool == null)
                return false;

            checkinsWithOriginalTool = checkinsWithOriginalTool.OrderByDescending(c => c.CheckinDateTime).ToList();
            var checkinWithThisToolBeforeActual = GetFirstCheckinBeforeActual(checkinsWithOriginalTool,
                checkinToUpdate);
            var checkinWithThisToolAfterActual = GetFirstCheckinAfterActual(checkinsWithOriginalTool,
                checkinToUpdate);

            if (checkinWithThisToolBeforeActual == null && checkinWithThisToolAfterActual == null)
                return false;

            if (checkinWithThisToolBeforeActual != null && checkinWithThisToolBeforeActual.EmployeeCompanyHolderId == checkinWithThisToolAfterActual.EmployeeCompanyHolderId)
                throw new CheckinHolderTwiceThenException();

            if (!MjrSettings.Default.CanCheckinToolBetweenCompanies)
                if (IsActualCheckinCreatingSequenceOfCompanyInconsistency(checkinWithThisToolBeforeActual,
                    checkinWithThisToolAfterActual))
                    throw new CheckinCompanyToCompanyException();


            checkinsWithOriginalTool = ListCheckinToolsWithActualTool(checkinUpdated.Tool.Id);

            if (checkinsWithOriginalTool == null)
                return false;

            checkinsWithOriginalTool = checkinsWithOriginalTool.OrderByDescending(c => c.CheckinDateTime).ToList();
            checkinWithThisToolBeforeActual = GetFirstCheckinBeforeActual(checkinsWithOriginalTool, checkinUpdated);
            checkinWithThisToolAfterActual = GetFirstCheckinAfterActual(checkinsWithOriginalTool, checkinUpdated);

            if (checkinWithThisToolBeforeActual == null && checkinWithThisToolAfterActual == null)
                return false;

            if (checkinWithThisToolBeforeActual != null && (checkinWithThisToolBeforeActual.EmployeeCompanyHolderId == checkinUpdated.EmployeeCompanyHolderId || checkinWithThisToolAfterActual.EmployeeCompanyHolderId == checkinUpdated.EmployeeCompanyHolderId))
                throw new CheckinHolderTwiceThenException();


            if (!MjrSettings.Default.CanCheckinToolBetweenCompanies)
                if (IsCheckinOfThisToolInCompany(checkinUpdated.EmployeeCompanyHolderId))
                    if (IsCheckinOfThisToolInCompany(checkinWithThisToolBeforeActual.EmployeeCompanyHolderId) || IsCheckinOfThisToolInCompany(checkinWithThisToolAfterActual.EmployeeCompanyHolderId))
                        throw new CheckinCompanyToCompanyException();

            return false;
        }

        private bool IsChangeCreatingInconsitencyBetweenOtherCheckinsWhenToolNotChanged(CheckinTool checkinToUpdate, CheckinTool checkinUpdated)
        {
            var checkinChangedPosition = true;
            var checkinsWithOriginalTool = ListCheckinToolsWithActualTool(checkinToUpdate.Tool.Id);

            if (checkinsWithOriginalTool == null)
                return false;

            checkinsWithOriginalTool = checkinsWithOriginalTool.OrderByDescending(c => c.CheckinDateTime).ToList();
            var checkinToUpdateBeforeActual  = GetFirstCheckinBeforeActual(checkinsWithOriginalTool,
                checkinToUpdate);
            var checkinToUpdateAfterActual = GetFirstCheckinAfterActual(checkinsWithOriginalTool,
                checkinToUpdate);

            var checkinUpdatedBeforeActual = GetFirstCheckinBeforeActual(checkinsWithOriginalTool,
                checkinUpdated);
            var checkinUpdatedAfterActual = GetFirstCheckinAfterActual(checkinsWithOriginalTool,
                checkinUpdated);

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
                if (checkinToUpdateBeforeActual == null && checkinToUpdateAfterActual == null)
                    return false;

                if (checkinToUpdateAfterActual != null && (checkinToUpdateBeforeActual != null && checkinToUpdateBeforeActual.EmployeeCompanyHolderId == checkinToUpdateAfterActual.EmployeeCompanyHolderId))
                    throw new CheckinHolderTwiceThenException();

                if (!MjrSettings.Default.CanCheckinToolBetweenCompanies)
                    if (IsActualCheckinCreatingSequenceOfCompanyInconsistency(checkinToUpdateBeforeActual,
                        checkinToUpdateAfterActual))
                        throw new CheckinCompanyToCompanyException();


                if (checkinUpdatedBeforeActual == null && checkinUpdatedAfterActual == null)
                    return false;

                if (((checkinUpdatedBeforeActual != null && checkinUpdatedBeforeActual.EmployeeCompanyHolderId == checkinUpdated.EmployeeCompanyHolderId) || (checkinUpdatedAfterActual != null && checkinUpdatedAfterActual.EmployeeCompanyHolderId == checkinUpdated.EmployeeCompanyHolderId)))
                    throw new CheckinHolderTwiceThenException();


                if (!MjrSettings.Default.CanCheckinToolBetweenCompanies)
                    if (IsCheckinOfThisToolInCompany(checkinUpdated.EmployeeCompanyHolderId))
                        if (IsCheckinOfThisToolInCompany(checkinUpdatedBeforeActual.EmployeeCompanyHolderId) || IsCheckinOfThisToolInCompany(checkinUpdatedAfterActual.EmployeeCompanyHolderId))
                            throw new CheckinCompanyToCompanyException();
            }
            else
            {
                if ((checkinUpdatedBeforeActual != null && checkinUpdatedBeforeActual.EmployeeCompanyHolderId == checkinUpdated.EmployeeCompanyHolderId) || (checkinUpdatedAfterActual.EmployeeCompanyHolderId == checkinUpdated.EmployeeCompanyHolderId))
                    throw new CheckinHolderTwiceThenException();


                if (!MjrSettings.Default.CanCheckinToolBetweenCompanies)
                    if (IsCheckinOfThisToolInCompany(checkinUpdated.EmployeeCompanyHolderId))
                        if (IsCheckinOfThisToolInCompany(checkinUpdatedBeforeActual.EmployeeCompanyHolderId) || IsCheckinOfThisToolInCompany(checkinUpdatedAfterActual.EmployeeCompanyHolderId))
                            throw new CheckinCompanyToCompanyException();
            }

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
            return IsCheckinOfThisToolInCompany(checkinWithThisToolBeforeActual.EmployeeCompanyHolderId) &&
                   IsCheckinOfThisToolInCompany(checkinWithThisToolAfterActual.EmployeeCompanyHolderId);
        }

        private bool IsActualCheckinCreatingSequenceOfHoldersInconsistency(CheckinTool checkinWithThisToolBeforeActual, CheckinTool checkinWithThisToolAfterActual, CheckinTool originalCheckin)
        {
            return checkinWithThisToolBeforeActual.EmployeeCompanyHolderId == originalCheckin.EmployeeCompanyHolderId || checkinWithThisToolAfterActual.EmployeeCompanyHolderId == originalCheckin.EmployeeCompanyHolderId || checkinWithThisToolBeforeActual.EmployeeCompanyHolderId == checkinWithThisToolAfterActual.EmployeeCompanyHolderId;
        }
        
    }
}
