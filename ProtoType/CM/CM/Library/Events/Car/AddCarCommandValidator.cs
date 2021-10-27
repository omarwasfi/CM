using CM.Library.Queries.CarBrand;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CM.Library.Events.Car
{
    public class AddCarCommandValidator : AbstractValidator<AddCarCommand>
    {
        private readonly IMediator _mediator;

        public AddCarCommandValidator(IMediator mediator)
        {
            this._mediator = mediator;
            RuleFor(x => x.Name).NotNull().NotEmpty().WithMessage("The car name can't be null or empty !");
            RuleFor(x => x.Name).MaximumLength(100).WithMessage("The Maximum length for the name is 100");
            RuleFor(x => x.Description).MaximumLength(200).WithMessage("The Maximum length for the description is 200");
            RuleFor(x => x.CarBrandId).MustAsync(async (CarBrandId, cancellation) =>
            {
                return await beAnExistCarBrandId(CarBrandId);
            }).WithMessage("This Owned car id not exist");
        }

        private async Task<bool> beAnExistCarBrandId(string id)
        {
            if (await _mediator.Send(new GetCarBrandByIdQuery(id)) != null)
                return true;
            else
                return false;
        }
    }
}
