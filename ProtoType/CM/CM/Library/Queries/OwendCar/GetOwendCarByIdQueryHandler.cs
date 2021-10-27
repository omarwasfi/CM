using CM.Library.DataModels.BusinessModels;
using CM.Library.DBContexts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CM.Library.Queries.OwnedCar
{
    public class GetOwnedCarByIdQueryHandler : IRequestHandler<GetOwnedCarByIdQuery, OwnedCarDataModel>
    {
        private readonly CurrentStateDBContext _currentStateDBContext;

        public GetOwnedCarByIdQueryHandler(CurrentStateDBContext currentStateDBContext)
        {
            _currentStateDBContext = currentStateDBContext;
        }

        public async Task<OwnedCarDataModel> Handle(GetOwnedCarByIdQuery request, CancellationToken cancellationToken)
        {
            return await _currentStateDBContext.OwnedCars.FindAsync(request.Id);
        }
    }
}
