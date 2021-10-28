using CM.Library.Queries.Car;
using CM.Library.Queries.Person;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CM.Library.Events.OwnedCar
{
    public class AddOwnedCarCommandValidator : AbstractValidator<AddOwnedCarCommand>
    {
        private readonly IMediator _mediator;

        public AddOwnedCarCommandValidator(IMediator mediator)
        {
            this._mediator = mediator;

            RuleFor(x => x.Name).NotNull().NotEmpty().WithMessage("The owned car name can't be null or empty.");
            RuleFor(x => x.Name).MaximumLength(100).WithMessage("The maximum length for the owned car name command is 100.");

            RuleFor(x => x.PersonId).MustAsync(async (PersonId, cancellation) =>
            {
                return await beAnExistPersonId(PersonId);
            }).WithMessage("This person id not exist");

            RuleFor(x => x.CarId).MustAsync(async (CarId, cancellation) =>
            {
                return await beAnExistCarId(CarId);
            }).WithMessage("This Car id not exist");

           
        }

        private async Task<bool> beAnExistPersonId(string id)
        {
            if (await _mediator.Send(new GetPersonByIdQuery(id)) != null)
                return true;
            else
                return false;
        }

        private async Task<bool> beAnExistCarId(string id)
        {
            if (await _mediator.Send(new GetCarByIdQuery(id)) != null)
                return true;
            else
                return false;
        }

       
    }
}
