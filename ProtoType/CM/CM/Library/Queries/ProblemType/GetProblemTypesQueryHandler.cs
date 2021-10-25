using CM.Library.DataModels.BusinessModels;
using CM.Library.DBContexts;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CM.Library.Queries.ProblemType
{
    public class GetProblemTypesQueryHandler : IRequestHandler<GetProblemTypesQuery, List<ProblemTypeDataModel>>
    {
        private readonly CurrentStateDBContext _currentStateDBContext;

        public GetProblemTypesQueryHandler(CurrentStateDBContext currentStateDBContext)
        {
            this._currentStateDBContext = currentStateDBContext;

        }

        public async Task<List<ProblemTypeDataModel>> Handle(GetProblemTypesQuery request, CancellationToken cancellationToken)
        {
            return await _currentStateDBContext.ProblemTypes.ToListAsync();
        }
    }
}
