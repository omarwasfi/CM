using CM.Library.DataModels.BusinessModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CM.Library.Queries.OwendCar
{
    public class GetOwendCarByIdQuery : IRequest<OwendCarDataModel>
    {
        public string Id { get; set; }

        public GetOwendCarByIdQuery(string id)
        {
            Id = id;
        }
    }
}
