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

namespace CM.Library.Queries.Problem
{
    public class GetProblemsQueryHandler : IRequestHandler<GetProblemsQuery, List<ProblemDataModel>>
    {
        private readonly CurrentStateDBContext _currentStateDBContext;

        public GetProblemsQueryHandler(CurrentStateDBContext currentStateDBContext)
        {
            _currentStateDBContext = currentStateDBContext;
        }

        public async Task<List<ProblemDataModel>> Handle(GetProblemsQuery request, CancellationToken cancellationToken)
        {
            return await _currentStateDBContext.Problems.ToListAsync();
        }
    }
}
