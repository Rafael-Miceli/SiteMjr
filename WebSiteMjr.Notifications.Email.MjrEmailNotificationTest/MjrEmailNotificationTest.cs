using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebSiteMjr.Notifications.Email.MjrEmailNotification;

namespace WebSiteMjr.Notifications.Email.MjrEmailNotificationTest
{
    [TestClass]
    public class MjrEmailNotificationTest
    {
        [TestMethod]
        [Ignore]
        public void Should_Send_Email_Succefully()
        {
            var emailService = new EmailService();

            emailService.SendFirstLoginToEmployee("SenhaSecreta", "rafael.miceli@hotmail.com", "Rafael", "Miceli");
        }
    }
}
