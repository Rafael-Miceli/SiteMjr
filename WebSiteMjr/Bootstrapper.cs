using System.Web.Http;
using System.Web.Mvc;
using FlexProviders.Aspnet;
using FlexProviders.Membership;
using FlexProviders.Roles;
using Microsoft.Practices.Unity;
using Unity.Mvc3;
using WebSiteMjr.Domain.Interfaces.Role;
using WebSiteMjr.Domain.Interfaces.Services;
using WebSiteMjr.Domain.Model.Membership;
using WebSiteMjr.Domain.Model.Roles;
using WebSiteMjr.Domain.services;
using WebSiteMjr.Domain.services.Membership;
using WebSiteMjr.Domain.services.Stuffs;
using WebSiteMjr.EfData.Context;
using WebSiteMjr.EfData.DataRepository;
using WebSiteMjr.EfData.UnitOfWork;
using WebSiteMjr.EfStuffData.DataRepository;
using WebSiteMjr.EfStuffData.UnitOfWork;

namespace WebSiteMjr
{
    public static class Bootstrapper
    {
        public static void Initialise()
        {
            var container = BuildUnityContainer();

            GlobalConfiguration.Configuration.DependencyResolver = new Unity.WebApi.UnityDependencyResolver(container);
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }

        private static IUnityContainer BuildUnityContainer()
        {
            var container = new UnityContainer();

            // register all your components with the container here
            // e.g. container.RegisterType<ITestService, TestService>(); 

            //container.RegisterType<IRepository<Associate>, AssociateDtoRepository>();
            //container.RegisterType<IRepository<User>, UserDtoRepository>();
            //container.RegisterType<IRepository<Employee>, EmployeeDtoRepository>();
            var personUow = new PersonsUow();
            var stuffUnow = new StuffUow();

            //Services Instances
            container.RegisterType<IFlexRoleProvider, FlexRoleProvider>();
            container.RegisterType<IStuffCategoryService, StuffCategoryService>();
            container.RegisterType<IStuffService, StuffService>();
            container.RegisterType<IToolService, ToolService>();
            container.RegisterType<IToolLocalizationService, ToolLocalizationService>();
            container.RegisterType<ICheckinToolService, CheckinToolService>();
            container.RegisterType<IStuffManufactureService, StuffManufactureService>();
            container.RegisterType<ICompanyService, CompanyService>();
            container.RegisterType<IEmployeeService, EmployeeService>();
            container.RegisterType<IHolderService, HolderService>();

            //Repositories Instances
            var companyServiceInstance = new CompanyService(new CompanyRepository(personUow), personUow);
            var employeeServiceInstance = new EmployeeService(new EmployeeRepository(personUow), personUow);
            var toolServiceInstance = new ToolService(new ToolRepository(stuffUnow), stuffUnow);
            var toolLocalizationServiceInstance = new ToolLocalizationService(new ToolLocalizationRepository(personUow), personUow);

            container.RegisterInstance(new HolderService(new HolderRepository(personUow)));
            container.RegisterInstance(new UserService(new UserRepository(personUow), new CompanyRepository(personUow), personUow));
            container.RegisterInstance(new StuffService(new StuffRepository(stuffUnow), stuffUnow));
            container.RegisterInstance(toolServiceInstance);
            container.RegisterInstance(toolLocalizationServiceInstance);
            container.RegisterInstance(new CheckinToolService(new CheckinToolRepository(stuffUnow), stuffUnow, companyServiceInstance, employeeServiceInstance, toolServiceInstance));
            container.RegisterInstance(new StuffCategoryService(new StuffCategoryRepository(stuffUnow), stuffUnow));
            container.RegisterInstance(new StuffManufactureService(new StuffManufactureRepository(stuffUnow), stuffUnow));
            container.RegisterInstance(companyServiceInstance);
            container.RegisterInstance(employeeServiceInstance);
            container.RegisterInstance(new FlexRoleProvider(new RoleRepository<Role, User>(new PersonsContext())));
            container.RegisterInstance(new MembershipService(new FlexMembershipProvider(new MembershipRepository<User>(new PersonsContext()), new AspnetEnvironment())));

            return container;
        }
    }
}