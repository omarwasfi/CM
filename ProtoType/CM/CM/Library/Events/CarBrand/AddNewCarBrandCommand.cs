using CM.Library.DataModels.BusinessModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CM.Library.Events.CarBrand
{
    public class AddNewCarBrandCommand : IRequest<CarBrandDataModel>
    {


        public string Name { get; set; }
        public string Description { get; set; }

        public AddNewCarBrandCommand(string name, string description)
        {
            Name = name;
            Description = description;
        }
    }
}
