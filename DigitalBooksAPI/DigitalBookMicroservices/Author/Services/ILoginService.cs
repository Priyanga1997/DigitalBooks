using SharedModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Author.Services
{
    public interface ILoginService
    {
        Task<string> Login(Login login, bool IsLogin);
        Task<string> Register(Login login, bool IsRegister);
    }
}
