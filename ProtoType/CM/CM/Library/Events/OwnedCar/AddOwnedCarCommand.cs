using CM.Library.DataModels.BusinessModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CM.Library.Events.OwnedCar
{
    public class AddOwnedCarCommand : IRequest<OwnedCarDataModel>
    {
       public string Name { get; set; }
        public string Description { get; set; }
        public string CarId { get; set; }
        public string PersonId { get; set; }
       

        public AddOwnedCarCommand( string name, string description, string carId, string personId)
        {
            Name = name;
            Description = description;
            CarId = carId;
            PersonId = personId;
        }

      


      
    }
}
