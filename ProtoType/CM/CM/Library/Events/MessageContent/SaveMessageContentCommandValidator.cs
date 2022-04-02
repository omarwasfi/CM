using System;
using System.Threading.Tasks;
using CM.Library.Queries.Picture;
using FluentValidation;
using MediatR;

namespace CM.Library.Events.MessageContent
{
	public class SaveMessageContentCommandValidator : AbstractValidator<SaveMessageContentCommand>
	{
		private readonly IMediator _mediator;

		public SaveMessageContentCommandValidator(IMediator mediator)
		{
			this._mediator = mediator;

			RuleFor(x => x).MustAsync(async (saveMessageContentCommand, cancellation) => {
				return await oneOfThemIsNotNull(saveMessageContentCommand.Text, saveMessageContentCommand.PictureId);

			}).WithMessage("The text or pictureId should be not null & the pictureId should be exist in the database");
		}

		private async Task<bool> oneOfThemIsNotNull(string text, string pricureId)
		{
			if(String.IsNullOrEmpty(text) == false && String.IsNullOrEmpty(pricureId) == true)
            {
				return true;
            }
			else if(String.IsNullOrEmpty(text) == true && String.IsNullOrEmpty(pricureId) == false)
            {
				return await isAValidPictureId(pricureId);
            }
            else
            {
				return false;
            }
		}

		private async Task<bool> isAValidPictureId(string pictureId)
        {
			return await _mediator.Send(new IsPictureIdExistQuery(pictureId));
        }

	}
}

