using CM.Library.DataModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CM.Library.Events.Person
{
    public class RegisterPersonCommand : IRequest<string>
    {
        public string UserName { get; set; }

        public string Password { get; set; }

        public string ConfirmPassword { get; set; }

        public string Issuer { get; set; }

        public string Audience { get; set; }

        public string JwtKey { get; set; }


        public RegisterPersonCommand(string username , string password, string confirmPassword, string issuer, string audience, string jwtKey)
        {
            this.UserName = username;
            this.Password = password;
            this.ConfirmPassword = confirmPassword;
            this.Issuer = issuer;
            this.Audience = audience;
            this.JwtKey = jwtKey;
        }

        public RegisterPersonCommand DeepCopy()
        {
            return (RegisterPersonCommand)this.MemberwiseClone();
        }
    }
}
