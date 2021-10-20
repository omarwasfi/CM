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

        public RegisterPersonCommand(PersonDataModel personDataModel)
        {
            this.PersonDataModel = personDataModel;
        }
    }
}
