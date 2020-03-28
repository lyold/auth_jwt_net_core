
using AuthJWT.Domain.Model.Entities;
using System;

namespace AuthJWT.Domain.Model.DTO
{
    public class ResultAutenticate
    {
        public bool Autenticated { get; }
        
        public User User { get; }

        public DateTime DateCreated { get; }

        public DateTime DateExpiration { get; }

        public string ResultMessage { get; set; }

        public ResultAutenticate(bool autenticated, User user, double timeSession)
        {
            Autenticated = autenticated;
            User = user;
            DateCreated = DateTime.Now;
            DateExpiration = DateCreated + TimeSpan.FromSeconds(timeSession);
        }
    }
}
