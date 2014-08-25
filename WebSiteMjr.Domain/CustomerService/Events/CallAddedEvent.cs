using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSiteMjr.Domain.CustomerService.Model;

namespace WebSiteMjr.Domain.CustomerService.Events
{
    public class CallAddedEvent
    {
        public CallAddedEvent(Call call) : this()
        {
            CallAdded = call;
        }

        public CallAddedEvent()
        {
            Id = Guid.NewGuid();
            DateTimeEventOccurred = DateTime.Now;
        }

        public DateTime DateTimeEventOccurred { get; private set; }
        public Guid Id { get; private set; }
        public Call CallAdded { get; private set; }
    }
}
