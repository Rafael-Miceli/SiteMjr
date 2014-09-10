using System.Collections.Generic;

namespace WebSiteMjr.Domain.Interfaces.Role
{
    public interface IFlexRoleProvider
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
    }
}