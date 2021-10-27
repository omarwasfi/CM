using CM.Library.DataModels.BusinessModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CM.Library.Events.Car
{
    public class AddCarCommand : IRequest<CarDataModel>
    {


        public string Name { get; set; }
        public string  Description { get; set; }

        public string CarBrandId { get; set; }

        public AddCarCommand(string name, string description, string carBrandId)
        {
            Name = name;
            Description = description;
            CarBrandId = carBrandId;
        }

    }
}
