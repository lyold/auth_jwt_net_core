
using AuthJWT.Domain.Model.DTO;
using AuthJWT.Domain.Model.Entities;
using System.Collections.Generic;

namespace AuthJWT.Domain.Services.Interfaces
{ 
    public interface IUserService
    {
        ResultAutenticate Authenticate(Users user);

        void Loggout(Users person);

        Users Create(Users person);

        Users Update(Users person);

        void Delete(int id);

        Users Find(int id);
        
        List<Users> FindAll();
    }
}
