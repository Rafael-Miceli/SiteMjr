namespace WebSiteMjr.Domain.Interfaces.Services
{
    public interface IEmailService
    {
        void SendFirstLoginToEmployee(string password, string email, string name, string lastName);
        void SendCreatePasswordRequest(string name, string email, int userId);
    }
}