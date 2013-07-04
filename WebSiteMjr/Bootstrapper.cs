using System.Web.Mvc;
using FlexProviders.Aspnet;
using FlexProviders.Membership;
using Microsoft.Practices.Unity;
using Unity.Mvc3;
using WebSiteMjr.Domain.Model.Membership;
using WebSiteMjr.Domain.services.Membership;
using WebSiteMjr.EfData.DataRepository;

namespace WebSiteMjr
{
    //Just to remember
    //This Bootstrapper has nothing to do with bootstraps css files!
    //This bootstrapper is to work with MVC.Unity, wich is a library to facilitate the Injection of dependencies in the component root of an App

    public static class Bootstrapper
    {
        public static void Initialise()
        {
            var container = BuildUnityContainer();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }

        private static IUnityContainer BuildUnityContainer()
        {
            var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers
            
            // e.g. container.RegisterType<ITestService, TestService>(); 

            //container.RegisterType<IRepository<Associate>, AssociateDtoRepository>();
            //container.RegisterType<IRepository<User>, UserDtoRepository>();
            //container.RegisterType<IRepository<Employee>, EmployeeDtoRepository>();

            container.RegisterInstance(new UserService(new PersonsRepository<User>()));
            container.RegisterInstance(new MembershipService(new FlexMembershipProvider(new MembershipRepository<User>(), new AspnetEnvironment())));

            return container;
        }
    }
}
