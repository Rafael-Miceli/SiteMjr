using System.Web.Http;
using System.Web.Mvc;
using FlexProviders.Aspnet;
using FlexProviders.Membership;
using FlexProviders.Roles;
using Microsoft.Practices.Unity;
using SharedKernel;
using SharedKernel.Interfaces;
using Unity.Mvc3;
using WebSiteMjr.Domain.CustomerService.Events;
using WebSiteMjr.Domain.CustomerService.Services;
using WebSiteMjr.Domain.Interfaces.CustomerService;
using WebSiteMjr.Domain.Interfaces.Role;
using WebSiteMjr.Domain.Interfaces.Services;
using WebSiteMjr.Domain.Model.Membership;
using WebSiteMjr.Domain.Model.Roles;
using WebSiteMjr.Domain.services;
using WebSiteMjr.Domain.services.Membership;
using WebSiteMjr.Domain.services.Roles;
using WebSiteMjr.Domain.services.Stuffs;
using WebSiteMjr.EfCustomerServiceData.DataRepository;
using WebSiteMjr.EfCustomerServiceData.UnitOfWork;
using WebSiteMjr.EfData.DataRepository;
using WebSiteMjr.EfData.UnitOfWork;
using WebSiteMjr.EfStuffData.DataRepository;
using WebSiteMjr.EfStuffData.UnitOfWork;
using WebSiteMjr.Facade;
using WebSiteMjr.Models;
using WebSiteMjr.Notifications.Email.MjrEmailNotification;

namespace WebSiteMjr
{
    public static class Bootstrapper
    {
        public static void Initialise()
        {
            var container = BuildUnityContainer();

            GlobalConfiguration.Configuration.DependencyResolver = new Unity.WebApi.UnityDependencyResolver(container);
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
            DomainEvents.Container = container;
        }

        private static IUnityContainer BuildUnityContainer()
        {
            var container = new UnityContainer();

            // register all your components with the container here
            // e.g. container.RegisterType<ITestService, TestService>(); 

            var personUow = new PersonsUow();
            var stuffUnow = new StuffUow();
            var customerServiceUow = new CustomerServiceUow();

            //Services Instances
            container.RegisterType<IFlexRoleProvider, FlexRoleProvider>();
            container.RegisterType<IRoleService, MjrAppRoleService>();
            container.RegisterType<IStuffCategoryService, StuffCategoryService>();
            container.RegisterType<IStuffService, StuffService>();
            container.RegisterType<IToolService, ToolService>();
            container.RegisterType<ICompanyAreasService, CompanyAreasService>();
            container.RegisterType<ICheckinToolService, CheckinToolService>();
            container.RegisterType<IStuffManufactureService, StuffManufactureService>();
            container.RegisterType<ICompanyService, CompanyService>();
            container.RegisterType<IEmployeeService, EmployeeService>();
            container.RegisterType<IHolderService, HolderService>();
            //container.RegisterType<IUserService, UserService>();
            container.RegisterType<IMembershipService, MembershipService>();
            container.RegisterType<ICallService, CallService>();

            //Facade Instances
            container.RegisterType<IEmployeeLoginFacade, EmployeeLoginFacade>();

            //Repositories Instances
            var emailServiceInstance = new EmailService();
            var roleServiceInstance = new MjrAppRoleService(new FlexRoleProvider(new RoleRepository<MjrAppRole,User>(personUow)));
            var membershipServiceInstance = new MembershipService(new FlexMembershipProvider(new MembershipRepository(personUow), new AspnetEnvironment()), roleServiceInstance, personUow);
            var companyServiceInstance = new CompanyService(new CompanyRepository(personUow), personUow);
            var employeeServiceInstance = new EmployeeService(new EmployeeRepository(personUow), personUow);
            var toolServiceInstance = new ToolService(new ToolRepository(stuffUnow), stuffUnow);
            var companyAreaServiceInstance = new CompanyAreasService(new CompanyAreaRepository(personUow), personUow);
            var employeeLoginFacadeInstance = new EmployeeLoginFacade(employeeServiceInstance, membershipServiceInstance,
                emailServiceInstance, personUow);
            var callServiceInstance = new CallService(new CallRepository(customerServiceUow), customerServiceUow);

            container.RegisterInstance(new HolderService(new HolderRepository(personUow)));
            //container.RegisterInstance(new UserService(new UserRepository(personUow), companyServiceInstance, personUow));
            container.RegisterInstance(new StuffService(new StuffRepository(stuffUnow), stuffUnow));
            container.RegisterInstance(toolServiceInstance);
            container.RegisterInstance(companyAreaServiceInstance);
            container.RegisterInstance(new CheckinToolService(new CheckinToolRepository(stuffUnow), stuffUnow, companyServiceInstance));
            container.RegisterInstance(new StuffCategoryService(new StuffCategoryRepository(stuffUnow), stuffUnow));
            container.RegisterInstance(new StuffManufactureService(new StuffManufactureRepository(stuffUnow), stuffUnow));
            container.RegisterInstance(companyServiceInstance);
            container.RegisterInstance(membershipServiceInstance);
            container.RegisterInstance(employeeServiceInstance);
            container.RegisterInstance(employeeLoginFacadeInstance);
            container.RegisterInstance(callServiceInstance);

            RegisterHandlersType(container);

            return container;
        }

        private static void RegisterHandlersType(UnityContainer container)
        {
            container.RegisterType<IHandle<CallAddedEvent>, CallAddedHandler>("CallAddedEventHandler");
        }
    }
}