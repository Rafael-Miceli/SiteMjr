using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using WebSiteMjr.Domain.Interfaces.Membership;
using WebSiteMjr.Domain.Interfaces.Role;
using WebSiteMjr.Domain.Model.Roles;
using WebSiteMjr.EfBaseData.UnitOfWork;
using WebSiteMjr.EfData.Context;
using WebSiteMjr.EfData.DataRepository.GenericRepositorys;
using WebSiteMjr.EfData.UnitOfWork;

namespace WebSiteMjr.EfData.DataRepository
{
    public class RoleRepository<TRole, TUser> : GenericPersonRepository<MjrAppRole, int>, IFlexRoleStore
        where TRole : class, IFlexRole<TUser>, new()
        where TUser : class, IFlexMembershipUser
    {
        private readonly DbContext _context;

        public RoleRepository(UnitOfWork<PersonsContext> uow) : base(uow)
        {}

        public RoleRepository() : base(new PersonsUow())
        {
            _context = new PersonsContext();
        }

        public void CreateRole(string roleName)
        {
            //var role = new TRole {Name = roleName};
            //_context.Set<TRole>().Add(role);
            //_context.SaveChanges();
        }

        public string[] GetRolesForUser(string username)
        {
            return null;
        }

        public string[] GetUsersInRole(string roleName)
        {
            //TODO must verify why getting error when doing the same calls as below but with PersonsContext
            return _context.Set<TRole>().Where(role => role.Name.Equals(roleName)).SelectMany(role => role.Users).Select(user => user.Username).ToArray();
            
        }

        public IEnumerable<MjrAppRole> GetAllRoles()
        {
            return GetAll();
        }

        public string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            return null;
        }

        public void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            //var users = _context.Set<TUser>().Where(u => usernames.Contains(u.Username)).ToList();

            //foreach (var roleName in roleNames)
            //{
            //    var role = _context.Set<TRole>().Include(r=>r.Users).SingleOrDefault(r => r.Name == roleName);
            //    if (role != null)
            //    {
            //        foreach (var user in users)
            //        {
            //            role.Users.Remove(user);
            //        }
            //    }
            //}
            //_context.SaveChanges();
        }

        public void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            //var users = _context.Set<TUser>().Where(u => usernames.Contains(u.Username)).ToList();

            //foreach (var roleName in roleNames)
            //{
            //    var role = _context.Set<TRole>().SingleOrDefault(r => r.Name == roleName);
            //    if (role != null)
            //    {
            //        if(role.Users == null)
            //        {
            //            role.Users = new Collection<TUser>();
            //        }
            //        foreach (var user in users)
            //        {
            //            role.Users.Add(user);
            //        }
            //    }
            //}
            //_context.SaveChanges();
        }

        public bool RoleExists(string roleName)
        {
            return true;
        }

        public bool DeleteRole(string roleName)
        {
            return false;
        }
    }
}