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

        public RegisterPersonCommand(string username , string password, string confirmPassword)
        {
            this.UserName = username;
            this.Password = password;
            this.ConfirmPassword = confirmPassword;
        }
    }
}
