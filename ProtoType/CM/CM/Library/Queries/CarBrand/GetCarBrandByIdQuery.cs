using CM.Library.DataModels.BusinessModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CM.Library.Queries.CarBrand
{
    public class GetCarBrandByIdQuery : IRequest<CarBrandDataModel>
    {
        public string Id { get; set; }

        public GetCarBrandByIdQuery(string id)
        {
            Id = id;
        }
    }
}
