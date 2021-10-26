using CM.Library.DataModels.BusinessModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CM.Library.Events.Problem
{
    public class AddProblemCommand : IRequest<ProblemDataModel>
    {

        public string ProblemTypeId { get; set; }
        public string PersonId { get; set; }
        public string OwendCarId { get; set; }

        public string Description { get; set; }

        public string Location { get; set; }
       

        public AddProblemCommand(string problemTypeId, string personId, string owendCarId, string description, string location)
        {
            ProblemTypeId = problemTypeId;
            PersonId = personId;
            OwendCarId = owendCarId;
            Description = description;
            Location = location;
        }
    }
}
