using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CM.Library.DataModels;
using CM.Library.DBContexts;
using MediatR;

namespace CM.Library.Queries.Person
{
	public class GetContactsQueryHandler : IRequestHandler<GetContactsQuery,List<PersonDataModel>>
	{
        private readonly CurrentStateDBContext _currentStateDBContext;

        private readonly IMediator _mediator;
        

        public GetContactsQueryHandler(CurrentStateDBContext currentStateDBContext,IMediator mediator)
        {
            _currentStateDBContext = currentStateDBContext;
            _mediator = mediator;
        }

        public async Task<List<PersonDataModel>> Handle(GetContactsQuery request, CancellationToken cancellationToken)
        {
            List<PersonDataModel> people =  _currentStateDBContext.Users.OrderBy(x=>x.UserName).ToList();

            PersonDataModel autherizedPerson = await _mediator.Send(new GetTheAuthorizedPersonQuery(request.ClaimsPrincipal));

            people.Remove(autherizedPerson);
            return people;
        }
    }
}

