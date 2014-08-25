using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using SharedKernel.Interfaces;

namespace SharedKernel
{
    public static class DomainEvents
    {
        static DomainEvents()
        {
            //ToDo Need to create a project with the container of the application for reference
            //Container = 
        }

        public static IUnityContainer Container { get; set; }

        public static void Raise<T>(T args) where T : IDomainEvent
        {
            foreach (var handler in Container.ResolveAll<IHandle<T>>())
            {
                handler.Handle(args);
            }
        }
    }
}
