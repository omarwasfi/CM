using System;
using FluentValidation;

namespace CM.Library.Events.Person
{
	public class UploadProfilePictureCommandValidator : AbstractValidator<UploadProfilePictureCommand>
	{
		public UploadProfilePictureCommandValidator()
		{
			RuleFor(x => x.FormFile).NotNull().WithMessage("The FormFile is null !");
			RuleFor(x => x.FormFile).NotEmpty().WithMessage("The FormFile is Empty !");
		}
	}
}

