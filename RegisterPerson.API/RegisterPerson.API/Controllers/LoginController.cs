using System;
using Microsoft.AspNetCore.Mvc;
using AuthJWT.Domain.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using AuthJWT.Domain.Model.Entities;

namespace AuthJWT.API.Controllers
{
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        private IUserService _userService;
        
        public LoginController(IUserService userService)
        {
            this._userService = userService;
        }
        
        [AllowAnonymous]
        [HttpPost]
        public object Post([FromBody]User user)
        {
            try
            {
                return Ok(_userService.Authenticate(user));
            }catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
