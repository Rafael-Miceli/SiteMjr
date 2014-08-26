﻿using System;
using SharedKernel.Interfaces;
using WebSiteMjr.Domain.CustomerService.Events;

namespace WebSiteMjr.Notifications.Email.MjrEmailNotification
{
    public class CallAddedHandler: IHandle<CallAddedEvent>
    {
        public void Handle(CallAddedEvent args)
        {
            Console.WriteLine("Handled Call: {0} ", args.CallAdded.Title);
        }
    }
}
