using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace WebSiteMjr.Models
{
    [Table("UserProfile")]
    public class UserProfile
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }   
    }

    public class RegisterExternalLoginModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Digite seu nome de usuário")]
        [Display(Name = "Nome de usuário")]
        public string UserName { get; set; }

        public string ExternalLoginData { get; set; }
    }

    public class LocalPasswordModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Digite sua senha antiga")]
        [DataType(DataType.Password)]
        [Display(Name = "Senha atual")]
        public string OldPassword { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Digite sua nova senha")]
        [StringLength(100, ErrorMessage = "A {0} precisa conter no mínimo {2} caracteres.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Nova senha")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirme sua nova senha")]
        [Compare("NewPassword", ErrorMessage = "Sua nova senha e a confirmação de senha, são diferentes.")]
        public string ConfirmPassword { get; set; }
    }

    public class LoginModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Digite seu Login")]
        [Display(Name = "Login")]
        public string UserName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Digite sua senha")]
        [DataType(DataType.Password)]
        [Display(Name = "Senha")]
        public string Password { get; set; }

        [Display(Name = "Mantenha-me conectado")]
        public bool RememberMe { get; set; }
    }

    public class RegisterModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Digite seu login de usuário")]
        [Display(Name = "Login")]
        public string UserName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Digite o nome deste usuário")]
        [Display(Name = "Nome")]
        public string Name { get; set; }

        [Display(Name = "Sobrenome")]
        public string LastName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Digite o E-mail deste usuário")]
        [Display(Name = "E-mail")]
        [RegularExpression(@"^[_a-z0-9-]+(\.[_a-z0-9-]+)*@[a-z0-9-]+(\.[a-z0-9-]+)*(\.[a-z]{2,4})$", ErrorMessage = "E-mail inválido")]
        public string Email { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Digite sua senha")]
        [StringLength(100, ErrorMessage = "A {0} precisa conter no mínimo {2} caracteres.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Senha")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirme sua senha")]
        [Compare("Password", ErrorMessage = "Sua senha e a confirmação de senha, são diferentes.")]
        public string ConfirmPassword { get; set; }
    }

    public class ExternalLogin
    {
        public string Provider { get; set; }
        public string ProviderDisplayName { get; set; }
        public string ProviderUserId { get; set; }
    }
}
