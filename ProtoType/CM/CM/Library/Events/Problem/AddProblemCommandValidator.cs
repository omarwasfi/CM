using CM.Library.Queries.OwnedCar;
using CM.Library.Queries.Person;
using CM.Library.Queries.ProblemType;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CM.Library.Events.Problem
{
    public class AddProblemCommandValidator : AbstractValidator<AddProblemCommand>
    {

        private readonly IMediator _mediator;

        public AddProblemCommandValidator(IMediator mediator)
        {
            this._mediator = mediator;

            RuleFor(x => x.ProblemTypeId).MustAsync(async (problemTypeId, cancellation) =>
            {
                return await beAnExistProblemTypeId(problemTypeId);
            }).WithMessage("This problem type id not exist");


            RuleFor(x => x.PersonId).MustAsync(async (PersonId, cancellation) =>
            {
                return await beAnExistPersonId(PersonId);
            }).WithMessage("This person id not exist");

            RuleFor(x => x.OwnedCarId).MustAsync(async (OwnedCarId, cancellation) =>
            {
                return await beAnExistOwnedCarId(OwnedCarId);
            }).WithMessage("This Owned car id not exist");

        }

        private async Task<bool> beAnExistProblemTypeId(string id)
        {
            if (await _mediator.Send(new GetProblemTypeByIdQuery(id)) != null)
                return true;
            else
                return false;
        }

        private async Task<bool> beAnExistPersonId(string id)
        {
            if (await _mediator.Send(new GetPersonByIdQuery(id)) != null)
                return true;
            else
                return false;
        }

        private async Task<bool> beAnExistOwnedCarId(string id)
        {
            if (await _mediator.Send(new GetOwnedCarByIdQuery(id)) != null)
                return true;
            else
                return false;
        }


    }
}
