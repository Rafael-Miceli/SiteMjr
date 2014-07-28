using System.Collections.Generic;
using WebSiteMjr.Domain.Model.Roles;

namespace WebSiteMjr.Domain.Interfaces.Services
{
    public interface IRoleService
    {
        bool IsUserInRole(string username, string roleName);
        string[] GetRolesForUser(string username);
        void CreateRole(string roleName);
        bool DeleteRole(string roleName, bool throwOnPopulatedRole);
        bool RoleExists(string roleName);
        void AddUsersToRoles(string[] usernames, string[] roleNames);
        void RemoveUsersFromRoles(string[] usernames, string[] roleNames);
        string[] GetUsersInRole(string roleName);
        IEnumerable<Domain.Model.Roles.MjrAppRole> GetAllRoles();
        ICollection<Domain.Model.Roles.MjrAppRole> GetRole_User_ForEmployee();
        ICollection<MjrAppRole> GetRole_MjrUser_ForEmployee();
    }
}