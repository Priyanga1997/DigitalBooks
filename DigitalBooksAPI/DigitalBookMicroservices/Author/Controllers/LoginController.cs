using Author.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SharedModel.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Author.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        AuthorContext db = new AuthorContext();
        private IConfiguration _config;
        ILoginService loginService;
        public LoginController(IConfiguration config, ILoginService _loginService)
        {
            _config = config;
            loginService = _loginService;
        }
        [HttpPost]
        [Route("login-user")]
        public async Task<IActionResult> Login(Login login)
        {
            IActionResult response = Unauthorized();
            var userdata = await loginService.Login(login, false);
            if (login != null)
            {
                response = Ok(new { token = userdata });
            }
            return response;
        }

        [HttpPost]
        [Route("register-user")]
        public async Task<IActionResult> Register(Login login)
        {
            IActionResult response = Unauthorized();
            var userdata = await loginService.Register(login, true);
            if (login != null)
            {
                response = Ok(new { token = userdata });
            }
            return response;
        }
    }
}

