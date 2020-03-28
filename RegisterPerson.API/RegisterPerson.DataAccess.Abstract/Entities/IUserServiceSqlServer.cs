
using AuthJWT.Domain.Model.Entities;
using System.Collections.Generic;

namespace AuthJWT.DataAccess.Abstract.Entities
{
    public interface IUserServiceSqlServer
    {
        
        User Create(User person);

        User Update(User person);

        void Delete(int id);

        User Find(int id);

        User FindByLogin(string login, string accessKey);

        IEnumerable<User> FindAll();
        
    }
}
