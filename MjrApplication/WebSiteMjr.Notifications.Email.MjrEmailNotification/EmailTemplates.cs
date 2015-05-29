namespace WebSiteMjr.Notifications.Email.MjrEmailNotification
{
    public class EmailTemplates
    {
        public string FirstLoginTemplate(string password, string name, string lastName)
        {
            return "Aqui está sua senha " + name + " " + lastName + "<br />Senha: " + password;
        }

        public string RequestAdminCreatePassword(string name, int adminId)
        {
            return "Caro " + name + ",<br />Para ter seu acesso ao aplicativo S.E.N.A. crie sua senha no link:<br/>http://sena.com.br/usuario/" + adminId;
        }
    }
}