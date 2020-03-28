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
        
        private SignConfiguration _signConfiguration;
        
        private TokenConfiguration _tokenConfiguration;

        public UserService(IUserServiceSqlServer userServiceSqlServer, SignConfiguration signConfiguration, TokenConfiguration tokenConfiguration)
        {
            _userServiceSqlServer = userServiceSqlServer;
            _signConfiguration = signConfiguration;
            _tokenConfiguration = tokenConfiguration;
        }
        
        public Users Create(Users user)
        {
            return _userServiceSqlServer.Create(user);
        }

        public void Delete(int id)
        {
            _userServiceSqlServer.Delete(id);
        }

        public Users Find(int id)
        {
            return _userServiceSqlServer.Find(id);
        }

        public Users Update(Users user)
        {
            return _userServiceSqlServer.Update(user);
        }

        public List<Users> FindAll()
        {
            var list = _userServiceSqlServer.FindAll();

            if (list != null)
                return list.ToList();

            return new List<Users>();
        }
        
        public ResultAutenticate Authenticate(Users u)
        {
            Users user = _userServiceSqlServer.FindByLogin(u.Login, u.AccessKey);

            if(user != null)
            {
                ClaimsIdentity identity = new ClaimsIdentity(
                    new GenericIdentity(user.Login, "Login"), new[] {
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                        new Claim(JwtRegisteredClaimNames.UniqueName, user.Login)
                    });
                
                var handler = new JwtSecurityTokenHandler();

                return CreateToken(identity, handler, user);
            }

            return new ResultAutenticate(false);
        }

        private ResultAutenticate CreateToken(ClaimsIdentity identity, JwtSecurityTokenHandler handler, Users user)
        {
            DateTime DateCreated = DateTime.Now;
            DateTime DateExpired = DateCreated + TimeSpan.FromSeconds(_tokenConfiguration.TimeSession);

            var securityToken = handler.CreateToken(new Microsoft.IdentityModel.Tokens.SecurityTokenDescriptor()
            {
                Issuer = _tokenConfiguration.Issuer,
                Audience = _tokenConfiguration.Audience,
                SigningCredentials = _signConfiguration.Credentials,
                Subject = identity,
                NotBefore = DateCreated,
                Expires = DateExpired
            });
            
            var token = handler.WriteToken(securityToken);

            return new ResultAutenticate(true, user, DateCreated, DateExpired, token);
        }

        public void Loggout(Users person)
        {
            //TODO: Implementar loggof
        }
    }
}
