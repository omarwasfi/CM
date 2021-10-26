using CM.Library.DataModels.BusinessModels;
using CM.Library.DBContexts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CM.Library.Queries.ProblemType
{
    public class GetProblemTypeByIdQueryHandler : IRequestHandler<GetProblemTypeByIdQuery, ProblemTypeDataModel>
    {
        private readonly CurrentStateDBContext _currentStateDBContext;

        public GetProblemTypeByIdQueryHandler(CurrentStateDBContext currentStateDBContext)
        {
            this._currentStateDBContext = currentStateDBContext;
        }
        public async Task<ProblemTypeDataModel> Handle(GetProblemTypeByIdQuery request, CancellationToken cancellationToken)
        {
            return await _currentStateDBContext.ProblemTypes.FindAsync(request.Id);
        }
    }
}
