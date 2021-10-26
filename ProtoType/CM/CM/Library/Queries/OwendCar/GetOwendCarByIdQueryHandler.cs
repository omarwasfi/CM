using CM.Library.DataModels.BusinessModels;
using CM.Library.DBContexts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CM.Library.Queries.OwendCar
{
    public class GetOwendCarByIdQueryHandler : IRequestHandler<GetOwendCarByIdQuery, OwendCarDataModel>
    {
        private readonly CurrentStateDBContext _currentStateDBContext;

        public GetOwendCarByIdQueryHandler(CurrentStateDBContext currentStateDBContext)
        {
            _currentStateDBContext = currentStateDBContext;
        }

        public async Task<OwendCarDataModel> Handle(GetOwendCarByIdQuery request, CancellationToken cancellationToken)
        {
            return await _currentStateDBContext.OwendCars.FindAsync(request.Id);
        }
    }
}
