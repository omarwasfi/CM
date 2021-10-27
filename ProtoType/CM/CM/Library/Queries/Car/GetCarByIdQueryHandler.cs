using CM.Library.DataModels.BusinessModels;
using CM.Library.DBContexts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CM.Library.Queries.Car
{
    public class GetCarByIdQueryHandler : IRequestHandler<GetCarByIdQuery, CarDataModel>
    {
        private readonly CurrentStateDBContext _currentStateDBContext;

        public GetCarByIdQueryHandler(CurrentStateDBContext currentStateDBContext)
        {
            _currentStateDBContext = currentStateDBContext;
        }

        public async Task<CarDataModel> Handle(GetCarByIdQuery request, CancellationToken cancellationToken)
        {

            return await _currentStateDBContext.Cars.FindAsync(request.Id);
        }
    }
}
