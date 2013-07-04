﻿using System.Collections.Generic;
using System.Linq;
using WebSiteMjr.Domain.Interfaces.Repository;
using WebSiteMjr.Domain.Model.Membership;
using WebSiteMjr.Domain.Model.Roles;

namespace WebSiteMjr.Domain.services.Membership
{
    public class UserService
    {
        private readonly IRepository<User> _user;

        public UserService(IRepository<User> user)
        {
            _user = user;
        }

        public void CreateUser(User user)
        {
            _user.Add(user);
            _user.Save();
        }

        public void UpdateUser(User user)
        {
            _user.Update(user);
            _user.Save();
        }

        public void DeleteUser(object user)
        {
            _user.Remove(user);
            _user.Save();
        }

        public IEnumerable<User> ListUser()
        {
            return _user.GetAll();
        }

        public User GetLoggedUser(string userName)
        {
            return _user.GetByUserName(userName);
        }

        public Role GetUserRole(string userName)
        {
            return _user.GetByUserName(userName).Roles.FirstOrDefault();
        }

        public User FindUser(object iduser)
        {
            return _user.GetById(iduser);
        }



        public bool AuthUser(string login, string password)
        {
            return false; //return _user.GetAll().FirstOrDefault(ass => ass.UserName == login && ass.Password == password) != null;
        }
    }
}
