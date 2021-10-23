using CM.Library.DataModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CM.Library.Events.Person
{
    public class RegisterPersonCommand : IRequest<PersonDataModel>
    {
        public PersonDataModel PersonDataModel { get; set; }

        public string Password { get; set; }

        public RegisterPersonCommand(PersonDataModel personDataModel , string password)
        {
            this.PersonDataModel = personDataModel;
            this.Password = password;
        }
    }
}
