using System.Collections.Generic;
using System.Linq;
using ClubManager.Domain.DTOs;
using ClubManager.Domain.Interfaces;

namespace ClubManager.Domain.services
{
    public class AssociateService
    {
        private readonly IAssociate _associate;

        public AssociateService(IAssociate associate)
        {
            _associate = associate;
        }

        public void CreateAssociate(AssociateDTO associate)
        {
            _associate.AddAssociate(associate);
        }

        public void UpdateAssociate(AssociateDTO associate)
        {
            _associate.UpdateAssociate(associate);
        }

        public void DeleteAssociate(AssociateDTO associate)
        {
            _associate.DeleteAssociate(associate);
        }

        public IEnumerable<AssociateDTO> ListAssociate()
        {
            return _associate.ListAssociate();
        }

        public AssociateDTO FindAssociate(int idassociate)
        {
            return _associate.FindAssociate(idassociate);
        }

        public bool AuthAssociate(string login, string password)
        {
            return _associate.ListAssociate().FirstOrDefault(ass => ass.Title == login && ass.Password == password) != null;
        }

    }
}
