using System;
using FluentValidation;

namespace CM.Library.Events.Person
{
	public class UploadProfilePictureCommandValidator : AbstractValidator<UploadProfilePictureCommand>
	{
		public UploadProfilePictureCommandValidator()
		{
			RuleFor(x => x.FormFiles[0]).NotNull().WithMessage("The FormFiles[0] is null !");
			RuleFor(x => x.FormFiles[0]).NotEmpty().WithMessage("The FormFiles[0] is Empty !");
		}
	}
}

