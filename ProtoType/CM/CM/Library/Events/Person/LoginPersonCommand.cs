using MediatR;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CM.Library.Events.Person
{
    public class LoginPersonCommand : IRequest<string>
    {
 

        public string Usernamme { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; }

        public LoginPersonCommand(string usernamme, string password, bool rememberMe)
        {
            Usernamme = usernamme;
            Password = password;
            RememberMe = rememberMe;
        }
    }
}
