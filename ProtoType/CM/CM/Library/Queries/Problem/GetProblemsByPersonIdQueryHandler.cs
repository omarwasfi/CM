using CM.Library.DataModels;
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
    public class GetProblemsByPersonIdQueryHandler : IRequestHandler<GetProblemsByPersonIdQuery, List<ProblemDataModel>>
    {
        private readonly CurrentStateDBContext _currentStateDBContext;

        public GetProblemsByPersonIdQueryHandler(CurrentStateDBContext currentStateDBContext)
        {
            _currentStateDBContext = currentStateDBContext;
        }

        public async Task<List<ProblemDataModel>> Handle(GetProblemsByPersonIdQuery request, CancellationToken cancellationToken)
        {
            PersonDataModel person =  await _currentStateDBContext.Users.FindAsync(request.Id);

            return person.Problems.ToList();
        }
    }
}
