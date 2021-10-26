using CM.Library.DataModels;
using CM.Library.DBContexts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CM.Library.Queries.Person
{
    public class GetPersonByIdQueryHandler : IRequestHandler<GetPersonByIdQuery, PersonDataModel>
    {
        private readonly CurrentStateDBContext _currentStateDBContext;

        public GetPersonByIdQueryHandler(CurrentStateDBContext currentStateDBContext)
        {
            _currentStateDBContext = currentStateDBContext;
        }

        public async Task<PersonDataModel> Handle(GetPersonByIdQuery request, CancellationToken cancellationToken)
        {
            return await _currentStateDBContext.Users.FindAsync(request.Id);
        }
    }
}
