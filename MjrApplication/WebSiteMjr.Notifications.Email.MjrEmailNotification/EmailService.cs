using System;
using System.Net;
using System.Net.Mail;
using WebSiteMjr.Domain.Interfaces.Services;

namespace WebSiteMjr.Notifications.Email.MjrEmailNotification
{
    public class EmailService: IEmailService
    {
        private readonly EmailTemplates _templates;
        private readonly MailMessage _mailMessage;
        private readonly SmtpClient _smtpClient;
        private readonly MailAddress _mjrMailAddress;
        private const string Host = "smtp.gmail.com"; //"smtp.gmail.com" "smtp.live.com"

        private const string FirstLoginSubject = "Bem vindo - ";
        private const string AdminFirstLoginSubject = "Bem vindo ao S.E.N.A. - ";

        public EmailService()
        {
            _templates = new EmailTemplates();
            _mailMessage = new MailMessage();
            _smtpClient = new SmtpClient();
            _mjrMailAddress = new MailAddress("mjr@mjr.com.br", "No-Reply Mjr");

            InitializeFields();
        }

        private void InitializeFields()
        {
            _mailMessage.IsBodyHtml = true;
            _smtpClient.Credentials = new NetworkCredential("mjr@mjr.com.br", "88023933");
            _smtpClient.Host = Host;
            _smtpClient.EnableSsl = true;
        }

        public void SendFirstLoginToEmployee(string password, string email, string name, string lastName)
        {
            var body = _templates.FirstLoginTemplate(password, name, lastName);

            _mailMessage.From = _mjrMailAddress;
            _mailMessage.To.Add(email);
            _mailMessage.Subject = FirstLoginSubject + name;
            _mailMessage.Body = body;

            _smtpClient.Send(_mailMessage);
        }        

        public void SendCreatePasswordRequest(string name, string email, int userId)
        {
            var body = _templates.RequestAdminCreatePassword(name, userId); 

            _mailMessage.From = _mjrMailAddress;
            _mailMessage.To.Add(email);
            _mailMessage.Subject = FirstLoginSubject + name;
            _mailMessage.Body = body;

            _smtpClient.Send(_mailMessage);
        }
    }
}
