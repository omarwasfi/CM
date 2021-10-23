using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CM.Library.Events.Person
{
    public class LogoutPersonCommand : IRequest
    {

        public string Username { get; set; }

        public LogoutPersonCommand(string usernamme)
        {
            Username = usernamme;
        }
    }
}
