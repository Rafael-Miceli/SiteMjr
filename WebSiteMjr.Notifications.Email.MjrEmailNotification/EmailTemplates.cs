namespace WebSiteMjr.Notifications.Email.MjrEmailNotification
{
    public class EmailTemplates
    {
        public string FirstLoginTemplate(string password, string name, string lastName)
        {
            return "Aqui está sua senha " + name + " " + lastName + "<br />Senha: " + password;
        }
    }
}