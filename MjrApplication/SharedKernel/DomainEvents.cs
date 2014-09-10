using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;
using SharedKernel.Interfaces;

namespace SharedKernel
{
    public static class DomainEvents
    {
        //ToDo Figure out a better way to Inject the Container in the Container Property

        [ThreadStatic]
        private static List<Delegate> _actions;


        public static IUnityContainer Container { get; set; }

        public static void Register<T>(Action<T> callback) where T : IDomainEvent
        {
            if (_actions == null)
            {
                _actions = new List<Delegate>();
            }
            _actions.Add(callback);
        }

        public static void ClearCallbacks()
        {
            _actions = null;
        }

        public static void Raise<T>(T args) where T : IDomainEvent
        {
            foreach (var handler in Container.ResolveAll<IHandle<T>>())
                handler.Handle(args);

            if (_actions != null)
                foreach (var action in _actions)
                    if (action is Action<T>)
                        ((Action<T>)action)(args);


        }
    }
}
