using CM.Library.DataModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CM.Library.Queries.Person
{
    public class GetPersonByIdQuery : IRequest<PersonDataModel>
    {
 

        public string Id { get; set; }


        public GetPersonByIdQuery(string id)
        {
            Id = id;
        }
    }
}
