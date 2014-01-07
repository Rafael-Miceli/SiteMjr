using WebSiteMjr.Domain.Model;

namespace WebSiteMjr.Domain.Test.Model
{
    public static class CompanyDumies
    {
        public static Company CreateOneCompany()
        {
            return new Company
            {
                Name = "Portoverano",
                Address = "Rua gastao senges",
                City = "Rio de Janeiro",
                Email = "adm@portoverano.com",
                Id = 1,
                Phone = "2455-3100"
            };
        }
    }
}