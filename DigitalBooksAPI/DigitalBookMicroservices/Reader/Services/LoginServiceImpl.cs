using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SharedModel.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reader.Services
{
    public class LoginServiceImpl:ILoginService
    {
        AuthorContext db;
        private IConfiguration _config;
        public LoginServiceImpl(AuthorContext _db, IConfiguration config)
        {
            db = _db;
            _config = config;
        }
        public async Task<string> Login(Login login, bool IsRegister)
        {
            var user = AuthenticateUser(login, IsRegister);
            if (user != null)
            {
                var tokenString = GenerateToken(user);
                return tokenString;
            }
            else
            {
                return "Data is not valid";
            }          
        }
        private Login AuthenticateUser(Login login, bool IsRegister)
        {
            if (IsRegister)
            {
                db.Logins.Add(login);
                db.SaveChanges();
                return login;
            }
            else
            {
                if (db.Logins.Any(x => x.UserName == login.UserName && x.EmailId == login.EmailId && x.Password == login.Password))
                {
                    return db.Logins.Where(x => x.UserName == login.UserName && x.EmailId == login.EmailId && x.Password == login.Password).FirstOrDefault();
                }
                else
                {
                    return null;
                }
            }

        }
        private string GenerateToken(Login login)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_config["jwt:Issuer"],
                _config["jwt:Audience"],
                null,
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<string> Register(Login login, bool IsRegister)
        {
            var user = AuthenticateUser(login, IsRegister);
            if (user != null)
            {
                var tokenString = GenerateToken(user);
                return tokenString;
            }
            else
            {
                return "Data is not valid";
            }           
        }
    }
}
