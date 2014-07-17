using System;

namespace WebSiteMjr.Domain.Interfaces.Membership
{
    public interface IFlexMembershipUser
    {
        string Username { get; set; }
        string Password { get; set; }
        string Salt { get; set; }
        string PasswordResetToken { get; set; }
        DateTime PasswordResetTokenExpiration { get; set; }
        StatusUser StatusUser { get; set; }
    }
}