
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

        public Users Create(Users user)
        {
            try
            {
                _context.Add(user);
                _context.SaveChanges();


            }catch(Exception e)
            {
                throw e;
            }

            return user;
        }

        public void Delete(int id)
        {
            try
            {
                Users person = Find(id);

                _context.Remove(person);
                _context.SaveChanges();

            }catch(Exception e)
            {
                throw e;
            }
        }

        public Users Find(int id)
        {
            return _context.Users.Where(x=>x.Id == id).FirstOrDefault();
        }

        public IEnumerable<Users> FindAll()
        {
            return _context.Users.ToList(); 
        }

        public Users FindByLogin(string login, string accessKey)
        {
            return _context.Users.Where(x => x.Login == login && x.AccessKey == accessKey).FirstOrDefault();
        }

        public Users Update(Users user)
        {
            Users oldUser = Find(user.Id.GetValueOrDefault());

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
