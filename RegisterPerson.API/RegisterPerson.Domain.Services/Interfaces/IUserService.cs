
using AuthJWT.Domain.Model.DTO;
using AuthJWT.Domain.Model.Entities;
using System.Collections.Generic;

namespace AuthJWT.Domain.Services.Interfaces
{ 
    public interface IUserService
    {
        ResultAutenticate Authenticate(User user);

        void Loggout(User person);

        User Create(User person);

        User Update(User person);

        void Delete(int id);

        User Find(int id);
        
        List<User> FindAll();
    }
}
