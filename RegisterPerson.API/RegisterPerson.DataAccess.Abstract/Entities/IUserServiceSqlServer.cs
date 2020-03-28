
using AuthJWT.Domain.Model.Entities;
using System.Collections.Generic;

namespace AuthJWT.DataAccess.Abstract.Entities
{
    public interface IUserServiceSqlServer
    {

        Users Create(Users user);

        Users Update(Users user);

        void Delete(int id);

        Users Find(int id);

        Users FindByLogin(string login, string accessKey);

        IEnumerable<Users> FindAll();
        
    }
}
