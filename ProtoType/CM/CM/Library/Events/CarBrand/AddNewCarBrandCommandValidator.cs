using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CM.Library.Events.CarBrand
{
    public class AddNewCarBrandCommandValidator : AbstractValidator<AddNewCarBrandCommand>
    {
        public AddNewCarBrandCommandValidator()
        {
            RuleFor(x => x.Name).NotNull().NotEmpty().WithMessage("Car brand name cant be null or empty !");
            RuleFor(x => x.Name).MaximumLength(100).WithMessage("Max length for the car name is 100 ");
            RuleFor(x => x.Description).MaximumLength(200).WithMessage("Max length for the description is 200.");


        }
    }
}
