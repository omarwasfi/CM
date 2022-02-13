using System;
using FluentValidation;

namespace CM.Library.Events.Person
{
	public class UpdateProfileCommandValidator : AbstractValidator<UpdateProfileCommand>
	{
		public UpdateProfileCommandValidator()
		{
			RuleFor(x => x.FirstName).NotNull().NotEmpty().MinimumLength(1).WithMessage("First name should not be empty !");
			RuleFor(x => x.LastName).NotNull().NotEmpty().MinimumLength(1).WithMessage("last name should not be empty !");
			RuleFor(x => x.Gender).NotNull().NotEmpty().MinimumLength(1).WithMessage("Gender should not be empty !");

		}
	}
}

