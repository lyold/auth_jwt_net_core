
using AuthJWT.Domain.Model.Entities;
using System;

namespace AuthJWT.Domain.Model.DTO
{
    public class ResultAutenticate
    {
        public bool Autenticated { get; }
        
        public Users User { get; }

        public DateTime DateCreated { get; }

        public DateTime DateExpiration { get; }

        public string Token { get; }

        public ResultAutenticate(bool autenticated)
        {
            Autenticated = autenticated;
        }

        public ResultAutenticate(bool autenticated, Users user, DateTime dateCreated, DateTime dateExpiration, string token)
        {
            Autenticated = autenticated;
            User = user;
            DateCreated = dateCreated;
            DateExpiration = dateExpiration;
            Token = token;
        }
    }
}
