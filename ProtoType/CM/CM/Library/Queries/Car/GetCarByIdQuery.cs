using CM.Library.DataModels.BusinessModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CM.Library.Queries.Car
{
    public class GetCarByIdQuery : IRequest<CarDataModel>
    {
        public string Id { get; set; }

        public GetCarByIdQuery(string id)
        {
            Id = id;
        }
    }
}
