using CM.Library.DataModels.BusinessModels;
using CM.Library.DBContexts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CM.Library.Queries.Problem
{
    public class GetProblemByIdQueryHandler : IRequestHandler<GetProblemByIdQuery, ProblemDataModel>
    {
        private readonly CurrentStateDBContext _currentStateDBContext;

        public GetProblemByIdQueryHandler(CurrentStateDBContext currentStateDBContext)
        {
            _currentStateDBContext = currentStateDBContext;
        }

        public async Task<ProblemDataModel> Handle(GetProblemByIdQuery request, CancellationToken cancellationToken)
        {
            return await _currentStateDBContext.Problems.FindAsync(request.Id);
        }
    }
}
