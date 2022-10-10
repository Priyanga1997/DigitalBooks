using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SharedModel.Models;
using Reader.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Reader.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReaderLoginController : ControllerBase
    {
        AuthorContext db = new AuthorContext();
        private IConfiguration _config;
        ILoginService loginService;
        public ReaderLoginController(IConfiguration config, ILoginService _loginService)
        {
            _config = config;
            loginService = _loginService;
        }
        [HttpPost]
        [Route("reader-login")]
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
        [Route("reader-register")]
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
