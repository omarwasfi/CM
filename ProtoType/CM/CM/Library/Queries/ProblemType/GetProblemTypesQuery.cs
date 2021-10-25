using CM.Library.DataModels.BusinessModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CM.Library.Queries.ProblemType
{
    public class GetProblemTypesQuery : IRequest<List<ProblemTypeDataModel>>
    {
    }
}
