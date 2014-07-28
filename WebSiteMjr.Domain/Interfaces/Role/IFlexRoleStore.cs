using System.Collections.Generic;

namespace WebSiteMjr.Domain.Interfaces.Role
{
    public interface IFlexRoleStore
    {
        void CreateRole(string roleName);
        string[] GetRolesForUser(string username);
        string[] GetUsersInRole(string roleName);
        IEnumerable<Domain.Model.Roles.MjrAppRole> GetAllRoles();
        void RemoveUsersFromRoles(string[] usernames, string[] roleNames);
        void AddUsersToRoles(string[] usernames, string[] roleNames);
        bool RoleExists(string roleName);
        bool DeleteRole(string roleName);
    }
}