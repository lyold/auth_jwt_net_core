using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using AuthJWT.DataAccess.Abstract.Entities;
using AuthJWT.Domain.Model.DTO;
using AuthJWT.Domain.Model.Entities;
using AuthJWT.Domain.Services.Interfaces;
using AuthJWT.Domain.Services.Security;

namespace AuthJWT.Domain.Services.Implementation
{ 
    public class UserService : IUserService
    {
        private IUserServiceSqlServer _userServiceSqlServer;
        
        private SignConfigurationcs _signConfiguration;
        
        private TokenConfigurationcs _tokenConfiguration;

        public UserService(IUserServiceSqlServer userServiceSqlServer, SignConfigurationcs signConfiguration, TokenConfigurationcs tokenConfiguration)
        {
            _userServiceSqlServer = userServiceSqlServer;
            _signConfiguration = signConfiguration;
            _tokenConfiguration = tokenConfiguration;
        }
        
        public User Create(User user)
        {
            return _userServiceSqlServer.Create(user);
        }

        public void Delete(int id)
        {
            _userServiceSqlServer.Delete(id);
        }

        public User Find(int id)
        {
            return _userServiceSqlServer.Find(id);
        }

        public User Update(User user)
        {
            return _userServiceSqlServer.Update(user);
        }

        public List<User> FindAll()
        {
            var list = _userServiceSqlServer.FindAll();

            if (list != null)
                return list.ToList();

            return new List<User>();
        }
        
        public ResultAutenticate Authenticate(User u)
        {
            User user = _userServiceSqlServer.FindByLogin(u.Login, u.AccessKey);

            if(user != null)
            {
                ClaimsIdentity identity = new ClaimsIdentity(
                    new GenericIdentity(user.Login, "Login"), new[] {
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                        new Claim(JwtRegisteredClaimNames.UniqueName, user.Login)
                    });

                ResultAutenticate result = new ResultAutenticate(true, user, _tokenConfiguration.TimeSession);

                var handler = new JwtSecurityTokenHandler();
                string token = CreateToken(identity, result, handler);

                return result;
            }

            return new ResultAutenticate(false, user, _tokenConfiguration.TimeSession);
        }

        private string CreateToken(ClaimsIdentity identity, ResultAutenticate result, JwtSecurityTokenHandler handler)
        {
            var securityToken = handler.CreateToken(new Microsoft.IdentityModel.Tokens.SecurityTokenDescriptor()
            {
                Issuer = _tokenConfiguration.Issuer,
                Audience = _tokenConfiguration.Audience,
                SigningCredentials = _signConfiguration.Credentials,
                Subject = identity,
                NotBefore = result.DateCreated,
                Expires = result.DateExpiration
            });

            var token = handler.WriteToken(securityToken);

            return token;
        }

        public void Loggout(User person)
        {
            //TODO: Implementar loggof
        }
    }
}
