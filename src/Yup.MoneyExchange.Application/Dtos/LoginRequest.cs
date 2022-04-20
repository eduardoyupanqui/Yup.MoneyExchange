using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yup.MoneyExchange.Application.Dtos
{
    public class LoginRequest
    {
        public string User { get; }
        public string Password { get; }
        public LoginRequest(string user, string password)
        {
            User = user;
            Password = password;
        }
    }
}
