using System;
using System.Threading;
using System.Threading.Tasks;
using CM.Library.DataModels;
using CM.Library.DBContexts;
using CM.Library.Queries.Person;
using MediatR;

namespace CM.Library.Events.Person
{
	public class UpdateProfileCommandHandler : IRequestHandler<UpdateProfileCommand>
	{
        private CurrentStateDBContext _currentStateDBContext;
        private IMediator _mediator;


        public UpdateProfileCommandHandler(CurrentStateDBContext currentStateDBContext, IMediator mediator)
		{
            this._currentStateDBContext = currentStateDBContext;
            this._mediator = mediator;
		}

        public async Task<Unit> Handle(UpdateProfileCommand request, CancellationToken cancellationToken)
        {
            PersonDataModel person = await _mediator.Send(new GetTheAuthorizedPersonQuery(request.ClaimsPrincipal));

            person.FirstName = request.FirstName;
            person.LastName = request.LastName;
            person.Gender = request.Gender;

            await _currentStateDBContext.SaveChangesAsync();

            return Unit.Value;
        }
    }
}

