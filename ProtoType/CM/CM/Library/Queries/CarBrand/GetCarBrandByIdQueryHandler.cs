using CM.Library.DataModels.BusinessModels;
using CM.Library.DBContexts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CM.Library.Queries.CarBrand
{
    public class GetCarBrandByIdQueryHandler : IRequestHandler<GetCarBrandByIdQuery, CarBrandDataModel>
    {
        private readonly CurrentStateDBContext _currentStateDBContext;

        public GetCarBrandByIdQueryHandler(CurrentStateDBContext currentStateDBContext)
        {
            _currentStateDBContext = currentStateDBContext;
        }

        public async Task<CarBrandDataModel> Handle(GetCarBrandByIdQuery request, CancellationToken cancellationToken)
        {
            return await _currentStateDBContext.CarBrands.FindAsync(request.Id);
        }
    }
}
