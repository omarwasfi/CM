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
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string JwtKey { get; set; }


        public LoginPersonCommand(string usernamme, string password, bool rememberMe, string issuer, string audience,string jwtKey)
        {
            Usernamme = usernamme;
            Password = password;
            RememberMe = rememberMe;
            this.Issuer = issuer;
            this.Audience = audience;
            this.JwtKey = jwtKey;
        }
    }
}
