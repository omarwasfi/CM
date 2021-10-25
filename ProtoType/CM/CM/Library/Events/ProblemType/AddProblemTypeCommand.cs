using CM.Library.DataModels.BusinessModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CM.Library.Events.ProblemType
{
    public class AddProblemTypeCommand : IRequest<ProblemTypeDataModel>
    {
        public string Name { get; set; }

        public AddProblemTypeCommand(string name)
        {
            this.Name = name;
        }
    }
}
