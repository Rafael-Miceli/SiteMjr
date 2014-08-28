using System;
using System.Collections.Generic;
using WebSiteMjr.Domain.Interfaces.Membership;
using WebSiteMjr.Domain.Model.Person;
using WebSiteMjr.Domain.Model.Roles;

namespace WebSiteMjr.Domain.Model.Membership
{
    public class User : IntId, IFlexMembershipUser 
    {
        public User()
        {
            var tz = TimeZoneInfo.FindSystemTimeZoneById("Central Brazilian Standard Time");
            PasswordResetTokenExpiration = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, tz);
        }
        
        public virtual Employee Employee { get; set; }

        public virtual StatusUser StatusUser { get; set; }
        public virtual string Username { get; set; }
        public virtual string Password { get; set; }
        public string Salt { get; set; }
        public string PasswordResetToken { get; set; }
        public DateTime PasswordResetTokenExpiration { get; set; }
        public bool IsLocal { get; set; }
        public virtual ICollection<MjrAppRole> Roles { get; set; }
        
    }
}

public enum StatusUser
{
    Active,
    Blocked,
    Unactive
}
