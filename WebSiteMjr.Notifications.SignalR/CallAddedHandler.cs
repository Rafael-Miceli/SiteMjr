using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedKernel.Interfaces;
using WebSiteMjr.Domain.CustomerService.Events;

namespace WebSiteMjr.Notifications.SignalR
{
    public class CallAddedHandler : IHandle<CallAddedEvent>
    {
        public void Handle(CallAddedEvent args)
        {
            Console.WriteLine("Handling Event in SignalR: {0}", args.CallAdded.Title);
        }
    }
}
