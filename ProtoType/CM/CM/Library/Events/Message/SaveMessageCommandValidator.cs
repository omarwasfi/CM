using System;
using System.Security.Claims;
using System.Threading.Tasks;
using CM.Library.DataModels;
using CM.Library.DataModels.Chat;
using CM.Library.Queries.MessageContent;
using CM.Library.Queries.Person;
using CM.Library.Queries.Room;
using FluentValidation;
using MediatR;

namespace CM.Library.Events.Message
{
	public class SaveMessageCommandValidator : AbstractValidator<SaveMessageCommand>
	{
		private readonly IMediator _mediator;

		public SaveMessageCommandValidator(IMediator mediator)
		{
			this._mediator = mediator;

			RuleFor(x => x).MustAsync(async (saveMessageCommand, cancellation) => {
				return await isValidFromPerson(saveMessageCommand.FromPersonId, saveMessageCommand.RoomId, saveMessageCommand.ClaimsPrincipal);
			}).WithMessage("The from Person not the same authenticated person !");

			RuleFor(x => x).MustAsync(async (saveMessageCommand, cancellation) => {
				return await isValidRoomId(saveMessageCommand.RoomId);
			}).WithMessage("This room is not exist !");

			RuleFor(x => x).MustAsync(async (saveMessageCommand, cancellation) => {
				return await isValidContentMessage(saveMessageCommand.MessageContentId);
			}).WithMessage("This message content is not exist !");


		}

		

		private async Task<bool> isValidFromPerson(string fromPersonId, string roomId, ClaimsPrincipal claimsPrincipal)
		{
			PersonDataModel authorizedPerson = await _mediator.Send(new GetTheAuthorizedPersonQuery(claimsPrincipal));
			RoomDataModel roomDataModel = await _mediator.Send(new GetRoomByIdQuery(roomId));



			if (fromPersonId  == authorizedPerson.Id && roomDataModel.People.Contains(authorizedPerson))
            {
				return true;
            }
            else
            {
				return false;
            }

		}

		private async Task<bool> isValidRoomId(string roomId)
		{
			RoomDataModel roomDataModel = await _mediator.Send(new GetRoomByIdQuery(roomId));

			if (roomDataModel != null)
			{
				return true;
			}
			else
			{
				return false;
			}

		}


		private async Task<bool> isValidContentMessage(string contentMessageId)
        {
			MessageContentDataModel messageContentDataModel = await _mediator.Send(new GetMessageContentByIdQuery(contentMessageId));

			if (messageContentDataModel != null)
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		
	}
}

