﻿using WebSiteMjr.Domain.Interfaces.Role;

namespace WebSiteMjr.Domain.services.Roles
{
    public class RoleService
    {
        private readonly IFlexRoleProvider _roleProvider;

        public RoleService(IFlexRoleProvider roleProvider)
        {
            _roleProvider = roleProvider;
        }

       
        public bool IsUserInRole(string username, string roleName)
        {
            return _roleProvider.IsUserInRole(username, roleName);
        }

        
        public string[] GetRolesForUser(string username)
        {
            return _roleProvider.GetRolesForUser(username);
        }


        public void CreateRole(string roleName)
        {
            _roleProvider.CreateRole(roleName);
        }

        public bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            return _roleProvider.DeleteRole(roleName, throwOnPopulatedRole);
        }

        public bool RoleExists(string roleName)
        {
            return _roleProvider.RoleExists(roleName);
        }
        
        public void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            _roleProvider.AddUsersToRoles(usernames, roleNames);
        }

        public void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            _roleProvider.RemoveUsersFromRoles(usernames, roleNames);
        }

        public string[] GetUsersInRole(string roleName)
        {
            return _roleProvider.GetUsersInRole(roleName);
        }

        public string[] GetAllRoles()
        {
            return _roleProvider.GetAllRoles();
        }
    }
}
