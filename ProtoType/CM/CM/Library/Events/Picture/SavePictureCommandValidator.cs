using System;
using FluentValidation;

namespace CM.Library.Events.Picture
{
	public class SavePictureCommandValidator : AbstractValidator<SavePictureCommand>
	{
		public SavePictureCommandValidator()
		{
			RuleFor(x => x.FormFile).NotNull().WithMessage("The FormFile is null !");
			RuleFor(x => x.FormFile).NotEmpty().WithMessage("The FormFile is Empty !");
			RuleFor(x => x.PersonId).NotNull().WithMessage("The PersonId is null !");
			RuleFor(x => x.PersonId).NotEmpty().WithMessage("The PersonId is Empty !");
			RuleFor(x => x.SubFolderName).NotNull().WithMessage("The SubFolderName is null !");
			RuleFor(x => x.SubFolderName).NotEmpty().WithMessage("The SubFolderName is Empty !");
		}
	}
}

