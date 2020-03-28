
using AuthJWT.DataAccess.Abstract.Entities;
using AuthJWT.DataAccess.SqlServer.Context;
using AuthJWT.Domain.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AuthJWT.API.Services.Context.Implementation
{
    public class UserServiceSqlServer : IUserServiceSqlServer
    {
        private SQLServerContext _context;

        public UserServiceSqlServer(SQLServerContext context)
        {
            this._context = context;
        }

        public User Create(User person)
        {
            try
            {
                _context.Add(person);
                _context.SaveChanges();


            }catch(Exception e)
            {
                throw e;
            }

            return person;
        }

        public void Delete(int id)
        {
            try
            {
                User person = Find(id);

                _context.Remove(person);
                _context.SaveChanges();

            }catch(Exception e)
            {
                throw e;
            }
        }

        public User Find(int id)
        {
            return _context.User.Where(x=>x.Id == id).FirstOrDefault();
        }

        public IEnumerable<User> FindAll()
        {
            return _context.User.ToList(); 
        }

        public User FindByLogin(string login, string accessKey)
        {
            return _context.User.Where(x => x.Login == login && x.AccessKey == accessKey).FirstOrDefault();
        }

        public User Update(User user)
        {
            User oldUser = Find(user.Id.GetValueOrDefault());

            try
            {
                oldUser.Login = user.Login;
                oldUser.AccessKey = user.AccessKey;

                _context.Update(oldUser);
                _context.SaveChanges();
            }
            catch(Exception e)
            {
                throw e;
            }
            
            return oldUser;
        }
    }
}
